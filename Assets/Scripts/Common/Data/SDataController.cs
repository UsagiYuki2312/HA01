using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pixelplacement;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.PlayerLoop.Initialization))]
public class SDataController : Singleton<SDataController>
{
    private string dataPath = "";

    public GameData gameData;

    public PlayerData PlayerData => gameData.playerData;

    protected override void OnRegistration()
    {
        dataPath =
            System.IO.Path.Combine(Application.persistentDataPath, "data.dat");

#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
    }

    public void LoadData()
    {
        if (File.Exists(dataPath))
        {
            Debug.Log("file exist");
            try
            {
                string data = File.ReadAllText(dataPath);
                gameData = JsonUtility.FromJson<GameData>(data);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);

            }
        }
    }

    public void SaveData()
    {
        string origin = JsonUtility.ToJson(gameData);
        File.WriteAllText(dataPath, origin);
    }

    private void OnEnable()
    {
        LoadData();
    }
}

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public GameStateData gameStateData;

    public GameData()
    {
        this.playerData = new PlayerData();
    }
    public void ResetGameStateData()
    {
        gameStateData = new GameStateData();
    }
}
[System.Serializable]
public class GameStateData
{
    public float playerHealth;
    public int totalDefeatedAliens;
    public int currentTotalSeconds;
    public int collectingCoins;
    public List<int> savedEvents;

    public GameStateData()
    {
        playerHealth = 0;
        collectingCoins = 0;
        savedEvents = new List<int>(20);
    }
    public void SaveEvent(int eventID)
    {
        savedEvents.Add(eventID);
    }
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public float speed;
    public float health;
    public PlayerData()
    {
        name = "Player";
        speed = 5f;
        health = 110f;
    }
}

