using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill : ActiveSkill
{
    private SNormalAttackObject normalAttackPrefabs;
    private UDynamicPool<SNormalAttackObject> normalAttackPool;

    private SNormalAttackObject choosenObject;
    public ShadowSkill() : base()
    {
        skillType = SkillType.SecondSkill;
        iconSkill = Resources.Load<Texture>("Textures/Skill/" + "27");
        normalAttackPrefabs = Resources.Load<SNormalAttackObject>("Prefabs/Skill/AiriSkill/" + "NormalAttackBullet");
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
        if (position.x > 0)
        {
            choosenObject.transform.position = SGameInstance.Instance.player.transform.position + new Vector3(3, 1, 0);
        }
        if (position.x < 0)
        {
            choosenObject.transform.position = SGameInstance.Instance.player.transform.position + new Vector3(-3, 1, 0); ;
        }
        choosenObject.transform.rotation = rotation;
        choosenObject.gameObject.SetActive(true);
    }

    public override void UseSkill(Vector3 position, Quaternion rotation)
    {
        SGameInstance.Instance.player.GoDash(position);
        SpawnBullet(normalAttackPool, position, rotation);
    }

    public override void UpdateCoolDown()
    {
        base.UpdateCoolDown();
        baseCooldown = 3f;
    }
}
