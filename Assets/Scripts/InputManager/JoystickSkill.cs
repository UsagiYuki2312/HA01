using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class JoystickSkill : FixedJoystick
{
    public UnityAction pressUp;
    public UnityAction pressDown;
    protected Vector3 dir;
    public override void OnPointerDown(PointerEventData eventData)
    {
        pressDown?.Invoke();
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        pressUp?.Invoke();
        dir = Direction;
        base.OnPointerUp(eventData);
    }
}
