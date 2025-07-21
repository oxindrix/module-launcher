using General.Implementation.Infrastructure.GSM;
using Infrastructure.GSM;
using Infrastructure.GSM.States;
using VContainer;
using VContainer.Unity;


namespace Modules.CatLady.GameStates
{
	public class StatesInstaller : IInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			builder.Register<IGameState, Preload>(Lifetime.Scoped);
			builder.Register<IGameState, BeforeGame>(Lifetime.Scoped);
			builder.Register<IGameState, Game>(Lifetime.Scoped);
			builder.Register<IGameState, GameOver>(Lifetime.Scoped);
			
			builder.Register<GameStateMachine>(Lifetime.Singleton).As<IGameStateSwitcher>();
		}
	}
}