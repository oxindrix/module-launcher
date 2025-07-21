using UnityEngine;


namespace Providers
{
	public interface ISceneComponentsProvider
	{
		bool HasComponent<T>(string id)  where T : Component;

		bool TryGetComponent<T>(string id, out T obj)  where T : Component;
		
		void RegisterComponent<T>(string id, T component)  where T : Component;
		
		void RemoveComponent<T>(string id)  where T : Component;
	}
}