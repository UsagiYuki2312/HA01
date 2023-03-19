using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;


public class SPopupMenu : SGameUI
{
    public UnityAction OnPopupClose;
    protected Action OnOpenUpTransitionEnd;
    private Action OnPopupClosecAction;
    private Action OnPopupDestroyActionEnd;
    public static UnityAction<System.Type> displayAnotherPopup = delegate { };
    private static Vector3 popupMenuLowerScale = new Vector3(0.7f, 0.7f, 0.7f);
    public Transform wrapper;

    public virtual void Reset()
    {
        wrapper = transform.GetChild(0);
    }

    protected virtual void Awake()
    {
        OnPopupClosecAction = OpCloseAnimationEnd;
        OnPopupDestroyActionEnd = OnDestroyAnimationEnd;
        OnOpenUpTransitionEnd = OnOpenAnimationEnd;
    }

    protected virtual void OnOpenAnimationEnd()
    {

    }

    protected virtual void OpCloseAnimationEnd()
    {
        OnPopupClose?.Invoke();
        base.Hide();
    }

    protected virtual void OnDestroyAnimationEnd()
    {
        Destroy();
    }

    public override void Destroy()
    {
        OnPopupClose?.Invoke();
        base.Destroy();
    }

    public override void Display(float delay)
    {
        DelayCall(() => { Display(); }, new WaitForSeconds(delay));
    }

    public override void Display()
    {
        gameObject.SetActive(true);
        Tween.LocalScale(wrapper, popupMenuLowerScale, Vector3.one, 0.2f, 0, Tween.EaseOutBack, Tween.LoopType.None, null, OnOpenUpTransitionEnd, false);
    }

    public void Display(AnimationCurve animation)
    {
        gameObject.SetActive(true);
        Tween.LocalScale(wrapper, popupMenuLowerScale, Vector3.one, 0.2f, 0, animation, Tween.LoopType.None, null, OnOpenUpTransitionEnd, false);
    }

    public override void Hide()
    {
        Tween.LocalScale(wrapper, popupMenuLowerScale, 0.2f, 0, Tween.EaseInBack, Tween.LoopType.None, null, OnPopupClosecAction, false);
    }

    public virtual void DestroyWithAnimation()
    {
        Tween.LocalScale(wrapper, popupMenuLowerScale, 0.2f, 0, Tween.EaseInBack, Tween.LoopType.None, null, OnPopupDestroyActionEnd, false);
    }

    protected virtual void OnDisable()
    {

    }
}
