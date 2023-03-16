using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : ClassInstanceCore
{
    public Transform target;
    private UDynamicPool<SAlien> alienPool;
    private GameStateData currentStateData;
    private object spawnDelay;
    private object spawnCrowdDelayEachWave;
    private object spawnCrowdDelay;
    public float spawnSpeed;
    public int[] availableTypes;
    public bool isSpawnning;
    private float alienSpawnRange = 13;
    public int numberOfAliensSpawned;
    private int maxNumberOfActiveAliens;

    public AlienSpawner(Transform target, UDynamicPool<SAlien> alienPool, float spawnSpeed)
    {
        this.target = target;
        this.alienPool = alienPool;
        this.spawnSpeed = spawnSpeed;
        spawnDelay = new WaitForSeconds(spawnSpeed);
        spawnCrowdDelayEachWave = new WaitForSeconds(4);
        spawnCrowdDelay = new WaitForSeconds(0.01f);
        maxNumberOfActiveAliens = 20;
    }

    public void StartSpawning()
    {
        SpawnAlien();
    }

    public void StopSpawning()
    {
        isSpawnning = false;

    }

    public T GetAlienFromPool<T>(ref UDynamicPool<T> alienPool, int type) where T : SAlien
    {
        T alien = alienPool.GetObject();
        Vector3 position = Vector3.zero;
        alien.transform.position = position;
        alien.ChangeType(1);
        return alien;
    }

    public void SpawnAlien()
    {
        isSpawnning = true;
        int type = 1;
        for (int i = 0; i <= 5; i++)
        {
            SAlien alien = GetAlienFromPool(ref alienPool, type);
            alien.gameObject.SetActive(true);
            alien.transform.position = new Vector3(Random.Range(-12f,5f),Random.Range(6f,-3f),0);
        }


    }
}

