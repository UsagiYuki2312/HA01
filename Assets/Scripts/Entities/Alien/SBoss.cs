using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SBoss : SAlien
{
    private Action OnMovementDelayEnd;
    protected SFollower follower;
    public bool isLastBoss;

    protected virtual void Awake()
    {
        follower = Resources.Load<SFollower>("Follower");
    }

    protected override void Start()
    {
        base.Start();

        follower = Instantiate(follower, transform.position, Quaternion.identity);
        follower.target = transform;

    }
}
