using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacCauSkill : ActiveSkill
{
    private SFireBall spinPrefabs;
    private UDynamicPool<SFireBall> bulletPool;
    private SFireBall choosenBullet;

    public HacCauSkill() : base()
    {
        spinPrefabs = Resources.Load<SFireBall>("Prefabs/Skill/ItachiSkill/" + "HacCau");
        bulletPool = new UDynamicPool<SFireBall>(spinPrefabs, new Vector3(0, -10, 0), 6, 10);
    }

    public override void SpawnSkillObjects()
    {
        bulletPool.CreateObjects(6);
    }

    private void SpawnBullet<T>(UDynamicPool<T> bulletPool, Vector3 position, Quaternion rotation) where T : SFireBall
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
