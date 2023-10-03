using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnManager
{
	[Inject]
	private SaveManager _saveManager;

	public BoardController TicTacToeController { get; private set; }
	public bool AnyComputerPlay => !PlayerOne.AllowInput || !PlayerTwo.AllowInput;
	public uint HorizontalTilesCount => (uint)_saveManager.SaveData.horizontalNodes;
	public uint VerticalTilesCount => (uint)_saveManager.SaveData.verticalNodes;
	public uint WinningTilesCount => (uint)_saveManager.SaveData.winningNodes;
	public float TurnElapsed
	{
		get => _turnElapsed;
		set
		{
			_turnElapsed = value;
			EventsManager.Instance.OnTimerChanged(value);
		}
	}

	public IPlayer PlayerOne { get; set; }
	public IPlayer PlayerTwo { get; set; }

	private IPlayer _currentPlayer;
	public IPlayer CurrentPlayer
	{
		get => _currentPlayer;
		set
		{
			_currentPlayer = value;
			StartTurn(_currentPlayer);
			EventsManager.Instance.OnPlayerChanged(value);
		}
	}

	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();
	private IPlayer XPlayer => PlayerOne.NodeType == NodeType.X ? PlayerOne : PlayerTwo;
	private Coroutine _turnEndCoroutine;
	private float _turnElapsed;

    private void StartTurn(IPlayer player)
	{
		StopTurnEndCoroutine();
		_turnEndCoroutine = GameManager.Instance.StartCoroutine(TimerCoroutine(player));
		player.OnStartTurn();
	}

	private void StopTurnEndCoroutine()
	{
		if (_turnEndCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnEndCoroutine);
		}
		TurnElapsed = GameManager.Instance.Settings.PlayerTurnTime;
	}

	private IEnumerator TimerCoroutine(IPlayer player)
	{
		TurnElapsed = GameManager.Instance.Settings.PlayerTurnTime;
		while (TurnElapsed > 0)
        {
			TurnElapsed -= Time.deltaTime;
			yield return null;
        }
		SetLoser(player.NodeType);
	}

	public void StartGame()
    {
		//todo
		AssignPlayersTypes(new PlayerInput(this), new PlayerInput(this));
		AssignRandomNodesToPlayers();
		AssignNumbersToPlayers();
		SetPlayersNames();
		_movesHistory.Clear();

		TicTacToeController = new BoardController(HorizontalTilesCount, VerticalTilesCount, WinningTilesCount);

		CurrentPlayer = XPlayer;
	}

    public void AssignPlayersTypes(IPlayer playerOne, IPlayer playerTwo)
    {
		PlayerOne = playerOne;
		PlayerTwo = playerTwo;
	}

    private void AssignNumbersToPlayers()
    {
		PlayerOne.SetPlayerNumber(PlayerNumber.PlayerOne);
		PlayerTwo.SetPlayerNumber(PlayerNumber.PlayerTwo);
	}

    private void SetPlayersNames()
    {
		var playerName = PlayerOne.AllowInput ? "Human" : "Computer";
		if (PlayerOne.GetType().Equals(PlayerTwo.GetType()))
		{
			playerName += " 1";
		}
		PlayerOne.SetName(playerName);
		
		playerName = PlayerTwo.AllowInput ? "Human" : "Computer";
		if (PlayerTwo.GetType().Equals(PlayerOne.GetType()))
		{
			playerName += " 2";
		}
		PlayerTwo.SetName(playerName);
	}

    private void AssignRandomNodesToPlayers()
    {
		var random = UnityEngine.Random.Range(0, 2);
		var playerOneNodeType = random == 0 ? NodeType.X : NodeType.O;
		var playerTwoNodeType = playerOneNodeType == NodeType.O ? NodeType.X : NodeType.O;
		PlayerOne.SetNodeType(playerOneNodeType);
		PlayerTwo.SetNodeType(playerTwoNodeType);
	}

	public void NodeMark(Vector2Int index)
    {
		TicTacToeController.SetNode(index, CurrentPlayer.NodeType);
		
		if (TryEndGame(index)) return;

		StopTurnEndCoroutine();
		_movesHistory.Push(new Tuple<IPlayer, Vector2Int>(CurrentPlayer, index));

		SwitchPlayer();
	}

	private bool TryEndGame(Vector2Int index)
    {
		var result = false;
		var winner = TicTacToeController.CheckWin(index, CurrentPlayer.NodeType);
		if (winner != NodeType.None)
		{
			SetWinner(CurrentPlayer);
			result = true;
		}
		else if (!TicTacToeController.CheckEmptyNodes())
		{
			SetWinner(null);
			result = true;
		}
		
		return result;
	}

	public Node GetNodeToHint()
	{
		var emptyNode = TicTacToeController.GetRandomEmptyNode();
		return emptyNode;
	}

	public void UndoMove()
	{
		if (_movesHistory.Count == 0) return;

		var lastMove = _movesHistory.Pop();

		TicTacToeController.SetNode(lastMove.Item2, NodeType.None);

		StopTurnEndCoroutine();
		CurrentPlayer.OnTurnEnd();
		CurrentPlayer = lastMove.Item1;
	}

	private void SetWinner(IPlayer winner)
	{
		GameplayManager.Instance.SetGameOverState();
		EventsManager.Instance.OnGameOver(winner);
	}

	public void SetLoser(NodeType loser)
	{
		if(loser == PlayerOne.NodeType)
		{
			SetWinner(PlayerTwo);
		}
		else if(loser == PlayerTwo.NodeType)
		{
			SetWinner(PlayerOne);
		}
	}

	public void OnGameplayFinished()
	{
		StopTurnEndCoroutine();
		PlayerOne.OnTurnEnd();
		PlayerTwo.OnTurnEnd();
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
