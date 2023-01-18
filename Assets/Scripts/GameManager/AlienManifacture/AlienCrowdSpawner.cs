using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlienCrowdSpawner : ClassInstanceCore
{
    private Transform target;
    private NeighbourPositions neighbourPositions;
    public int[] availableTypes;



    private Vector3 GetCrowdSpawnPosition(float range)
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range));
        return pos;
    }

}
