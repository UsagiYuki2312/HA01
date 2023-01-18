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
    private AlienSpawner alienSpawner;
    private AlienEventSpawner alienEventSpawner;
    private AlienCrowdSpawner alienCrowdSpawner;
    private Action<SAlien> OnEveryAlienCreated;
    public UnityAction OnBossSpawned;
    //private SceneType sceneType;
    private int[] eventID;
    private int chapter;

    public AlienController(Transform target, int chapter)
    {
        alienPrefab = Resources.Load<SAlien>("Alien");
        miniBossPrefab = Resources.Load<SMiniBoss>( "MiniBoss");
        bossPrefab = Resources.Load<SBoss>("Boss");
        linearMoveAlienPrefab = Resources.Load<SAlien>( "LinearMoveAlien");
        this.target = target;
        this.chapter = chapter;
       // sceneType = DataFactory.GetSceneType(chapter);
    }

    public void Init()
    {
        // OnEveryAlienCreated = GameInstance.AddAlien;
        // alienPool = new UDynamicPool<SAlien>(alienPrefab, Constant.hidenPosition, 3, 700, OnEveryAlienCreated);
        // alienPool.CreateObjects(10);
        // miniBossPool = new UDynamicPool<SMiniBoss>(miniBossPrefab, Constant.hidenPosition, 2, 10, OnEveryAlienCreated);
        // miniBossPool.CreateObjects(4);
        // bossPool = new UDynamicPool<SBoss>(bossPrefab, Constant.hidenPosition, 2, 4, OnEveryAlienCreated);
        // bossPool.CreateObjects(4);
        // linearMoveAlienPool = new UDynamicPool<SAlien>(linearMoveAlienPrefab, Constant.hidenPosition, 20, 200, OnEveryAlienCreated);
        // linearMoveAlienPool.CreateObjects(60);
        // alienSpawner = new AlienSpawner(target, alienPool, 1);
        // alienEventSpawner = new AlienEventSpawner(miniBossPool, bossPool, target);
        // alienCrowdSpawner = new AlienCrowdSpawner(linearMoveAlienPool, target, sceneType);
        // alienSpawner.availableTypes = DataFactory.GetAlienTypes(chapter, 0);
    }


    public void StartSpawning()
    {
      //  alienSpawner.StartSpawning();
    }

    public void StopSpawning()
    {
      //  alienSpawner.StopSpawning();
    }
}
