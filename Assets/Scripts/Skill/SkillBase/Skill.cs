using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public SkillType skillType;
    public float power;
    public bool isHidden;
    public virtual void TriggerHidden()
    {
        isHidden = true;
    }
}
