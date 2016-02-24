using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;
using Sky.Mdo;
using System.Data;

namespace Sky.Util
{
	public delegate RtnT ReturnAction<RtnT>();
	public delegate RtnT ReturnAction<RtnT, ArgT>(ArgT a);
	public delegate RtnT ReturnAction<RtnT, ArgT1, ArgT2>(ArgT1 a1, ArgT2 a2);
	public delegate RtnT ReturnAction<RtnT, ArgT1, ArgT2, ArgT3>(ArgT1 a1, ArgT2 a2, ArgT3 a3);

	public class F
	{
		//TODO以后要把这个移到F外面去
		public delegate object ReturnAction();

		/// <summary>
		/// 在指定时间过后执行指定的表达式
		/// </summary>
		/// <param name="interval">事件之间经过的时间（以毫秒为单位）</param>
		/// <param name="action">要执行的表达式</param>
		public static void SetTimeout(double interval, Action action)
		{
			System.Timers.Timer timer = new System.Timers.Timer(interval);
			timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
		  {
			  timer.Enabled = false;
			  action();
		  };
			timer.Enabled = true;
		}

		public static void UiTimeout(double interval, Action action)
		{
			F.SetTimeout(interval, delegate()
			{
				Sky.UI.UITools.Invoke(action);
			});
		}
		/// <summary>
		/// 在指定时间周期重复执行指定的表达式
		/// </summary>
		/// <param name="interval">事件之间经过的时间（以毫秒为单位）</param>
		/// <param name="action">要执行的表达式</param>
		public static void SetInterval(double interval, Action<ElapsedEventArgs> action)
		{
			System.Timers.Timer timer = new System.Timers.Timer(interval);
			timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
			{
				action(e);
			};
			timer.Enabled = true;
		}

		//public static void SetTimeout(double interval, ReturnAction action)
		//{
		//    System.Timers.Timer timer = new System.Timers.Timer(interval);
		//    timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
		//    {
		//        timer.Enabled = false;
		//        action();
		//    };
		//    timer.Enabled = true;
		//}

		public static string AsciiCodeToString(byte asciiCode)
		{
			return Encoding.ASCII.GetString(new byte[] { asciiCode });
		}

		public static void SetDataSource(ListControl listControl, IList<ValueAndName> valueAndNames)
		{
			listControl.DisplayMember = "DisplayName";
			listControl.ValueMember = "Value";
			listControl.DataSource = valueAndNames;
		}

		public static void SetDataSource(DataGridViewComboBoxColumn col, IList<ValueAndName> valueAndNames)
		{
			col.DisplayMember = "DisplayName";
			col.ValueMember = "Value";
			col.DataSource = valueAndNames;
		}

		public static void SetDataSourceFromWordDict(ListControl listControl, Type enumType)
		{
			SetDataSource(listControl, Consts.ValueAndNamesOfWordDict(enumType));
		}

		/// <summary>
		/// 将控件修改为单向数据源绑定模式（即控件可修改数据源，但不能反向操作）。
		/// 同时增加事件OnBindingParse。即 控件向数据源推送时发生。
		/// </summary>
		/// <param name="controls"></param>
		/// <param name="OnBindingParse">控件向数据源推送时发生</param>
		public static void SetupEditControlsBinding(Control[] controls, ConvertEventHandler OnBindingParse)
		{
			foreach (Control c in controls)
			{
				foreach (Binding binding in c.DataBindings)
				{
					// 控件修改模式: 单向，不随数据源变化。
					binding.ControlUpdateMode = ControlUpdateMode.Never;
					// 数据源修改模式: 随控件变化。
					binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
					// 当控件将数据推向数据源时发生。比如：触发保存。
					binding.Parse += new ConvertEventHandler(OnBindingParse);
				}
			}
		}



		private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

		public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
		{
			if (control.InvokeRequired)
			{
				control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
			}
			else
			{
				control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
			}
		}









		/// <summary>
		/// 返回一个理智的（合理的）价格
		/// </summary>
		/// <param name="price">首选价格</param>
		/// <param name="optionalPrice">当首选价格不合理时返回这个价格</param>
		/// <returns></returns>
		public static double SanePrice(double price, double optionalPrice)
		{
			return IsSanePrice(price) ? price : optionalPrice;
		}

		/// <summary>
		/// 注：价差合约报价可能为负数
		/// </summary>
		/// <param name="price"></param>
		/// <returns></returns>
		public static bool IsSanePrice(double price)
		{
			return (price > -10000000 && price < 10000000);
		}

