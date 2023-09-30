
public abstract class IPlayer
{
	//todo inject
	protected TurnManager _turnManager => TurnManager.Instance;
	protected IPlayer(string name, NodeType type)
	{
		Name = name;
		Type = type;
	}

	public string Name { get; }

	public NodeType Type { get; set; }
	public abstract void StartTurn();
	public abstract void OnNodeMark();
}
