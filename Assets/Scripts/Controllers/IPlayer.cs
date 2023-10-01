using System.Collections;
using UnityEngine;

public abstract class IPlayer
{
	protected TurnManager _turnManager;

	public abstract bool AllowInput { get; }

	private WaitForSeconds _waitForTurnEnd;
	private Coroutine _turnEndCoroutine;
	public NodeType Type { get; set; }

	protected IPlayer(NodeType type, TurnManager turnManager)
	{
		Type = type;
		_turnManager = turnManager;
		_waitForTurnEnd = new WaitForSeconds(GameManager.Instance.Settings.PlayerTurnTime);
	}

	public virtual void OnTurnEnd()
	{
		StopTurnEndCoroutine();
	}

	public virtual void StartTurn()
	{
		StopTurnEndCoroutine();
		_turnEndCoroutine = GameManager.Instance.StartCoroutine(OnTimerEnd());
	}

	private void StopTurnEndCoroutine()
	{
		if (_turnEndCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnEndCoroutine);
		}
	}

	private IEnumerator OnTimerEnd()
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
