public abstract class State
{
    protected StateMachine _stateMachine;

    /// <summary>
    /// Called when state is set
    /// </summary>
    protected virtual void OnEnter() { }

    /// <summary>
    /// Called when state is about to replace with another state
    /// </summary>
    protected virtual void OnExit() { }

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    /// <summary>
    /// Set this state and call its OnEnter method
    /// </summary>
    public void Enter()
    {
        OnEnter();
    }

    /// <summary>
    /// Exit this state and call its OnExit method
    /// </summary>
    public void Exit()
    {
        OnExit();
    }
}
