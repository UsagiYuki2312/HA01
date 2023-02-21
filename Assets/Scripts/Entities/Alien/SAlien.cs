using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlienProperties : CharacterProperties
{
    public float exp;
    public int addtionalPower;
    public static int addtionalPowerByMinutes;
    public void LoadPropertiesOfType(int type)
    {
        speed = DataFactory.GetAlienSpeed(type);
        damage = DataFactory.GetDamageToPlayer(type);
        health = DataFactory.GetAlienHealth(type);
    }
}

//[RequireComponent(typeof(SMovement))]
public class SAlien : MonoBehaviourCore
{
    public AlienProperties alienProperties;
    public SDpsAttacker characterDps;
    public SMovement movement;
    public SBaseDpsReceiver[] dpsReceivers;
    public Vector3 dirMove;
    // public SBaseDpsReceiver[] dpsReceivers;
    // private static FireBallAlienSkill fireBallAlienSkill;
    // private static FireBallAlienSkill FireBallAlienSkill
    // {
    //     get
    //     {
    //         if (fireBallAlienSkill == null) fireBallAlienSkill = SGameInstance.Instance.fireBallAlienSkill;
    //         return fireBallAlienSkill;
    //     }
    // }

    protected virtual void Reset()
    {
        movement = GetComponent<SMovement>();
        characterDps = GetComponentInChildren<SDpsAttacker>();
        dpsReceivers = GetComponentsInChildren<SBaseDpsReceiver>();
    }

    protected virtual void Start()
    {
        //SettingSAlien();
    }

    protected void SettingSAlien()
    {
        SetDamageReceiver();
        // damageReceiver.OnCharacterDie = OnAlienDie;
        // damageReceiver.OnCharacterTakeDamage = OnAlienTakeDamage;
        // movement.characterProperties = alienProperties;
        // characterDps.characterProperties = alienProperties;

        // for (int i = 0; i < dpsReceivers.Length; i++)
        //     dpsReceivers[i].damageReceiver = damageReceiver;
    }

    public virtual void ChangeType(int type)
    {
        alienProperties.LoadPropertiesOfType(type);
        movement.defaultSpeed = alienProperties.speed;
    }

    protected virtual void SetDamageReceiver()
    {
        // damageReceiver = new DamageReceiver(alienProperties);
    }
    protected virtual void OnEnable()
    {

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
        protected void Update()
    {
        transform.Translate(dirMove * 0.005f);
    }
}
