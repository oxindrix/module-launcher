using Cysharp.Threading.Tasks;
using Infrastructure.GSM.States;


namespace Infrastructure.GSM
{
	public interface IGameStateSwitcher
	{
		UniTask SwitchTo<TState>() where TState : class, IGameState;
	}
}