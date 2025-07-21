using UnityEngine;



namespace Providers
{
	public interface ISettingsProvider
	{
		public bool TryGetSettings<T>(string id, out T settings) where T : ScriptableObject;
	}
}