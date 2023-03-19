using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPauseMenu : SPopupMenu
{

    private void Start()
    {

    }



    public void Resume()
    {
        Hide();
        MessageManager.SendMessageWithDelayRealTime(new Message(TeeMessageType.OnPauseMenuDestroyed), 0.2f);
        Destroy(gameObject, 0.4f);
    }

    public void Exit()
    {

        SceneManager.LoadScene("Main");
    }

    private void OnEnable()
    {
    }
}
