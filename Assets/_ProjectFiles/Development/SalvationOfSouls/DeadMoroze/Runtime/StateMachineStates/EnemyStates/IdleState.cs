using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers;
using SalvationOfSouls.DeadMoroze.Runtime.ChristmasTree;
using SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.StateMachineStates.EnemyStates
{
	internal sealed record MoveToPointState : IState
	{
		private readonly ChristmasTreeComponent _christmasTreeComponent;
		private readonly EnemyController _controller;
		
		private bool NeedRotate => _controller.EnemyEntityView.Agent.velocity.magnitude > 0.1f;

		public MoveToPointState(ChristmasTreeComponent christmasTreeComponent, EnemyController controller)
		{
			_christmasTreeComponent = christmasTreeComponent;
			_controller = controller;
		}
		
		public void OnEnter() 
		{
			Debug.Log($"OnEnter: {nameof(MoveToPointState)}");
		}

		public void OnUpdate(float deltaTime)
		{
			_controller.Move();
			
			if(NeedRotate) 
				_controller.Rotate();
		}

		public void OnExit()
		{
			
		}
	}

	internal sealed record CovidsAttackState : IState
	{
		private readonly EnemyController _controller;
		private float _waitingTime;

		public CovidsAttackState(EnemyController controller)
		{
			_controller = controller;
		}
		
		public void OnEnter()
		{
			Debug.Log($"OnEnter: {nameof(CovidsAttackState)}");
		}
		
		public void OnUpdate(float deltaTime)
		{
			if (_waitingTime < _controller.AttackDelay)
			{
				_waitingTime += deltaTime;
			}
			else
			{
				_waitingTime = 0;
				_controller.Attack();
			}
		}

		public void OnExit()
		{
		}
	}
	
	internal sealed record SnowmansAttackState : IState
	{
		public void OnEnter()
		{
		}

		public void OnUpdate(float deltaTime)
		{
		}

		public void OnExit()
		{
		}
	}
}