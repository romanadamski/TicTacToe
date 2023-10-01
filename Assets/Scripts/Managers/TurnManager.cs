using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
	public BoardController TicTacToeController { get; private set; }
	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();

	public uint HorizontalTilesCount => GameManager.Instance.Settings.HorizontalNodes;
	public uint VerticalTilesCount => GameManager.Instance.Settings.VerticalNodes;
	public uint WinningTilesCount => GameManager.Instance.Settings.WinNodes;

	public IPlayer PlayerOne { get; set; }
	public IPlayer PlayerTwo { get; set; }

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

	public bool AnyComputerPlay => !PlayerOne.AllowInput || !PlayerTwo.AllowInput;

	public void StartGame()
    {
		_movesHistory.Clear();

		TicTacToeController = new BoardController(HorizontalTilesCount, VerticalTilesCount, WinningTilesCount);

		CurrentPlayer = PlayerOne;
	}

	public void OnNodeMark(Vector2Int index, NodeType nodeType)
    {
		_movesHistory.Push(new Tuple<IPlayer, Vector2Int>(CurrentPlayer, index));

		TicTacToeController.SetNode(index, CurrentPlayer.Type);

		var winner = TicTacToeController.CheckWin(index, CurrentPlayer.Type);
		if (winner != NodeType.None || !TicTacToeController.CheckEmptyNodes())
		{
			SetWinner(winner);
			return;
		}

		SwitchPlayer();
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

		CurrentPlayer.OnTurnEnd();
		CurrentPlayer = lastMove.Item1;
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

	public void OnGameplayFinish()
	{
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
