
public class PlayerInput : IPlayer
{
	public PlayerInput(TurnManager turnManager) : base(turnManager)
	{
	}

	public override bool AllowInput => true;
}
