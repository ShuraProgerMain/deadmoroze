namespace SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine
{
	public interface IState
	{
		public void OnEnter();
		public void OnUpdate(float deltaTime);
		public void OnExit();
	}
}