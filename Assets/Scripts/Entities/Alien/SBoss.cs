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
    public Vector3 startingPosition;
    private GameStateData gameStateData;
    private GameStateData GameStateData
    {
        get
        {
            if (gameStateData == null) gameStateData = DataController.GameStateData;
            return gameStateData;
        }
    }

    protected override void Start()
    {
        base.Start();

        fireBallSkill = new FireBallSkill();
        fireBallSkill.SpawnSkillObjects();
        StartCoroutine(SkillCoroutine());

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

        while (alienProperties.health > 100)
        {
            anim.Play("Idle");
            movement.enabled = false;
            alienSkillController.UseSkillBoss();
            yield return new WaitForSeconds(3f);
            anim.Play("Run");
            movement.enabled = true;
            yield return new WaitForSeconds(10f);
        }
    }

    #region Calcu dir move
    // private Vector3 GetRoamingPosition()
    // {
    //     return startingPosition + GetRandomDir();
    // }

    // private Vector3 GetRandomDir()
    // {
    //     return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    // }

    // private void BossMovement()
    // {
    //     StartCoroutine(MoveInWave());
    // }
    // private IEnumerator MoveInWave()
    // {
    //     while (true)
    //     {
    //         //dirMove = GetRandomDir();
    //         dirMove = SGameInstance.Instance.player.transform.position - transform.position;
    //         yield return new WaitForSeconds(1f);
    //         dirMove = Vector3.zero;
    //         yield return new WaitForSeconds(2f);
    //     }
    // }

    // public void GoToTarget(Vector3 dir)
    // {
    //     transform.Translate(MiniumVector(dir) * 5 * Time.deltaTime, Space.World);
    // }

    // public Vector3 ConvertVector(Vector3 dir)
    // {
    //     float directionX = 0;
    //     float directionY = 0;
    //     if (dir.x == 0 && dir.y != 0)
    //     {
    //         directionY = dir.y / Mathf.Abs(dir.y);
    //         return new Vector3(directionX, directionY, 0);
    //     }
    //     if (dir.y == 0 && dir.x != 0)
    //     {
    //         directionX = dir.x / Mathf.Abs(dir.x);
    //         return new Vector3(directionX, directionY, 0);
    //     }
    //     if (dir.x != 0 && dir.y != 0)
    //     {
    //         if (dir.x < 0 && dir.y < 0)
    //         {
    //             directionY = ((dir.y / dir.x) * -1);
    //             directionX = -1;
    //         }
    //         if (dir.x > 0 && dir.y > 0)
    //         {
    //             directionY = (dir.y / dir.x);
    //             directionX = 1;
    //         }
    //         if (dir.x < 0 && dir.y > 0)
    //         {
    //             directionY = ((dir.y / dir.x) * -1);
    //             directionX = -1;
    //         }
    //         if (dir.x > 0 && dir.y < 0)
    //         {
    //             directionY = (dir.y / dir.x);
    //             directionX = 1;
    //         }
    //         return new Vector3(directionX, directionY, 0);
    //     }
    //     return Vector3.zero;
    // }
    // public Vector3 MiniumVector(Vector3 dir)
    // {
    //     Vector3 vectorEquation = ConvertVector(dir);
    //     float equation = dir.y / dir.x;
    //     Vector3 goToDirection = new Vector3(0, 0, 0);
    //     if (vectorEquation.x >= 1)
    //     {
    //         goToDirection = new Vector3(0.5f, 0.5f * equation);
    //     }
    //     if (vectorEquation.x <= -1)
    //     {
    //         goToDirection = new Vector3(-0.5f, -0.5f * equation);
    //     }
    //     if (vectorEquation.y >= 1)
    //     {
    //         goToDirection = new Vector3(0.5f / equation, 0.5f);
    //     }
    //     if (vectorEquation.y <= -1)
    //     {
    //         goToDirection = new Vector3(-0.5f / equation, -0.5f);
    //     }
    //     Debug.Log("ConvertVector: " + goToDirection);
    //     return goToDirection;
    // }
    #endregion

    protected override void SetDamageReceiver()
    {
        damageReceiver = new BossDamageReceiver(alienProperties);
    }

    private void StopMovement()
    {
        movement.enabled = false;
        DelayCallAction(OnMovementDelayEnd, new WaitForSeconds(1));
    }

    private void EnableMovement()
    {
        movement.enabled = true;
    }

    protected override void OnAlienTakeDamage(float damage)
    {
        base.OnAlienTakeDamage(damage);
        //bossHealthBar.SetValue(alienProperties.health);
    }

    protected override void OnAlienDie()
    {
        base.OnAlienDie();
        GameInstance.gameEvent.OnBossDefeated?.Invoke(this);
    }

    public override void ChangeType(int type)
    {
        alienProperties.speed = DataFactory.GetBossSpeed();
        alienProperties.health = DataFactory.GetBossHealthMultiple();

        alienProperties.damage = DataFactory.GetPlayerBaseHealth()
                * DataFactory.GetBossCollisionDamageMultiple() *
                        0.01f;

        movement.defaultSpeed = alienProperties.speed;
    }
    protected override void SettingSAlien()
    {
        base.SettingSAlien();
        movement.typeMove = 0;
    }
}
