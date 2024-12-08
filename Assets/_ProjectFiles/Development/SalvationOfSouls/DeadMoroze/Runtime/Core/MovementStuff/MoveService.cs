using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.Core.MovementStuff
{
	internal sealed class MoveService
	{
		internal Vector3 Move(Vector2 velocity, Transform directTransform, float speed, float deltaTime,
			ref Vector2 currentVelocity, ref Vector2 smoothInputVelocity)
		{
			currentVelocity = Vector2.SmoothDamp(currentVelocity, velocity, ref smoothInputVelocity, .08f);

			float moveSpeed = speed * velocity.magnitude * deltaTime;
			Vector3 newPosition = directTransform.forward * (currentVelocity.y * moveSpeed) +
								  directTransform.right * (currentVelocity.x * moveSpeed);
			newPosition.y = 0;

			return newPosition;
		}

		internal Quaternion SmoothRotation(Vector2 velocity, Quaternion characterRotation, float maxDegreesDelta)
		{
			if (velocity == Vector2.zero)
			{
				return characterRotation;
			}

			Vector3 localMovementDirection = Quaternion.Euler(0f, Mathf.Atan2(velocity.x, velocity.y)
																  * Mathf.Rad2Deg, 0f) * Vector3.forward;

			Vector3 forward = Quaternion.Euler(0f, 0f, 0f) * Vector3.forward;
			forward.y = 0f;
			forward.Normalize();

			Quaternion targetRotation = Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f)
				? Quaternion.LookRotation(-forward)
				: Quaternion.LookRotation(localMovementDirection);


			float angle = Quaternion.Angle(characterRotation, targetRotation);

			float rotationSpeed = Mathf.Lerp(0, maxDegreesDelta, angle / 180f);

			targetRotation = Quaternion.RotateTowards(characterRotation, targetRotation, rotationSpeed);

			return targetRotation;
		}
	}
}