using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{
	public interface IHasKey
	{
		string ToKeyString();

		bool KeyEquals(object o);
	}
}
