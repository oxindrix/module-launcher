using System.Collections.Generic;
using UnityEngine;


namespace Modules.CatLady.Entities
{
	public class Snake
	{
		public Vector2Int Head { get; private set; }

		public readonly List<Vector2Int> body = new();


		public void Clear()
		{
			body.Clear();
		}


		public void SetHeadPosition(Vector2Int position)
		{
			Head = position;
		}


		public bool Contains(Vector2Int position)
		{
			return position == Head || body.Contains(position);
		}


		public void Consume(Vector2Int direction)
		{
			body.Insert(0, Head);
			Head += direction;
		}


		public void AddTail(Vector2Int position)
		{
			body.Add(position);
		}


		public void Move(Vector2Int direction)
		{
			if (body.Count > 1)
				for (int i = body.Count - 1; i > 0; i--)
					body[i] = body[i - 1];

			if (body.Count > 0)
				body[0] = Head;
			
			Head += direction;
		}
	}
}