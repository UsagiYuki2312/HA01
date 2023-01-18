using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLinearMoveAlien : SAlien
{
    private Action OnAlienDisappear;
    private object alienExistent = new WaitForSeconds(6);

    private void Awake()
    {
        OnAlienDisappear = Hide;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DelayCallAction(OnAlienDisappear, alienExistent);
    }
}
