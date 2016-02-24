using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using Sky.UI;

namespace Sky.Util
{
	public struct IconInfo
	{
		public bool fIcon;
		public int xHotspot;
		public int yHotspot;
		public IntPtr hbmMask;
		public IntPtr hbmColor;
	}

	public class CursorTools
	{

		[DllImport("user32.dll")]
		public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

		public static Cursor CreateCursor(Bitmap bmp,
			int xHotSpot, int yHotSpot)
		{
			IconInfo tmp = new IconInfo();
			GetIconInfo(bmp.GetHicon(), ref tmp);
			tmp.xHotspot = xHotSpot;
			tmp.yHotspot = yHotSpot;
			tmp.fIcon = false;
			return new Cursor(CreateIconIndirect(ref tmp));
		}

		//检查鼠标是否在一个Rectangle范围内，后面4个参数为四边允许的扩展
		public static bool IsCursorInRange(Rectangle rect, int topSpan, int rightSpan, int bottomSpan, int leftSpan)
		{
			Point mp = Cursor.Position; // Mouse Position
			return mp.X >= rect.Left - leftSpan
				&& mp.X <= rect.Right + rightSpan
				&& mp.Y >= rect.Top - topSpan
				&& mp.Y <= rect.Bottom + bottomSpan;
		}

		public static Cursor CreateCursor(string str)
		{
			Font font = new Font("微软雅黑", 12, FontStyle.Bold);
			return CreateCursor(str, font);
		}

		public static Cursor CreateCursor(string str, Font font)
		{
			Size size = UITools.StringDrawingSize(str, font);

			Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
			using (Bitmap bitmap = new Bitmap(rect.Width + 2, rect.Height + 2))
			{
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					g.FillRectangle(Brushes.White, rect);
					g.DrawRectangle(Pens.Black, rect);
					g.DrawString(str, font, Brushes.Black, 0, 0);
				}
				return CursorTools.CreateCursor(bitmap, 0, 0);
			}
		}


	}
}
