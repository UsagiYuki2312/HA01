using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCrashLookMovement : SPursuingLookMovement
{
    private SBossCrash sBossCrash;
    private Quaternion rotateFace;
    private float rotateSpeed = 12;
     public bool isAimToPlayer;
    protected void Awake()
    {
        sBossCrash = GetComponent<SBossCrash>();
    }
    protected override void Update()
    {
        if (!isMovable) return;

        if (isAimToPlayer)
        {
            rotateFace = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateFace, rotateSpeed * Time.deltaTime);
        }
        transform.Translate(transform.forward * characterProperties.speed * Time.deltaTime, Space.World);
    }
}
