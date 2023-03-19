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
    public SMiniBoss miniBossPrefab;
    public SAlien linearMoveAlienPrefab;
    public SBoss bossPrefab;
    private UDynamicPool<SAlien> alienPool;
    private UDynamicPool<SMeleeAlien> meleeAlienPool;
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
        meleeAlienPrefab= Resources.Load<SMeleeAlien>("Prefabs/Alien/" + "MeleeAlien");
        bossPrefab = Resources.Load<SBoss>("Prefabs/Boss/" + "Boss");
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
        bossPool = new UDynamicPool<SBoss>(bossPrefab, new Vector3(0, -10, 0), 2, 4, OnEveryAlienCreated);
        bossPool.CreateObjects(4);

        alienSpawner = new AlienSpawner(target, alienPool, meleeAlienPool, 1);
        //alienEventSpawner = new AlienEventSpawner(bossPool, target);
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
        TriggerEvent(1);
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
            case 1:
                alienSpawner.StartSpawnAliensInCrowd();
                break;
            case 8:
                //GameInstance.gameEvent.OnExplosionSpawned?.Invoke();
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 1, false, 1);
                alienSpawner.StopSpawning();
                GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            case 9:
                //GameInstance.gameEvent.OnExplosionSpawned?.Invoke();
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 0, false, 1);
                alienSpawner.StopSpawning();
                GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            case 10:
                //GameInstance.gameEvent.OnExplosionSpawned?.Invoke();
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 0, false, 1);
                alienSpawner.StopSpawning();
                GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            case 13:
                //GameInstance.gameEvent.OnExplosionSpawned?.Invoke();
                alienEventSpawner.ControlSpawnBoss(bossPrefab, 5, true, 1);
                alienSpawner.StopSpawning();
                GameInstance.gameEvent.OnBossSpawned?.Invoke();
                break;
            default:
                break;
        }
    }
}
