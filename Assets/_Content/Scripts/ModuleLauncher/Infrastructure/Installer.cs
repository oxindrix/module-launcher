using General.Core.Settings;
using General.Implementation.Providers;
using Providers;
using UnityEngine;
using VContainer;


namespace ModuleLauncher.Infrastructure
{
	[CreateAssetMenu(fileName = "ModuleLauncher Installer", menuName = "Installers/ModuleLauncher")]
	public class Installer : SettingsInstaller
	{
		public SettingsProvider settingsProvider;
		public Settings settings;


		public override void Install(IContainerBuilder builder)
		{
			builder.RegisterInstance(settings);
			builder.RegisterInstance(settingsProvider).As<ISettingsProvider>();
		}
	}
}
