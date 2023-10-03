
using Zenject;

public class GameManager : BaseManager<GameManager>
{
    #region States

    private StateMachine _gameStateMachine;

    public MainMenuState MainMenuState;
    public LevelState LevelState;
    public SettingsState SettingsState;

	#endregion

	public GameSettingsSO Settings;

    [Inject]
    private SaveManager _saveManager;

    private void Start()
    {
        InitStates();
        SetMainMenuState();
    }

    public void SetMainMenuState()
    {
        _gameStateMachine.SetState(MainMenuState);
    }

    private void InitStates()
    {
        _gameStateMachine = gameObject.AddComponent<StateMachine>();

        MainMenuState = new MainMenuState(_gameStateMachine);
        LevelState = new LevelState(_gameStateMachine);
        SettingsState = new SettingsState(_gameStateMachine);
    }

    public void SetLevelState()
	{
		_gameStateMachine.SetState(LevelState);
	}

    public void SetSettingsState()
	{
		_gameStateMachine.SetState(SettingsState);
	}

    private void OnApplicationQuit()
    {
        _saveManager.Save();
    }
}
