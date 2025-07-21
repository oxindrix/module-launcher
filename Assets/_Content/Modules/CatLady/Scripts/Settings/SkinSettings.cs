using System;
using System.Collections.Generic;
using UnityEngine;


namespace Modules.CatLady.Settings
{
	[CreateAssetMenu(menuName = "Settings/Cat Lady/Skin Settings", fileName = "Cat Lady Skin Settings")]
	public class SkinSettings : ScriptableObject
	{
		[Serializable]
		public class Skin
		{
			public List<Sprite> Sprites;
		}
		
		
		public Skin HeadVariant;
		public List<Skin> SegmentVariants;
	}
}