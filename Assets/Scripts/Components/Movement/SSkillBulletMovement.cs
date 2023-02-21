using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSkillBulletMovement : MonoBehaviourCore
{
    private PlayerProperties playerProperties;
    public float speed;
    private float baseSpeed;

    protected virtual void Awake()
    {
        ResolveDependencies();
        baseSpeed = speed = 20;
    }

    protected void ResolveDependencies()
    {
        playerProperties = GameInstance.player.playerProperties;
    }

    protected virtual void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    protected virtual void OnEnable()
    {
        //  speed = baseSpeed * (1 + (playerProperties.bulletSpeed + playerProperties.projectileSpeedPercentage) * 0.01f);
        speed = baseSpeed * 2;
        Destroy(gameObject,10);
    }
}
