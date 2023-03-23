using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoaDonSkill : ActiveSkill
{
    private SHoaDon spinPrefabs;
    private UDynamicPool<SHoaDon> bulletPool;
    private SHoaDon choosenBullet;
    public HoaDonSkill() : base()
    {
        spinPrefabs = Resources.Load<SHoaDon>("Prefabs/Skill/ItachiSkill/" + "HoaDon");
        bulletPool = new UDynamicPool<SHoaDon>(spinPrefabs, new Vector3(0, -10, 0), 6, 10);
        iconSkill = Resources.Load<Texture>("Textures/Skill/" + "11");
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

    private void SpawnBullet<T>(UDynamicPool<T> bulletPool, Vector3 position, Quaternion rotation) where T : SHoaDon
    {
        choosenBullet = bulletPool.GetObject();
        choosenBullet.transform.position = position;
        choosenBullet.transform.rotation = rotation;
                choosenBullet.damagePlayer.power = power;
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
      public override void UpdatePower(int damage =0 )
    {
        power = damage;
    }
}
