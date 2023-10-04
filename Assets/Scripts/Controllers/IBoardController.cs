using System;
using UnityEngine;

public interface IBoardController
{
	void SaveMove(IPlayer player, Vector2Int index);
	bool TryUndoMove(out Tuple<IPlayer, Vector2Int> lastMove);
	Node GetRandomEmptyNode();
	void SetNode(Vector2Int index, NodeType nodeType);
	NodeType CheckWin(Vector2Int index, NodeType nodeType);
	bool CheckEmptyNodes();
}