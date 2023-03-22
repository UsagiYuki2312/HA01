using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaterasuSkill : ActiveSkill
{
 private SAmaterasu spinPrefabs;
    private UDynamicPool<SAmaterasu> bulletPool;
    private SAmaterasu choosenBullet;
    public AmaterasuSkill() : base()
    {
        spinPrefabs = Resources.Load<SAmaterasu>("Prefabs/Skill/ItachiSkill/" + "Amaretasu");
        bulletPool = new UDynamicPool<SAmaterasu>(spinPrefabs, new Vector3(0, -10, 0), 6, 10);
    }

    public override void SpawnSkillObjects()
    {
        bulletPool.CreateObjects(6);
    }

    public override void DestroySkillObject()
    {
        foreach (SBullet bullet in bulletPool.GetInActiveObjects())
            GameObject.Destroy(bullet.gameObject);
    }

    private void SpawnBullet<T>(UDynamicPool<T> bulletPool, Vector3 position, Quaternion rotation) where T : SAmaterasu
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
