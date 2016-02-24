using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Sky.Mdo
{
	public class KeyEqualityComparer<T> : IEqualityComparer<T>  where T : IHasKey
	{
		
		public bool Equals(T mdo1, T mdo2)
		{
			return mdo1.ToKeyString().Equals(mdo2.ToKeyString());
		}

		public int GetHashCode(T mdo)
		{
			return mdo.ToKeyString().GetHashCode();
		}
	}
}
