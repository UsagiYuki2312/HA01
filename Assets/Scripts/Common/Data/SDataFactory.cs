using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

[System.Serializable]
public enum SkillType
{
    FirstSkill = 1,
    SecondSkill = 2,
    UltimateSkill = 3

}

[System.Serializable]
public class PlayerConfig
{
    public string[] name;
    public float[] baseHealth;
    public float[] baseSpeed;
    public float[] damageToMonster;
}

[System.Serializable]
public class MonsterConfig
{
    public float[] speed;
    public int[] health;
    public float[] damageToPlayer;
}

public class SDataFactory : Singleton<SDataFactory>
{
    private PlayerConfig playerConfig;
    private MonsterConfig monsterConfig;
    void Awake()
    {
        playerConfig =
            JsonUtility
                .FromJson<PlayerConfig>(Resources
                    .Load<TextAsset>("Json/" + "player_config")
                    .text);
        monsterConfig = JsonUtility
                .FromJson<MonsterConfig>(Resources
                    .Load<TextAsset>("Json/" + "monster_config")
                    .text);
    }

    #region Player
    // public int GetNextLeverExp(int currentLevel)
    // {
    //     currentLevel = Mathf.Clamp(currentLevel, 0, playerConfig.levelExperience.Length - 1);
    //     return playerConfig.levelExperience[currentLevel];
    // }

    public float GetPlayerBaseHealth()
    {
        return playerConfig.baseHealth[0];
    }

    public float GetPlayerBaseDamage()
    {
        return playerConfig.damageToMonster[0];
    }

    public float GetPlayerBaseSpeed()
    {
        return playerConfig.baseSpeed[0];
    }
    #endregion

    #region Alien
    public int GetAlienHealth(int type)
    {
        int index = Mathf.Clamp(type - 1, 0, monsterConfig.health.Length - 1);
        return monsterConfig.health[index];
    }

    public float GetAlienSpeed(int type)
    {
        int index = Mathf.Clamp(type - 1, 0, monsterConfig.speed.Length - 1);
        return monsterConfig.speed[index];
    }

    public float GetDamageToPlayer(int type)
    {
        int index =
            Mathf.Clamp(type - 1, 0, monsterConfig.damageToPlayer.Length - 1);
        return monsterConfig.damageToPlayer[index];
    }
    #endregion
    public float GetSpeedPlayer(int type)
    {
        int index =
                   Mathf.Clamp(type - 1, 0, monsterConfig.damageToPlayer.Length - 1);
        return playerConfig.baseSpeed[index];
    }
}
