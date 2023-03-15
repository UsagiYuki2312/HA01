using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEventSpawner : ClassInstanceCore
{
    public Transform target;
    private UDynamicPool<SBoss> bossPool;
    public List<SBoss> listBoss;

    public AlienEventSpawner(UDynamicPool<SBoss> bossPool, Transform target)
    {
        this.target = target;
        this.bossPool = bossPool;
        GameInstance.gameEvent.OnBossDefeated += RemoveBossFromList;
        listBoss = new List<SBoss>();
    }

    private T GetAlienFromPool<T>(ref UDynamicPool<T> alienPool, int type) where T : SAlien
    {
        T alien = alienPool.GetObject();
        SPursuingMovement alienMovement = (SPursuingMovement)alien.movement;
        alienMovement.target = target;
        alien.ChangeType(type);
        //alien.transform.position = ground.GetAlienSpawnPosition(target.transform.position, 10);
        return alien;
    }

    public void SpawnBoss(SBoss boss, int type = 0, bool isLastBoss = false)
    {

        Vector3 position = SGameInstance.Instance.player.transform.position + new Vector3(3, 3, 3);
        boss = GameObject.Instantiate(boss, position, Quaternion.identity);
        //GameInstance.gameEvent.OnBossHealthBarRegistered?.Invoke(boss);
        boss.ChangeType(type);
        GameInstance.AddAlien(boss);
        boss.isLastBoss = isLastBoss;
        boss.gameObject.SetActive(true);
        listBoss.Add(boss);
    }

    private void SpawnBossWithDelay(SBoss boss, int type, float delay, bool isLastBoss = false)
    {
        if (delay == 0) SpawnBoss(boss, type, isLastBoss);
        else
        {
            SpawnBoss(boss, type, isLastBoss);
        }

    }

    public void ControlSpawnBoss(SBoss boss, int type, bool isLastBoss = false, int delay = 0)
    {
        if (type == 2)
        {
            SpawnBossWithDelay(boss, type, delay, isLastBoss);
            SpawnBossWithDelay(boss, type, delay, isLastBoss);
        }
        else
        {
            SpawnBossWithDelay(boss, type, delay, isLastBoss);
        }
    }

    public void RemoveBossFromList(SBoss boss)
    {
        listBoss.Remove(boss);
        if (listBoss.Count == 0)
        {
            GameInstance.gameEvent.OnBossDie?.Invoke(boss);
        }
    }
}
