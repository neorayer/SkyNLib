using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;

namespace Sky.Util
{
	public class CommonReflector
	{
		private static IDictionary<Type, IDictionary<string, PropertyInfo>> TypePropertyInfos = new ConcurrentDictionary<Type, IDictionary<string, PropertyInfo>>();
		private static IDictionary<Type, IDictionary<string, FieldInfo>> TypeFieldInfos = new ConcurrentDictionary<Type, IDictionary<string, FieldInfo>>();
		private static IDictionary<string, PropertyInfo> GetTypePropertyInfos(Type type)
		{
			if (!TypePropertyInfos.ContainsKey(type))
			{
				//加了lock，然后再判断，达到严格的并发处理
				lock (TypePropertyInfos)
				{
					if (!TypePropertyInfos.ContainsKey(type))
					{
						var pDict = new ConcurrentDictionary<string, PropertyInfo>();
						foreach (PropertyInfo pi in type.GetProperties())
							pDict[pi.Name] = pi;
						TypePropertyInfos[type] = pDict;
					}
				}
			}
			return TypePropertyInfos[type];
		}

		private static IDictionary<string, FieldInfo> GetTypeFieldInfos(Type type)
		{
			if (!TypeFieldInfos.ContainsKey(type))
			{
				//加了lock，然后再判断，达到严格的并发处理
				lock (TypeFieldInfos)
				{
					if (!TypeFieldInfos.ContainsKey(type))
					{
						var fDict = new ConcurrentDictionary<string, FieldInfo>();
						foreach (FieldInfo fi in type.GetFields())
							fDict[fi.Name] = fi;
						TypeFieldInfos[type] = fDict;
					}
				}
			}
			return TypeFieldInfos[type];
		}

		private static void GetPropertyOrFieldInfo(Type type, string propName, out PropertyInfo propertyInfo, out FieldInfo fieldInfo)
		{
			propertyInfo = null;
			fieldInfo = null;

			// PropertyInfos
			var pDict = GetTypePropertyInfos(type);
			if (pDict.ContainsKey(propName))
			{
				propertyInfo = pDict[propName];
				return;
			}

			// FieldInfos
			var fDict = GetTypeFieldInfos(type);
			if (fDict.ContainsKey(propName))
			{
				fieldInfo = fDict[propName];
				return;
			}
		}
	}
}




