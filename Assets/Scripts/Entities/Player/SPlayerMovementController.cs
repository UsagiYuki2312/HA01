using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SPlayerMovementController : MonoBehaviour
{
    public FixedJoystick floatingJoystick;
    public Animator animator;
    [SerializeField] private Vector2 joyStickDir;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    [HideInInspector] public PlayerProperties playerProperties;
    private float countValue;
    private float rotateAngle;
    [SerializeField] private bool isRunning;
    public bool isClickGoTo;
    public Vector3 targetGoTo;
    public const string JOYSTICK_PATH = "Prefabs/Joystick/";
    public bool isCollidedWithUpWall, isCollidedWithDownWall;
    public bool isCollidedWithLeftWall, isCollidedWithRightWall;
    public bool isCollidedWithObstacle;

    private void Start()
    {
        floatingJoystick = SGameInstance.Instance.floatingJoystick;
    }

    public void CreateJoystick(RectTransform joystickZone)
    {
        floatingJoystick = Instantiate(floatingJoystick, joystickZone);
    }

    // Update is called once per frame
    void Update()
    {
        joyStickDir = floatingJoystick.Direction;
        CalculateDirection(joyStickDir);
        isRunning = MoveToDir(moveDirection);

        AnimationController();

    }

    private bool MoveToDir(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.1f) return false;
        transform.Translate(dir * playerProperties.speed * Time.deltaTime, Space.World);
        return true;
    }

    public void GoToTarget(Vector3 dir)
    {
        transform.Translate(MiniumVector(dir) * playerProperties.speed * Time.deltaTime, Space.World);
    }

    public Vector3 ConvertVector(Vector3 dir)
    {
        float directionX = 0;
        float directionY = 0;
        if (dir.x == 0 && dir.y != 0)
        {
            directionY = dir.y / Mathf.Abs(dir.y);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.y == 0 && dir.x != 0)
        {
            directionX = dir.x / Mathf.Abs(dir.x);
            return new Vector3(directionX, directionY, 0);
        }
        if (dir.x != 0 && dir.y != 0)
        {
            if (dir.x < 0 && dir.y < 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y > 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            if (dir.x < 0 && dir.y > 0)
            {
                directionY = ((dir.y / dir.x) * -1);
                directionX = -1;
            }
            if (dir.x > 0 && dir.y < 0)
            {
                directionY = (dir.y / dir.x);
                directionX = 1;
            }
            return new Vector3(directionX, directionY, 0);
        }
        return Vector3.zero;
    }
    public Vector3 MiniumVector(Vector3 dir)
    {
        Vector3 vectorEquation = ConvertVector(dir);
        float equation = dir.y / dir.x;
        Vector3 goToDirection = new Vector3(0, 0, 0);
        if (vectorEquation.x >= 1)
        {
            goToDirection = new Vector3(0.5f, 0.5f * equation);
        }
        if (vectorEquation.x <= -1)
        {
            goToDirection = new Vector3(-0.5f, -0.5f * equation);
        }
        if (vectorEquation.y >= 1)
        {
            goToDirection = new Vector3(0.5f / equation, 0.5f);
        }
        if (vectorEquation.y <= -1)
        {
            goToDirection = new Vector3(-0.5f / equation, -0.5f);
        }
        Debug.Log("ConvertVector: " + goToDirection);
        return goToDirection;
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

    private void CalculateDirectionObstacle(Vector3 dir)
    {
        moveDirection = dir;
        moveDirection.z = 0;
        if (isCollidedWithObstacle && Mathf.Abs(moveDirection.x) >= Mathf.Abs(moveDirection.y)) moveDirection.x = 0;
        if (isCollidedWithObstacle && Mathf.Abs(moveDirection.x) < Mathf.Abs(moveDirection.y)) moveDirection.y = 0;
    }

    public enum State
    {
        Idle,
        Run,
        Attack,
    }
    public State state;

    public void SetStateAttack(){
          state = State.Attack;
    }
    private void AnimationController()
    {
        if (isRunning == true)
        {

            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                lastMoveDirection = moveDirection;
            }
            if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                lastMoveDirection = moveDirection;
            }
            if (state != State.Run)
            {
                animator.Play("Run");
                state = State.Run;
            }
        }
        else
        {
            if (state != State.Idle)
            {
                if (moveDirection == Vector3.zero)
                {
                    if (lastMoveDirection.x > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        animator.Play("Idle");
                    }
                    if (lastMoveDirection.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        animator.Play("Idle");
                    }
                }
                state = State.Idle;
            }

        }
    }

}
