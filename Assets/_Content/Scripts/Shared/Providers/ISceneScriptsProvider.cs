using UnityEngine;


namespace Providers
{
	public interface ISceneScriptsProvider
	{
		bool HasScript<T>(string id) where T : MonoBehaviour;

		bool TryGetScript<T>(string id, out T obj) where T : MonoBehaviour;
		
		void RegisterScript<T>(string id, T script) where T : MonoBehaviour;
		
		void RemoveScript<T>(string id) where T : MonoBehaviour;
	}
}