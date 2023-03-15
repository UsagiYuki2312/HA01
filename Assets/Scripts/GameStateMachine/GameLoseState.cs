using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GameLoseState : GameWinState
{
    private SGameLoseUI gameLoseUI;

    protected override void Start()
    {
        gameLoseUI = Resources.Load<SGameLoseUI>("Prefabs/UI/" + "GameLose");
        gameLoseUI = Instantiate(gameLoseUI);
        gameLoseUI.OnReturnClick = LoadScene;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
