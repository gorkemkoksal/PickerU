using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayingState : State
{
    Vector3 _forwardMovement;
    public PlayingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        StateChanger.StopAtCollector += OnCollector;
        StateChanger.RampFlyState += OnRamp;
        _forwardMovement = new Vector3(0, 0, stateMachine.ForwardSpeed);
    }
    public override void Thick(float deltaTime) { }
    public override void FixedThick(float fixedDeltaTime)
    {
        Move(stateMachine.Controls.HorizontalVector + _forwardMovement);
    }
    public override void Exit()
    {
        StateChanger.StopAtCollector -= OnCollector;
        StateChanger.RampFlyState -= OnRamp;
    }

    private void OnCollector()
    {
        stateMachine.SwitchState(new StayingState(stateMachine, false));
    }
    private void OnRamp()
    {
        stateMachine.SwitchState(new RampingState(stateMachine));
    }
}
