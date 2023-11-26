using UnityEngine;

public enum PlayerState
{
    IDLE,
    WALK,
    RUN,
    JUMP,
}


public class PlayerStateMachine : MonoBehaviour
{

    private PlayerState _currentState;

    public PlayerState CurrentState { get => _currentState; private set => _currentState = value; }
    private void Start()
    {
        TransitionToState(CurrentState, PlayerState.IDLE);
    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
    }
    private void OnStateEnter(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnEnterIdle();
                break;
            case PlayerState.WALK:
                OnEnterWalk();
                break;
            case PlayerState.RUN:
                OnEnterRun();
                break;
            case PlayerState.JUMP:
                OnEnterJump();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnUpdateIdle();
                break;
            case PlayerState.WALK:
                OnUpdateWalk();
                break;
            case PlayerState.RUN:
                OnUpdateRun();
                break;
            case PlayerState.JUMP:
                OnUpdateJump();
                break;
            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                OnExitIdle();
                break;
            case PlayerState.WALK:
                OnExitWalk();
                break;
            case PlayerState.RUN:
                OnExitRun();
                break;
            case PlayerState.JUMP:
                OnExitJump();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    private void TransitionToState(PlayerState fromState, PlayerState toState)
    {
        OnStateExit(fromState);
        CurrentState = toState;
        OnStateEnter(toState);
    }
    private void TransitionToState(PlayerState toState)
    {
        TransitionToState(CurrentState, toState);
    }
    private void OnEnterIdle()
    {
    }
    private void OnUpdateIdle()
    {
    }
    private void OnExitIdle()
    {
    }
    private void OnEnterWalk()
    {
    }
    private void OnUpdateWalk()
    {
    }
    private void OnExitWalk()
    {
    }
    private void OnEnterRun()
    {
    }
    private void OnUpdateRun()
    {
    }
    private void OnExitRun()
    {
    }
    private void OnEnterJump()
    {
    }
    private void OnUpdateJump()
    {
    }
    private void OnExitJump()
    {
    }
}
