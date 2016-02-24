using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Sky.Mdo
{
	public class PropertyComparer<T> : IComparer<T> 
	{
		private List<ListSortDescription> SortDescriptions = new List<ListSortDescription>();
		private Dictionary<ListSortDescription, PropertyInfo> PropertyInfos = new Dictionary<ListSortDescription, PropertyInfo>();

		public PropertyComparer(ICollection<ListSortDescription> sortDescriptions)
		{
			SortDescriptions.AddRange(sortDescriptions);
			Type type = typeof(T);
			foreach (ListSortDescription desc in SortDescriptions)
			{
				string propName = desc.PropertyDescriptor.Name;
				PropertyInfo pi = type.GetProperty(propName);
				if (pi == null)
					throw new Exception("没有这个属性：" + propName);
				PropertyInfos[desc] = pi;
			}
		}

		public  int Compare(T x, T y)
		{
			foreach (ListSortDescription desc in SortDescriptions)
			{
				int c = Compare(x, y, PropertyInfos[desc], desc.SortDirection);
				if (c != 0)
					return c;
			}
			return 0;
		}

		private static int Compare(T x, T y, PropertyInfo pi, ListSortDirection direction)
		{
			object x_value = pi.GetValue(x, null);
			object y_value = pi.GetValue(y, null);

			if (x_value == null && y_value == null)
				return 0;

			if (x_value == null)
				return -1;

			if (y_value == null)
				return 1;

			IComparable x_c = x_value as IComparable;
			IComparable y_c = y_value as IComparable;
			if (x_c == null && y_c == null)
				return 0;

			if (x_c == null)
				return -1;

			if (y_c == null)
				return 1;

			int r = x_c.CompareTo(y_c);
			return direction == ListSortDirection.Ascending ? r : -r;
		}

	


	}
}
