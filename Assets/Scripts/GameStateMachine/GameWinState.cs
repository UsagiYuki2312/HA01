using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GameWinState : State
{
    private SGameWinUI gameWinUI;
    protected virtual void Start()
    {
        gameWinUI = Resources.Load<SGameWinUI>("Prefabs/UI/"  + "GameWin");

        gameWinUI = Instantiate(gameWinUI);
        gameWinUI.OnReturnClick = GetRewardAndLoadScene;

    }

    public void GetRewardAndLoadScene()
    {
        SceneManager.LoadScene("Main");
    }

}
