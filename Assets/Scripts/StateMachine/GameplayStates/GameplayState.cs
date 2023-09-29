public class GameplayState : State
{
    private GameplayMenu _gameplayMenu;

    public GameplayState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        GameplayManager.Instance.StartGameplay();
        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.Hide();
        }
    }
}
