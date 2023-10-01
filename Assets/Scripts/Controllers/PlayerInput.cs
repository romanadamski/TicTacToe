
public class PlayerInput : IPlayer
{
	public PlayerInput(NodeType type, TurnManager turnManager) : base(type, turnManager)
	{
	}

	public override bool AllowInput => true;
}
