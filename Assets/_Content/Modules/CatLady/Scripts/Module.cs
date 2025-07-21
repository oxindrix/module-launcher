using System;
using Cysharp.Threading.Tasks;
using Infrastructure.GSM;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.GameStates;
using Modules.CatLady.UI;
using Modules.CatLady.Settings;
using ModuleSystem;
using Providers;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;


namespace Modules.CatLady
{
	public class Module : IModule
	{
		public IModuleDescriptor Descriptor { get; }
		
		private Location location;
		private Hud hud;

		private readonly IObjectResolver container;
		private readonly ISceneScriptsProvider scriptsProvider;
		private readonly ISceneComponentsProvider componentsProvider;
		private readonly ISettingsProvider settingsProvider;
		private readonly IGameStateSwitcher gameStateSwitcher;


		public Module(
			IObjectResolver container,
			ISceneScriptsProvider scriptsProvider,
			ISceneComponentsProvider componentsProvider,
			ISettingsProvider settingsProvider,
			IGameStateSwitcher gameStateSwitcher,
			IModuleDescriptor descriptor
		)
		{
			this.container = container;
			this.scriptsProvider = scriptsProvider;
			this.componentsProvider = componentsProvider;
			this.settingsProvider = settingsProvider;
			this.gameStateSwitcher = gameStateSwitcher;
			Descriptor = descriptor;
		}


		public UniTask Load()
		{
			if (!settingsProvider.TryGetSettings(Constants.ID_SETTINGS, out ModuleSettings settings))
				throw new Exception($"No {nameof(ModuleSettings)} found");

			if (!scriptsProvider.TryGetScript(Constants.ID_LOCATION, out location))
			{
				if (!componentsProvider.TryGetComponent("RootTransform", out Transform rootTransform))
					throw new Exception($"Can't find object with id \"{"RootTransform"}\"");

				location = Object.Instantiate(settings.Location, rootTransform);
				location.transform.SetParent(rootTransform);
				container.InjectGameObject(location.gameObject);

				scriptsProvider.RegisterScript(Constants.ID_LOCATION, location);
			}
			
			if (!scriptsProvider.TryGetScript(Constants.ID_HUD, out hud))
			{
				if (!componentsProvider.TryGetComponent(Constants.ID_HOST_ROOT_RECT, out RectTransform rootRectTransform))
					throw new Exception($"Can't find object with id \"{Constants.ID_HOST_ROOT_RECT}\"");

				hud = Object.Instantiate(settings.Hud, rootRectTransform);
				hud.transform.SetParent(rootRectTransform);
				container.Inject(hud);

				scriptsProvider.RegisterScript(Constants.ID_HUD, hud);
			}

			return UniTask.CompletedTask;
		}


		public void Start()
		{
			gameStateSwitcher.SwitchTo<Preload>();
		}


		public void Dispose()
		{
			if (location)
			{
				scriptsProvider.RemoveScript<Location>(Constants.ID_LOCATION);
				Object.Destroy(location.gameObject);
			}

			if (hud)
			{
				scriptsProvider.RemoveScript<Hud>(Constants.ID_HUD);
				Object.Destroy(hud.gameObject);
			}
		}
	}
}