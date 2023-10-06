using System;
using UnityEngine;

/// <summary>
/// Holds reference to board related events
/// </summary>
[CreateAssetMenu(fileName = "BoardEvents", menuName = "ScriptableObjects/BoardEvents")]
public class BoardEventsSO: ScriptableObject
{
	public event Action OnHint;
	public event Action<IPlayer, Vector2Int> OnSetNode;
	public event Action OnUndoMove;

	public void Hint()
    {
		OnHint?.Invoke();
	}

	public void SetNode(IPlayer player, Vector2Int index)
    {
		OnSetNode?.Invoke(player, index);
	}

	public void UndoMove()
    {
		OnUndoMove?.Invoke();
	}
}
