using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Sky.Util
{

	public static class Consts
	{
		public static Dictionary<Type, StringDict> WordDicts = new Dictionary<Type, StringDict>();

		public static string LookUpWordDict(Type type, object key)
		{
			//如果没有类型字典，则返回key对象本身
			if (!WordDicts.ContainsKey(type))
				return key + "" ;

			return WordDicts[type].Lookup(type, key);
		}

		public static string LookUpWordDict<T>(object key)
		{
			return LookUpWordDict(typeof(T), key);
		}

		///////////////////////////////////////////////////////////

		public static Dictionary<Type, ColorDict> ForeColorDicts = new Dictionary<Type, ColorDict>();

		public static Color LookupForeColorDict(Type type, object key, Color defColor)
		{
			if (!ForeColorDicts.ContainsKey(type))
				return defColor;

			return ForeColorDicts[type].Lookup(type, key);
		}

		public static Color LookupForeColorDict<T>(object key, Color defColor)
		{
			return LookupForeColorDict(typeof(T), key, defColor);

		}

		///////////////////////////////////////////////////////////

		public static Dictionary<Type, ColorDict> BackColorDicts = new Dictionary<Type, ColorDict>();

		public static Color LookupBackColorDict(Type type, object key, Color defColor)
		{
			if (!BackColorDicts.ContainsKey(type))
				return defColor;

			return BackColorDicts[type].Lookup(type, key);
		}

		public static Color LookupBackColorDict<T>(object key, Color defColor)
		{
			return LookupBackColorDict(typeof(T), key, defColor);
		}

		public static List<ValueAndName> ValueAndNamesOfWordDict(Type enumType)
		{
			if (WordDicts.ContainsKey(enumType))
				return WordDicts[enumType].ValueAndNames();

			List<ValueAndName> list = new List<ValueAndName>();
			Array values = Enum.GetValues(enumType);
			foreach (object value in values)
			{
				list.Add(new ValueAndName(value, value + ""));
			}
			return list;
		}
	}

	public abstract class EnumDict<T> : Dictionary<object, T>
	{
		public T Lookup(Type enumType, object key)
		{
			// 如果能够直接找到，则返回
			if (ContainsKey(key))
				return this[key];

			//// 如果找不到，则从基础类型转换成目标枚举类型。
			//// TODO 需要注意的是枚举类型的基本型可能并不是int
			//int intValue = Convert.ToInt32(key.ToString());
			//if (Enum.IsDefined(enumType, intValue))
			//{
			//    object enumValue = Enum.ToObject(enumType, intValue);
			//    if (ContainsKey(enumValue))
			//        return this[enumValue];
			//}

			return GetDefaultValue(key);
		}

		//public T AdvLookup(Type type, object key)
		//{
		//    T defVal = GetDefaultValue(key);
		//    T val = Lookup(type, key);
		//    if (!val.Equals(defVal))
		//        return val;

		//    // 如果找不到，再尝试使用字符串转为‘字符'，再用字符的ASCII码查找
		//    // 注:这种变态的查找方式是为了适应InputOrder的comboOffset属性
		//    string s = key.ToString();
		//    if (s.Length > 0)
		//    {
		//        return Lookup(type, (int)s[0]);
		//    }

		//    return defVal;
		//}

		protected abstract T GetDefaultValue(object key);
	}

	public class StringDict : EnumDict<string>
	{
		protected override string GetDefaultValue(object key)
		{
			return key.ToString();
		}

		public List<ValueAndName> ValueAndNames()
		{
			List<ValueAndName> list = new List<ValueAndName>();
			foreach (object key in Keys)
			{
				object value = key;
				list.Add(new ValueAndName(value, this[key]));
			}
			return list;
		}
	}

	

	public class ColorDict : EnumDict<Color>
	{
		private Color DefaultColor = Color.Black;

		public ColorDict(Color defultColor)
		{
			DefaultColor = defultColor;
		}


		protected override Color GetDefaultValue(object key)
		{
			return DefaultColor;
		}
	}


	public class ValueAndName : INotifyPropertyChanged
	{
		public object Value { get; set; }
		public string DisplayName { get; set; }

		public ValueAndName(object value, string displayName)
		{
			Value = value;
			DisplayName = displayName;
		}

		public void PutToDict(System.Collections.IDictionary dict)
		{
			dict.Add(Value, this);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			if (PropertyChanged == null)
				return;

			if (Sky.UI.UITools.UiContext == null)
			{
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
			else
			{
				Sky.UI.UITools.Invoke(delegate()
				{
					if (PropertyChanged != null)
						PropertyChanged(this, new PropertyChangedEventArgs(propName));
				});
			}
		}
	}

}
