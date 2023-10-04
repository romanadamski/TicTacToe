
public class PlayerInput : IPlayer
{
	public PlayerInput(BoardTurnController turnController) : base(turnController)
	{
	}

	public override bool AllowInput => true;
}
