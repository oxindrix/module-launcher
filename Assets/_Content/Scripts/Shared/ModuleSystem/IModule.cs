using System;
using Services.LoadingSystem;


namespace ModuleSystem
{
	public interface IModule : ILoadableUnit, IDisposable
	{
		public IModuleDescriptor Descriptor { get; }
		void Start();
	}
}