using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{
	public interface IMdoCache
	{
		void put(string key, object mdo);
		object get(string key);
		void remove(string key);
	}
}
