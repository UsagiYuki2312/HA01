using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSkill : ActiveSkill
{
    private SNormalAttackObject normalAttackPrefabs;
    private UDynamicPool<SNormalAttackObject> normalAttackPool;
    private SNormalAttackObject choosenObject;


    public LightingSkill() : base()
    {
        skillType = SkillType.UltimateSkill;
        normalAttackPrefabs = Resources.Load<SNormalAttackObject>("Prefabs/Skill/SasukeSkill/" + "UltimateObject");
        normalAttackPool = new UDynamicPool<SNormalAttackObject>(normalAttackPrefabs, new Vector3(0, -10, 0), 5, 100);
    }

    public override void SpawnSkillObjects()
    {
        normalAttackPool.CreateObjects(10);
    }


    public override void DestroySkillObject()
    {
        foreach (SNormalAttackObject obj in normalAttackPool.GetInActiveObjects())
            GameObject.Destroy(obj.gameObject);
    }

    private void SpawnBullet<T>(UDynamicPool<T> bulletPool, Vector3 position, Quaternion rotation) where T : SNormalAttackObject
    {
        choosenObject = normalAttackPool.GetObject();
        choosenObject.transform.position = position;
        choosenObject.transform.rotation = rotation;
        choosenObject.gameObject.SetActive(true);
    }

    public override void UseSkill(Vector3 position, Quaternion rotation)
    {
        SpawnBullet(normalAttackPool, position, rotation);
    }
    public override void UpdateCoolDown()
    {
        base.UpdateCoolDown();
        baseCooldown = 15f;
    }
}
