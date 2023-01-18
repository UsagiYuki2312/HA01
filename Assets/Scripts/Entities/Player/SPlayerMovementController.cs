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
    [HideInInspector] public PlayerProperties playerProperties;
    private float countValue;
    private float rotateAngle;
    private bool isRunning;
    public bool isClickGoTo;
    public Vector3 targetGoTo;
    [SerializeField] private Rigidbody rigidbody;
    public const string JOYSTICK_PATH = "Prefabs/Joystick/";
    private void Start()
    {
        floatingJoystick = Resources.Load<FixedJoystick>(JOYSTICK_PATH + "Fixed Joystick");
        animator = GetComponentInChildren<Animator>();
    }

    public void CreateJoystick(RectTransform joystickZone)
    {
        floatingJoystick = Instantiate(floatingJoystick, joystickZone);
    }

    // Update is called once per frame
    void Update()
    {
        joyStickDir = floatingJoystick.Direction;
        isRunning = MoveToDir(joyStickDir);
        if (isClickGoTo)
        {
            GoToTarget(targetGoTo);
        }
        if (joyStickDir != Vector2.zero)
        {
            isClickGoTo = false;
        }
    }

    private bool MoveToDir(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.1f) return false;
        transform.Translate(dir * playerProperties.speed * Time.deltaTime, Space.World);
        return true;
    }

    private void CalculateDirection()
    {
        moveDirection = transform.forward;
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

}
