using Cysharp.Threading.Tasks;
using General.Implementation.Infrastructure.GSM.States;
using Modules.CatLady.DTO;
using Modules.CatLady.DTO.Signals;
using Modules.CatLady.UI;
using Providers;
using Services.SignalBusSystem;


namespace Modules.CatLady.GameStates
{
	/// <summary>Shows final text and the ability to restart</summary>
	public class GameOver : GameState
	{
		private Hud hud;

		private readonly GameContext context;
		private readonly ISignalBus signalBus;
		private readonly ISceneScriptsProvider scriptsProvider;


		public GameOver(GameContext context, ISignalBus signalBus, ISceneScriptsProvider scriptsProvider)
		{
			this.context = context;
			this.signalBus = signalBus;
			this.scriptsProvider = scriptsProvider;
		}
		
		public override UniTask Enter()
		{
			signalBus.Subscribe<OnRestartRequested>(HandleRestart);
			
			if (!scriptsProvider.TryGetScript(Constants.ID_HUD, out hud))
				throw new System.Exception("Hud not found on scene");
			
			hud.SetFinalScoreVisible(true);
			hud.SetFinalMessageVisible(true);
			hud.SetRestartButtonVisible(true);
			
			return UniTask.CompletedTask;
		}


		public override UniTask Exit()
		{
			signalBus.UnSubscribe<OnRestartRequested>(HandleRestart);
			
			hud.SetFinalScoreVisible(false);
			hud.SetFinalMessageVisible(false);
			hud.SetRestartButtonVisible(false);
			
			return base.Exit();
		}


		private void HandleRestart(OnRestartRequested data)
		{
			_ = Restart();
		}


		private async UniTask Restart()
		{
			await UniTask.Yield();
			switcher.SwitchTo<BeforeGame>().Forget();
		}
	}
}