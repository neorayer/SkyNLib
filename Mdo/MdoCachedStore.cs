using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{
	public class MdoCachedStore : Sky.Mdo.IMdoStore
	{
		private MdoStore realStore;
		public MdoStore RealStore { get { return realStore; } }

		private IMdoCache cache = new MdoLocalCache();

		public MdoCachedStore(MdoStore realStore)
		{
			this.realStore = realStore;
		}

		public int CountFormat<T>(string format, params object[] values)
		{
			return realStore.CountFormat<T>(format, values);
		}

		private void PutInCache<T>(T mdo) 
		{
			MdoReflector reflector = MdoReflector.get(typeof(T));
			cache.put(getKey(mdo), mdo);
		}
		
		public T Create<T>(T mdo)
		{
			PutInCache(mdo);
			return realStore.Create<T>(mdo);
		}

		public void Create<T>(ICollection<T> mdos)
		{
			realStore.Create(mdos);

			//存入cache
			foreach (T mdo in mdos)
			{
				PutInCache(mdo);
			}
		}

		public T CreateIfNotExists<T>(T mdo)
		{
			string key = getKey(mdo);
			object cachedObj = cache.get(key);
			if (cachedObj != null)
				return (T)cachedObj;

			T obj = realStore.CreateIfNotExists(mdo);
			return obj;
		}

		public void Delete<T>(T mdo)
		{
			cache.remove(getKey(mdo));
			realStore.Delete(mdo);
		}

		public void DeleteMass<T>(ICollection<T> mdos)
		{
			foreach(T mdo in mdos)
				cache.remove(getKey(mdo));
			realStore.DeleteMass(mdos);
		}


		public MdoList<T> Find<T>() where T : Mdo<T>
		{
			MdoList<T> objs = realStore.Find<T>();
			foreach(T o in objs)
				cache.put(getKey(o), o);
			return objs;
		}

		public MdoList<T> FindFormat<T>(string format, params object[] values) where T : Mdo<T>
		{
			MdoList<T> objs = realStore.FindFormat<T>(format, values);
			foreach (T o in objs)
				cache.put(getKey(o), o);
			return objs;
		}

		public T Load<T>(T mdo)
		{
			string key = getKey(mdo);
			object cachedObj = cache.get(key);
			if (cachedObj != null) {
				MdoReflector.CopyMdoPropertiesOfDest(mdo, cachedObj);
				return (T)mdo;
			}
			try
			{
				realStore.Load(mdo);
				cache.put(key, mdo);
				return mdo;
			}catch(NotExistException<T> e) {
				throw e;
			}
		}

		public bool Exists<T>(T mdo)
		{
			try
			{
				string key = getKey(mdo);
				object cachedObj = cache.get(key);
				if (cachedObj != null)
					return true;

				return realStore.Exists(mdo);
			}

			catch (NotExistException<T> )
			{
				return false;
			}
		}

		public void Update<T>(T mdo, IDictionary<string, object> data)
		{
			string key = getKey(mdo);
			cache.remove(key);
			realStore.Update(mdo, data);
		}
		public void Update<T>(T mdo, string format, params object[] values)
		{
			string key = getKey(mdo);
			cache.remove(key);
			realStore.Update(mdo, format, values);
		}


		/// <summary>
		/// 获得由一个对象的主键值组成的key
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		private string getKey(Object o)
		{
			string sql = "FROM " + o.GetType() + " WHERE ";
			MdoReflector reflector = MdoReflector.get(o.GetType());
			foreach (MdoField f in reflector.Fields.Values)
			{
				if (!f.IsKey)
					continue;

				sql += f.Name.ToUpper() + '=' +f.getValue(o);
				sql += " AND ";
			}
			//去掉结尾的 " AND "
			return sql.Remove(sql.Length - 5);
		}

		public void DeleteFormat<T>(string format, params object[] values) where T : Mdo<T>
		{
			MdoList<T> mdos = FindFormat<T>(format, values);
			foreach (T mdo in mdos)
			{
				cache.remove(getKey(mdo));
			}
			realStore.DeleteFormat<T>(format, values);
		}
	}
}
