using System;
using Services.LoadingSystem;
using VContainer.Unity;


namespace ModuleLauncher.Infrastructure
{
	public class Flow : IStartable, IDisposable
	{
		private readonly Settings settings;
		private readonly ILoadingService loadingService;
		private readonly Launcher launcher;


		public Flow(
			Settings settings,
			ILoadingService loadingService,
			Launcher launcher
		)
		{
			this.settings = settings;
			this.loadingService = loadingService;
			this.launcher = launcher;
		}


		public async void Start()
		{
			await loadingService.Load(launcher, settings.LaunchableModuleDescriptor);

			launcher.Start();
		}


		public void Dispose()
		{
		}
	}
}
