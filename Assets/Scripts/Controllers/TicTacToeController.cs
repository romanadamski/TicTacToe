using UnityEngine;

public enum PlayerType
{
	None = 0,
	X = 1,
	O = 2
}

public class TicTacToeController
{
	private PlayerType _currentPlayer = PlayerType.X;
	public PlayerType CurrentPlayer
	{
		get => _currentPlayer;
		set
		{
			_currentPlayer = value;
			EventsManager.Instance.OnPlayerChanged(value);
		}
	}

	public TileController[,] _tileControllers;

	public void SwitchPlayer()
	{
		if (CurrentPlayer == PlayerType.O)
		{
			CurrentPlayer = PlayerType.X;
		}
		else
		{
			CurrentPlayer = PlayerType.O;
		}
	}

	public PlayerType CheckWin(TileController tile) =>
		CheckWinVertical((int)tile.Index.x)
		| CheckWinHorizontal((int)tile.Index.y)
		| CheckWinDiagonalTopLeftToBottomRight(tile.Index)
		| CheckWinDiagonalTopRightToLeftBottom(tile.Index);

	private PlayerType CheckWinDiagonalTopLeftToBottomRight(Vector2 index)
	{
		var winCount = 0u;
		var startX = index.x >= index.y ? index.x - index.y : 0;
		var startY = index.y >= index.x ? index.y - index.x : 0;

		var verCount = GameSettingsManager.Instance.Settings.VerticalTilesCount;
		var horCount = GameSettingsManager.Instance.Settings.HorizontalTilesCount;

		var endX = horCount - index.x <= verCount - index.y ? horCount - 1 : (verCount - index.y - 1) + index.x;
		var endY = verCount - index.y <= horCount - index.x ? verCount - 1 : (horCount - index.x - 1) + index.y;

		for (int i = (int)startX, j = (int)startY; i <= endX && j <= endY; i++, j++)
		{
			if (_tileControllers[i, j].PlayerType == _currentPlayer)
			{
				winCount++;
				if (winCount.Equals(GameSettingsManager.Instance.Settings.WinningTilesCount))
				{
					return _currentPlayer;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return PlayerType.None;
	}

	private PlayerType CheckWinDiagonalTopRightToLeftBottom(Vector2 index)
	{
		var winCount = 0u;

		var horCount = GameSettingsManager.Instance.Settings.HorizontalTilesCount;
		var verCount = GameSettingsManager.Instance.Settings.VerticalTilesCount;

		var xToEdgeDist = horCount - index.x - 1;
		var startX = horCount - index.x <= index.y ? horCount - 1 : index.x + index.y;
		var startY = index.y <= xToEdgeDist ? 0 : index.y - xToEdgeDist;

		var yToEdgeDist = verCount - index.y - 1;
		var endX = index.x >= yToEdgeDist ? index.x - yToEdgeDist : 0;
		var endY = verCount - index.y - 1 >= index.x ? index.y + index.x : verCount - 1;

		for (int i = (int)startX, j = (int)startY; i >= endX && j <= endY; i--, j++)
		{
			if (_tileControllers[i, j].PlayerType == _currentPlayer)
			{
				winCount++;
				if (winCount.Equals(GameSettingsManager.Instance.Settings.WinningTilesCount))
				{
					return _currentPlayer;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return PlayerType.None;
	}

	private PlayerType CheckWinHorizontal(int horizontal)
	{
		var winCount = 0u;
		for (int i = 0; i < GameSettingsManager.Instance.Settings.HorizontalTilesCount; i++)
		{
			if (_tileControllers[i, horizontal].PlayerType == _currentPlayer)
			{
				winCount++;
				if (winCount.Equals(GameSettingsManager.Instance.Settings.WinningTilesCount))
				{
					return _currentPlayer;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return PlayerType.None;
	}

	private PlayerType CheckWinVertical(int vertical)
	{
		var winCount = 0u;
		for (int i = 0; i < GameSettingsManager.Instance.Settings.VerticalTilesCount; i++)
		{
			if (_tileControllers[vertical, i].PlayerType == _currentPlayer)
			{
				winCount++;
				if (winCount.Equals(GameSettingsManager.Instance.Settings.WinningTilesCount))
				{
					return _currentPlayer;
				}
			}
			else if (winCount > 0)
			{
				winCount = 0;
			}
		}

		return PlayerType.None;
	}
}
