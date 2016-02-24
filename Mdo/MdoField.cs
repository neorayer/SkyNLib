using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Sky.Mdo
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class MdoAttribute : Attribute
	{
		public String caption;
		public String tableName;
		public MdoAttribute()
		{
		}
	}


	/// <summary>
	/// Mdo对象属性注释
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class MdoFieldAttribute : Attribute
	{
		public string Caption;
		public bool IsKey = false;
		public int Len = 64;
		public string Format = String.Empty;
		public Type EnumType;
		public bool IsGroupable = false;
		public MdoFieldAttribute(String caption)
		{
			this.Caption = caption;
		}
	}



	public class MdoField
	{
		private PropertyInfo Pi;

		public String Name { get { return Pi.Name; } }

		public String Caption { get; set; }

		public Type Type { get { return Pi.PropertyType; } }

		public bool IsKey { get; set; }

		public int Len { get; set; }

		public string Format { get; private set; }

		public Type EnumType { get; private set; }

		public bool IsGroupable { get; private set; }

		public bool CanWrite { get { return Pi.CanWrite; } }

		public bool CanRead { get { return Pi.CanRead; } }

		private MdoFieldAttribute Attr { get;  set; }

		//private FastInvokeHandler Setter;

		//public SetValueDelegate setter;
		public MdoField(PropertyInfo pi)
		{
			Pi = pi;

			Caption = pi.Name;

		//	MethodInfo setterMi = pi.GetSetMethod();
		//	if (setterMi != null)
		//	{
		////		Setter = FastInvoke.GetMethodInvoker(pi.GetSetMethod());
		//	}
		//	if (!pi.DeclaringType.IsInterface)
		//	{

		//		setter = EmitInvoke.CreatePropertySetter(pi);
		//	}

			parseAttributes();
		}

		private void parseAttributes()
		{
			object[] attrs = Pi.GetCustomAttributes(typeof(MdoFieldAttribute), false);
			if (attrs.Length < 1)
				return;
			Attr = (MdoFieldAttribute)attrs[0];

			Caption = Attr.Caption;
			IsKey = Attr.IsKey;
			Len = Attr.Len;
			Format = Attr.Format;
			EnumType = Attr.EnumType;
			IsGroupable = Attr.IsGroupable;
		}

		
		public void setValue(object mdo, object v)
		{
			try
			{
				//if (Setter != null)
				//{
				//	Setter(mdo, new object[] { v });
				//}
				//if (setter != null)
				//{
				//	setter(mdo, v);
				//}
				if (Pi.CanWrite) //TODO 这里是为了防止没有set的属性(并且注释了MdoFieldAttribute）。
				{
					Pi.SetValue(mdo, v, null);
				}
			}
			catch (Exception e)
			{
				Sky.Logger.GlobalLog.Debug("MdoField.SetValue() Exception:" + Name);
				throw e;
			}
		}

		public Object getValue(object mdo)
		{
			return Pi.GetValue(mdo, null);
		}

	}
}
