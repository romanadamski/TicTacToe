
using System.Collections;
using UnityEngine;

public abstract class IPlayer
{
	//todo inject
	protected TurnManager _turnManager => TurnManager.Instance;
	private WaitForSeconds _waitForTurnEnd;
	private Coroutine _turnEndCoroutine;
	public NodeType Type { get; set; }

	protected IPlayer(NodeType type)
	{
		Type = type;
		_waitForTurnEnd = new WaitForSeconds(_turnManager.Settings.PlayerTurnTime);
	}

	public virtual void StartTurn()
	{
		_turnEndCoroutine = _turnManager.StartCoroutine(OnTurnEnd());
	}

	private IEnumerator OnTurnEnd()
	{
		yield return _waitForTurnEnd;
		_turnManager.SetLoser(Type);
	}

	public void NodeMark(Vector2Int index)
	{
		if (_turnEndCoroutine != null)
		{
			_turnManager.StopCoroutine(_turnEndCoroutine);
		}
		_turnManager.OnNodeMark(index, Type);
	}
}
