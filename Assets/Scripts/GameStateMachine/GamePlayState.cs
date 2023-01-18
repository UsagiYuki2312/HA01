using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : MonoBehaviourCore
{
    private SGamePlayUI gamePlayUI;
    public SPlayer player;
    public const string UI_PATH = "Prefabs/UI/";
    private GameStateData gameStateData;
    private void Awake()
    {
        gamePlayUI = Resources.Load<SGamePlayUI>(UI_PATH + "GamePlay");
    }

    void Start()
    {
        gamePlayUI = Instantiate(gamePlayUI);

        player.movementComponent.CreateJoystick(gamePlayUI.joystickZone);
        player.skillComponent.CreatePanel(gamePlayUI.skillZone);


    }
}
