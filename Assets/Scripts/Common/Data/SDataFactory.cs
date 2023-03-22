using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

[System.Serializable]
public enum SkillType
{
    FirstSkill = 1,
    SecondSkill = 2,
    UltimateSkill = 3,
    NormalAttack = 0

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
    public int[] addtionalPowerByMinutes;
}

[System.Serializable]
public class BossConfig
{
    public float bossHealthMultiple;
    public float bossSpeed;
    public float bossCollisionDamageMultiple;
    public float miniBossDamageMultiple;
}

public class SDataFactory : Singleton<SDataFactory>
{
    private PlayerConfig playerConfig;
    private AlienConfig alienConfig;
    private BossConfig bossConfig;
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
        bossConfig = JsonUtility
                .FromJson<BossConfig>(Resources
                    .Load<TextAsset>("Json/" + "boss_config")
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

    public int GetAddtionalPowerByMinute(int minutes)
    {
         int index =
                   Mathf.Clamp(minutes, 0, alienConfig.addtionalPowerByMinutes.Length - 1);
        return alienConfig.addtionalPowerByMinutes[minutes];
    }

    #region Boss
    public float GetBossHealthMultiple()
    {
        return bossConfig.bossHealthMultiple;
    }

    public float GetBossSpeed()
    {
        return bossConfig.bossSpeed;
    }

    public float GetBossCollisionDamageMultiple()
    {
        return bossConfig.bossCollisionDamageMultiple;
    }

    public float GetMiniBossDamageMultiple()
    {
        return bossConfig.miniBossDamageMultiple;
    }
    #endregion
}
