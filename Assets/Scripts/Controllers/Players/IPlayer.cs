using System.Collections;
using UnityEngine;

public enum PlayerNumber
{
	PlayerOne,
	PlayerTwo
}

public abstract class IPlayer
{
	protected BoardTurnController _turnController;
	public PlayerNumber PlayerNumber { get; private set; }
	public string Name { get; private set; }

	public abstract bool AllowInput { get; }
	public NodeType NodeType { get; private set; }

	protected IPlayer(BoardTurnController turnController)
	{
		_turnController = turnController;
	}

	public void SetName(string name)
    {
		Name = name;
	}

	public void SetPlayerNumber(PlayerNumber playerNumber)
    {
		PlayerNumber = playerNumber;
	}

	public void SetNodeType(NodeType nodeType)
    {
		NodeType = nodeType;
	}

	public virtual void OnStartTurn() { }
	public virtual void OnTurnEnd() { }
}
