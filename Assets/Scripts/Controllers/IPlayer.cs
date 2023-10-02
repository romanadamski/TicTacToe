using System.Collections;
using UnityEngine;

public abstract class IPlayer
{
	protected TurnManager _turnManager;

	public abstract bool AllowInput { get; }
	public NodeType Type { get; set; }

	protected IPlayer(NodeType type, TurnManager turnManager)
	{
		Type = type;
		_turnManager = turnManager;
	}

	public virtual void OnStartTurn() { }
	public virtual void OnTurnEnd() { }
}
