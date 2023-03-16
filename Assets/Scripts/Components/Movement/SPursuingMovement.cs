using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPursuingMovement : SMovement
{
    public Transform target;
    private Vector3 direction;

    protected override void Update()
    {
        if (!isMovable || target == null) return;
        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        rb.velocity = direction * characterProperties.speed;
    }
}
