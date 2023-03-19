using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : ClassInstanceCore
{
    public Transform target;
    private UDynamicPool<SAlien> alienPool;
    private UDynamicPool<SMeleeAlien> meleeAlienPool;
    public Coroutine spawningRoutine;
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

    public AlienSpawner(Transform target, UDynamicPool<SAlien> alienPool, UDynamicPool<SMeleeAlien> meleeAlienPool, float spawnSpeed)
    {
        this.target = target;
        this.alienPool = alienPool;
        this.meleeAlienPool = meleeAlienPool;
        this.spawnSpeed = spawnSpeed;
        spawnDelay = new WaitForSeconds(spawnSpeed);
        spawnCrowdDelayEachWave = new WaitForSeconds(4);
        spawnCrowdDelay = new WaitForSeconds(0.01f);
        maxNumberOfActiveAliens = 15;
        SetupSpawnPosition();
    }

    public void StartSpawning()
    {
        GameInstance.numberOfActiveAliens = 0;
        spawningRoutine = StartCoroutine(SpawnAlien());
    }

    public void StopSpawning()
    {
        isSpawnning = false;
        StopCoroutine(spawningRoutine);

    }

    public T GetAlienFromPool<T>(ref UDynamicPool<T> alienPool, int type) where T : SAlien
    {
        T alien = alienPool.GetObject();
        Vector3 position = GetAlienSpawnPosition(target.transform.position, alienSpawnRange);
        alien.transform.position = position;
        alien.ChangeType(type);
        return alien;
    }

    public void StartSpawnAliensInCrowd()
    {
        StartCoroutine(SpawnAliensInCrowd());
    }

    public void StartSpawnAliensInCrowdByWaves()
    {
        StartCoroutine(SpawnAliensInCrowdByWaves());
    }

    IEnumerator SpawnAliensInCrowd()
    {
        for (int i = 0; i < 100; i++)
        {
            SAlien alien = GetAlienFromPool(ref alienPool, 1);
            alien.gameObject.SetActive(true);
            yield return spawnCrowdDelay;
        }
    }
    IEnumerator SpawnAliensInCrowdByWaves()
    {
        int numberOfWave = 10;
        for (int i = 0; i < numberOfWave; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                SAlien alien = GetAlienFromPool(ref alienPool, 1);
                alien.gameObject.SetActive(true);
                if (j < 99) yield return spawnCrowdDelay;
            }
            if (i < numberOfWave - 1) yield return spawnCrowdDelayEachWave;
        }
    }

    IEnumerator SpawnAlien() //Now useing this
    {
        isSpawnning = true;
        while (true)
        {
            if (GameInstance.numberOfActiveAliens <= maxNumberOfActiveAliens)
                for (int i = 0; i < 3; i++)
                {
                    int type = i;
                    switch (type)
                    {
                        case 1:
                            SAlien alien = GetAlienFromPool(ref alienPool, 1);
                            alien.gameObject.SetActive(true);
                            break;
                        case 2:
                            SMeleeAlien meleeAlien = GetAlienFromPool(ref meleeAlienPool, 2);
                            meleeAlien.gameObject.SetActive(true);
                            break;
                        case 3:
                            Debug.Log("Spawn Range Enymy");
                            break;
                        default:
                            break;
                    }


                }
            yield return spawnDelay;
        }
    }

    public void SpawnAlienType()
    {
        // int type = availableTypes.Random(1,4);
        SAlien alien = GetAlienFromPool(ref alienPool, 1);
        alien.gameObject.SetActive(true);

        //SMeleeAlien meleeAlien = GetAlienFromPool(ref meleeAlienPool, 1);
        //meleeAlien.gameObject.SetActive(true);
    }


    private Vector3[] directions;
    public virtual void SetupSpawnPosition()
    {
        directions = new Vector3[720];
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = Quaternion.Euler(0, 0, i * 0.5f) * Vector3.left;
        }
    }
    public virtual Vector3 GetAlienSpawnPosition(Vector3 orition, float range)
    {
        return orition + directions[UnityEngine.Random.Range(0, directions.Length)] * range;
    }
}

