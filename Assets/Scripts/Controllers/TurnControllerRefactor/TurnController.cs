using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurnController : MonoBehaviour, ITurnController
{
	[SerializeField]
	private NodeType startNodeType = NodeType.X;
	[SerializeField]
	private SettingsSO settingsSO;
	[SerializeField]
	private BoardEventsSO boardEventsSO;
	[SerializeField]
	protected TurnEventsSO turnEventsSO;
	[SerializeField]
	protected GameplayEventsSO gameplayEventsSO;

	private float PlayerTurnTimeLimit => settingsSO.PlayerTurnTimeLimit;
	private Coroutine _turnEndCoroutine;
	private float _turnElapsed;
	public float TurnElapsed
	{
		get => _turnElapsed;
		set
		{
			_turnElapsed = value;
			turnEventsSO.TimerChanged(value);
		}
	}

	public List<IPlayer> Players { get; set; } = new List<IPlayer>();

	private IPlayer _currentPlayer;
	public IPlayer CurrentPlayer
	{
		get => _currentPlayer;
		private set
		{
			_currentPlayer = value;
			StartTurn(value);
			turnEventsSO.PlayerChanged(value);
		}
	}

	public bool AnyComputerPlay => Players.Any(player => !player.AllowInput);

	private int _currentIndex;

	private void Awake()
    {
        SubscribeToEvents();
    }

	private void SubscribeToEvents()
    {
        gameplayEventsSO.OnGameplayStarted += OnGameplayStarted;
        gameplayEventsSO.OnGameplayFinished += OnGameplayFinished;
        boardEventsSO.OnSetNode += OnSetNode;
		turnEventsSO.OnAddPlayer += AddPlayer;
	}

	public void UndoTurn()
    {
		StopTurnEndCoroutine();
		CurrentPlayer.OnTurnEnd();
		SetPreviousPlayer();
	}

	private void OnSetNode(IPlayer player, Vector2Int index)
    {
		StopTurnEndCoroutine();
		SetNextPlayer();
	}

	protected virtual void SetPlayers() { }

    private void OnGameplayStarted()
	{
		SetPlayers();
		SetFirstPlayer();
	}

	private void OnGameplayFinished()
    {
		Players.Clear();
		StopTurnEndCoroutine();
	}

    public void SetFirstPlayer()
	{
		for (int i = 0; i < Players.Count; i++)
		{
			if (Players[i].NodeType != startNodeType) continue;

			_currentIndex = i;
			CurrentPlayer = Players[i];
		}
	}

	public void SetNextPlayer()
	{
		if (_currentIndex < Players.Count - 1)
		{
			_currentIndex++;
		}
		else
		{
			_currentIndex = 0;
		}
		CurrentPlayer = Players[_currentIndex];
	}

	public void SetPreviousPlayer()
	{
		if (_currentIndex > 0)
		{
			_currentIndex--;
		}
		else
		{
			_currentIndex = Players.Count - 1;
		}
		CurrentPlayer = Players[_currentIndex];
	}

	public void AddPlayer(IPlayer player)
	{
		Players.Add(player);
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

	protected virtual void SetLoser(IPlayer loser) { }
}
