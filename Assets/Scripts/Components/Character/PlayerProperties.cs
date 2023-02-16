using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProperties : CharacterProperties
{
    public float maxHealth;
    public float Damage
    {
        get { return damage; }
    }
    public float baseDamage;

    private void ResetEquipmentEffectAttributes()
    {
    }

    public void CalculateProperties()
    {
        health += DataFactory.GetPlayerBaseHealth();
        damage = DataFactory.GetPlayerBaseDamage();
        baseDamage = damage;
        armor += 1;
        maxHealth = health;
        speed = DataFactory.GetPlayerBaseSpeed();
    }

    public void UpdateAndCalculateTalentData()
    {
        CalculateProperties();
    }
}
