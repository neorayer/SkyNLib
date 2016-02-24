using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sky.Mdo
{
	public interface INotifyer: INotifyPropertyChanged
	{
		void NotifyPropertyChanged(String propName);

		void NotifyAllPropertiesChanged();
	}
}
