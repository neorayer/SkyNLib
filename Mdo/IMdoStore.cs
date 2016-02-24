using System;
using System.Collections.Generic;
namespace Sky.Mdo
{
	public interface IMdoStore
	{
		MdoStore RealStore { get; }
		int CountFormat<T>(string format, params object[] values);
		T Create<T>(T mdo);
		void Create<T>(ICollection<T> mdos);
		T CreateIfNotExists<T>(T mdo);
		void Delete<T>(T mdo);
		void DeleteMass<T>(ICollection<T> mdos);
		void DeleteFormat<T>(string format, params object[] values) where T:Mdo<T>;
		MdoList<T> Find<T>() where T : Mdo<T>;
		MdoList<T> FindFormat<T>(string format, params object[] values) where T : Mdo<T>;
		T Load<T>(T mdo);
		void Update<T>(T mdo, System.Collections.Generic.IDictionary<string, object> data);
		void Update<T>(T mdo, string format, params object[] values);

		bool Exists<T>(T mdo);

	}
}
