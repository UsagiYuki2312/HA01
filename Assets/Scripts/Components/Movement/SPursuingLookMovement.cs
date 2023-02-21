using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPursuingLookMovement : SPursuingMovement
{
    protected override void Update()
    {
        if (!isMovable) return;
        
        transform.LookAt(target, Vector3.up);
        transform.Translate(transform.forward * characterProperties.speed * Time.deltaTime, Space.World);
    }
}
