using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GamePlayState : State, IMessageHandle
{
    private SGamePlayUI gamePlayUI;
    public SPlayer player;
    public GameObject wall;
    public GameObject boss;
    private AlienController alienController;
    public const string UI_PATH = "Prefabs/UI/";
    public const string PLAYER_PATH = "Prefabs/Player/";
    public const string WALL_PATH = "Prefabs/Obstacle/";
    public const string BOSS_PATH = "Prefabs/Boss/";
    private GameStateData gameStateData;

    void IMessageHandle.Handle(Message message)
    {
        switch (message.type)
        {
            case TeeMessageType.OnPlayerDie:
                this.SetTimeScale(1);
                player.EnableBehaviours(false);
                alienController.StopSpawning();
                ChangeState("GameLoseState");
                break;
            case TeeMessageType.OnPauseButtonClicked:
                this.SetTimeScale(0);
                //SPauseMenu menu = Instantiate(pauseMenu);
                //menu.Display();
                break;
            case TeeMessageType.OnPauseMenuDestroyed:
                this.SetTimeScale(1);
                break;
        }
    }

    private void Awake()
    {
        Debug.Log("Awake Run");
        gamePlayUI = Resources.Load<SGamePlayUI>(UI_PATH + "GamePlay");
        player = Resources.Load<SPlayer>(PLAYER_PATH + "Player");
        wall = Resources.Load<GameObject>(WALL_PATH + "Wall");
        boss = Resources.Load<GameObject>(BOSS_PATH + "Boss");

        MessageManager.AddSubcriber(TeeMessageType.OnPauseButtonClicked, this);
        MessageManager.AddSubcriber(TeeMessageType.OnPauseMenuDestroyed, this);
        MessageManager.AddSubcriber(TeeMessageType.OnPlayerDie, this);
    }

    void Start()
    {
        StartCoroutine(RunLoading());
    }

    IEnumerator RunLoading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level1");
        yield return new WaitForSeconds(1f);
        gamePlayUI = Instantiate(gamePlayUI);
        player = Instantiate(player);
        player.SetupDependencies(DataController.GameStateData);
        SGameInstance.Instance.player = player;
        wall = Instantiate(wall);
        boss = Instantiate(boss);
        SGameInstance.Instance.cinemachineCamera.Follow = SGameInstance.Instance.player.transform;

        alienController = new AlienController(player.transform, 1);
        alienController.Init();
        alienController.ResolveGameStateData();
        alienController.StartSpawning();

        GameInstance.gameEvent.OnBossDie += OnBossDie;
        GameInstance.gameEvent.OnAlienDie += OnAlienDie;

        gameStateData = DataController.GameStateData;
    }

    private void OnAlienDie(Vector3 position, AlienProperties alienProperties)
    {
        gameStateData.totalDefeatedAliens++;
    }

    private void OnBossDie(SBoss bossObject)
    {
        Next();
    }

    private void OnDestroy()
    {
        MessageManager.RemoveSubcriber(TeeMessageType.OnPauseButtonClicked, this);
        MessageManager.RemoveSubcriber(TeeMessageType.OnPauseMenuDestroyed, this);
        MessageManager.RemoveSubcriber(TeeMessageType.OnPlayerDie, this);
    }
}
