using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SBoss : SAlien
{
    private Action OnMovementDelayEnd;
    public bool isLastBoss;
    private FireBallSkill fireBallSkill;
    public Vector3 dirMove;
    public Vector3 startingPosition;

    protected override void Start()
    {
        base.Start();

        fireBallSkill = new FireBallSkill();
        fireBallSkill.SpawnSkillObjects();
        StartCoroutine(SkillCoroutine());

        BossMovement();
    }

    protected void Update()
    {
        transform.Translate(dirMove * 0.005f);
    }

    protected void UseSkill()
    {
        Vector3 dir = SGameInstance.Instance.player.transform.position
                           - transform.position;
        float rotateAngleThirdSkill = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        if (rotateAngleThirdSkill < 0) rotateAngleThirdSkill += 360;
        fireBallSkill.UseSkill(transform.position, Quaternion.Euler(Vector3.forward * -rotateAngleThirdSkill));
    }
    protected IEnumerator SkillCoroutine()
    {
        while (true)
        {
            UseSkill();
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir();
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void BossMovement()
    {
        StartCoroutine(MoveInWave());
    }
    private IEnumerator MoveInWave()
    {
        while (true)
        {
            //dirMove = GetRandomDir();
            dirMove = SGameInstance.Instance.player.transform.position - transform.position;
            yield return new WaitForSeconds(1f);
            dirMove = Vector3.zero;
            yield return new WaitForSeconds(2f);
        }
    }

      public void GoToTarget(Vector3 dir)
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
        Debug.Log("ConvertVector: " + goToDirection);
        return goToDirection;
    }
}
