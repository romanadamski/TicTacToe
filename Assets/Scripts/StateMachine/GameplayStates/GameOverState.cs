public class GameOverState : State
{
    private GameOverMenu _gameOverMenu;

    public GameOverState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        if (_gameOverMenu || UIManager.Instance.TryGetMenuByType(out _gameOverMenu))
        {
            _gameOverMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_gameOverMenu || UIManager.Instance.TryGetMenuByType(out _gameOverMenu))
        {
            _gameOverMenu.Hide();
        }
    }
}
