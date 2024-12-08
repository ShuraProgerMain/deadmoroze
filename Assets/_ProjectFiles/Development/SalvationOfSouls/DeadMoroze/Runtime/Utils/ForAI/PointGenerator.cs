using System.Collections.Generic;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.Utils.ForAI
{
	public class PointGenerator : MonoBehaviour
	{
		public Vector3 targetPosition = new(0, 0, 10); // Центр объекта
		public float objectWidth = 4f; // Ширина объекта
		public float unitRadius = 1.5f; // Радиус юнита
		public int pointsInOuterArea = 15; // Количество точек во внешнем круге
		public float minOuterDistance = 3f; // Минимальное расстояние от второго ряда
		public float maxOuterDistance = 5f; // Максимальное расстояние от второго ряда

		[SerializeField] private List<Vector3> generatedPoints = new();

		private void Start()
		{
			GeneratePoints();
			DrawPoints();
		}

		[ContextMenu("Generate Points")]
		private void GeneratePoints()
		{
			generatedPoints.Clear();

			// Первый ряд
			float row1Radius = objectWidth / 2 + unitRadius;
			GenerateRingPoints(targetPosition, row1Radius, 8);

			// Второй ряд
			float row2Radius = row1Radius + unitRadius * 2;
			GenerateRingPoints(targetPosition, row2Radius, 12);

			// Внешняя зона (без пересечений)
			GenerateOuterPoints(targetPosition, row2Radius + minOuterDistance, row2Radius + maxOuterDistance,
				pointsInOuterArea);
		}

		private void GenerateRingPoints(Vector3 center, float radius, int pointCount)
		{
			float minDistance = unitRadius * 2;

			for (var i = 0; i < pointCount; i++)
			{
				float angle = i * Mathf.PI * 2 / pointCount;
				float x = center.x + Mathf.Cos(angle) * radius;
				float z = center.z + Mathf.Sin(angle) * radius;
				var newPoint = new Vector3(x, center.y, z);

				if (IsFarEnough(newPoint, minDistance))
				{
					generatedPoints.Add(newPoint);
				}
			}
		}

		private void GenerateOuterPoints(Vector3 center, float minDistance, float maxDistance, int pointCount)
		{
			float minSeparation = unitRadius * 2;

			for (var i = 0; i < pointCount; i++)
			{
				var attempts = 0;
				Vector3 newPoint;

				do
				{
					float distance = Random.Range(minDistance, maxDistance);
					float angle = Random.Range(0, Mathf.PI * 2);
					float x = center.x + Mathf.Cos(angle) * distance;
					float z = center.z + Mathf.Sin(angle) * distance;
					newPoint = new Vector3(x, center.y, z);

					attempts++;
					if (attempts > 100)
					{
						break; // На случай, если точка не найдена
					}
				} while (!IsFarEnough(newPoint, minSeparation));

				generatedPoints.Add(newPoint);
			}
		}

		private bool IsFarEnough(Vector3 point, float minDistance)
		{
			foreach (Vector3 existingPoint in generatedPoints)
			{
				if (Vector3.Distance(existingPoint, point) < minDistance)
				{
					return false;
				}
			}

			return true;
		}

		private void OnDrawGizmos()
		{
			foreach (Vector3 point in generatedPoints)
			{
				Gizmos.DrawWireSphere(point, unitRadius);
			}
		}

		private void DrawPoints()
		{
			foreach (Vector3 point in generatedPoints)
			{
				Debug.DrawLine(point, point + Vector3.up * 0.5f, Color.red, 10f);
			}
		}

		public List<Vector3> GetGeneratedPoints() => generatedPoints;
	}
}