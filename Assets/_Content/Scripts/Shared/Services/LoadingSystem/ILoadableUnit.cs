using Cysharp.Threading.Tasks;


namespace Services.LoadingSystem
{
	public interface ILoadableUnit
	{
		UniTask Load();
	}


	public interface ILoadableUnit<in TPayload>
	{
		UniTask Load(TPayload payload);
	}
}
