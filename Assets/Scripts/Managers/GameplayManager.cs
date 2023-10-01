public class GameplayManager : BaseManager<GameplayManager>
{
    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

	#endregion

	private TurnManager _turnManager => TurnManager.Instance;

    private void Awake()
    {
        InitStates();
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        GameOverState = new GameOverState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
		_turnManager.OnGameplayFinish();

	}

    public void StartGameplay()
    {
        StartCurrentLevel();
        EventsManager.Instance.OnGameplayStarted();
    }

    public void RestartGameplay()
    {
		ClearGameplay();
		StartCurrentLevel();
        EventsManager.Instance.OnGameplayStarted();
	}

	private void StartCurrentLevel()
	{
		_turnManager.StartGame();
	}

	public void SetGameplayState()
    {
        _gameplayStateMachine.SetState(GameplayState);
    }

    public void SetGameOverState()
    {
        _gameplayStateMachine.SetState(GameOverState);
    }

    public void SetEndGameplayState()
    {
        _gameplayStateMachine.SetState(EndGameplayState);
    }

    public void ClearGameplayStateMachine()
    {
        ClearGameplay();
        _gameplayStateMachine.Clear();
    }
}
