using System;
using General.Utils;
using Modules.CatLady.DTO;
using Modules.CatLady.Entities;
using Services.InputSystem;
using UnityEngine;
using VContainer.Unity;


namespace Modules.CatLady
{
	public class GameSession: ITickable
	{
		public event Action OnSnakeDead;
		public event Action OnSnakeConsumedPoint;
		public event Action OnNewTargetCreated;
		public event Action OnDirectionChanged;
		public event Action OnSpeedChanged;
		public event Action OnSnakeMoved;

		public Vector2Int GridSize => settings.GridSize;
		public Vector2Int TargetPosition { get; private set; }
		public Vector2Int CurrentDirection { get; private set; }
		public Snake Snake => snake;
		public float Speed { get; private set; }

		private float delay;
		private GameSettings settings;
		
		private readonly IInputSystem input;
		private readonly Snake snake;


		public GameSession(
			IInputSystem input,
			Snake snake
		)
		{
			this.input = input;
			this.snake = snake;
		}


		/// <summary>Place new snake in random position</summary>
		public void Reset(GameSettings settings)
		{
			this.settings = settings;

			Speed = settings.BaseSpeed;
			delay = GetDelay();
			
			snake.Clear();
			var position = GetRandomFreePosition(GridSize - Vector2Int.one * 4) + Vector2Int.one * 2;
			snake.SetHeadPosition(position);
			
			// TODO: Add random segments to the snake
			
			PlaceRandomTarget();
			SetDefaultDirectionToTarget();
		}


		private void SetDefaultDirectionToTarget()
		{
			var distance = TargetPosition - snake.Head;
			var abs = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y));
			var sign = new Vector2Int(distance.x.GetSign(), distance.y.GetSign());
			if (abs.x == abs.y)
				distance = sign;
			else if (abs.x > abs.y)
				distance = new Vector2Int(sign.x, 0);
			else
				distance = new Vector2Int(0, sign.y);
			
			CurrentDirection = distance;
			OnDirectionChanged?.Invoke();
		}


		public void Tick()
		{
			UpdateDirection();
			if (IsDelayActive())
				return;
			MoveSnake(CurrentDirection);
			UpdateDelay();
		}


		private void UpdateDirection()
		{
			var inputDirection = new Vector2Int(
				input.GetAxis(Constants.AXIS_X).GetSign(),
				input.GetAxis(Constants.AXIS_Y).GetSign()
			);
			if (inputDirection != Vector2Int.zero)
			{
				CurrentDirection = inputDirection;
				OnDirectionChanged?.Invoke();
			}
		}


		private bool IsDelayActive()
		{
			delay -= Time.deltaTime;
			return delay > 0f;
		}


		private void UpdateDelay()
		{
			delay = GetDelay();
			OnSpeedChanged?.Invoke();
		}


		private void IncreaseSpeed()
		{
			Speed += settings.SpeedIncrement;
		}


		private void MoveSnake(Vector2Int direction)
		{
			var nextCellPosition = snake.Head + direction;

			if (IsCellTarget(nextCellPosition))
			{
				snake.Consume(direction);
				OnSnakeConsumedPoint?.Invoke();
				PlaceRandomTarget();
				IncreaseSpeed();
				return;
			}

			if (IsCellEmpty(nextCellPosition))
			{
				snake.Move(direction);
				OnSnakeMoved?.Invoke();
				return;
			}

			OnSnakeDead?.Invoke();
		}


		private void PlaceRandomTarget()
		{
			TargetPosition = GetRandomFreePosition(GridSize);
			OnNewTargetCreated?.Invoke();
		}


		private bool IsCellEmpty(Vector2Int position)
		{
			if (IsCellTarget(position))
				return false;
			if (IsOutOfGrid(position))
				return false;
			if (snake.Contains(position))
				return false;
			return true;
		}


		private bool IsCellTarget(Vector2Int position)
		{
			return position == TargetPosition;
		}


		private bool IsOutOfGrid(Vector2Int position)
		{
			if (position.x < 0 || position.x >= GridSize.x)
				return true;
			if (position.y < 0 || position.y >= GridSize.y)
				return true;
			return false;
		}


		private Vector2Int GetRandomFreePosition(Vector2Int range)
		{
			Vector2Int position;
			do
			{
				position = GetRandomPosition(range);
			} while (!IsCellEmpty(position));

			return position;
		}


		private Vector2Int GetRandomPosition(Vector2Int range)
		{
			return new Vector2Int(
				UnityEngine.Random.Range(0, range.x),
				UnityEngine.Random.Range(0, range.y)
			);
		}


		private float GetDelay()
		{
			return 1f / Speed;
		}
	}
}