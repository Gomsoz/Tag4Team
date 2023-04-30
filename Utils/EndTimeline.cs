using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimeline : MonoBehaviour
{
    public Action EndTimeLineEvent = null;
    private void OnEnable()
    {
        EndTimeLineEvent?.Invoke();
    }

    private void OnDisable()
    {
        EndTimeLineEvent = null;
    }
}
