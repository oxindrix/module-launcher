using System.Collections.Generic;
using UnityEngine;


namespace Modules.CatLady.Entities
{
	public class SnakeSegment : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer;
		public List<Sprite> Sprites;
		public float AnimationSpeed;

		[SerializeField] private float switchInterval;
		private int currentIndex;
		private float timer;


		private void Awake()
		{
			SetSpeed(AnimationSpeed);
		}


		public void SetSpeed(float speed)
		{
			AnimationSpeed = speed;
			switchInterval = 1f / speed / Sprites.Count;
		}


		public void SetSprites(List<Sprite> sprites)
		{
			Sprites = sprites;
			if (Sprites.Count > 0)
				SpriteRenderer.sprite = Sprites[0];
		}


		private void Update()
		{
			if (Sprites == null || Sprites.Count == 0)
				return;

			timer += Time.deltaTime;

			if (timer >= switchInterval)
			{
				timer = 0f;
				currentIndex = (currentIndex + 1) % Sprites.Count;
				SpriteRenderer.sprite = Sprites[currentIndex];
			}
		}
	}
}