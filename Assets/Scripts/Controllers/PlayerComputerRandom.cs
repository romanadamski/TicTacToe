using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerComputerRandom : IPlayer
{
	private WaitForSeconds _waitForTurn;
	private Coroutine _turnCoroutine;

	public PlayerComputerRandom(NodeType type, TurnManager turnManager) : base(type, turnManager)
	{
		_waitForTurn = new WaitForSeconds(1);
	}

	public override void StartTurn()
	{
		base.StartTurn();

		_turnManager.GameView.ToggleInput(false);

		StopTurnCoroutine();
		_turnCoroutine = _turnManager.StartCoroutine(WaitAndTakeTurn());
	}

	private IEnumerator WaitAndTakeTurn()
	{
		yield return _waitForTurn;

		var emptyNodes = _turnManager.GameView.TileControllers.Cast<TileController>().
			Where(x => x.PlayerType == NodeType.None);
		var nodesCount = emptyNodes.Count();
		var randomIndex = Random.Range(0, nodesCount);
		var randomNode = emptyNodes.ElementAt(randomIndex);

		NodeMark(randomNode.Index);
	}

	private void StopTurnCoroutine()
	{
		if(_turnCoroutine != null)
		{
			_turnManager.StopCoroutine(_turnCoroutine);
		}
		_turnCoroutine = null;
	}

	public override void OnGameFinish()
	{
		StopTurnCoroutine();
	}
}
