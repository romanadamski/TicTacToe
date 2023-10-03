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
	private TurnController _turnController;

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
		_turnController.GameplayFinished();
	}

    public void StartGameplay()
    {
		_turnController.StartGame();
    }

    public void RestartGameplay()
    {
		ClearGameplay();
		StartGameplay();
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
