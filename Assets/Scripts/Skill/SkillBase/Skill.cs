using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public SkillType skillType;
    public float power;
    public bool isHidden;
    private SPlayer player;
    protected SPlayer Player
    {
        get
        {
            if (player == null) player = SGameInstance.Instance.player;
            return player;
        }
    }
    public virtual void TriggerHidden()
    {
        isHidden = true;
    }

    public virtual void UpdatePower()
    {
        power = 10;
    }
}
