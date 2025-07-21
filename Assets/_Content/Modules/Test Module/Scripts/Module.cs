using System;
using Cysharp.Threading.Tasks;
using ModuleSystem;
using Providers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace Modules.TestModule
{
	public class Module : IModule
	{
		public IModuleDescriptor Descriptor { get; }
		
		private const string MAIN_OBJECT = "MainObject";
		private const string MAIN_TEXT = "MainText";

		private Transform mainObject;
		private Text mainText;

		private readonly ISceneComponentsProvider componentsProvider;
		private readonly ISettingsProvider settingsProvider;


		public Module(ISceneComponentsProvider componentsProvider, ISettingsProvider settingsProvider, IModuleDescriptor descriptor)
		{
			this.componentsProvider = componentsProvider;
			this.settingsProvider = settingsProvider;
			Descriptor = descriptor;
		}


		public UniTask Load()
		{
			// prepare everything

			// TODO: load prefabs
			if (!settingsProvider.TryGetSettings("settings", out ModuleSettings settings))
				throw new Exception($"No {nameof(ModuleSettings)} found");

			if (!componentsProvider.TryGetComponent(MAIN_OBJECT, out mainObject))
			{
				// TODO: get root transform
				if (!componentsProvider.TryGetComponent("RootTransform", out Transform rootTransform))
					throw new Exception($"Can't find object with id \"{"RootTransform"}\"");

				// TODO: instantiate prefabs
				mainObject = Object.Instantiate(settings.mainObjectPrefab, rootTransform);
				mainObject.transform.SetParent(rootTransform);

				componentsProvider.RegisterComponent(MAIN_OBJECT, mainObject);
			}

			if (!componentsProvider.TryGetComponent(MAIN_TEXT, out mainText))
			{
				// TODO: get root rectTransform
				if (!componentsProvider.TryGetComponent("RootRectTransform", out RectTransform rootRectTransform))
					throw new Exception($"Can't find object with id \"{"RootTransform"}\"");

				// TODO: instantiate prefabs
				mainText = Object.Instantiate(settings.mainTextPrefab, rootRectTransform);
				mainText.transform.SetParent(rootRectTransform);

				componentsProvider.RegisterComponent(MAIN_TEXT, mainText);
			}

			return UniTask.CompletedTask;
		}


		public void Start()
		{
			// TODO: start first behavior
			mainText.text = "Hello World!";
			mainObject.Translate(Vector3.right * 4);
		}


		public void Dispose()
		{
			// TODO: stop behaviors
			
			// TODO: dispose all resources
			if (mainObject)
			{
				componentsProvider.RemoveComponent<Transform>(MAIN_OBJECT);
				Object.Destroy(mainObject.gameObject);
			}

			if (mainText)
			{
				componentsProvider.RemoveComponent<Text>(MAIN_TEXT);
				Object.Destroy(mainText.gameObject);
			}

			Debug.Log($"Objects disposed!");
		}
	}
}