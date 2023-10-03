public class SettingsState : State
{
    private SettingsMenu _settingsMenu;

    public SettingsState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        if (_settingsMenu || UIManager.Instance.TryGetMenuByType(out _settingsMenu))
        {
            _settingsMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_settingsMenu || UIManager.Instance.TryGetMenuByType(out _settingsMenu))
        {
            _settingsMenu.Hide();
        }
    }
}
