using Cysharp.Threading.Tasks;


namespace Infrastructure.StateMachines.States
{
	public interface IPayloadedState<TPayload> : IExitableState
	{
		public UniTask Enter(TPayload payload);
	}
}
