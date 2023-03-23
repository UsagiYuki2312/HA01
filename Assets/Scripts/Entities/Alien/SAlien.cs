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

        health = health + addtionalPowerByMinutes;
        damage = damage + addtionalPowerByMinutes;
    }
}

//[RequireComponent(typeof(SMovement))]
public class SAlien : MonoBehaviourCore
{
    public AlienProperties alienProperties;
    public SDpsAttacker characterDps;
    public SMovement movement;
    public DamageReceiver damageReceiver;
    public SAlienSkillController alienSkillController;
    public Vector3 dirMove;
    public Animator anim;

    protected virtual void Reset()
    {
        movement = GetComponent<SMovement>();
        characterDps = GetComponentInChildren<SDpsAttacker>();
        //dpsReceivers = GetComponentsInChildren<SBaseDpsReceiver>();
    }

    protected virtual void Start()
    {
        SettingSAlien();
    }

    protected virtual void SettingSAlien()
    {
        SetDamageReceiver();
        damageReceiver.OnCharacterDie = OnAlienDie;
        damageReceiver.OnCharacterTakeDamage = OnAlienTakeDamage;
        movement.characterProperties = alienProperties;
        movement.typeMove = 1;
        characterDps.characterProperties = alienProperties;
        alienSkillController.properties = alienProperties;

        // for (int i = 0; i < dpsReceivers.Length; i++)
        //     dpsReceivers[i].damageReceiver = damageReceiver;
    }

    public virtual void ChangeType(int type) //Now this game have only one type
    {
        alienProperties.LoadPropertiesOfType(type);
        movement.defaultSpeed = alienProperties.speed;
    }

    protected virtual void SetDamageReceiver()
    {
        damageReceiver = new DamageReceiver(alienProperties);
    }
    protected virtual void OnAlienTakeDamage(float damage)
    {
        // if (spritePingPong != null) spritePingPong.TriggerPingPong(damage);
        // GameInstance.gameEvent.OnAlienTakeDamage?.Invoke(transform.position, damage);
    }

    protected virtual void OnAlienDie()
    {
        // GameInstance.gameEvent.OnAlienDie(transform.position, alienProperties);
        // if (GameInstance.player.playerProperties.alienExplosionAfterDie)
        //     GameInstance.gameEvent.OnAlienExplosionTriggered?.Invoke(transform.position);
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        GameInstance.numberOfActiveAliens++;
    }

    protected virtual void OnDisable()
    {
        GameInstance.numberOfActiveAliens--;
    }

}
