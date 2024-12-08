namespace SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine
{
	public sealed class BaseTransition : ITransition
	{
		public IState To { get; }
		public IPredicate Predicate { get; }
		
		public BaseTransition(IState to, IPredicate predicate)
		{
			To = to;
			Predicate = predicate;
		}
	}
}