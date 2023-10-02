using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerComputerRandom : IPlayer
{
	public PlayerComputerRandom(TurnManager turnManager) : base(turnManager)
	{
		_waitForTurn = new WaitForSeconds(2);
	}

	private WaitForSeconds _waitForTurn;
	private Coroutine _turnCoroutine;

	public override bool AllowInput => false;

	public override void OnStartTurn()
	{
		base.OnStartTurn();

		StopTurnCoroutine();
		_turnCoroutine = GameManager.Instance.StartCoroutine(WaitAndTakeTurn());
	}

	private IEnumerator WaitAndTakeTurn()
	{
		yield return _waitForTurn;

		var index = _turnManager.TicTacToeController.GetRandomEmptyNode().index;
		_turnManager.NodeMark(index);
	}

	private void StopTurnCoroutine()
	{
		if(_turnCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnCoroutine);
		}
		_turnCoroutine = null;
	}

	public override void OnTurnEnd()
	{
		base.OnTurnEnd();

		StopTurnCoroutine();
	}
}
