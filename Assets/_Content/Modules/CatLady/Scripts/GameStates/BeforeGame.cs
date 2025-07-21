using System;
using Cysharp.Threading.Tasks;
using General.Implementation.Infrastructure.GSM.States;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.UI;
using Providers;
using Services.InputSystem;


namespace Modules.CatLady.GameStates
{
	/// <summary>Customize the start position</summary>
	public class BeforeGame : InterruptibleGameState
	{
		private Hud hud;
		
		private readonly GameContext context;
		private readonly GameSession session;
		private readonly ISceneScriptsProvider scriptsProvider;
		private readonly IInputSystem input;


		public BeforeGame(GameContext context, GameSession session, ISceneScriptsProvider scriptsProvider, IInputSystem input)
		{
			this.context = context;
			this.session = session;
			this.scriptsProvider = scriptsProvider;
			this.input = input;
		}
		
		
		public override async UniTask Enter()
		{
			await base.Enter();
			
			if (!scriptsProvider.TryGetScript(Constants.ID_LOCATION, out Location location))
				throw new Exception("Location not found on scene");

			location.GameRenderer.SetSkins(context.GameSettings.SkinSettings);
			
			context.Score = 0;
			session.Reset(context.GameSettings);

			if (!scriptsProvider.TryGetScript(Constants.ID_HUD, out hud))
				throw new Exception("Hud not found on scene");

			hud.SetScore(0);
			hud.SetStartInfoVisible(true);
			
			location.GameRenderer.Clear();
			location.GameRenderer.AttachSnake(session.Snake);
			location.GameRenderer.CreateTarget(session.TargetPosition);
			location.GameRenderer.SetHeadDirection(session.CurrentDirection);
		}


		public override UniTask Exit()
		{
			hud.SetStartInfoVisible(false);
			
			return base.Exit();
		}


		protected override async UniTask Run()
		{
			while (!input.AnyKeyDown) 
				await UniTask.Yield();
			
			switcher.SwitchTo<Game>().Forget();
		}
	}
}