using ModuleLauncher;
using ModuleSystem;
using UnityEngine;
using VContainer;


namespace Modules.TestModule
{
	[CreateAssetMenu(menuName = "Module/Test Module", fileName = "Test Module Descriptor")]
	public class Descriptor : ModuleDescriptor
	{
		public override void Install(IContainerBuilder builder)
		{
			builder.Register<IModule, Module>(Lifetime.Singleton).WithParameter(typeof(IModuleDescriptor), this);
		}
	}
}