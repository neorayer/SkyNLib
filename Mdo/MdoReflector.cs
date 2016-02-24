using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Sky.Util;
using System.Collections.Concurrent;

namespace Sky.Mdo
{
	public class MdoReflector
	{
		/// <summary>
		/// cache 是个静态Map，它将cache住所有已经反射过的类的方法，这样下次调用的时候将不需要再次反射了。
		/// </summary>
		private static IDictionary<Type, MdoReflector> cache = new ConcurrentDictionary<Type, MdoReflector>();

		/// <summary>
		/// 取得某个类型的Reflection信息对象(在这里被称为Reflector)
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static MdoReflector get(Type type)
		{

			if (!cache.ContainsKey(type)) 
				cache[type] =  new MdoReflector(type);
			return cache[type];
		}

		public static void CopyMdoPropertiesOfDest(object dest, object src)
		{
			CopyMdoPropertiesOfDest( dest,  src, null);
		}




		/// <summary>
		/// 从src复制所有属性的值到的dest。属性以dest中标MdoFieldAttribute的属性为基准
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="src"></param>
		/// <param name="changedProps">将有变化的属性值放入此集合中</param>
		public static void CopyMdoPropertiesOfDest(object dest, object src, ICollection<string> changedProps)
		{
			Type destType = dest.GetType();
			Type srcType = src.GetType();

			MdoReflector mrf = MdoReflector.get(destType);
			foreach (MdoField mf in mrf.Fields.Values)
			{
				object newV;
				Type typeOfNewV;
				PropertyInfo pi = srcType.GetProperty(mf.Name);
				if (pi != null)
				{
					newV = pi.GetValue(src, null);
					typeOfNewV = pi.PropertyType;
				}
				else
				{
					FieldInfo fi = srcType.GetField(mf.Name);
					if (fi == null)
						continue;
					newV = fi.GetValue(src);
					typeOfNewV = fi.FieldType;
				}


				//PropertyInfo pi;
				//FieldInfo fi;
				//GetPropertyOrFieldInfo(srcType, mf.Name, out  pi, out fi);
				//if (pi != null)
				//{
				//	newV = pi.GetValue(src, null);
				//	typeOfNewV = pi.PropertyType;
				//}
				//else if (fi != null)
				//{
				//	newV = fi.GetValue(src);
				//	typeOfNewV = fi.FieldType;
				//}
				//else
				//	continue;

				object destValue;
				if (!ConvertValueType(newV, out destValue, typeOfNewV, mf.Type))
					continue;

				object oldV = mf.getValue(dest);

				mf.setValue(dest, destValue);

				//找出变化了的属性
				if (changedProps != null)
				{
					IComparable newCompV = destValue as IComparable;
					IComparable oldCompV = oldV as IComparable;

					if ((destValue == null && oldV != null) || ((destValue != null && oldV == null)))
						changedProps.Add(mf.Name);
					else if (newCompV != null && oldCompV != null && newCompV.CompareTo(oldCompV) != 0)
						changedProps.Add(mf.Name);
				}
			}
		}

		/// <summary>
		/// 枚举与数字之间的互转
		/// 将srcType类型的srcValue对象，转换成 destType类型的对象，输出到destValue参数中。
		/// 返回是否成功转换。
		/// </summary>
		private static bool ConvertValueType(object srcValue, out object destValue, Type srcType,  Type destType)
		{
			destValue = srcValue;
			if (srcType == destType)
				return true;

			if (srcType.IsEnum)
			{
				// Enum => Number
				if (destType == typeof(byte))
				{
					destValue = (byte)(int)srcValue;
					return true;
				}
				else if (destType == typeof(sbyte))
				{
					destValue = (sbyte)(int)srcValue;
					return true;
				}
				else if (destType == typeof(short))
				{
					destValue = (short)srcValue;
					return true;
				}
				else if (destType == typeof(ushort))
				{
					destValue = (ushort)srcValue;
					return true;
				}
				else if (destType == typeof(int))
				{
					destValue = (int)srcValue;
					return true;
				}
				else if (destType == typeof(uint))
				{
					destValue = (uint)srcValue;
					return true;
				}
				else if (destType == typeof(long))
				{
					destValue = (long)srcValue;
					return true;
				}
				else if (destType == typeof(ulong))
				{
					destValue = (ulong)srcValue;
					return true;
				}	
			

				return false;
			}

			if (destType.IsEnum)
			{
				// Number => Enum
				if (srcType == typeof(SByte)
					|| srcType == typeof(Int16)
					|| srcType == typeof(Int32)
					|| srcType == typeof(Int64)
					|| srcType == typeof(Byte)
					|| srcType == typeof(UInt16)
					|| srcType == typeof(UInt32)
					|| srcType == typeof(UInt64)
					)
				{
					destValue = Enum.ToObject(destType, srcValue);
					return true;
				}


			}

			return false;
		}

