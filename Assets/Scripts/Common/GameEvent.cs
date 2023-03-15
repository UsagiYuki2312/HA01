using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent
{
    public UnityAction<Vector3, AlienProperties> OnAlienDie;
    public UnityAction<SBoss> OnBossDie;
    public UnityAction OnCameraZoomOut;
    public UnityAction<Vector3, float> OnMonsterTakeDamage;
    public UnityAction OnBossSpawned;
    public UnityAction<Transform> OnGroundPositionChange;
    public UnityAction OnTrialBattleClicked;
    public UnityAction OnResumeBattleClicked;
    public UnityAction OnSkillClickedDown;
    public UnityAction OnSkillClickedUp;
    public UnityAction OnSpawnFirstSkill;
    public UnityAction OnPlayerUseSkill;

    public UnityAction<SBoss> OnBossDefeated;

}
