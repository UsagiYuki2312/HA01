using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GameLoseState : State
{
    private SGameLoseUI gameLoseUI;

    private void Start()
    {
        gameLoseUI = Resources.Load<SGameLoseUI>("Prefabs/UI/" + "GameLose");

        gameLoseUI = Instantiate(gameLoseUI);
        gameLoseUI.OnReturnClick = LoadScene;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
