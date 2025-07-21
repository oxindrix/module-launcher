using VContainer.Unity;


namespace ModuleSystem
{
	public interface IModuleDescriptor : IInstaller
	{
		public string Name { get; }
		public string Description { get; }
	}
}