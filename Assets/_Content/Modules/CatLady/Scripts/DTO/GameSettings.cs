using System;
using Modules.CatLady.Settings;
using UnityEngine;


namespace Modules.CatLady.DTO
{
	[Serializable]
	public class GameSettings
	{
		public float SpeedIncrement = 0.1f;
		public float BaseSpeed = 1f;
		public Vector2Int GridSize;
		public int StartSegmentsCount;
		public SkinSettings SkinSettings;
	}
}