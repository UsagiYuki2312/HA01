using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlienController : ClassInstanceCore
{
    public Transform target;
    public SAlien alienPrefab;
    public SMeleeAlien meleeAlienPrefab;
    public SRangeAlien rangeAlienPrefab;
    public SMiniBoss miniBossPrefab;
    public SAlien linearMoveAlienPrefab;
    public SBoss bossPrefab;
    private UDynamicPool<SAlien> alienPool;
    private UDynamicPool<SMeleeAlien> meleeAlienPool;
    private UDynamicPool<SRangeAlien> rangeAlienPool;
    private UDynamicPool<SBoss> bossPool;
    private Action<SAlien> OnEveryAlienCreated;
    private AlienSpawner alienSpawner;
    private AlienEventSpawner alienEventSpawner;
    //private SceneType sceneType;
    private int[] eventID;
    private int chapter;

    public AlienController(Transform target)
    {
        alienPrefab = Resources.Load<SAlien>("Prefabs/Alien/" + "Alien");
        //miniBossPrefab = Resources.Load<SMiniBoss>("MiniBoss");
        meleeAlienPrefab = Resources.Load<SMeleeAlien>("Prefabs/Alien/" + "MeleeAlien");
        rangeAlienPrefab = Resources.Load<SRangeAlien>("Prefabs/Alien/" + "RangeAlien");
        bossPrefab = Resources.Load<SBoss>("Prefabs/Boss/" + "BossItachi");
        //linearMoveAlienPrefab = Resources.Load<SAlien>("LinearMoveAlien");
        this.target = target;

    }

    public void Init()
    {
        OnEveryAlienCreated = GameInstance.AddAlien;
        alienPool = new UDynamicPool<SAlien>(alienPrefab, new Vector3(0, -10, 0), 5, 100, OnEveryAlienCreated);
        alienPool.CreateObjects(50);
        meleeAlienPool = new UDynamicPool<SMeleeAlien>(meleeAlienPrefab, new Vector3(0, -10, 0), 5, 100, OnEveryAlienCreated);
        meleeAlienPool.CreateObjects(50);
        rangeAlienPool = new UDynamicPool<SRangeAlien>(rangeAlienPrefab, new Vector3(0, -10, 0), 5, 100, OnEveryAlienCreated);
        rangeAlienPool.CreateObjects(50);
        bossPool = new UDynamicPool<SBoss>(bossPrefab, new Vector3(0, -10, 0), 2, 4, OnEveryAlienCreated);
        bossPool.CreateObjects(4);

        alienSpawner = new AlienSpawner(target, alienPool, meleeAlienPool, rangeAlienPool, 1);
        alienEventSpawner = new AlienEventSpawner(bossPool, target);
    }

    public void ResolveGameStateData()
    {
        List<int> savedEvents = DataController.GameStateData.savedEvents;
        if (savedEvents.Count == 0) return;

        //for (int i = 0; i < savedEvents.Count; i++) TriggerEvent(savedEvents[i], false);
    }

    public void StartSpawning()
    {
        alienSpawner.StartSpawning();
    }

    public void StopSpawning()
    {
        alienSpawner.StopSpawning();
    }

    public void CheckEvent(int timelineIndex)
    {
        TriggerEvent(timelineIndex);
    }

    public void ChangeNumberOfAlienPerSpawn(int timelineIndex)
    {
        int number = timelineIndex;
        alienSpawner.numberOfAliensSpawned = number;
    }

    public void TriggerEvent(int eventID, bool saveEvent = true)
    {
        switch (eventID)
        {
            case 0:
                alienSpawner.StartSpawnAliensInCrowd();
                //alienEventSpawner.ControlSpawnBoss(bossPrefab, 0);
                break;
            case 1:
                alienSpawner.StartSpawnAliensInCrowd();
                alienSpawner.StartSpawnMeleeAliensInCrowd();
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 0);
                break;
            case 2:
                alienSpawner.StartSpawnAliensInCrowd();
                alienSpawner.StartSpawnRangeAliensInCrowd();
                break;
            case 3:
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 0, true);
                break;
            case 4:
                alienSpawner.StartSpawnRangeAliensInCrowd();
                alienSpawner.StartSpawnMeleeAliensInCrowd();
                break;
            case 5:
                alienSpawner.StartSpawnAliensInCrowd();
                alienSpawner.StartSpawnRangeAliensInCrowd();
                alienSpawner.StartSpawnMeleeAliensInCrowd();
                break;
            case 6:
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 0, true);
                //alienSpawner.StopSpawning();
                GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            case 9:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 10:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 11:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 12:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 13:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 14:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 15:
                // alienEventSpawner.ControlSpawnBoss(bossPrefab, 0, true, 1);
                // alienSpawner.StopSpawning();
                // GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            default:
                break;
        }
    }
}
