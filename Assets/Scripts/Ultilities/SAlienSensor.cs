using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAlienSensor : MonoBehaviourCore
{
    public SAlien closestAliens;
    public Collider2D[] inRangeAliens;
    public float closestDistance;
    private float distanceToAlien;
    public int amount;
    public Collider2D alienRang;

    private void Awake()
    {
        inRangeAliens = new Collider2D[20];
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
        //alienRang = Physics2D.OverlapCircle(transform.position, 10, 1 << 10);
        var contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        contactFilter.SetLayerMask(LayerMask.GetMask("Alien"));
        amount = Physics2D.OverlapCircle(transform.position, 10, contactFilter, inRangeAliens);
        foreach (Collider2D nearbyObject in inRangeAliens)
        {
            //Doing stuff here        
        }
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
