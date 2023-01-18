using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAlienSensor : MonoBehaviourCore
{
    public SAlien closestAliens;
    public Collider[] inRangeAliens;
    public float closestDistance;
    private float distanceToAlien;
    public int amount;

    private void Awake()
    {
        inRangeAliens = new Collider[20];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        closestDistance = Mathf.Infinity;
        FindAliensInRange();
        PickNearestAlien();
    }

    private void FindAliensInRange()
    {
        amount = Physics.OverlapSphereNonAlloc(transform.position, 9, inRangeAliens, 1 << 6);
    }

    public void PickNearestAlien()
    {
        for (int i = 0; i < amount; i++)
        {
            distanceToAlien = (transform.position - inRangeAliens[i].transform.position).sqrMagnitude;
            if (distanceToAlien < closestDistance)
            {
                closestDistance = distanceToAlien;
                closestAliens = GameInstance.GetAlienReference(inRangeAliens[i].transform.GetInstanceID());
            }
        }
    }

    public SAlien PickRandomAlien()
    {
        int index = Random.Range(0, inRangeAliens.Length);
        if (inRangeAliens[index] == null) return null;
        return GameInstance.GetAlienReference(inRangeAliens[index].transform.GetInstanceID());
    }

    public Transform PickRandomAlienTransform()
    {
        int index = Random.Range(0, inRangeAliens.Length);
        if (inRangeAliens[index] == null) return null;
        return inRangeAliens[index].transform;
    }
}
