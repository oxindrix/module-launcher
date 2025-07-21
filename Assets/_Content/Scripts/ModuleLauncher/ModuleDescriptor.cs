using ModuleSystem;
using UnityEngine;
using VContainer;


namespace ModuleLauncher
{
	[CreateAssetMenu(menuName = "Module/Module Descriptor", fileName = "Module Descriptor")]
	public abstract class ModuleDescriptor : ScriptableObject, IModuleDescriptor
	{
		[field: SerializeField] public string Name { get; protected set; }
		[field: SerializeField] public string Description { get; protected set; }
		public abstract void Install(IContainerBuilder builder);
	}
}