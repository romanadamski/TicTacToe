using UnityEngine;

public class TurnManager : BaseManager<TurnManager>
{
	public GameSettingsScriptableObject Settings;

	private TicTacToeController _ticTacToeController;
	private IPlayer PlayerOne { get; set; }
	private IPlayer PlayerTwo { get; set; }

	private IPlayer _currentPlayer;
	public IPlayer CurrentPlayer
	{
		get => _currentPlayer;
		set
		{
			_currentPlayer = value;
			_currentPlayer.StartTurn();
			EventsManager.Instance.OnPlayerChanged(value);
		}
	}

	public void StartGame()
    {
        var horizontalTilesCount = Settings.HorizontalTilesCount;
		var verticalTilesCount = Settings.VerticalTilesCount;

		_ticTacToeController = new TicTacToeController(horizontalTilesCount, verticalTilesCount, Settings.WinningTilesCount);

		//todo inject
		PlayerOne = new PlayerInput("a", NodeType.X);
		//todo inject
		PlayerTwo = new PlayerInput("b", NodeType.O);
		CurrentPlayer = PlayerOne;
	}

	public void OnNodeMark(Vector2Int index)
    {
		CurrentPlayer.OnNodeMark();
		var winner = _ticTacToeController.CheckWin(index, CurrentPlayer.Type);
		if (winner != NodeType.None || !_ticTacToeController.CheckEmptyNodes())
		{
			SetWinner(CurrentPlayer);
			return;
		}

		SwitchPlayer();
	}

	public void SetWinner(IPlayer winner)
	{
		GameplayManager.Instance.SetGameOverState();
		EventsManager.Instance.OnGameOver(winner);
	}

	public void SetLoser(IPlayer loser)
	{
		if(loser == PlayerOne)
		{
			SetWinner(PlayerTwo);
		}
		else if(loser == PlayerTwo)
		{
			SetWinner(PlayerOne);
		}
	}

	private void SwitchPlayer()
	{
		if (CurrentPlayer == PlayerOne)
		{
			CurrentPlayer = PlayerTwo;
		}
		else
		{
			CurrentPlayer = PlayerOne;
		}
	}
}
