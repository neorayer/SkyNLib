using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sky.Mdo
{
	public class MdoList<T> : List<T> where T : Mdo<T>
	{
		public MdoList():base()
		{
		}	

		public MdoList(IEnumerable<T> collection):base(collection)
		{
		}	

		public void Delete()
		{
			foreach (T o in this)
				o.Delete();
		}

		/// <summary>
		/// 将所有成员的的propertyName属性的值取出来，单独组成一个List
		/// </summary>
		/// <typeparam name="PT"></typeparam>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public List<PT> FetchList<PT>(string propertyName)
		{
			List<PT> list = new List<PT>();
			foreach (T o in this)
			{
				list.Add((PT)o.GetPropertyValue(propertyName));
			}
			return list;
		}

		public List<GroupType> GetGroupBy<GroupType>(string groupProperty, string selectProperty)
		{
			List<GroupType> groups = new List<GroupType>();
			Dictionary<object, Boolean> keyDict = new Dictionary<object, bool>();
			foreach (T item in this.ToArray<T>())
			{ 
				object groupValue = item.GetPropertyValue(groupProperty);
				if (keyDict.ContainsKey(groupValue))
					continue;
				keyDict[groupValue] = true;
				object selectValue = item.GetPropertyValue(selectProperty);
				groups.Add((GroupType)selectValue);
			};

			return groups;
		}
	
	}
}
