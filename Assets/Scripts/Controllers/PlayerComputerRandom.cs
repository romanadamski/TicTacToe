using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerComputerRandom : IPlayer
{
	public PlayerComputerRandom(NodeType type, TurnManager turnManager) : base(type, turnManager)
	{
		_waitForTurn = new WaitForSeconds(1);
	}

	private WaitForSeconds _waitForTurn;
	private Coroutine _turnCoroutine;

	public override bool AllowInput => false;

	public override void StartTurn()
	{
		base.StartTurn();

		StopTurnCoroutine();
		_turnCoroutine = GameManager.Instance.StartCoroutine(WaitAndTakeTurn());
	}

	private IEnumerator WaitAndTakeTurn()
	{
		yield return _waitForTurn;

		NodeMark(_turnManager.TicTacToeController.GetRandomEmptyNode().index);
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
