using Cysharp.Threading.Tasks;


namespace Infrastructure.StateMachines.States
{
	public interface IExitableState
	{
		public UniTask Exit();
	}
}
