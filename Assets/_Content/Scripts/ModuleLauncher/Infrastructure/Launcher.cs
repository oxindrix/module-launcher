using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ModuleSystem;
using Services.LoadingSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace ModuleLauncher.Infrastructure
{
	public class Launcher : ILoadableUnit<IModuleDescriptor>, IStartable, IDisposable
	{
		private LifetimeScope moduleScope;
		private IModule module;
		
		private readonly LifetimeScope lifetimeScope;
		private readonly IObjectResolver container;
		private readonly HashSet<IDisposable> disposables = new();


		public Launcher(LifetimeScope lifetimeScope)
		{
			this.lifetimeScope = lifetimeScope;
		}


		public UniTask Load(IModuleDescriptor descriptor)
		{
			module = CreateModule(descriptor);
			module.Load();

			return UniTask.CompletedTask;
		}


		private IModule CreateModule(IModuleDescriptor descriptor)
		{
			moduleScope = lifetimeScope.CreateChild(descriptor);
			var module = moduleScope.Container.Resolve<IModule>();
			
			return module;
		}


		public void Dispose()
		{
			moduleScope.Dispose();
			moduleScope = null;
			module = null;
			
			foreach (var disposable in disposables)
				disposable?.Dispose();
		}


		public void Start()
		{
			Debug.Log($"Starting module: {module.Descriptor.Name}\n{module.Descriptor.Description}");
			module.Start();
		}
	}
}
