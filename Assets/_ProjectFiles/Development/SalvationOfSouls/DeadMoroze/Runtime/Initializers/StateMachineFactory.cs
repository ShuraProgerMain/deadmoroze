using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers;
using SalvationOfSouls.DeadMoroze.Runtime.ChristmasTree;
using SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine;
using SalvationOfSouls.DeadMoroze.Runtime.StateMachineStates.EnemyStates;
using UnityEngine;

namespace SalvationOfSouls.DeadMoroze.Runtime.Initializers
{
	internal sealed class StateMachineFactory
	{
		private readonly ChristmasTreeComponent _christmasTreeComponent;
		
		public StateMachineFactory(ChristmasTreeComponent christmasTreeComponent)
		{
			_christmasTreeComponent = christmasTreeComponent;
		}
		
		public StateMachine GetStateMachine(EnemyController controller)
		{
			StateMachine stateMachine = new ();

			IState moveToPointState = new MoveToPointState(_christmasTreeComponent, controller);
			IState attackState = new CovidsAttackState();

			IPredicate moveToPointBasePredicate = new FuncPredicate(() => 
				Vector3.Distance(controller.EnemyEntityView.EntityView.Transform.position, 
					controller.EnemyEntityView.Agent.destination) >= controller.EnemyEntityView.Agent.stoppingDistance);
			
			IPredicate attackPredicate = new FuncPredicate(() =>
			{
				bool result =  Vector3.Distance(controller.EnemyEntityView.EntityView.Transform.position,
					_christmasTreeComponent.Transform.position) <= controller.EnemyEntityView.Agent.stoppingDistance;

				Debug.Log($"{controller.Id} with relust: {result}");

				return result;
			});
			
			stateMachine.AddTransition(moveToPointState, attackState, attackPredicate);
			
			stateMachine.AddAnyTransition(moveToPointState, moveToPointBasePredicate);
			
			stateMachine.SetState(moveToPointState);

			return stateMachine;
		}
	}
}