using System;
using Cysharp.Threading.Tasks;
using General.Implementation.Infrastructure.GSM.States;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.UI;
using Providers;
using Services.SignalBusSystem;
using Services.UpdateSystem;


namespace Modules.CatLady.GameStates
{
	/// <summary>Runs until game over or quitting</summary>
	public class Game : GameState
	{
		private IDisposable tickSubscription;
		private Hud hud;
		private Location location;
		// private Obstacle hitObstacle;
		
		private readonly GameContext context;
		private readonly ISignalBus signalBus;
		private readonly IUpdateService updateService;
		private readonly ISceneScriptsProvider scriptsProvider;
		private readonly GameSession session;


		public Game(GameContext context,
			ISignalBus signalBus,
			IUpdateService updateService,
			ISceneScriptsProvider scriptsProvider,
			GameSession session
		)
		{
			this.context = context;
			this.signalBus = signalBus;
			this.updateService = updateService;
			this.scriptsProvider = scriptsProvider;
			this.session = session;
		}


		public override UniTask Enter()
		{
			if (!scriptsProvider.TryGetScript(Constants.ID_HUD, out hud))
				throw new Exception("Hud not found on scene");

			if (!scriptsProvider.TryGetScript(Constants.ID_LOCATION, out location))
				throw new Exception("Location not found on scene");

			tickSubscription = updateService.SubscribeTickable(session);

			session.OnSnakeDead += SnakeDeadHandler;
			session.OnSnakeConsumedPoint += SnakeConsumedPointHandler;
			session.OnDirectionChanged += SnakeDirectionChangedHandler;
			session.OnNewTargetCreated += NewTargetCreatedHandler;
			session.OnSpeedChanged += SpeedChangedHandler;
			session.OnSnakeMoved += SnakeMovedHandler;
			
			return UniTask.CompletedTask;
		}


		public override UniTask Exit()
		{
			session.OnSnakeDead -= SnakeDeadHandler;
			session.OnSnakeConsumedPoint -= SnakeConsumedPointHandler;
			session.OnDirectionChanged -= SnakeDirectionChangedHandler;
			session.OnNewTargetCreated -= NewTargetCreatedHandler;
			session.OnSpeedChanged -= SpeedChangedHandler;
			session.OnSnakeMoved -= SnakeMovedHandler;
			
			hud.SetGameScoreVisible(false);
			
			tickSubscription?.Dispose();
			tickSubscription = null;
			
			return base.Exit();
		}


		private void SnakeDeadHandler()
		{
			switcher.SwitchTo<GameOver>().Forget();
		}
		

		private void SnakeConsumedPointHandler()
		{
			context.Score += 1;
			hud.SetScore((int)context.Score);
			
			location.GameRenderer.ConsumeTarget();
		}
		

		private void SnakeDirectionChangedHandler()
		{
			location.GameRenderer.SetHeadDirection(session.CurrentDirection);
		}
		

		private void NewTargetCreatedHandler()
		{
			location.GameRenderer.CreateTarget(session.TargetPosition);
		}
		

		private void SpeedChangedHandler()
		{
			location.GameRenderer.SetSpeed(session.Speed);
		}
		

		private void SnakeMovedHandler()
		{
			location.GameRenderer.MoveSnake();
		}
	}
}