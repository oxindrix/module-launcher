using System.Collections.Generic;
using General.Utils;
using General.Utils.Collections;
using Modules.CatLady.Settings;
using PoolSystem;
using UnityEngine;
using VContainer;


namespace Modules.CatLady.Entities
{
	public class GameRenderer : MonoBehaviour
	{
		public SerializableWeightedPool<List<Sprite>> SnakeSegmentViewPool;
		
		public SnakeSegment Prefab;
		public SnakeSegment Target;
		public SnakeView SnakeView;
		
		private Snake snake;
		private IUniversalObjectsPool pool;

		private readonly List<Vector3> bodyPositions = new();


		[Inject]
		public void Construct(IUniversalObjectsPool pool)
		{
			this.pool = pool;
		}


		public void SetSkins(SkinSettings skin)
		{
			SnakeView.Head.SetSprites(skin.HeadVariant.Sprites);
			SnakeSegmentViewPool.Clear();
			foreach (var variant in skin.SegmentVariants)
				SnakeSegmentViewPool.AddItem(variant.Sprites, 1f);
		}


		public void Clear()
		{
			foreach (var segment in SnakeView.Body)
				pool.ReturnObject(segment);
			SnakeView.Clear();
			if (Target)
				pool.ReturnObject(Target);
			Target = null;
		}


		public void AttachSnake(Snake snake)
		{
			this.snake = snake;
			SnakeView.Head.transform.localPosition = snake.Head.ToVector3();
		}


		public void CreateTarget(Vector2Int position)
		{
			Debug.Assert(Target == null, "Target already exists");
			
			Target = pool.GetObject(Prefab);
			
			SnakeSegmentViewPool.TryTake(out var skin);
			Target.SetSprites(skin);
			Target.SetSpeed(1f);

			var segmentTransform = Target.transform;
			segmentTransform.SetParent(transform);
			segmentTransform.localPosition = position.ToVector3();
		}


		public void ConsumeTarget()
		{
			SnakeView.Consume(Target);
			Target = null;
		}


		public void MoveSnake()
		{
			var headPosition = snake.Head.ToVector3();
			bodyPositions.Clear();
			foreach (var position in snake.body)
				bodyPositions.Add(position.ToVector3());
			
			SnakeView.UpdatePositions(headPosition, bodyPositions);
		}


		public void SetHeadDirection(Vector2Int direction)
		{
			if (direction.x == 0f)
				return;
			
			var scale = Vector3.one;
			scale.x = direction.x;
			SnakeView.Head.transform.localScale = scale;
		}


		public void SetSpeed(float speed)
		{
			SnakeView.Head.SetSpeed(speed);
			foreach (var segment in SnakeView.Body)
				segment.SetSpeed(speed);
		}
	}
}