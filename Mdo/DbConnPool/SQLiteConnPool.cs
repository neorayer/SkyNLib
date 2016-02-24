using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

using System.Data.SQLite;

using Sky.Mdo;


namespace Sky.Mdo.DbConnPool 
{
	public class SQLiteConnPool : IDbConnPool
	{
		private FileInfo dbFileInfo { get; set; }

		public SQLiteConnPool(FileInfo dbFileInfo)
		{
			this.dbFileInfo = dbFileInfo;
		}

		public SQLiteConnPool(string dbPath)
		{
			this.dbFileInfo = new FileInfo(dbPath);
		}

		public DbConnection getConn()
		{

			string url = string.Format("Data Source={0};Version=3;New=False;Compress=True;", dbFileInfo.ToString());
			SQLiteConnection conn = new SQLiteConnection(url);
			return conn;
		}
	}

}
