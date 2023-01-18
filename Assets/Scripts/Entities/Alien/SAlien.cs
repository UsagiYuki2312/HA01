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

        health = health * addtionalPower * addtionalPowerByMinutes;
    }
}

//[RequireComponent(typeof(SMovement))]
public class SAlien : MonoBehaviourCore
{
    public AlienProperties alienProperties;
    // public SDpsAttacker characterDps;
    // public SMovement movement;
    // public SSpritePingPong spritePingPong;
    // public DamageReceiver damageReceiver;
    public Animator animator;
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
        // movement = GetComponent<SMovement>();
        // animator = GetComponentInChildren<Animator>();
        // characterDps = GetComponentInChildren<SDpsAttacker>();
        // dpsReceivers = GetComponentsInChildren<SBaseDpsReceiver>();
        // spritePingPong = GetComponentInChildren<SSpritePingPong>();
    }

    protected virtual void Start()
    {
        SettingSAlien();
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

    protected virtual void SetDamageReceiver()
    {
        // damageReceiver = new DamageReceiver(alienProperties);
    }
    protected virtual void OnEnable()
    {

    }
}
