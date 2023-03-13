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
    public float[] damage;
}

[System.Serializable]
public class AlienConfig
{
    public float[] speed;
    public int[] health;
    public float[] damageToPlayer;
}

public class SDataFactory : Singleton<SDataFactory>
{
    private PlayerConfig playerConfig;
    private AlienConfig alienConfig;
    void Awake()
    {
        playerConfig =
            JsonUtility
                .FromJson<PlayerConfig>(Resources
                    .Load<TextAsset>("Json/" + "player_config")
                    .text);
        alienConfig = JsonUtility
                .FromJson<AlienConfig>(Resources
                    .Load<TextAsset>("Json/" + "alien_config")
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
        return playerConfig.damage[0];
    }

    public float GetPlayerBaseSpeed()
    {
        return playerConfig.baseSpeed[0];
    }
    #endregion

    #region Alien
    public int GetAlienHealth(int type)
    {
        int index = Mathf.Clamp(type - 1, 0, alienConfig.health.Length - 1);
        return alienConfig.health[index];
    }

    public float GetAlienSpeed(int type)
    {
        int index = Mathf.Clamp(type - 1, 0, alienConfig.speed.Length - 1);
        return alienConfig.speed[index];
    }

    public float GetDamageToPlayer(int type)
    {
        int index =
            Mathf.Clamp(type - 1, 0, alienConfig.damageToPlayer.Length - 1);
        return alienConfig.damageToPlayer[index];
    }
    #endregion
    public float GetSpeedPlayer(int type)
    {
        int index =
                   Mathf.Clamp(type - 1, 0, alienConfig.damageToPlayer.Length - 1);
        return playerConfig.baseSpeed[index];
    }
}
