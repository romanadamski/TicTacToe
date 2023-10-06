using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region States

    private StateMachine _gameStateMachine;

    public MainMenuState MainMenuState;
    public LevelState LevelState;
    public SettingsState SettingsState;

    #endregion

    [SerializeField]
    private GameEventsSO gameEventsSO;

    private void Awake()
    {
        gameEventsSO.OnGoToMainMenu += SetMainMenuState;
        gameEventsSO.OnStartLevel += SetLevelState;
        gameEventsSO.OnGoToSettings += SetSettingsState;
    }

    private void Start()
    {
        InitStates();

        gameEventsSO.GoToMainMenu();
    }

    private void InitStates()
    {
        _gameStateMachine = gameObject.AddComponent<StateMachine>();

        MainMenuState = new MainMenuState(_gameStateMachine);
        LevelState = new LevelState(_gameStateMachine);
        SettingsState = new SettingsState(_gameStateMachine);
    }

    private void SetMainMenuState()
    {
        _gameStateMachine.SetState(MainMenuState);
    }

    private void SetLevelState()
	{
		_gameStateMachine.SetState(LevelState);
	}

    private void SetSettingsState()
	{
		_gameStateMachine.SetState(SettingsState);
	}
}
