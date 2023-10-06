using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NodeType
{
	None = 0,
	X = 1,
	O = 2
}

public struct Node
{
	public Vector2Int index;
	public NodeType nodeType;

	public Node(Vector2Int index, NodeType nodeType)
	{
		this.index = index;
		this.nodeType = nodeType;
	}
}

public class BoardController : IBoardController
{
	private uint _horizontalCount;
	private uint _verticalCount;
	private uint _winningCount;

	private Node[,] _board;

    public void Set(uint horizontalCount, uint verticalCount, uint winningCount)
    {
        _board = new Node[horizontalCount, verticalCount];
        for (int i = 0; i < verticalCount; i++)
        {
            for (int j = 0; j < horizontalCount; j++)
            {
                _board[j, i].index = new Vector2Int(j, i);
            }
        }

        _horizontalCount = horizontalCount;
        _verticalCount = verticalCount;
        _winningCount = winningCount;
		_movesHistory.Clear();
	}

	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();

	/// <summary>
	/// Save move to stack so it can be undo later
	/// </summary>
	/// <param name="player">Player that made that move</param>
	/// <param name="index">Index on board move was made on</param>
	public void SaveMove(IPlayer player, Vector2Int index)
	{
		_movesHistory.Push(new Tuple<IPlayer, Vector2Int>(player, index));
	}

	/// <summary>
	/// Try undo move. Provides boolean result of that try and out parameter with last move.
	/// </summary>
	/// <param name="lastMove">Out parameters with last move. Null if there was no move saved.</param>
	/// <returns>True if any move was in moves history.</returns>
	public bool TryUndoMove(out Tuple<IPlayer, Vector2Int> lastMove)
	{
		lastMove = null;
		if (_movesHistory.Count == 0) return false;

		lastMove = _movesHistory.Pop();

		SetNode(lastMove.Item2, NodeType.None);

		return true;
	}

	/// <summary>
	/// Returns random empty node from board
	/// </summary>
	/// <returns></returns>
	public Node GetRandomEmptyNode()
	{
		var emptyNodes = _board.Cast<Node>().
			Where(x => x.nodeType == NodeType.None);
		var nodesCount = emptyNodes.Count();
		var randomIndex = UnityEngine.Random.Range(0, nodesCount);
		return emptyNodes.ElementAt(randomIndex);
	}

	/// <summary>
	/// Set given node type on given index
	/// </summary>
	/// <param name="index">Index of cell on board</param>
	/// <param name="nodeType">Node type to set</param>
	public void SetNode(Vector2Int index, NodeType nodeType)
	{
		_board[index.x, index.y] = new Node(index, nodeType);
	}

	/// <summary>
	/// Returns node type of winner, None if no one win
	/// </summary>
	/// <param name="index">Index of cell on board</param>
	/// <param name="nodeType">Node type of player that made move</param>
	/// <returns></returns>
	public NodeType CheckWin(Vector2Int index, NodeType nodeType)
	{
		return CheckWinVertical(index.x, nodeType)
		| CheckWinHorizontal(index.y, nodeType)
		| CheckWinDiagonalTopLeftToBottomRight(index, nodeType)
		| CheckWinDiagonalTopRightToLeftBottom(index, nodeType);
	}

	/// <summary>
	/// True if any node on board was not set yet
	/// </summary>
	/// <returns></returns>
	public bool CheckEmptyNodes()
	{
		bool emptyNodes = false;
		foreach(var item in _board)
		{
			if(item.nodeType == NodeType.None)
			{
				emptyNodes = true;
				break;
			}
		}

		return emptyNodes;
	}

	private NodeType CheckWinDiagonalTopLeftToBottomRight(Vector2 index, NodeType nodeType)
	{
		var winCount = 0u;
		var startX = index.x >= index.y ? index.x - index.y : 0;
		var startY = index.y >= index.x ? index.y - index.x : 0;

		var verCount = _verticalCount;
		var horCount = _horizontalCount;

		var endX = horCount - index.x <= verCount - index.y ? horCount - 1 : (verCount - index.y - 1) + index.x;
		var endY = verCount - index.y <= horCount - index.x ? verCount - 1 : (horCount - index.x - 1) + index.y;

		for (int i = (int)startX, j = (int)startY; i <= endX && j <= endY; i++, j++)
		{
			if (_board[i, j].nodeType == nodeType)
			{
				winCount++;
				if (winCount.Equals(_winningCount))
				{
					return nodeType;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return NodeType.None;
	}

	private NodeType CheckWinDiagonalTopRightToLeftBottom(Vector2 index, NodeType nodeType)
	{
		var winCount = 0u;

		var horCount = _horizontalCount;
		var verCount = _verticalCount;

		var xToEdgeDist = horCount - index.x - 1;
		var startX = horCount - index.x <= index.y ? horCount - 1 : index.x + index.y;
		var startY = index.y <= xToEdgeDist ? 0 : index.y - xToEdgeDist;

		var yToEdgeDist = verCount - index.y - 1;
		var endX = index.x >= yToEdgeDist ? index.x - yToEdgeDist : 0;
		var endY = verCount - index.y - 1 >= index.x ? index.y + index.x : verCount - 1;

		for (int i = (int)startX, j = (int)startY; i >= endX && j <= endY; i--, j++)
		{
			if (_board[i, j].nodeType == nodeType)
			{
				winCount++;
				if (winCount.Equals(_winningCount))
				{
					return nodeType;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return NodeType.None;
	}

	private NodeType CheckWinHorizontal(int horizontal, NodeType nodeType)
	{
		var winCount = 0u;
		for (int i = 0; i < _horizontalCount; i++)
		{
			if (_board[i, horizontal].nodeType == nodeType)
			{
				winCount++;
				if (winCount.Equals(_winningCount))
				{
					return nodeType;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return NodeType.None;
	}

	private NodeType CheckWinVertical(int vertical, NodeType nodeType)
	{
		var winCount = 0u;
		for (int i = 0; i < _verticalCount; i++)
		{
			if (_board[vertical, i].nodeType == nodeType)
			{
				winCount++;
				if (winCount.Equals(_winningCount))
				{
					return nodeType;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return NodeType.None;
	}
}
