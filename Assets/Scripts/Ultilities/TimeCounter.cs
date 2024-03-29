using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeCounter : ClassInstanceCore
{
    public UnityAction<int> OnEverySecondsCount;
    public UnityAction<int> OnEveryTotalSecondsCount;
    public UnityAction<int> OnEveryMinutesCount;
    public UnityAction OnCounterStart;
    public UnityAction OnCounterEnd;
    private Coroutine timeCountingCoroutine;
    private int minutes;
    private int secondBuff;
    private int second20Buff;
    private int seconds;
    private int totalSeconds;
    private int endSeconds;
    private object delaySecond = new WaitForSeconds(1);

    public void StartCounting(int totalSeconds, int endSeconds)
    {
        this.totalSeconds = totalSeconds;
        this.minutes = (int)totalSeconds / 60;
        this.seconds = totalSeconds % 60;
        this.endSeconds = endSeconds;
        this.secondBuff = totalSeconds % 60;
        this.second20Buff = (int)totalSeconds / 20;
        timeCountingCoroutine = StartCoroutine(CountTime());
    }

    public void StartCounting(int endSeconds)
    {
        this.seconds = 0;
        this.totalSeconds = 0;
        this.minutes = 0;
        this.secondBuff = 0;
        this.second20Buff = 0;
        this.endSeconds = endSeconds;
        timeCountingCoroutine = StartCoroutine(CountTime());
    }

    public void StopCounting()
    {
        StopCoroutine(timeCountingCoroutine);
    }

    public void ContinueCounting()
    {
        timeCountingCoroutine = StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        OnCounterStart?.Invoke();
        while (totalSeconds <= endSeconds)
        {
            OnEverySecondsCount?.Invoke(seconds);
            OnEveryTotalSecondsCount?.Invoke(totalSeconds);
            seconds++;
            secondBuff++;
            totalSeconds++;
            if (seconds > 59)
            {
                seconds = 0;
                //OnEveryMinutesCount?.Invoke(minutes);
                minutes++;
            }
            if (secondBuff > 19)
            {
                secondBuff = 0;
                OnEveryMinutesCount?.Invoke(second20Buff);
                second20Buff++;
            }
            yield return delaySecond;
        }
        OnCounterEnd?.Invoke();
    }
}
