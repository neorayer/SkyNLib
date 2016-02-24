using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using Sky.Mdo;
using Sky.Logger;

namespace Sky.Util
{
	public class DataSourceList<T> : BindingList<T> where T : IHasKey, INotifyer //where T : Mdo<T>
	{
		public void Swap(int idx1, int idx2)
		{
			T item = this[idx1];
			this[idx1] = this[idx2];
			this[idx2] = item;
		}

		public void AddRange(ICollection<T> items)
		{
			//Turn off the ListChanged Events
			this.RaiseListChangedEvents = false;
			foreach(T item in items.ToArray())
				Add(item);
			//Turn on the ListChanged Events
			this.RaiseListChangedEvents = true;
			//Notify the changes to the controls
			this.ResetBindings();
		}

		/// <summary>
		/// 此方法比先Clear()，后AddRange()的好处是，不会造成View的强制刷新。
		/// 视觉效果更加自然。
		/// 但由于要update和notify所有的参数，肯定会造成效率底下。不适合大量数据的更新。
		/// </summary>
		/// <param name="newItems"></param>
		public void ReplaceItems(ICollection<T> newItems)
		{
			//更新已有的，或添加新增的
			foreach(T item in newItems)
			{
				AddOrUpdateItem(item, true);
			}

			KeyEqualityComparer<T> comp = new KeyEqualityComparer<T>();
			//删除newItems里没有的
			foreach (T item in this.ToArray<T>())
			{
				if (!newItems.Contains(item, comp))
					this.Remove(item);
			}
		}

		/// <summary>
		///  增加或者修改一项数据。
		/// </summary>
		/// <param name="newItem"></param>
		/// <param name="isNotifyAllChanged">修改完毕后, 是否进行 NotifyAllPropertiesChanged</param>
		public T AddOrUpdateItem(T newItem, bool isNotifyAllChanged)
		{
			// 新增
			if (!this.Contains(newItem, new KeyEqualityComparer<T>()))
			{
				Add(newItem);
				return newItem;
			}

			// 修改
			foreach (T item in this.ToArray<T>())
			{
				if (item.KeyEquals(newItem))
				{
					MdoReflector.CopyMdoPropertiesOfDest(item, newItem);
					if (isNotifyAllChanged)
						item.NotifyAllPropertiesChanged();
					return item;
				}
			}

			throw new Exception("设计错误，不应该发生这样的问题");
		}

		public void RemoveByKey(T itemRm)
		{
			if (!this.Contains(itemRm, new KeyEqualityComparer<T>()))
				return;

			List<T> itemRms = new List<T>();
			foreach (T item in this.ToArray<T>())
			{
				if (item.KeyEquals(itemRm))
				{
					itemRms.Add(item);
					break;
				}
			}

			foreach (T item in itemRms)
			{
				Remove(item);
			}
		}
		///// <summary>
		///// 完全替换所有数据。注意，这里并没有进行先清除，后添加的操作。而是对以有项进行修改，新增项进行添加。
		///// 完毕后没有进行NotifyAllPropertiesChanged
		///// </summary>
		///// <param name="newItems"></param>
		//public void SetItems(IList<T> newItems)
		//{
		//    Dictionary<string, T> newItemsDict = new Dictionary<string, T>();
		//    foreach (T item in newItems.ToArray())
		//    {
		//        newItemsDict.Add(item.ToKeyString(), item);
		//    }
		//    List<T> needRemoveItems = new List<T>();
		//    List<T> needModifyItems = new List<T>();
		//    List<T> needAddItems = new List<T>();
		//    //找出需要删除的列表，和需要修改的列表
		//    foreach (T item in this.ToArray<T>())
		//        if (!newItems.Contains(item, new KeyEqualityComparer<T>()))
		//            needRemoveItems.Add(item);
		//        else
		//            needModifyItems.Add(item);

		//    //找出需要新增的列表
		//    foreach (T item in newItems.ToArray())
		//        if (!this.Contains(item, new KeyEqualityComparer<T>()))
		//            needAddItems.Add(item);

		//    // 删除
		//    foreach (T item in needRemoveItems)
		//        this.Remove(item);

		//    // 修改
		//    foreach (T item in needModifyItems)
		//        MdoReflector.CopyMdoPropertiesOfDest(item, newItemsDict[item.ToKeyString()]);

		//    // 增加
		//    foreach (T item in needAddItems)
		//        this.Add(item);

		//}

		private bool IsSorted = false;
		protected override bool IsSortedCore
		{
			get
			{
				return IsSorted;
			}
		}

		private PropertyDescriptor SortProperty;
		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return SortProperty;
			}
		}

		private LinkedList<ListSortDescription> SortDescriptions = new LinkedList<ListSortDescription>();

		private void AddSortDescription(PropertyDescriptor prop, ListSortDirection direction)
		{
			//移除该属性以前在排序历史中的位置
			foreach (ListSortDescription sortDesc in new List<ListSortDescription>(SortDescriptions))
			{
				if (sortDesc.PropertyDescriptor.Name == prop.Name)
					SortDescriptions.Remove(sortDesc);
			}
			//将新的排序描述插入头部
			SortDescriptions.AddFirst(new ListSortDescription(prop, direction));
		}

		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			if (this.Items == null)
				return;
			List<T> items = this.Items as List<T>;

			IsSorted = true;

			SortProperty = prop;
			SortDirection = direction;
			AddSortDescription(prop, direction);

			PropertyComparer<T> comparer = new PropertyComparer<T>(SortDescriptions);
			items.Sort(comparer);

			this.OnListChanged(
			 new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		protected override void RemoveSortCore()
		{
			this.IsSorted = false;
			//TODO
		}

		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		private ListSortDirection SortDirection;
		protected override ListSortDirection SortDirectionCore
		{
			get {
			return SortDirection;
			}
		}
	
	}
}
