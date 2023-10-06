using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTurnControllerObsolete
{
	#region Events

	public event Action OnGameplayStarted;
	public event Action OnGameplayFinished;
	public event Action<IPlayer> OnGameOver;
	public event Action<IPlayer> OnPlayerChanged;
	public event Action<float> OnTimerChanged;
	public event Action<Vector2Int, NodeType> OnHint;
	public event Action<Vector2Int, NodeType> OnSetNode;

	#endregion

	public BoardController TicTacToeController { get; private set; }
	public Node RandomEmptyNode => TicTacToeController.GetRandomEmptyNode();
	public bool AnyComputerPlay => !PlayerOne.AllowInput || !PlayerTwo.AllowInput;

	private uint HorizontalTilesCount => GameManager.Instance.SettingsSO.HorizontalNodes;
	private uint VerticalTilesCount => GameManager.Instance.SettingsSO.VerticalNodes;
	private uint WinningTilesCount => GameManager.Instance.SettingsSO.WinningNodes;
	private float PlayerTurnTimeLimit => GameManager.Instance.SettingsSO.PlayerTurnTimeLimit;

	private float _turnElapsed;
	public float TurnElapsed
	{
		get => _turnElapsed;
		set
		{
			_turnElapsed = value;
			OnTimerChanged?.Invoke(value);
		}
	}

	public IPlayer PlayerOne { get; set; }
	public IPlayer PlayerTwo { get; set; }

	private IPlayer _currentPlayer;
	public IPlayer CurrentPlayer
	{
		get => _currentPlayer;
		private set
		{
			_currentPlayer = value;
			StartTurn(value);
			OnPlayerChanged?.Invoke(value);
		}
	}
	private IPlayer XPlayer => PlayerOne.NodeType == NodeType.X ? PlayerOne : PlayerTwo;

	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();
	private Coroutine _turnEndCoroutine;

	public void StartGame()
    {
		OnGameplayStarted?.Invoke();

		AssignRandomNodesToPlayers();
		AssignNumbersToPlayers();
		SetPlayersNames();
		_movesHistory.Clear();

		TicTacToeController = new BoardController();
		TicTacToeController.Set(HorizontalTilesCount, VerticalTilesCount, WinningTilesCount);

		CurrentPlayer = XPlayer;
	}

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
		SetLoser(player);
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

	public void HintNode()
    {
		var node = GetNodeToHint();
		OnHint?.Invoke(node.index, CurrentPlayer.NodeType);
	}

	private void SetWinner(IPlayer winner)
	{
		GameplayManager.Instance.SetGameOverState();
		OnGameOver?.Invoke(winner);
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

	public void EndGame()
	{
		OnGameplayFinished?.Invoke();

		StopTurnEndCoroutine();
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
