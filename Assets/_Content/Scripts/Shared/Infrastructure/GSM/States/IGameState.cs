using Infrastructure.StateMachines.States;


namespace Infrastructure.GSM.States
{
	public interface IGameState : IState
	{
		public void SetSwitcher(IGameStateSwitcher switcher);
	}

}