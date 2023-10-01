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
		_turnCoroutine = _turnManager.StartCoroutine(WaitAndTakeTurn());
	}

	private IEnumerator WaitAndTakeTurn()
	{
		yield return _waitForTurn;

		var emptyNodes = _turnManager.GameView.TileControllers.Cast<TileController>().
			Where(x => x.NodeType == NodeType.None);
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

	public override void OnTurnEnd()
	{
		base.OnTurnEnd();

		StopTurnCoroutine();
	}
}
