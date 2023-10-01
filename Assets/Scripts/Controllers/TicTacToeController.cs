using UnityEngine;

public enum NodeType
{
	None = 0,
	X = 1,
	O = 2
}

public class TicTacToeController
{
	private NodeType[,] _board;
	private uint _horizontalCount;
	private uint _verticalCount;
	private uint _winningCount;

	public TicTacToeController(uint horizontalCount, uint verticalCount, uint winningCount)
	{
		_board = new NodeType[horizontalCount, verticalCount];

		_horizontalCount = horizontalCount;
		_verticalCount = verticalCount;
		_winningCount = winningCount;
	}

	public void SetNode(Vector2Int index, NodeType nodeType)
	{
		_board[index.x, index.y] = nodeType;
	}

	public NodeType CheckWin(Vector2Int index, NodeType nodeType)
	{
		return CheckWinVertical(index.x, nodeType)
		| CheckWinHorizontal(index.y, nodeType)
		| CheckWinDiagonalTopLeftToBottomRight(index, nodeType)
		| CheckWinDiagonalTopRightToLeftBottom(index, nodeType);
	}

	public bool CheckEmptyNodes()
	{
		bool emptyNodes = false;
		foreach(var item in _board)
		{
			if(item == NodeType.None)
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
			if (_board[i, j] == nodeType)
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
			if (_board[i, j] == nodeType)
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
			if (_board[i, horizontal] == nodeType)
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
			if (_board[vertical, i] == nodeType)
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
