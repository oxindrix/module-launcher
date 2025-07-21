using UnityEngine;
using UnityEngine.UI;


namespace Modules.TestModule
{
	[CreateAssetMenu(menuName = "Settings/Test Module Settings", fileName = "Test Module Settings")]
	public class ModuleSettings : ScriptableObject
	{
		public Transform mainObjectPrefab;
		public Text mainTextPrefab;
	}
}