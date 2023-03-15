using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMovement : MonoBehaviourCore
{
    [HideInInspector] public CharacterProperties characterProperties;
    public static bool isMovable = true;
    public float defaultSpeed;
    public Rigidbody2D rb;

    protected virtual void Update()
    {
        if (isMovable)
        {
            // MoveToPlayer(SGameInstance.Instance.player.transform.position - transform.position);
            rb.velocity = MiniumVector(SGameInstance.Instance.player.transform.position - transform.position) * characterProperties.speed;
        }

    }

    public void OnSlowdownTriggered()
    {
        // defaultSpeed = characterProperties.speed;
        characterProperties.speed *= 0.7f; // reduce 30% speed
    }

    public void OnSpeedUpTriggered()
    {
        // defaultSpeed = characterProperties.speed;
        characterProperties.speed = 20f;
    }

    public void OnSlowdownFinished()
    {
        characterProperties.speed = defaultSpeed;
    }

    public void OnStopMovement()
    {
        characterProperties.speed = 0;
    }

    public void MoveToPlayer(Vector3 dir)
    {

        transform.Translate(MiniumVector(dir) * 5 * Time.deltaTime, Space.World);

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
        //Debug.Log("ConvertVector: " + goToDirection);
        return goToDirection;
    }
}
