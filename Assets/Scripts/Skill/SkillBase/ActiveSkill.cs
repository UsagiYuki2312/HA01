using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    protected Transform objectContainer;
    public Texture iconSkill { protected set; get; }
    public int number;
    public int baseNumber;
    public float baseCooldown = 2f;
    public bool skillCoroutineTriggered;
    protected object coolDownDelay;
    private object startgateCooldownDelay;
    protected virtual object CoolDownDelay
    {
        get
        {
            return coolDownDelay;
        }
    }
    protected object shootingDelay = new WaitForSeconds(0.2f);
    protected virtual object ShootingDelay => shootingDelay;
    protected Vector3 projectileOffset = new Vector3(0, 0.5f, 0);

    public ActiveSkill()
    {

    }

    public ActiveSkill(Transform objectContainer)
    {
        this.objectContainer = objectContainer;
    }

    public virtual void UpdateCoolDown()
    {
        coolDownDelay = new WaitForSeconds(1f);
        startgateCooldownDelay = new WaitForSeconds(1f);
        baseCooldown = 2f;
    }

    public virtual void SpawnSkillObjects()
    {

    }

    public virtual void DestroySkillObject()
    {

    }

    public virtual void HideSkillObjects()
    {

    }
    public virtual void UseSkill(Vector3 position, Quaternion rotation)
    {
    }

    public override void TriggerHidden()
    {
        base.TriggerHidden();
        HideSkillObjects();

    }
}
