using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnController
{
	public BoardController TicTacToeController { get; private set; }
	public bool AnyComputerPlay => !PlayerOne.AllowInput || !PlayerTwo.AllowInput;
	public uint HorizontalTilesCount => GameManager.Instance.SettingsSO.HorizontalNodes;
	public uint VerticalTilesCount => GameManager.Instance.SettingsSO.VerticalNodes;
	public uint WinningTilesCount => GameManager.Instance.SettingsSO.WinningNodes;
	public float PlayerTurnTimeLimit => GameManager.Instance.SettingsSO.PlayerTurnTimeLimit;
	private float _turnElapsed;
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

	private void StartTurn(IPlayer player)
	{
		StopTurnEndCoroutine();
		_turnEndCoroutine = GameManager.Instance.StartCoroutine(TimerCoroutine(player));
		player.OnStartTurn();
	}

	private void ResetTurnTime()
    {
		TurnElapsed = PlayerTurnTimeLimit;
	}

	private void StopTurnEndCoroutine()
	{
		if (_turnEndCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnEndCoroutine);
		}
		ResetTurnTime();
	}

	private IEnumerator TimerCoroutine(IPlayer player)
	{
		ResetTurnTime();
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