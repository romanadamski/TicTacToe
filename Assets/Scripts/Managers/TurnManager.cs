using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : BaseManager<TurnManager>
{
	private TicTacToeController _ticTacToeController;
	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();

	public GameView GameView => GameView.Instance;
	public uint HorizontalTilesCount => 3;
	public uint VerticalTilesCount => 3;
	public uint WinningTilesCount => 3;

	public GameSettingsSO Settings;

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
		GameView.SpawnTiles();

		_ticTacToeController = new TicTacToeController(HorizontalTilesCount, VerticalTilesCount, WinningTilesCount);

		CurrentPlayer = PlayerOne;
	}

	public void OnNodeMark(Vector2Int index, NodeType nodeType)
    {
		_movesHistory.Push(new Tuple<IPlayer, Vector2Int>(CurrentPlayer, index));

		GameView.OnNodeMark(index, nodeType);
		_ticTacToeController.SetNode(index, CurrentPlayer.Type);

		var winner = _ticTacToeController.CheckWin(index, CurrentPlayer.Type);
		if (winner != NodeType.None || !_ticTacToeController.CheckEmptyNodes())
		{
			SetWinner(winner);
			return;
		}

		SwitchPlayer();
	}

	public void HintMove()
	{
		
	}

	public void UndoMove()
	{
		if (_movesHistory.Count == 0) return;

		var lastMove = _movesHistory.Pop();

		_ticTacToeController.SetNode(lastMove.Item2, NodeType.None);
		GameView.OnNodeMark(lastMove.Item2, NodeType.None);

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
