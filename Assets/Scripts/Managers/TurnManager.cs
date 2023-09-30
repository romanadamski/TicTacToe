using UnityEngine;

public class TurnManager : BaseManager<TurnManager>
{
	public GameSettingsSO Settings;

	private TicTacToeController _ticTacToeController;
	//todo inject
	public GameView GameView => GameView.Instance;

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
		GameView.SpawnTiles();

        var horizontalTilesCount = Settings.HorizontalTilesCount;
		var verticalTilesCount = Settings.VerticalTilesCount;

		_ticTacToeController = new TicTacToeController(horizontalTilesCount, verticalTilesCount, Settings.WinningTilesCount);

		//todo inject
		PlayerOne = new PlayerComputerRandom(NodeType.X);
		//todo inject
		PlayerTwo = new PlayerInput(NodeType.O);
		CurrentPlayer = PlayerOne;
	}

	public void OnNodeMark(Vector2Int index, NodeType nodeType)
    {
		GameView.OnNodeMark(index, nodeType);
		var winner = _ticTacToeController.CheckWin(index, CurrentPlayer.Type);
		if (winner != NodeType.None || !_ticTacToeController.CheckEmptyNodes())
		{
			SetWinner(winner);
			return;
		}

		SwitchPlayer();
	}

	private void SetWinner(NodeType winner)
	{
		GameplayManager.Instance.SetGameOverState();
		EventsManager.Instance.OnGameOver(winner);
	}

	public void SetLoser(NodeType loser)
	{
		if(loser == PlayerOne.Type)
		{
			SetWinner(PlayerTwo.Type);
		}
		else if(loser == PlayerTwo.Type)
		{
			SetWinner(PlayerOne.Type);
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
