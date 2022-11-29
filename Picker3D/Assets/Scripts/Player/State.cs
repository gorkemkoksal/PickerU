using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public abstract class State
{
    protected PlayerStateMachine stateMachine;
    public State(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public abstract void Enter();
    public abstract void Thick(float deltaTime);
    public abstract void FixedThick(float fixedDeltaTime);
    public abstract void Exit();
    
    protected void Move(Vector3 movementVector)
    {
        stateMachine.PlayerRB.MovePosition(stateMachine.transform.position + movementVector * stateMachine.HorizontalSpeed);
    }
    protected bool IsTouched()
    {
        return Input.touchCount > 0;
    }
}
