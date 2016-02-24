using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Sky.Mdo
{
	public interface IDbConnPool
	{
		/// <summary>
		/// 取得一个可用的数据库连接
		/// </summary>
		/// <returns></returns>
		DbConnection getConn();
	}
}
