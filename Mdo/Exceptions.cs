using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{

	public class AppException : System.Exception
	{
	}
	
	public class NotExistException<T> : System.Exception
	{
		private T _mdo;
		public T mdo { get { return _mdo; } }

		public NotExistException(T mdo)
		{
			_mdo = mdo;
		}
	}


	public class NullKeyException : AppException
	{
		private MdoField _field;
		public MdoField field {get {return _field;}}

		public NullKeyException(MdoField _field)
		{
			this._field = _field;
		}
	}
}
