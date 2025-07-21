using System;
using Cysharp.Threading.Tasks;
using General.Implementation.Infrastructure.GSM.States;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.Settings;
using Modules.CatLady.UI;
using Providers;


namespace Modules.CatLady.GameStates
{
	/// <summary>Set up the whole game</summary>
	public class Preload : GameState
	{
		private readonly ISceneScriptsProvider scriptsProvider;
		private readonly ISettingsProvider settingsProvider;
		private readonly GameContext context;


		public Preload(ISceneScriptsProvider scriptsProvider, ISettingsProvider settingsProvider, GameContext context)
		{
			this.scriptsProvider = scriptsProvider;
			this.settingsProvider = settingsProvider;
			this.context = context;
		}
		
		
		public override UniTask Enter()
		{
			if (!settingsProvider.TryGetSettings(Constants.ID_SETTINGS, out ModuleSettings settings))
				throw new Exception($"{nameof(ModuleSettings)} not found!");
			
			context.GameSettings = settings.GameSettings;
			
			if (!scriptsProvider.TryGetScript(Constants.ID_HUD, out Hud hud))
				throw new Exception("Hud not found on scene");
			
			hud.SetStartInfoVisible(false);
			hud.SetGameScoreVisible(false);
			hud.SetFinalScoreVisible(false);
			hud.SetFinalMessageVisible(false);
			hud.SetRestartButtonVisible(false);
			
			if (!scriptsProvider.TryGetScript(Constants.ID_LOCATION, out Location location))
				throw new Exception("Location not found on scene");

			switcher.SwitchTo<BeforeGame>().Forget();
			return UniTask.CompletedTask;
		}
	}
}