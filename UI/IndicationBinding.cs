using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Sky.UI
{
	/// <summary>
	/// 建立一个控件与数据源的绑定。具备升降指示功能（红绿）。
	/// </summary>
	public class IndicationBinding : Binding
	{

		public IndicationBinding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
			: base(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode)
		{
			int formatTimes = 0;
			IComparable lastValue = null;
			Format += delegate(object sender, ConvertEventArgs e)
			{
				//升降红绿显示
				IComparable value = (IComparable)e.Value;
				if (value == null)
					return;

				if (lastValue == null)
				{
					lastValue = (IComparable)e.Value;
					return;
				}

				// format数次以后，才开始处理。目的：一开始有不合理，不完整数据。
				if (formatTimes < 3) {
					formatTimes++;
					lastValue = (IComparable)e.Value;
					return;
				}

				int compRes = value.CompareTo(lastValue);
				if (compRes > 0)
				{
					Control.ForeColor = Color.Red;
				}
				else if (compRes < 0)
				{
					Control.ForeColor = Color.Green;
				}
				else
				{
					//TODO 有可能需要隐掉。原因：marketdata的tick太快的时候，多数价格是不变化的。因此一直看到黑色。
					Control.ForeColor = Color.Black; 
				}

				lastValue = (IComparable)e.Value;
			};
		}

		public static IndicationBinding Create(Control control, string propertyName, object dataSource, string dataMember)
		{
			IndicationBinding binding = new IndicationBinding(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.Never);
			control.DataBindings.Add(binding);
			return binding;
		}

		public static IndicationBinding Create(Control control, string propertyName, object dataSource, string dataMember, ConvertEventHandler additionFormat)
		{
			IndicationBinding binding = new IndicationBinding(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.Never);
			binding.Format += additionFormat;
			control.DataBindings.Add(binding);
			return binding;
		}
	}
}
