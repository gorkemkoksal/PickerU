using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampJump : MonoBehaviour
{
    public static event Action RampEnd;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        RampEnd?.Invoke();
    }
}
