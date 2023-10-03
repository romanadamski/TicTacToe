using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
internal class TurnController0
{
	#region Events

	//TODO #split: game state
	public event Action OnGameplayStarted;
	public event Action OnGameplayFinished;
	public event Action<IPlayer> OnGameOver;
	//TODO #split: player plays
	public event Action<IPlayer> OnPlayerChanged;
	public event Action<float> OnTimerChanged;
	//TODO #split: board
	public event Action<Vector2Int, NodeType> OnHint;
	public event Action<Vector2Int, NodeType> OnSetNode;

	#endregion

	public BoardController TicTacToeController { get; private set; }
	public Node RandomEmptyNode => TicTacToeController.GetRandomEmptyNode();

	public void StartGame()
	{
		//TODO #split: game state
		OnGameplayStarted?.Invoke();

		//TODO #split: player initialization
		AssignRandomNodesToPlayers();
		AssignNumbersToPlayers();
		SetPlayersNames();
		CurrentPlayer = XPlayer;
		//TODO #split: board
		_movesHistory.Clear();
		TicTacToeController = new BoardController(HorizontalTilesCount, VerticalTilesCount, WinningTilesCount);

	}

	//TODO #split: player initialization
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

	//TODO #split: board
	public void NodeMark(Vector2Int index)
	{
		SetNode(index, CurrentPlayer.NodeType);

		if (TryEndGame(index)) return;

		StopTurnEndCoroutine();
		_movesHistory.Push(new Tuple<IPlayer, Vector2Int>(CurrentPlayer, index));

		SwitchPlayer();
	}

	public void UndoMove()
	{
		if (_movesHistory.Count == 0) return;

		var lastMove = _movesHistory.Pop();

		SetNode(lastMove.Item2, NodeType.None);

		StopTurnEndCoroutine();
		CurrentPlayer.OnTurnEnd();
		CurrentPlayer = lastMove.Item1;
	}

	private void SetNode(Vector2Int index, NodeType nodeType)
	{
		TicTacToeController.SetNode(index, nodeType);
		OnSetNode?.Invoke(index, nodeType);
	}

	public Node GetNodeToHint()
	{
		var emptyNode = TicTacToeController.GetRandomEmptyNode();
		return emptyNode;
	}

	public void HintNode()
	{
		var node = GetNodeToHint();
		OnHint?.Invoke(node.index, CurrentPlayer.NodeType);
	}
	//TODO #split: game state
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

	private void SetWinner(IPlayer winner)
	{
		GameplayManager.Instance.SetGameOverState();
		OnGameOver?.Invoke(winner);
	}

	public void SetLoser(IPlayer loser)
	{
		if (loser == PlayerOne)
		{
			SetWinner(PlayerTwo);
		}
		else if (loser == PlayerTwo)
		{
			SetWinner(PlayerOne);
		}
	}

	public void GameplayFinished()
	{
		OnGameplayFinished?.Invoke();

		StopTurnEndCoroutine();
	}
}
*/