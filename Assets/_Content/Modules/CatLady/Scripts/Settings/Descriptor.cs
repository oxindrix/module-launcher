using General.Implementation.Services.InputSystem;
using ModuleLauncher;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.GameStates;
using ModuleSystem;
using Services.InputSystem;
using UnityEngine;
using VContainer;


namespace Modules.CatLady.Settings
{
	[CreateAssetMenu(menuName = "Module/Cat Lady", fileName = "Cat Lady Descriptor")]
	public class Descriptor : ModuleDescriptor
	{
		public override void Install(IContainerBuilder builder)
		{
			new StatesInstaller().Install(builder);

			builder.Register<GameContext>(Lifetime.Singleton);
			builder.Register<Snake>(Lifetime.Transient);
			builder.Register<GameSession>(Lifetime.Singleton);
			builder.Register<IInputSystem, UnityInputSystem>(Lifetime.Singleton).WithParameter(typeof(IModuleDescriptor), this);
			builder.Register<IModule, Module>(Lifetime.Singleton).WithParameter(typeof(IModuleDescriptor), this);
		}
	}
}