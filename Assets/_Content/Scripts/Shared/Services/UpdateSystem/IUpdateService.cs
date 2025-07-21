using System;
using VContainer.Unity;


namespace Services.UpdateSystem
{
	public interface IUpdateService : ITickable, IPostTickable, ILateTickable, IPostLateTickable, IFixedTickable, IPostFixedTickable
	{
		IDisposable SubscribeTickable(ITickable tickable);
		IDisposable AddPostTickable(IPostTickable postTickable);
		IDisposable AddLateTickable(ILateTickable lateTickable);
		IDisposable AddPostLateTickable(IPostLateTickable postLateTickable);
		IDisposable AddFixedTickable(IFixedTickable fixedTickable);
		IDisposable AddPostFixedTickable(IPostFixedTickable postFixedTickable);
	}
}
