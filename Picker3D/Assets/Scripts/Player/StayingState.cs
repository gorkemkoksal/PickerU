using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StayingState : State
{
    bool _isBeforeLevel;
    bool _unityStartButtonTouch=true;
    public StayingState(PlayerStateMachine stateMachine, bool isBeforeLevel) : base(stateMachine)
    {
        _isBeforeLevel = isBeforeLevel;
    }

    public override void Enter()
    {
        Collector.StartPlayer += OnOpenBorder;
        Collector.PlayerFail += OnFail;
    }
    public override void Thick(float deltaTime)
    {        
        if (_isBeforeLevel && !_unityStartButtonTouch && IsTouched())
        {
            stateMachine.SwitchState(new PlayingState(stateMachine));
        }
        if (IsTouched())
        {
            _unityStartButtonTouch = false;        // avoid counting start button from the unity interface as a touch
        }
    }
    public override void FixedThick(float fixedDeltaTime)
    {
        if(IsTouched() && !_isBeforeLevel)
        {
            Move(stateMachine.Controls.HorizontalVector);
        }
    }
    public override void Exit()
    {
        Collector.StartPlayer -= OnOpenBorder;
        Collector.PlayerFail -= OnFail;
    }
    private void OnOpenBorder()
    { 
        stateMachine.SwitchState(new PlayingState(stateMachine));
    }
    private void OnFail()
    {
        stateMachine.SwitchState(new LevelEndState(stateMachine,false));
    }
}
