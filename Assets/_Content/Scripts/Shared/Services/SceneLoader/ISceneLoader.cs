using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;


namespace Services.SceneLoader
{
	public interface ISceneLoader
	{
		Scene LastLoadedScene { get; }

		UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded = null);

		UniTaskVoid LoadSceneTrackingProcessAsync(string sceneName, Action<float> OnUpdate = null, Action onLoaded = null);
	}
}
