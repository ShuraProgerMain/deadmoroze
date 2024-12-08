using System;
using System.Collections.Generic;

namespace SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine
{
	public sealed class StateMachine
	{
		private StateNode _currentState;
		
		private readonly Dictionary<Type, StateNode> _nodes = new ();
		private readonly List<ITransition> _anyTransitions = new ();
		
		public void SetState(IState state)
		{
			_currentState = _nodes[state.GetType()];
			_currentState.State.OnEnter();
		}

		public void Update(float deltaTime)
		{
			ITransition transition = GetTransition();
			if (transition != null)
			{
				ChangeState(transition.To);
			}

			_currentState.State.OnUpdate(deltaTime);
		}

		public void AddTransition(IState from, IState to, IPredicate predicate)
		{
			GetOrAddNode(from).AddTransition(new BaseTransition(GetOrAddNode(to).State, predicate));
		}

		public void AddAnyTransition(IState to, IPredicate predicate)
		{
			_anyTransitions.Add(new BaseTransition(GetOrAddNode(to).State, predicate));
		}

		private void ChangeState(IState state)
		{
			if(state == _currentState.State) return;
			
			IState previousState = _currentState.State;
			previousState.OnExit();
			
			_currentState = _nodes[state.GetType()];
			_currentState.State.OnEnter();
		}

		private StateNode GetOrAddNode(IState state)
		{
			_nodes.TryGetValue(state.GetType(), out StateNode node);

			if (node == null)
			{
				node = new StateNode(state);
				_nodes.Add(state.GetType(), node);
			}
			
			return node;
		}

		private ITransition GetTransition()
		{
			foreach (ITransition transition in _anyTransitions)
			{
				if (transition.Predicate.Evaluate())
				{
					return transition;
				}
			}

			foreach (ITransition transition in _currentState.Transitions)
			{
				if (transition.Predicate.Evaluate())
				{
					return transition;
				}
			}

			return null;
		}
	}
}