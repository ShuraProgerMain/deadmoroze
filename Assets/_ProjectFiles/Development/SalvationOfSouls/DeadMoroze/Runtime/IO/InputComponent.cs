using SalvationOfSouls.IO;

namespace SalvationOfSouls.DeadMoroze.Runtime.IO
{
	public sealed class InputComponent
	{
		public readonly PlayerInput PlayerInput;
		
		public InputComponent(PlayerInput playerInput)
		{
			PlayerInput = playerInput;
		}
	}
}