		/// <summary>
		/// 从src复制所有属性的值到的dest。属性以src中标MdoFieldAttribute的属性为基准
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="src"></param>
		public static void CopyMdoPropertiesOfSrc(object dest, object src)
		{
			Type destType = dest.GetType();
			Type srcType = src.GetType();

			MdoReflector mrf = MdoReflector.get(srcType);
			foreach (MdoField mf in mrf.Fields.Values)
			{
				object value = mf.getValue(src);
				object newValue;



				PropertyInfo pi = destType.GetProperty(mf.Name);
				if (pi != null)
				{
					if (!ConvertValueType(value, out newValue, mf.Type, pi.PropertyType))
						continue;
					pi.SetValue(dest, newValue, null);
					continue;
				}

				FieldInfo fi = destType.GetField(mf.Name);
				if (fi != null)
				{
					if (!ConvertValueType(value, out newValue, mf.Type, fi.FieldType))
						continue;
					fi.SetValue(dest, newValue);
					continue;
				}


				//PropertyInfo pi;
				//FieldInfo fi;
				//GetPropertyOrFieldInfo(destType, mf.Name, out  pi, out fi);
				//if (pi != null)
				//{
				//	if (ConvertValueType(value, out newValue, mf.Type, pi.PropertyType))
				//		pi.SetValue(dest, newValue, null);
				//}else if (fi != null)
				//{
				//	if (ConvertValueType(value, out newValue, mf.Type, fi.FieldType))
				//		fi.SetValue(dest, newValue);
				//}
			}
		}

		public Type Type { get; private set; }

		/// <summary>
		/// Mdo类所包含的所有字段的信息
		/// </summary>
		public Dictionary<String, MdoField> Fields { get; private set; }
		public List<MdoField> GetterFields { get; private set; }
		public List<MdoField> SetterFields { get; private set; }
		public List<MdoField> DulpexFields { get; private set; }

		private String m_TableName;

		/// <summary>
		/// 数据表名称，从类特性MdoAttribute.tableName获得。如果未设置，则使用"tb_类名小写"
		/// </summary>
		public String TableName { get { return m_TableName; } }

		public MdoReflector(Type _type)
		{
			Fields = new Dictionary<String, MdoField>();
			GetterFields = new List<MdoField>();
			SetterFields = new List<MdoField>();
			DulpexFields = new List<MdoField>();

			Type = _type;

			// 分析所有字段fields
			PropertyInfo[] pis = Type.GetProperties();
			foreach(PropertyInfo pi in pis) {
				object[] piAttrs = pi.GetCustomAttributes(typeof(MdoFieldAttribute), false);
				if (piAttrs.Length == 0)
					continue;

				MdoField field = Fields[pi.Name] = new MdoField(pi);

				//可读属性集
				if (pi.CanRead) 
					GetterFields.Add(field);
				//可写属性集
				if (pi.CanWrite) 
					SetterFields.Add(field);
				//可读写属性集
				if (pi.CanRead && pi.CanWrite) 
					DulpexFields.Add(field);
			}

			// 分析数据表名
			m_TableName = "tb_" + Type.Name.ToLower(); //tableName的缺省值
			object[] attrs = Type.GetCustomAttributes(typeof(MdoAttribute), false);
			if (attrs.Length > 0)
			{
				MdoAttribute attr = (MdoAttribute)attrs[0];
				m_TableName = attr.tableName;
			}
		}


		public IDictionary<string, object> getDictionary(object obj)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			foreach (MdoField field in this.DulpexFields)
			{
				dict.Add(field.Name, field.getValue(obj));
			}
			return dict;
		}
	}
}
