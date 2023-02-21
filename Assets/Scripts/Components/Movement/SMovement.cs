using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMovement : MonoBehaviourCore
{
    [HideInInspector] public CharacterProperties characterProperties;
    public static bool isMovable = true;
    public float defaultSpeed;

    protected virtual void Update()
    {
        if (isMovable)
            transform.Translate(transform.forward * characterProperties.speed * Time.deltaTime, Space.World);
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
}
