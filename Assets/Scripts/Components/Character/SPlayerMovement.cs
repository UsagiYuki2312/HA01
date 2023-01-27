using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerMovement : MonoBehaviourCore
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody rigid;
    private Vector3 moveDirection;
    public bool isCollidedWithUpWall, isCollidedWithDownWall;
    public bool isCollidedWithLeftWall, isCollidedWithRightWall;

    void FixedUpdate()
    {
        Vector3 directionJoystick = joystick.Direction;
        CalculateDirection(directionJoystick);
        rigid.velocity = moveDirection *5;
    }
    private void CalculateDirection(Vector3 dir)
    {
        moveDirection = dir;
        moveDirection.z = 0;
        if (isCollidedWithUpWall && moveDirection.y > 0) moveDirection.y = 0;
        if (isCollidedWithDownWall && moveDirection.y < 0) moveDirection.y = 0;
        if (isCollidedWithLeftWall && moveDirection.x < 0) moveDirection.x = 0;
        if (isCollidedWithRightWall && moveDirection.x > 0) moveDirection.x = 0;
    }
}
