using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SSecondSkillJoystick : JoystickSkill
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        SGameInstance.Instance.player.skillComponent.UseSecondSkill();
        base.OnPointerUp(eventData);
    }
}

