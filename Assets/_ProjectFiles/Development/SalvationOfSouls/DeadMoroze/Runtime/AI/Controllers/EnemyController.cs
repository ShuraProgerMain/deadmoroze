using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers.COVID;
using SalvationOfSouls.DeadMoroze.Runtime.Core.MovementStuff;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers
{
	internal sealed class EnemyController
	{
		public int Id { get; private set; }
		private readonly MoveWrapper _moveWrapper;

		private Vector3 _destination;

		public EnemyEntityView EnemyEntityView { get; }

		public EnemyController(int id, EnemyEntityView enemyEntityView, MoveWrapper moveWrapper)
		{
			Id = id;
			EnemyEntityView = enemyEntityView;
			_moveWrapper = moveWrapper;
		}

		public void Prepare(Vector3 destination)
		{
			_destination = destination;
			EnemyEntityView.Agent.enabled = true;
		}

		public void Release()
		{
			EnemyEntityView.Agent.enabled = false;
		}

		public void Move()
		{
			EnemyEntityView.Agent.SetDestination(_destination); 
		}
		
		public void Rotate()
		{
			// EnemyEntityView.EntityView.Transform.rotation = _moveWrapper.SmoothRotation(new Vector2(EnemyEntityView.Agent.velocity.x, 
			// 		EnemyEntityView.Agent.velocity.z), 
			// 	EnemyEntityView.EntityView.Transform.rotation, 
			// 	1000f * Time.deltaTime);
		}
	}
}