using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndState : State
{
    bool isLevelPassed;
    public LevelEndState(PlayerStateMachine stateMachine, bool isLevelPassed) : base(stateMachine)
    {
        this.isLevelPassed = isLevelPassed;
    }

    public override void Enter()
    {
        if (isLevelPassed)
            stateMachine.UIManager.LevelEndCanvas.gameObject.SetActive(true);
        else
            stateMachine.UIManager.RestartCanvas.gameObject.SetActive(true);
    }
    public override void Exit() { }

    public override void FixedThick(float fixedDeltaTime) { }

    public override void Thick(float deltaTime) { }
}
