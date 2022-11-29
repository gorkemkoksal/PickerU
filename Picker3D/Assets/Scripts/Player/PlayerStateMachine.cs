using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [field: SerializeField] public Rigidbody PlayerRB { get; private set; }
    [field: SerializeField] public Controls Controls { get; private set; }
    [field: SerializeField] public MeshCollider MeshCollider { get; private set; }
    [field: SerializeField] public CamFollow CameraFollow { get; private set; }
    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public float ForwardSpeed { get; private set; }
    [field: SerializeField] public float HorizontalSpeed { get; private set; }
    [field: SerializeField] public Vector3 RampingTouchForce { get; private set; }
    [field: SerializeField] public Vector3 ConstantRampingSpeed { get; private set; }
    [field: SerializeField] public Vector3 RampJumpForce { get; private set; }

    private State _currentState;
    private float _minXBorder = -1.5f;
    private float _maxXBorder = 4.5f;
    public int Money;

    void Start()
    {
        Money = PlayerPrefs.GetInt("Money", Money);
        SwitchState(new StayingState(this, true));
    }
    void Update()
    {
        BorderRestrict();
        _currentState?.Thick(Time.deltaTime);
        UIManager.moneyText.text = Money.ToString();
    }
    private void FixedUpdate()
    {
        _currentState?.FixedThick(Time.fixedDeltaTime);
    }
    public void SwitchState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
    protected void BorderRestrict()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minXBorder, _maxXBorder), transform.position.y, transform.position.z);
    }
}
