using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

public class SGameStartUI : SGameUI
{
    public Button playButton;

    private void Awake()
    {
        GameInstance.gameEvent.OnResumeBattleClicked = ResumePlaying;
        GameInstance.gameEvent.OnTrialBattleClicked = StartPlaying;
    }

    private void Start()
    {
        playButton.onClick.AddListener(StartPlaying);
    }
    private void StartPlaying()
    {
        MessageManager.SendMessageWithDelay(new Message(TeeMessageType.OnPlayButtonClicked), 1.51f);
    }
    private void ResumePlaying()
    {
        MessageManager.SendMessageWithDelay(new Message(TeeMessageType.OnResumeButtonClicked), 1.51f);
    }
}
