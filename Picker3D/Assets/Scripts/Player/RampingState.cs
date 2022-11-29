using UnityEngine;

public class RampingState : State
{
    private bool _isJumped;
    public RampingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        RampJump.RampEnd += RampJumping;
        stateMachine.CameraFollow.isRamping = true;
        stateMachine.MeshCollider.convex = true;
        stateMachine.PlayerRB.useGravity = true;
        stateMachine.PlayerRB.isKinematic = false;
        stateMachine.PlayerRB.constraints = RigidbodyConstraints.FreezePositionX;
    }
    public override void Thick(float deltaTime)
    {
        GetSpeedByTouch();
        AtStopStartCollect();       
    }
    public override void FixedThick(float fixedDeltaTime)
    {
        ConstantSpeedAtRamp();
    }
    public override void Exit()
    {
        RampJump.RampEnd -= RampJumping;
        //stateMachine.PlayerRB.isKinematic = true;
        //stateMachine.MeshCollider.convex = false;
        //stateMachine.PlayerRB.useGravity = false;
    }
    private void RampJumping()
    {
        _isJumped = true;
        stateMachine.PlayerRB.constraints = RigidbodyConstraints.None;
        stateMachine.PlayerRB.AddForce(stateMachine.RampJumpForce, ForceMode.Impulse);
    }
    private void GetSpeedByTouch()
    {
        if (IsTouched() && !_isJumped)
            stateMachine.PlayerRB.AddRelativeForce(stateMachine.RampingTouchForce, ForceMode.Impulse);
    }
    private void AtStopStartCollect()
    {
        if (stateMachine.PlayerRB.velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            stateMachine.Money += (int)stateMachine.gameObject.transform.position.z - 130;
            PlayerPrefs.SetInt("Money", stateMachine.Money);
            stateMachine.SwitchState(new LevelEndState(stateMachine, true));
        }
    }
    private void ConstantSpeedAtRamp()
    {
        if (stateMachine.PlayerRB.velocity.sqrMagnitude < stateMachine.ConstantRampingSpeed.sqrMagnitude && !_isJumped)
            stateMachine.PlayerRB.AddRelativeForce(stateMachine.ConstantRampingSpeed, ForceMode.VelocityChange);
    }
}
