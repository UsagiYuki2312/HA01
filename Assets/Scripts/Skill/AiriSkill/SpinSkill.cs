using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpinSkill : ActiveSkill
{
    private SBullet spinPrefabs;
    private UDynamicPool<SBullet> bulletPool;
    private SBullet choosenBullet;
    public SpinSkill() : base()
    {
        skillType = SkillType.FirstSkill;
        spinPrefabs = Resources.Load<SBullet>("Prefabs/Skill/AiriSkill/" + "pin");
        bulletPool = new UDynamicPool<SBullet>(spinPrefabs, new Vector3(0, -10, 0), 5, 100);
        iconSkill = Resources.Load<Texture>("Textures/Skill/" + "11");
    }
    public override void SpawnSkillObjects()
    {
        bulletPool.CreateObjects(10);
    }

    public override void DestroySkillObject()
    {
        foreach (SBullet bullet in bulletPool.GetInActiveObjects())
            GameObject.Destroy(bullet.gameObject);
    }

    private void SpawnBullet<T>(UDynamicPool<T> bulletPool, Vector3 position, Quaternion rotation) where T : SBullet
    {
        choosenBullet = bulletPool.GetObject();
        choosenBullet.transform.position = position;
        choosenBullet.transform.rotation = rotation;
        choosenBullet.gameObject.SetActive(true);
    }

    public override void UseSkill(Vector3 position, Quaternion rotation)
    {
        SpawnBullet(bulletPool, position, rotation);
    }
    public override void UpdateCoolDown()
    {
        base.UpdateCoolDown();
    }

}
