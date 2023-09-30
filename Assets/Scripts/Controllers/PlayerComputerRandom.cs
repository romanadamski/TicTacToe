using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerComputerRandom : IPlayer
{
	private WaitForSeconds _waitForTurn;

	public PlayerComputerRandom(NodeType type) : base(type)
	{
		_waitForTurn = new WaitForSeconds(1);
	}

	public override void StartTurn()
	{
		base.StartTurn();

		_turnManager.GameView.ToggleInput(false);
		_turnManager.StartCoroutine(WaitAndTakeTurn());
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
}
