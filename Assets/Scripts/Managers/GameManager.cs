public class GameManager : BaseManager<GameManager>
{
    #region States

    private StateMachine _gameStateMachine;

    public MainMenuState MainMenuState;
    public LevelState LevelState;

    #endregion

    private void Start()
    {
        InitStates();
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        _gameStateMachine.SetState(MainMenuState);
    }

    private void InitStates()
    {
        _gameStateMachine = gameObject.AddComponent<StateMachine>();

        MainMenuState = new MainMenuState(_gameStateMachine);
        LevelState = new LevelState(_gameStateMachine);
    }

    public void SetLevelState()
    {
        _gameStateMachine.SetState(LevelState);
    }
}
