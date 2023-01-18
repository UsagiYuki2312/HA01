using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill : ActiveSkill
{
    public ShadowSkill() : base()
    {
        skillType = SkillType.SecondSkill;
        iconSkill = Resources.Load<Texture>("Textures/Skill/" + "11");
    }
    public override void UseSkill(Vector3 position, Quaternion rotation)
    {
        SGameInstance.Instance.player.GoDash(position);
    }
}
