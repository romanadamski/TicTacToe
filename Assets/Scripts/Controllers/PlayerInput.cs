using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayer
{
	public PlayerInput(NodeType type) : base(type)
	{
	}

	public override void StartTurn()
	{
		base.StartTurn();

		_turnManager.GameView.ToggleInput(true);
	}
}
