using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTriggeredTracker
{
    private int trackTime;
    private bool trackingState = false;
    public bool DoubleTriggered { get; set; }
    private int millisecondInterval;

    public DoubleTriggeredTracker(int millisecondInterval)
    {
        this.trackTime = 0;
        this.millisecondInterval = millisecondInterval;
    }

    public void TrackEvent()
    {
        trackTime++;
        if (trackingState) DoubleTriggered = true;
        if (trackTime == 1) StartCounting();
    }

    private async void StartCounting()
    {
        trackingState = true;
        Debug.Log("tracking state: " + trackingState);
        await Task.Delay(millisecondInterval);
        trackingState = false;
        trackTime = 0;
        Debug.Log("tracking state: " + trackingState);
    }
}
