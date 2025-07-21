using Cysharp.Threading.Tasks;


namespace Infrastructure.StateMachines.States
{
	public interface IState : IExitableState
	{
		public UniTask Enter();
	}
}
