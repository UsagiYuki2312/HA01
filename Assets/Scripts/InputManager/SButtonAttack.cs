using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SButtonAttack : Button
{
    // Start is called before the first frame update
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        SGameInstance.Instance.buttonAttackController.OnClickAttackButton();
    }

    // Button is released
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Debug.Log("OnPointerUp");
    }
}
