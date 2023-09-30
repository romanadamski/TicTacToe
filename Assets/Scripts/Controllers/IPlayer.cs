
using System.Collections;
using UnityEngine;

public abstract class IPlayer
{
	protected TurnManager _turnManager;
	private WaitForSeconds _waitForTurnEnd;
	private Coroutine _turnEndCoroutine;
	public NodeType Type { get; set; }

	protected IPlayer(NodeType type, TurnManager turnManager)
	{
		Type = type;
		_turnManager = turnManager;
		_waitForTurnEnd = new WaitForSeconds(_turnManager.Settings.PlayerTurnTime);
	}

	public virtual void OnGameFinish() { }

	public virtual void StartTurn()
	{
		StopTurnEndCoroutine();
		_turnEndCoroutine = _turnManager.StartCoroutine(OnTurnEnd());
	}

	private void StopTurnEndCoroutine()
	{
		if (_turnEndCoroutine != null)
		{
			_turnManager.StopCoroutine(_turnEndCoroutine);
		}
	}

	private IEnumerator OnTurnEnd()
	{
		yield return _waitForTurnEnd;
		_turnManager.SetLoser(Type);
	}

	public void NodeMark(Vector2Int index)
	{
		StopTurnEndCoroutine();
		_turnManager.OnNodeMark(index, Type);
	}
}
