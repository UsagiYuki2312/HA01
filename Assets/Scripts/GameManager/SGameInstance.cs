using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;
using Cinemachine;

public class SGameInstance : Singleton<SGameInstance>
{
    public SPlayer player;
    private Dictionary<int, SAlien> alienDictionary;
    public NeighbourPositions neighbourPositions;
    public GameEvent gameEvent;
    public Camera mainCam;
    public bool isSavingAvailable = true;
    public SAlien[] aliens = new SAlien[7];
    public ButtonAttackController buttonAttackController;
    public FixedJoystick floatingJoystick;
    public SSkillJoytickPanel skillJoytickPanel;
    public CinemachineVirtualCamera cinemachineCamera;
    private static SGameInstance instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        MonoUtility.SetTimeScale(1, this);
        alienDictionary = new Dictionary<int, SAlien>();
        gameEvent = new GameEvent();
        neighbourPositions = new NeighbourPositions(Camera.main);
        //AddAlien();
        buttonAttackController = new ButtonAttackController();
    }

    public void AddAlien()
    {
        //  if (!alienDictionary.ContainsKey(alien.transform.GetInstanceID()))
        // alienDictionary.Add(alien.transform.GetInstanceID(), alien);
        for (int i = 0; i < aliens.Length; i++)
        {
            alienDictionary.Add(aliens[i].transform.GetInstanceID(), aliens[i]);
        }
    }

    public SAlien GetAlienReference(int transformInstanceID)
    {
        if (alienDictionary.ContainsKey(transformInstanceID))
            return alienDictionary[transformInstanceID];

        return null;
    }

}
[System.Serializable]
public class Cheat
{
    public bool enableSkillCheat;
    public bool dailyRewardCheat;
    public int loginDays;
}



