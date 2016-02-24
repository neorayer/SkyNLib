using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{
	public class MdoLocalCache : IMdoCache
	{
		IDictionary<string, object> dict = new ConcurrentDictionary<string, object>();

		public MdoLocalCache()
		{

		}

		public void put(string key, object mdo)
		{
			dict[key] = mdo;
		}

		public object get(string key)
		{
			if (dict.ContainsKey(key))
				return dict[key];
			else
				return null;
		}

		public void remove(string key)
		{
			dict.Remove(key);
		}


	}
}
