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
        health += DataController.gameData.playerData.health;
        damage += damage;
        baseDamage = damage;
        armor += 1;
        maxHealth = DataController.gameData.playerData.health;
        speed = DataFactory.GetSpeedPlayer(1);
    }

    public void UpdateAndCalculateTalentData()
    {
        CalculateProperties();
    }
}
