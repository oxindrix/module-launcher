using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace PoolSystem
{
	public interface IUniversalObjectsPool
	{
		public Action<Object> ActionOnCreate { get; set; }
		public Action<Object> ActionOnGet { get; set; }
		public Transform Parent { get; set; }

		public T GetObject<T>(T prefab) where T : Object;
		public void ReturnObject<T>(T obj) where T : Object;
		public void ReturnAll<T>(T prefab) where T : Object;
		public void ReturnAll();
		public void Clear<T>() where T : Object;
		public void ClearAll();
	}
}
