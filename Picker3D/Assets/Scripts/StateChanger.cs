using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] bool IsItForRamp;

    public static event Action StopAtCollector;
    public static event Action RampFlyState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }

        if (IsItForRamp)
        {
            RampFlyState?.Invoke();
        }
        else
            StopAtCollector?.Invoke();
    }
}
