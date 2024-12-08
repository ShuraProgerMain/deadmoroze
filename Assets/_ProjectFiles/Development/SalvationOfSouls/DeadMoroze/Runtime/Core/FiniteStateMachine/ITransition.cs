namespace SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine
{
	public interface ITransition
	{
		public IState To { get; }
		public IPredicate Predicate { get; }
	}
}