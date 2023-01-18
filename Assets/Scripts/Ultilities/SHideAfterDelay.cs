using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHideAfterDelay : MonoBehaviourCore
{
    private Action OnObjectDisappear;
    private object delay;
    public float delaySeconds;

    private void Awake()
    {
        OnObjectDisappear = Hide;
        delay = new WaitForSeconds(delaySeconds);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        DelayCallAction(OnObjectDisappear, delay);
    }
}
