using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

using FirebirdSql;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;


namespace Sky.Mdo.DbConnPool
{
	public class FireBirdConnPool : IDbConnPool
	{

		private FileInfo dbFileInfo { get; set; }

		public FireBirdConnPool(FileInfo dbFileInfo)
		{
			this.dbFileInfo = dbFileInfo;
		}

		public FireBirdConnPool(string dbPath)
		{
			this.dbFileInfo = new FileInfo(dbPath);
		}


		public DbConnection getConn()
		{
			
			FbConnectionStringBuilder cs = new FbConnectionStringBuilder();
			cs.Database = dbFileInfo.ToString();
			cs.UserID = "default-uid";
			cs.Password = "default-pwd";
			cs.Charset = "UTF8";
			cs.ServerType = FbServerType.Embedded;
			String dbUrl = cs.ToString();
			if (!File.Exists(dbFileInfo.ToString()))
			{
				FbConnection.CreateDatabase(dbUrl);
			}

			return new FbConnection(dbUrl);

		}
	}
}
