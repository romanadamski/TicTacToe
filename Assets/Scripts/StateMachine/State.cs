public abstract class State
{
    protected StateMachine _stateMachine;

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        OnEnter();
    }

    public void Exit()
    {
        OnExit();
    }
}
