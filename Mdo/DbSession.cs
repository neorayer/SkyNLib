using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;

using System.Data;

using Sky.Logger;

namespace Sky.Mdo
{
	public class DbSession
	{
		private DbConnection conn;

		public DbSession(DbConnection conn)
		{
			this.conn = conn;
		}

		private void LogSQL(String s)
		{
			//TODO: 暂时屏蔽
			//if (s.Count() < 100)
			//	GlobalLog.Debug("[SQL] " + s);
			//else
			//	GlobalLog.Debug("[SQL] " + s.Substring(0, 100));
		}


		public static String SafeSqlString(String inStr)
		{
			if (inStr == null)
				return "";
			String s = inStr.Replace(@"\", @"\\");
			s = s.Replace("'", "''");
			return s;
		}

		public static String GetSQLSafeValue(Object value)
		{
			if (value == null)
				return "null";
			Type type = value.GetType();
			switch (type.Name)
			{
				case "Boolean":
					return (bool)value ? "1" : "0";
				case "String":
					return "'" + SafeSqlString(value.ToString()) + "'";
				case "DateTime":
					return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
			}

			if (type.IsEnum)
				return "'" + SafeSqlString(value.ToString()) + "'";

			return value.ToString();
		}

		private void close()
		{
			if (conn != null)
			{
				conn.Close();
				conn = null;
			}
		}

		public static string GetDeleteSQL(Object obj)
		{
			Type type = obj.GetType();
			MdoReflector reflector = MdoReflector.get(type);
			String sql = "DELETE FROM " + reflector.TableName + " WHERE " + KeySQL(obj);
			return sql;
		}
		public static String GetInsertSQL(Object obj)
		{
			Type type = obj.GetType();
			MdoReflector reflector = MdoReflector.get(type);
			String table = reflector.TableName;

			String sql = "INSERT INTO " + table + " (";

			// 字段名列表
			foreach (MdoField f in reflector.DulpexFields)
			{
				sql += f.Name.ToUpper() + ",";
			}
			//去掉最后一个逗号
			sql = sql.TrimEnd(',');

			sql += ") VALUES (";

			// 数值列表
			foreach (MdoField f in reflector.DulpexFields)
			{
				Object v = f.getValue(obj);

				// 主键字段的数值不能为null
				if (v == null && f.IsKey)
				{
					throw new NullKeyException(f);
				}

				sql += GetSQLSafeValue(v) + ",";
			}
			//去掉最后一个逗号
			sql = sql.TrimEnd(',');
			sql += (");");

			return sql;
		}

		public static string GetDropTableSQL(Type type)
		{
			MdoReflector reflector = MdoReflector.get(type);
			return "DROP TABLE " + reflector.TableName;
		}

		//注意：这里只支持SQLLite 3.3版本以上。其他的数据库及版本不支持。
		public static string GetCreateTableIfNotExstsSQL(Type type)
		{
			string sql = GetCreateTableSQL(type);
			sql = sql.Replace("CREATE TABLE ", "CREATE TABLE IF NOT EXISTS ");
			return sql;
		}

		public static string SQLTypeName(MdoField f)
		{
			if (f.Type == typeof(sbyte))
				return "BIGINT";
			else if (f.Type == typeof(byte))
				return "BIGINT";
			else if (f.Type == typeof(char))
				return "BIGINT";
			else if (f.Type == typeof(short))
				return "BIGINT";
			else if (f.Type == typeof(ushort))
				return "BIGINT";
			else if (f.Type == typeof(int))
				return "BIGINT";
			else if (f.Type == typeof(uint))
				return "BIGINT";
			else if (f.Type == typeof(long))
				return "BIGINT";
			else if (f.Type == typeof(ulong))
				return "BIGINT";
			else if (f.Type == typeof(float))
				return "FLOAT";
			else if (f.Type == typeof(double))
				return "FLOAT";
			else if (f.Type == typeof(bool))
				return "BIGINT";
			else if (f.Type == typeof(DateTime))
				return "DATETIME ";
			else if (f.Type == typeof(String))
				return "VARCHAR(" + f.Len + ") ";
			else
				return "VARCHAR(" + f.Len + ") ";
		}

		/// <summary>
		/// 根据类型获得创建数据表的SQL
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetCreateTableSQL(Type type)
		{
			MdoReflector reflector = MdoReflector.get(type);

			// 创建主键的ALTER TABLE 代码
			String keySQL = "PRIMARY KEY (";
			String sql = "CREATE TABLE " + reflector.TableName + " (";
			bool hasKey = false;
			foreach (MdoField f in reflector.DulpexFields)
			{
				sql += f.Name.ToUpper() + " ";

				sql += SQLTypeName(f) + " ";

				if (f.IsKey)
					sql += "NOT NULL";

				sql += ",";

				if (f.IsKey)
				{
					hasKey = true;
					keySQL += f.Name.ToUpper() + ",";
				}
			}
			if (!hasKey)
				throw new Exception(String.Format("Mdo派生类{0}没有定义primary key属性", type.Name));

			keySQL = keySQL.TrimEnd(',') + ")";
			sql += keySQL + ");";


			return sql;
		}

		/// <summary>
		/// 创建类型type的数据表
		/// </summary>
		/// <param name="type"></param>
		public void CreateTable(Type type)
		{
			String sql = GetCreateTableSQL(type);
			ExeUpdateSQL(sql);
		}

		public void AlterTableAddColumn(Type type, string propName, object defaultValue)
		{
			MdoReflector reflector = MdoReflector.get(type);
			MdoField f = reflector.Fields[propName];
			string sql = string.Format("ALTER TABLE {0} ADD COLUMN {1} {2} DEFAULT {3}",
					reflector.TableName, propName.ToUpper(), SQLTypeName(f), GetSQLSafeValue(defaultValue));
			ExeUpdateSQL(sql);
		}

		public void CreateTableIfNotExsts(Type type)
		{
			String sql = GetCreateTableIfNotExstsSQL(type);
			ExeUpdateSQL(sql);
		}

		public void dropTable(Type type)
		{
			String sql = GetDropTableSQL(type);
			ExeUpdateSQL(sql);
		}

		public void ExeUpdateSQL(ICollection<String> sqls)
		{
			string lastSql = null;
			try
			{
				conn.Open();
				DbTransaction trans = conn.BeginTransaction();
				foreach (String sql in sqls)
				{
					lastSql = sql;
					DbCommand command = conn.CreateCommand();
					command.CommandText = sql;
					command.Transaction = trans;
					LogSQL(sql);
					command.ExecuteNonQuery();
				}
				trans.Commit();
			}
			catch (Exception e)
			{
				GlobalLog.Debug(string.Format("{0} \r\n{1}", lastSql, e.Message));
				throw e;
			}
			finally
			{
				close();
			}
		}

		/// <summary>
		/// 执行SQL操作（非查询类）
		/// </summary>
		/// <param name="sql"></param>
		public void ExeUpdateSQL(String sql)
		{
			ExeUpdateSQL(new List<String>() { sql });
		}


		/// <summary>
		/// 在数据库里创建(Create/Insert)一组对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objs"></param>
		public void Create<T>(ICollection<T> objs)
		{
			List<String> sqls = new List<String>();
			foreach (T o in objs)
			{
				sqls.Add(GetInsertSQL(o));
			}
			ExeUpdateSQL(sqls);
		}

		/// <summary>
		/// 在数据库里创建(Create/Insert)一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		public void Create<T>(T o)
		{
			string sql = GetInsertSQL(o);
			ExeUpdateSQL(sql);
		}



		public bool Exists<T>(T o)
		{
			MdoReflector reflector = MdoReflector.get(typeof(T));
			String sql = "SELECT * FROM " + reflector.TableName + " WHERE " + KeySQL(o);
			bool isExists = false;
			ExeQuerySQL(sql, delegate(DbDataReader reader)
			{
				isExists = reader.Read();
			});

			return isExists;
		}

		public void CreateIfNotExists<T>(T o)
		{
			if (Exists(o))
				return;
			Create(o);
		}



		/// <summary>
		/// 获得由一个对象的主键值组成的SQL查询条件
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static String KeySQL(Object o)
		{
			String sql = "";
			MdoReflector reflector = MdoReflector.get(o.GetType());
			foreach (MdoField f in reflector.DulpexFields)
			{
				if (!f.IsKey)
					continue;

				sql += f.Name.ToUpper() + '=' + GetSQLSafeValue(f.getValue(o));
				sql += " AND ";
			}
			//去掉结尾的 " AND "
			return sql.Remove(sql.Length - 5);
		}

		private void readToField(DbDataReader reader, Object o, MdoField f)
		{
			int idx = reader.GetOrdinal(f.Name.ToUpper());
			if (idx < 0)
				throw new Exception("Can't find a field named '" + f.Name + "' in database table.");
			object val = reader.GetValue(idx);
			if (val == null || val.GetType().Name == "DBNull")
			{
				f.setValue(o, null);
				return;
			}
			// 根据字段类型，对数值进行强制类型转换(Type Casting)
			switch (f.Type.Name)
			{
				case "String":
					val = reader.GetString(idx);
					break;
				case "SByte":
					val = (sbyte)reader.GetByte(idx);
					break;
				case "Byte":
					val = reader.GetByte(idx);
					break;
				case "Char":
					val = reader.GetChar(idx);
					break;
				case "Int16":
					val = reader.GetInt16(idx);
					break;
				case "UInt16":
					val = reader.GetInt16(idx);
					break;
				case "Int32":
					val = reader.GetInt32(idx);
					break;
				case "UInt32":
					val = reader.GetInt32(idx);
					break;
				case "Int64":
					val = reader.GetInt64(idx);
					break;
				case "UInt64":
					val = reader.GetInt64(idx);
					break;
				case "Single":
					val = reader.GetFloat(idx);
					break;
				case "Double":
					val = reader.GetDouble(idx);
					break;
				case "Boolean":
					val = reader.GetBoolean(idx);
					break;
				case "DateTime":
					val = reader.GetDateTime(idx);
					break;
			}

			if (f.Type.IsEnum)
			{
				try
				{
					val = Enum.Parse(f.Type, reader.GetString(idx));
				}
				catch (InvalidCastException e)
				{
					GlobalLog.Debug(e.StackTrace);
				}
			}	

			f.setValue(o, val);
		}

		/// <summary>
		///  从数据库中，根据对象的o的主键值，载入对象的其他字段
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		public T Load<T>(T o)
		{
			MdoReflector reflector = MdoReflector.get(typeof(T));
			String sql = "SELECT * FROM " + reflector.TableName + " WHERE " + KeySQL(o);
			ExeQuerySQL(sql, delegate(DbDataReader reader)
			{
				if (reader.Read())
				{
					foreach (MdoField f in reflector.DulpexFields)
						readToField(reader, o, f);
				}
				else
				{
					throw new NotExistException<T>(o);
				}
			});
			return o;
		}

		public void Delete<T>(T o)
		{
			ExeUpdateSQL(GetDeleteSQL(o));
		}

		public void DeleteMass<T>(ICollection<T> objs)
		{
			List<String> sqls = new List<String>();
			foreach (T o in objs)
			{
				sqls.Add(GetDeleteSQL(o));
			}
			ExeUpdateSQL(sqls);
		}

		internal void DeleteFormat<T>(string format, object[] values)
		{
			MdoReflector reflector = MdoReflector.get(typeof(T));
			String sql = "DELETE FROM " + reflector.TableName + FormatToWhereSQL(format, values);
			ExeUpdateSQL(sql);
		}

		private string getUpdateSQL<T>(T o, string setSQL)
		{
			return "UPDATE " + MdoReflector.get(typeof(T)).TableName + " SET " + setSQL + " WHERE " + KeySQL(o);
		}

		private string getUpdateSQL<T>(T o, IDictionary<String, Object> data)
		{
			string setSQL = "";
			foreach (String key in data.Keys)
				setSQL += key.ToUpper() + "=" + GetSQLSafeValue(data[key]) + " ,";
			//去掉最后一个逗号
			setSQL = setSQL.TrimEnd(',');
			return getUpdateSQL(o, setSQL);

		}

		/// <summary>
		/// 修改对象o的部分字段（定义在newData中。newData的结构为 字段名:数值）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <param name="newData"></param>
		public void Update<T>(T o, IDictionary<String, Object> data)
		{
			ExeUpdateSQL(getUpdateSQL<T>(o, data));
		}

		private string[] getSqlSafeStrings(object[] values)
		{
			string[] safeVals = new string[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				object v = values[i];
				if (v == null)
				{
					safeVals[i] = null;
					continue;
				}
				if (v.GetType() == typeof(string))
				{
					safeVals[i] = "'" + SafeSqlString((string)values[i]) + "'";
					continue;
				}

				safeVals[i] = v.ToString();
			}
			return safeVals;
		}

		public void Update<T>(T o, string format, params object[] values)
		{
			string[] safeVals = getSqlSafeStrings(values);
			string setSQL = string.Format(format, safeVals);
			ExeUpdateSQL(getUpdateSQL<T>(o, setSQL));
		}

		/// <summary>
		/// 执行查询的SQL语句。查询完毕后回调 callback参数。
		/// callback委派格式 delegate void OnExeQuerySQL(DbDataReader reader);
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="callback"></param>
		public void ExeQuerySQL(String sql, Action<DbDataReader> callback)
		{
			DbCommand command = conn.CreateCommand();
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			try
			{
				conn.Open();

				LogSQL(sql);
				callback(command.ExecuteReader());
			}
			catch(Exception e)
			{
				GlobalLog.Debug(string.Format("SQL Error: {0} \r\n{1}", sql, e.Message));
				throw e;
			}
			finally
			{
				conn.Close();
			}
		}

		public int CountFormat<T>(string format, object[] values)
		{
			Type type = typeof(T);
			string sql = "SELECT COUNT(*) FROM " + MdoReflector.get(typeof(T)).TableName + FormatToWhereSQL(format, values);
			int count = 0;
			ExeQuerySQL(sql, delegate(DbDataReader reader)
			{
				reader.Read();
				count = reader.GetInt32(0);
			});
			return count;
		}

		private static String FormatToSQL(string format, params object[] values)
		{
			string condition = "";
			if (format != null && format.Trim().Length > 0)
			{
				string[] safeVals = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					object v = values[i];
					if (v == null)
					{
						safeVals[i] = null;
						continue;
					}
					if (v.GetType() == typeof(string))
					{
						safeVals[i] = "'" + SafeSqlString((string)values[i]) + "'";
						continue;
					}

					safeVals[i] = v.ToString();
				}
				condition = String.Format(format, safeVals);
			}
			return condition;
		}

		public static String FormatToWhereSQL(string format, params object[] values)
		{
			string condition = FormatToSQL(format, values);
			return condition.Trim().Length > 0 ? " WHERE " + condition : "";
		}

		public MdoList<T> FindFormat<T>(string format, params object[] values) where T : Mdo<T>
		{
			MdoList<T> objs = new MdoList<T>();
			Type type = typeof(T);
			string sql = "SELECT * FROM " + MdoReflector.get(typeof(T)).TableName + FormatToWhereSQL(format, values);

			ExeQuerySQL(sql, delegate(DbDataReader reader)
			{
				List<MdoField>  setterFields = MdoReflector.get(type).SetterFields;
				while (reader.Read())
				{

					T o = (T)Activator.CreateInstance(type);
					foreach (MdoField f in setterFields)
					{
						readToField(reader, o, f);
					}
					o.DeepLoad();
					objs.Add(o);
				}
			});
			return objs;
		}

		/// <summary>
		/// 从数据库中将所有的T类型对象全部取出
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public MdoList<T> Find<T>() where T : Mdo<T>
		{
			return FindFormat<T>(null);
		}

	}


}
