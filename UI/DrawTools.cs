using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sky.UI
{
	public static class DrawTools 
	{
		/// <summary>
		/// 绘制圆头矩形背景（注：不是圆角矩形）
		/// </summary>
		/// <param name="g"></param>
		/// <param name="cRect">外围尺寸</param>
		/// <param name="color">背景色</param>
		public static void DrawRoundedBackground(Graphics g, Rectangle cRect, Color color)
		{
			using (Brush brush = new SolidBrush(color))
			{
				//左边的圆形 
				{
					Rectangle rect = new Rectangle()
					{
						X = cRect.Left,
						Y = cRect.Top,
						Width = cRect.Height,
						Height = cRect.Height
					};
					if (!rect.IsEmpty)
						g.FillPie(brush, rect, 0, 360);
				}
				//中间的矩形
				{
					Rectangle rect = new Rectangle()
					{
						X = cRect.Left + cRect.Height / 2,
						Y = cRect.Top,
						Width = cRect.Width - cRect.Height,
						Height = cRect.Height
					};
					if (!rect.IsEmpty)
						g.FillRectangle(brush, rect);
				}
				//右边的圆形
				{
					Rectangle rect = new Rectangle()
					{
						X = cRect.Right - cRect.Height,
						Y = cRect.Top,
						Width = cRect.Height,
						Height = cRect.Height
					};
					if (!rect.IsEmpty)
						g.FillPie(brush, rect, 0, 360);
				}

				brush.Dispose();
			}
		}
	}
}
