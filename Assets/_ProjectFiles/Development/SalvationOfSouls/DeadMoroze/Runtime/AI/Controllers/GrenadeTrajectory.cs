using System.Collections;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers
{
	public class GrenadeTrajectory : MonoBehaviour
	{
		public Transform startPoint; // Точка начала
		public Vector3 targetPoint; // Точка падения
		public float flightTime = 2f; // Время полета

		private Vector3 _velocity; // Начальная скорость
		private float _gravity; // Ускорение свободного падения

		public void Push(Vector3 targetPosition)
		{
			targetPoint = targetPosition;
			_gravity = Mathf.Abs(Physics.gravity.y);

			// Рассчитываем разницу в положении
			Vector3 displacement = targetPoint - startPoint.position;

			// Горизонтальная скорость
			float vx = displacement.x / flightTime;

			// Вертикальная скорость
			float vy = displacement.y / flightTime + 0.5f * _gravity * flightTime;

			// Объединяем вектор скорости
			_velocity = new Vector3(vx, vy, displacement.z / flightTime);

			// Запускаем гранату
			StartCoroutine(LaunchGrenade());
		}

		private IEnumerator LaunchGrenade()
		{
			float elapsedTime = 0;
			Vector3 currentPosition = startPoint.position;

			while (elapsedTime < flightTime)
			{
				elapsedTime += Time.deltaTime;
				currentPosition += _velocity * Time.deltaTime;
				currentPosition.y -= 0.5f * _gravity * Mathf.Pow(elapsedTime, 2);
				transform.position = currentPosition;
				yield return null;
			}

			// Устанавливаем точное попадание в целевую точку
			transform.position = targetPoint;
		}
	}
}