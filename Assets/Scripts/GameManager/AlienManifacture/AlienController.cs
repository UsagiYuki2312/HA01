using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlienController : ClassInstanceCore
{
    public Transform target;
    public SAlien alienPrefab;
    public SMiniBoss miniBossPrefab;
    public SAlien linearMoveAlienPrefab;
    public SBoss bossPrefab;
    private UDynamicPool<SAlien> alienPool;
    private UDynamicPool<SBoss> bossPool;
    private Action<SAlien> OnEveryAlienCreated;
    private AlienSpawner alienSpawner;
    //private SceneType sceneType;
    private int[] eventID;
    private int chapter;

    public AlienController(Transform target, int chapter)
    {
        alienPrefab = Resources.Load<SAlien>("Prefabs/Alien/" + "Alien");
        miniBossPrefab = Resources.Load<SMiniBoss>("MiniBoss");
        bossPrefab = Resources.Load<SBoss>("Boss");
        linearMoveAlienPrefab = Resources.Load<SAlien>("LinearMoveAlien");
        this.target = target;
        this.chapter = chapter;
        // sceneType = DataFactory.GetSceneType(chapter);
        alienPool = new UDynamicPool<SAlien>(alienPrefab, new Vector3(0, -10, 0), 5, 100);
    }

    public void Init()
    {
        OnEveryAlienCreated = GameInstance.AddAlien;
        alienPool.CreateObjects(10);
        alienSpawner = new AlienSpawner(target, alienPool, 1);
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
}
