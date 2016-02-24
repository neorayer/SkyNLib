using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sky.Util;

namespace Sky.UI
{
	/// <summary>
	/// 注意，使用之前，一定要先在合适的位置调用：Sky.UI.UITools.UiContext = SynchronizationContext.Current;
	/// </summary>
	public static class UITools
	{
		
		public static SynchronizationContext UiContext;

		/// <summary>
		/// 单纯用Invoke(Action action),会造成执行顺序上的困惑。
		/// 因为UiContext.Post()在发送了一个消息后就结束了，并不会等待action()执行完毕。 
		/// 尽量使用。 Invoke(ISynchronizeInvoke control, Action action)
		/// </summary>
		/// <param name="action"></param>
		public static void Invoke(Action action)
		{
			UiContext.Post(delegate(object state) { action(); }, null);
		}

		/// <summary>
		/// 跟 Invoke(Action action)的区别是。这个方法会根据control的需求进行判断。
		/// 否则单纯用Invoke(Action action),会造成执行顺序上的困惑。
		/// 因为UiContext.Post()在发送了一个消息后就结束了，并不会等待action()执行完毕。
		/// </summary>
		/// <param name="control"></param>
		/// <param name="action"></param>
		public static void Invoke(ISynchronizeInvoke control, Action action)
		{
			if (control.InvokeRequired)
				UiContext.Post(delegate(object state) { action(); }, null);
			else
				action();
		}

		public static Size StringDrawingSize(string str, Font font)
		{
			SizeF size;
			using (Bitmap tmpBmp = new Bitmap(1, 1))
			using (Graphics g = Graphics.FromImage(tmpBmp))
				size = g.MeasureString(str, font);
			return new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
		}

		public static Control FindFocusedControl(Control control)
		{
			var container = control as ContainerControl;
			return (null != container ? FindFocusedControl(container.ActiveControl) : control);
		}

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="action"> 返回值为bool的方法。返回true,表示Foreach不再继续。否则继续。</param>
		public static void ForeachControls<T>(Control container, ReturnAction<bool, T> action) where T : Control
		{
			foreach (Control c in container.Controls)
			{
				if (c is T)
				{
					bool isStop = action((T)c);
					if (isStop)
						return;
				}
				ForeachControls<T>(c, action);
			}
		}

		public static void ForeachControls<T>(Control container, Action<T> action) where T : Control
		{
			foreach (Control c in container.Controls)
			{
				if (c is T)
					action((T)c);
				ForeachControls<T>(c, action);
			}
		}
		
		public static T FindParentOfType<T>(Control ctl) where T : Control
		{
			if (ctl.Parent == null)
				return null;
			if (ctl.Parent is T)
				return ctl.Parent as T;
			return FindParentOfType<T>(ctl.Parent);
		}
	}
}
