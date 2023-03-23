using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SGamePlayUI : SGameUI
{
    public RectTransform joystickZone;
    public RectTransform skillZone;
    public FixedJoystick floatingJoystick;
    public SSkillJoytickPanel skillPanel;
    public SPlayerGause playerGause;

    private SBossHealthBar bossHealthBarPrefab;
    public Transform bossHealthBarContainer;

    public TMP_Text chapterTime;
    public const string JOYSTICK_PATH = "Prefabs/Joystick/";
    public const string SKILL_JOYSTICK_PATH = "Prefabs/Skill/";

    private void Awake()
    {
        //bossHealthBarPrefab= Resources.Load<SBossHealthBar>(ResourcePath.UI_PATH + "BossHealthBar/BossHealthBar");
        floatingJoystick = Resources.Load<FixedJoystick>(JOYSTICK_PATH + "Fixed Joystick");
        CreateJoystick(joystickZone);
        skillPanel = Resources.Load<SSkillJoytickPanel>(SKILL_JOYSTICK_PATH + "SkillPanel");
        CreatePanelSkill(skillZone);
        GameInstance.gameEvent.OnBossHealthBarRegistered = OnBossInstantiated;
        GameInstance.gameEvent.OnBossDefeated += DestroyBossHealthBar;
    }
    public void CreateJoystick(RectTransform joystickZone)
    {
        SGameInstance.Instance.floatingJoystick = Instantiate(floatingJoystick, joystickZone);
    }
    public void CreatePanelSkill(RectTransform joystickZone)
    {
        SGameInstance.Instance.skillJoytickPanel = Instantiate(skillPanel, joystickZone);
    }

    public void SetTime(int totalSeconds)
    {
        chapterTime.SetText(Utils.FormatTimeSecond(totalSeconds));
    }

    public void OnPauseButtonClick()
    {
        MessageManager.SendMessage(new Message(TeeMessageType.OnPauseButtonClicked));
    }

    private void OnBossInstantiated(SBoss boss)
    {
        SBossHealthBar bossHealthBar = Instantiate(bossHealthBarPrefab, bossHealthBarContainer);
        boss.bossHealthBar = bossHealthBar;
    }

    private void DestroyBossHealthBar(SBoss boss)
    {
        Destroy(boss.bossHealthBar.gameObject);
    }


}
