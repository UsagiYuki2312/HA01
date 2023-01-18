using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SJoystickController : MonoBehaviourCore
{
    public SkillType skillType;
    public Image handleImage;
    public RawImage imageSkill;
    public JoystickSkill joystickSkill;
    public Image coolDownImage;
    public TextMeshProUGUI coolDownTxt;
    public UnityAction<float> OnCoolDown;
    public UnityAction OnResetCoolDown;
    private Coroutine popupCountDown;

    public void Awake()
    {
        SetColorPressUp();
        joystickSkill.pressUp = SetColorPressUp;
        joystickSkill.pressDown = SetColorPressDown;
        OnCoolDown = SetCooldown;
        OnResetCoolDown = ResetCoolDown;
    }
    public void SetColorPressDown()
    {
        handleImage.color = new Color32(225, 225, 225, 225);
    }
    public void SetColorPressUp()
    {
        handleImage.color = new Color32(225, 225, 225, 0);
    }
    public void SetCooldown(float coolDown)
    {
        popupCountDown = StartCoroutine(PopupCountDown(coolDown));
    }
    public void ResetCoolDown()
    {
        coolDownImage.gameObject.SetActive(false);
        StopCoroutine(popupCountDown);
    }
    public IEnumerator PopupCountDown(float cooldown)
    {
        coolDownImage.gameObject.SetActive(true);
        coolDownTxt.text = cooldown.ToString();
        for (int i = 1; i <= cooldown; i++)
        {
            yield return new WaitForSeconds(1f);
            coolDownTxt.text = (cooldown - i).ToString();
        }
        coolDownImage.gameObject.SetActive(false);
    }
}
