using System.Collections.Generic;
using UnityEngine;


namespace Modules.CatLady.Entities
{
	public class SnakeView : MonoBehaviour
	{
		public SnakeSegment Head;
		public List<SnakeSegment> Body = new();


		public void Clear()
		{
			Body.Clear();
		}


		public void Consume(SnakeSegment segment)
		{
			Body.Insert(0, segment);

			(segment.transform.localPosition, Head.transform.localPosition) =
				(Head.transform.localPosition, segment.transform.localPosition);
			
			segment.transform.localScale = Head.transform.localScale;
		}


		public void AddTail(SnakeSegment segment)
		{
			Body.Add(segment);
		}


		public void UpdatePositions(Vector3 headPosition, List<Vector3> bodyPositions)
		{
			Head.transform.localPosition = headPosition;
			for (int i = 0; i < Body.Count; i++)
				Body[i].transform.localPosition = bodyPositions[i];
			
			if (Body.Count > 1)
				for (int i = Body.Count - 1; i > 0; i--)
					Body[i].transform.localScale = Body[i - 1].transform.localScale;;

			if (Body.Count > 0)
				Body[0].transform.localScale = Head.transform.localScale;
		}
	}
}