using System.Collections.Generic;

namespace SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine
{
	internal sealed record StateNode
	{
		public readonly IState State; 
		private readonly List<ITransition> _transitions;
			
		public IReadOnlyList<ITransition> Transitions => _transitions;

		public StateNode(IState state)
		{
			State = state;
			_transitions = new List<ITransition>();
		}

		public void AddTransition(ITransition transition)
		{
			_transitions.Add(transition);
		}
	}
}