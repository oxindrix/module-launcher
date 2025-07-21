using UnityEngine;


namespace Services.InputSystem
{
	public interface IInputSystem
	{
		bool AnyKeyDown { get; }

		public bool GetKeyDown(KeyCode key);

		public bool GetMouseButtonDown(int button);

		public float GetAxis(string axis);
	}
}