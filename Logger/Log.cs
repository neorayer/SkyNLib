using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using log4net;
using log4net.Config;
using System.Diagnostics;

namespace Sky.Logger
{
	public class GlobalLog
	{
		public static ILog Log { get; private set; }

		private static Stopwatch Stopwatch;

		public static void Init(FileInfo fi)
		{
			XmlConfigurator.Configure(fi);
			if (Log == null)
				Log = LogManager.GetLogger("SkyNLib");
		}

		public static void Debug(String s)
		{
			Log.Debug(s);
		}

		public static void DebugFormat(String format, params object[] args)
		{
			Log.DebugFormat(format, args);
		}

		public static void ErrorFormat(String format, params object[] args)
		{
			Log.ErrorFormat(format, args);
		}

		//private static long LastTick;

		//public static void BeginTick()
		//{
		//	LastTick = DateTime.Now.Ticks;
		//}

		//public static void Tick(string tag)
		//{
		//	Log.Debug(tag + " " + (DateTime.Now.Ticks - LastTick));
		//	LastTick = DateTime.Now.Ticks;
		//}

		public static void StopwatchStart()
		{
			Log.Debug("StopWatch Start");
			Stopwatch = Stopwatch.StartNew();
		}

		public static void StopwatchClick(string tag)
		{
			Stopwatch.Stop();

			TimeSpan ts = Stopwatch.Elapsed;
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
					ts.Hours, ts.Minutes, ts.Seconds,
					ts.Milliseconds);
			Log.Debug("StopWatch " + tag + " " + elapsedTime);

			Stopwatch = Stopwatch.StartNew();
		}

		public static void StopwatchStop()
		{
			Stopwatch.Stop();
		}
	}
}
