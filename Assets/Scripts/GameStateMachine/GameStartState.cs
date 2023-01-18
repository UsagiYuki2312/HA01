using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class GameStartState : State, IMessageHandle
{
    private SGameStartUI gameStartUI;
    void IMessageHandle.Handle(Message message)
    {
        switch (message.type)
        {
            case TeeMessageType.OnPlayButtonClicked:
                Destroy(gameStartUI.gameObject);
                SetupScene();
                Next();
                break;
            case TeeMessageType.OnResumeButtonClicked:
                Destroy(gameStartUI.gameObject);
                SetupScene();
                Next();
                break;
        }
    }
    private void Awake()
    {
        gameStartUI = Resources.Load<SGameStartUI>("Prefabs/UI/" + "GameStart");
        MessageManager.AddSubcriber(TeeMessageType.OnPlayButtonClicked, this);
        MessageManager.AddSubcriber(TeeMessageType.OnResumeButtonClicked, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameStartUI = Instantiate(gameStartUI);
    }
    private void SetupScene()
    {
    }
    private void OnDestroy()
    {
        MessageManager.RemoveSubcriber(TeeMessageType.OnPlayButtonClicked, this);
        MessageManager.RemoveSubcriber(TeeMessageType.OnResumeButtonClicked, this);
    }
}
