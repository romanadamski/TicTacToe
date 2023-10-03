
public class PlayerInput : IPlayer
{
	public PlayerInput(TurnController turnController) : base(turnController)
	{
	}

	public override bool AllowInput => true;
}
