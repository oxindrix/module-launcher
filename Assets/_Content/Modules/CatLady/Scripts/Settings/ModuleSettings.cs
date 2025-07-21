using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Modules.CatLady.UI;
using UnityEngine;


namespace Modules.CatLady.Settings
{
	[CreateAssetMenu(menuName = "Settings/Cat Lady Settings", fileName = "Cat Lady Settings")]
	public class ModuleSettings : ScriptableObject
	{
		public GameSettings GameSettings;
		public Hud Hud;
		public Location Location;
	}
}