using Zenject;

public class GameplayManager : BaseManager<GameplayManager>
{
    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

	#endregion

	[Inject]
	private TurnManager _turnManager;

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

    private void ClearGameplay()
    {
        EventsManager.Instance.OnGameplayFinished();
		_turnManager.OnGameplayFinished();

	}

    public void StartGameplay()
    {
        EventsManager.Instance.OnGameplayStarted();
        StartCurrentLevel();
	}

    public void RestartGameplay()
    {
		ClearGameplay();
		StartGameplay();
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
