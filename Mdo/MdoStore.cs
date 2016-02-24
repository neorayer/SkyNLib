using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Sky.Mdo
{
	public class MdoStore : Sky.Mdo.IMdoStore
	{
		private IDbConnPool connPool;

		public MdoStore RealStore { get { return this; } }

		public MdoStore(IDbConnPool connPool)
		{
			this.connPool = connPool;
		}

		public DbSession newDbSession()
		{
			return new DbSession(connPool.getConn());
		}

		public T Load<T>(T mdo)
		{
			return newDbSession().Load<T>(mdo);
		}


		public void CreateTable(Type type)
		{
			newDbSession().CreateTable(type);
		}

		public void dropTable(Type type)
		{
			newDbSession().dropTable(type);
		}

		public void CreateTableIfNotExsts(Type type)
		{
			newDbSession().CreateTableIfNotExsts(type);
		}

		public void AlterTableAddColumn(Type type, string propName, object defaultValue)
		{
			newDbSession().AlterTableAddColumn(type, propName, defaultValue);
		}

		public MdoList<T> Find<T>() where T : Mdo<T>
		{
			return newDbSession().Find<T>();
		}


		public MdoList<T> FindFormat<T>(string format, params object[] values) where T : Mdo<T>
		{
			return newDbSession().FindFormat<T>(format, values);
		}

		public int CountFormat<T>(string format, params object[] values)
		{
			return newDbSession().CountFormat<T>(format, values);
		}

		public T Create<T>(T mdo)
		{
			newDbSession().Create<T>(mdo);
			return mdo;
		}

		public void Create<T>(ICollection<T> mdos)
		{
			newDbSession().Create<T>(mdos);
		}

		public T CreateIfNotExists<T>(T mdo) 
		{
			newDbSession().CreateIfNotExists<T>(mdo);
			return mdo;
		}

	
		public bool Exists<T>(T mdo)
		{
			return newDbSession().Exists(mdo);
		}
		
		public void Update<T>(T mdo, IDictionary<string, object> data)
		{
			newDbSession().Update(mdo, data);
		}

		public void Update<T>(T mdo, string format, params object[] values)
		{
			newDbSession().Update(mdo, format, values);
		}

		public void Delete<T>(T mdo)
		{
			newDbSession().Delete(mdo);
		}

		public void DeleteMass<T>(ICollection<T> mdos)
		{
			newDbSession().DeleteMass(mdos);
		}

		public void DeleteFormat<T>(string format, params object[] values) where T : Mdo<T>
		{
			newDbSession().DeleteFormat<T>(format, values);
		}

	}
}
