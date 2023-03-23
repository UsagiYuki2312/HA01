using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBossController
{
    public List<ActiveSkill> skills;
    public void Init()
    {
        skills = new List<ActiveSkill>();
        skills.Add(new FireBallSkill());
        skills.Add(new HacCauSkill());
        skills.Add(new HoaDonSkill());
        skills.Add(new AmaterasuSkill());
        skills[0].SpawnSkillObjects();
        skills[0].UpdateCoolDown();
        skills[1].SpawnSkillObjects();
        skills[1].UpdateCoolDown();
        skills[2].SpawnSkillObjects();
        skills[2].UpdateCoolDown();
        skills[3].SpawnSkillObjects();
        skills[3].UpdateCoolDown();
    }
    public void UseMeleeSkilll(Vector3 position, Quaternion rotation)
    {
        skills[0].UseSkill(position, rotation);
    }
    public void UseRangeSkill(Vector3 position, Quaternion rotation)
    {
        skills[0].UseSkill(position, rotation);
    }
    public float GetFirstSkillCoolDown()
    {
        return skills[0].baseCooldown;
    }
    public float GetSecondSkillCoolDown()
    {
        return skills[1].baseCooldown;
    }

    public void UseHacCauItachiSkill(Vector3 position, Quaternion rotation)
    {
        skills[1].UseSkill(position, rotation);
    }
    public void UseHoaDonItachiSkill(Vector3 position, Quaternion rotation)
    {
        skills[2].UseSkill(position, rotation);
    }
    public void UseAmaretasuItachiSkill(Vector3 position, Quaternion rotation)
    {
        skills[3].UseSkill(position, rotation);
    }

    public void UpdatePowerSkill(int type, int damage)
    {
        skills[type].UpdatePower(damage);
    }
}
