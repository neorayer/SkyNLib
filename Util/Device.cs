using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Sky.Util
{
	public static class Device
	{
		public static List<string> ManagementPropertyValues(string path, string key)
		{
			List<string> values = new List<string>();

			ManagementClass cimobject = new ManagementClass(path);
			ManagementObjectCollection moc = cimobject.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				PropertyData pd = mo.Properties[key];
				if (pd == null)
					continue;
				values.Add(pd.Value.ToString());
			}
			return values;
		}

		public static List<string> ProcessorIDs()
		{
			return ManagementPropertyValues("Win32_Processor", "ProcessorId");
		}

		public static List<string> HardiskIDs()
		{
			return ManagementPropertyValues("Win32_DiskDrive", "Model");
		}

		public static List<string> MacAddresses()
		{
			return ManagementPropertyValues("Win32_NetworkAdapterConfiguration", "MacAddress");
		}
	}
}
