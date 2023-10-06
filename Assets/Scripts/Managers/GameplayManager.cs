using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    [SerializeField]
    private GameplayEventsSO gameplayEventsSO;
    [SerializeField]
    private GameEventsSO gameEventsSO;

    private void Awake()
    {
        InitStates();
        gameplayEventsSO.OnGameOver += winner => SetGameOverState();
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        GameOverState = new GameOverState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine, gameEventsSO);
    }

    private void ClearGameplay()
    {
        gameplayEventsSO.GameplayFinished();
	}

    public void StartGameplay()
    {
        gameplayEventsSO.GameplayStarted();
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
