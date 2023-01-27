using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGamePlayUI : SGameUI
{
    public RectTransform joystickZone;
    public RectTransform skillZone;
    public FixedJoystick floatingJoystick;
    public SSkillJoytickPanel skillPanel;
    public const string JOYSTICK_PATH = "Prefabs/Joystick/";
    public const string SKILL_JOYSTICK_PATH = "Prefabs/Skill/";

    private void Start()
    {
        floatingJoystick = Resources.Load<FixedJoystick>(JOYSTICK_PATH + "Fixed Joystick");
        CreateJoystick(joystickZone);
        skillPanel = Resources.Load<SSkillJoytickPanel>(SKILL_JOYSTICK_PATH + "SkillPanel");
        CreatePanelSkill(skillZone);
    }
    public void CreateJoystick(RectTransform joystickZone)
    {
        SGameInstance.Instance.floatingJoystick = Instantiate(floatingJoystick, joystickZone);
    }
    public void CreatePanelSkill(RectTransform joystickZone)
    {
        SGameInstance.Instance.skillJoytickPanel = Instantiate(skillPanel, joystickZone);
    }

}
