using Cysharp.Threading.Tasks;


namespace Services.LoadingSystem
{
	public interface ILoadingService
	{
		UniTask Load(ILoadableUnit unit);
		UniTask Load<TPayload>(ILoadableUnit<TPayload> unit, TPayload payload);
	}
}