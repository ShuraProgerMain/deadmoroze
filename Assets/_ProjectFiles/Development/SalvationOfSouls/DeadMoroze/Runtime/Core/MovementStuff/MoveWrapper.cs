using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.Core.MovementStuff
{
	internal struct MoveWrapper
	{
		private Vector2 _smoothInputVelocity;
		private Vector2 _currentVelocity;

		public Vector2 CurrentVelocity => _currentVelocity;

		private readonly MoveService _moveService;

		public MoveWrapper(MoveService moveService)
		{
			_moveService = moveService;
			
			_smoothInputVelocity = Vector2.zero;
			_currentVelocity = Vector2.zero;
		}

		public Vector3 Move(Vector2 velocity, Transform directTransform, float speed, float deltaTime) =>
			_moveService.Move(velocity, directTransform, speed, deltaTime, ref _currentVelocity,
				ref _smoothInputVelocity);

		internal readonly Quaternion SmoothRotation(Vector2 velocity, Quaternion characterRotation, float maxDegreesDelta) =>
			_moveService.SmoothRotation(velocity, characterRotation, maxDegreesDelta);
	}

	// internal interface IMoveService
	// {
	// 	public void Move(Vector3 destination);
	// 	public void RotateTo(Vector3 target);
	// }
	//
	// internal class AgentMoveService : IMoveService
	// {
	// 	private NavMeshAgent _agent;
	//
	// 	public AgentMoveService(NavMeshAgent agent, Transform rotatableTransform)
	// 	{
	// 		_agent = agent;
	// 	}
	// 	
	// 	public void Move(Vector3 destination)
	// 	{
	// 		
	// 	}
	//
	// 	public void RotateTo(Vector3 target)
	// 	{
	// 		
	// 	}
	// }
}