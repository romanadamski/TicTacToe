using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayer
{
	private WaitForSeconds waitForTurnEnd;
	private Coroutine waitForTurnEndCoroutine;

	public PlayerInput(string name, NodeType type) : base(name, type)
	{
		waitForTurnEnd = new WaitForSeconds(_turnManager.Settings.PlayerTurnTime);
	}

	public override void StartTurn()
	{
		waitForTurnEndCoroutine = _turnManager.StartCoroutine(OnTurnEnd());
	}

	private IEnumerator OnTurnEnd()
	{
		yield return waitForTurnEnd;
		_turnManager.SetLoser(this);
	}

	public override void OnNodeMark()
	{
		if (waitForTurnEndCoroutine == null) return;

		_turnManager.StopCoroutine(waitForTurnEndCoroutine);
	}
}
