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
        alien.transform.position = GetAlienSpawnPosition(target.transform.position, 5);
        return alien;
    }

    public void SpawnBoss(SBoss boss, int type = 0, bool isLastBoss = false)
    {

        Vector3 position = SGameInstance.Instance.player.transform.position + new Vector3(3, 3, 3);
        boss = GameObject.Instantiate(boss, position, Quaternion.identity);
        //boss.transform.localScale
            boss.isLastBoss = isLastBoss;
        if (isLastBoss)
        {
            SGameInstance.Instance.gameEvent.OnBossHealthBarRegistered?.Invoke(boss);
        }
        boss.ChangeType(type);
        GameInstance.AddAlien(boss);
    
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
            Debug.Log("Spawn Boss");
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

    public Vector3 GetAlienSpawnPosition(Vector3 orition, float range)
    {
        Vector3[] directions = new Vector3[720];
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = Quaternion.Euler(0, i * 0.5f, 0) * Vector3.forward;
        }
        return orition + directions[UnityEngine.Random.Range(0, directions.Length)] * range;
    }
}