		public const double PRICE_ACCU = 0.000001D;

		public static bool PriceEquals(double p1, double p2)
		{
			return Math.Abs(p1 - p2) < PRICE_ACCU;
		}

		public static bool PriceIsLess(double p1, double p2)
		{
			return p1 < p2 - PRICE_ACCU;
		}

		public static bool PriceIsMore(double p1, double p2)
		{
			return p1 > p2 + PRICE_ACCU;
		}

		public static bool PriceIsMoreOrEq(double p1, double p2)
		{
			return PriceIsMore(p1, p2) || PriceEquals(p1, p2);
		}

		public static bool PriceIsLessOrEq(double p1, double p2)
		{
			return PriceIsLess(p1, p2) || PriceEquals(p1, p2);
		}

		public static bool PriceIsBetween(double price, double theSmaller, double theLager)
		{
			return PriceIsLess(price, theLager) && PriceIsMore(price, theSmaller);
		}

		// price是否是最小价格单位的整数倍
		public static bool PriceIsTimesTick(double price, double priceTick)
		{
			double res = price / priceTick;
			double diff = Math.Abs(res - Math.Round(res));
			bool isTimes = diff < PRICE_ACCU;
			return isTimes;
		}

		public static string PriceKey(double price)
		{
			return string.Format("{0:F6}", price);
		}

		//按tick的小数位数舍入
		public static double RoundByTickDicimals(double v, double tick)
		{
			int dicimals = DecimalPlaces(tick);
			return Math.Round(v, dicimals);
		}

		//按tick的精度梯度进行舍入
		public static double RoundByTick(double v, double tick)
		{
			return Math.Round(v / tick) * tick;
		}

		public static int DecimalPlaces(double tick)
		{
			double log = Math.Log10(tick);
			if (log >= 0)
				return 0;
			if (log == (int)log)
				return -(int)log;
			else
				return 1 - (int)(log);
		}

		//简单的表达式执行引擎,变量只能为A B C D。参数的值按ABCDE的顺序排列。
		//注：参数还不能太多。实现得并不严密。
		public static double Eval(string formular, params double[] vars)
		{
			formular = formular.ToUpper();
			for (int i = 0; i < vars.Length; i++)
			{
				formular = formular.Replace(EvalVarNames[i], vars[i].ToString());
			}

			try
			{
				string s = EvalDateTable.Compute(formular, "").ToString();
				return double.Parse(s);
			}
			catch (Exception e)
			{
				string errStr = "F.Eval()执行错误：formular=" + formular;
				throw new Exception(errStr, e);
			}
		}
		private static string[] EvalVarNames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
		private static DataTable EvalDateTable = new DataTable();


		public static int DateTimeToTimestamp(DateTime dt)
		{
			return (int)(DateTime.Now.AddHours(-8) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}

		public static DateTime TimestampToDateTime(double timestamp)
		{
			return (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(timestamp);
		}

		public static bool IsNumericType(Type srcType)
		{
			return srcType == typeof(Byte)
					|| srcType == typeof(SByte)
					|| srcType == typeof(UInt16)
					|| srcType == typeof(UInt32)
					|| srcType == typeof(UInt64)
					|| srcType == typeof(Int16)
					|| srcType == typeof(Int32)
					|| srcType == typeof(Int64)
					|| srcType == typeof(Double)
					|| srcType == typeof(Decimal)
					|| srcType == typeof(Single);
		}

		public static object StringToNumber(string strValue, Type type)
		{
			if (type == typeof(Byte))
				return Convert.ToByte(strValue);
			if (type == typeof(SByte))
				return Convert.ToSByte(strValue);
			if (type == typeof(UInt16))
				return Convert.ToUInt16(strValue);
			if (type == typeof(UInt32))
				return Convert.ToUInt32(strValue);
			if (type == typeof(UInt64))
				return Convert.ToUInt64(strValue);
			if (type == typeof(Int16))
				return Convert.ToInt16(strValue);
			if (type == typeof(Int32))
				return Convert.ToInt32(strValue);
			if (type == typeof(Int64))
				return Convert.ToInt64(strValue);
			if (type == typeof(Decimal))
				return Convert.ToDecimal(strValue);
			if (type == typeof(Double))
				return Convert.ToDouble(strValue);
			if (type == typeof(Single))
				return Convert.ToSingle(strValue);

			throw new Exception("type 必须为数字型");
		}
	}

}



