public class MainMenuState : State
{
    private MainMenu _mainMenu;

    public MainMenuState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        if (_mainMenu || UIManager.Instance.TryGetMenuByType(out _mainMenu))
        {
            _mainMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_mainMenu || UIManager.Instance.TryGetMenuByType(out _mainMenu))
        {
            _mainMenu.Hide();
        }
    }
}
