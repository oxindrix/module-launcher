using General.Core.Scopes;
using General.Implementation.Providers;
using General.Implementation.Services.Support;
using Providers;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace ModuleLauncher.Infrastructure
{
	public class Scope : LifetimeScopeWithSettings
	{
		[SerializeField] private SceneObjectsProvider sceneObjectsProvider;
		
		
		protected override void Configure(IContainerBuilder builder)
		{
			base.Configure(builder);

			builder.RegisterInstance(sceneObjectsProvider).As<ISceneComponentsProvider, ISceneScriptsProvider>();
			
			builder.Register<PlatformDetector>(Lifetime.Singleton);
			builder.Register<Launcher>(Lifetime.Singleton);

			builder.RegisterEntryPoint<Flow>();
		}
	}
}
