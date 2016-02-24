using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sky.Mdo
{

	public class Mdo<T> : INotifyer, IHasKey where T : Sky.Mdo.Mdo<T> 
	{
		private static IMdoStore m_Store;
		public static IMdoStore Store { 
			set { m_Store = value; }
			get { return m_Store; }
		}


		public static T NewInstance()
		{
			return (T)typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);
		}

		public T Clone()
		{
			T newMdo = Mdo<T>.NewInstance();
			newMdo.SetValuesFrom(this);
			return newMdo;
		}

		public void SetValuesFrom(object src)
		{
			MdoReflector.CopyMdoPropertiesOfDest(this, src);
		}

		public void SetValuesFrom(object src, bool isNotifyAll)
		{
			if (!isNotifyAll)
			{
				SetValuesFrom(src);
				return;
			}

			if (isNotifyAll)
			{
				List<string> changedProps = new List<string>();
				MdoReflector.CopyMdoPropertiesOfDest(this, src, changedProps);
				foreach (string propName in changedProps)
				{
					NotifyPropertyChanged(propName);
				}
			}
		}

		public void CopyPropertiesTo(object dest)
		{
			MdoReflector.CopyMdoPropertiesOfSrc(dest, this);
		}

		public override string ToString()
		{
			string s = typeof(T).Name + ": ";
			MdoReflector mrf = MdoReflector.get(typeof(T));
			foreach (MdoField mf in mrf.Fields.Values)
			{
				s += mf.Name + "=" + mf.getValue(this) + " ";
			}
			return s;
		}

		public virtual string ToKeyString()
		{
			string s = typeof(T).Name + ": ";
			MdoReflector mrf = MdoReflector.get(typeof(T));
			foreach (MdoField mf in mrf.Fields.Values)
			{
				if (mf.IsKey)
					s += mf.Name + "=" + mf.getValue(this) + " ";
			}
			return s;
		}

		public object GetPropertyValue(string propertyName)
		{
			MdoReflector mrf = MdoReflector.get(typeof(T));
			return mrf.Fields[propertyName].getValue(this);
		}

		/// <summary>
		/// 主键是否相同
		/// </summary>
		/// <param name="newObj"></param>
		/// <returns></returns>
		public bool KeyEquals(object newObj)
		{
			IHasKey objHasKey = newObj as IHasKey;
			if (objHasKey == null)
				return false;
			return this.ToKeyString().Equals(objHasKey.ToKeyString());
		}

		public virtual T Load()
		{
			T o = m_Store.Load<T>((T)this);
			DeepLoad();
			return o;
		}

		public virtual void DeepLoad()
		{
		}

		public virtual T Create()
		{
			return m_Store.Create((T)this);
		}

		public virtual T Update()
		{
			return Update(ToDictionary());
		}

		public T CreateOrUpdate()
		{
			if (!Exists())
				return Create();

			return Update(ToDictionary());
		}

		public IDictionary<string, object> ToDictionary()
		{
			MdoReflector reflector = MdoReflector.get(typeof(T));
			return reflector.getDictionary(this);
		}


		public T CreateIfNotExists() {
			return m_Store.CreateIfNotExists((T)this);
		}

		public bool Exists()
		{
			return m_Store.Exists((T)this);
		}

		/// <summary>
		/// update以后，本身的数值并没有修改。
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual T Update(IDictionary<string, object> data)
		{
			m_Store.Update((T)this, data);
			return (T)this;
		}

		public virtual T Update(string format, params object[] values)
		{
			 m_Store.Update((T)this, format, values);
			 return (T)this;
		}

		public virtual void Delete()
		{
			m_Store.Delete((T)this);
		}

		public static MdoList<T> Find()
		{
			return m_Store.Find<T>();
		}

		public static int Count()
		{
			return CountFormat(null);
		}

		public static int CountFormat(string format, params object[] values) {
			return m_Store.CountFormat<T>(format, values);
		}

		/// <summary>
		/// 条件查询。
		/// 与String.Format不同，这里在生成SQL时会自动修改成SQL安全的语句。
		/// 如你可以直接写成：ItemAndOrder.FindFormat("TypeID={0}", "AccountGrid"); 
		/// 而不需要在写成："TypeID='{0}'"。
		/// </summary>
		/// <param name="format"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static MdoList<T> FindFormat(string format, params object[] values)
		{
			return m_Store.FindFormat<T>(format, values);
		}

		public static void CreateTable()
		{
			m_Store.RealStore.CreateTable(typeof(T));
		}

		public static void DropTable()
		{
			m_Store.RealStore.dropTable(typeof(T));
		}	

		public static void RecreateTable()
		{
			DropTable();
			CreateTable();
		}

		public static void CreateTableIfNotExsts()
		{
			m_Store.RealStore.CreateTableIfNotExsts(typeof(T));
		}	

		public static void AlterTableAddColumn(string propName, object defaultValue)
		{
			m_Store.RealStore.AlterTableAddColumn(typeof(T), propName, defaultValue);
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

		public void NotifyAllPropertiesChanged()
		{
			MdoReflector mrf = MdoReflector.get(typeof(T));
			foreach (MdoField mf in mrf.Fields.Values)
			{
				NotifyPropertyChanged(mf.Name);
			}
		}


	}
}
