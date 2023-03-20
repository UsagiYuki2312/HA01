using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GamePlayState : State, IMessageHandle
{
    private SGamePlayUI gamePlayUI;
    private SPauseMenu pauseMenu;
    public SPlayer player;
    public GameObject wall;
    public GameObject boss;
    private AlienController alienController;
    public const string UI_PATH = "Prefabs/UI/";
    public const string PLAYER_PATH = "Prefabs/Player/";
    public const string WALL_PATH = "Prefabs/Obstacle/";
    public const string BOSS_PATH = "Prefabs/Boss/";
    private GameStateData gameStateData;
    private int currentTimelineIndex = -1;
    private TimeCounter timeCounter;

    void IMessageHandle.Handle(Message message)
    {
        switch (message.type)
        {
            case TeeMessageType.OnPlayerDie:
                this.SetTimeScale(0);
                player.EnableBehaviours(false);
                alienController.StopSpawning();
                timeCounter.StopCounting();
                ChangeState("GameLoseState");
                break;
            case TeeMessageType.OnPauseButtonClicked:
                this.SetTimeScale(0);
                SPauseMenu menu = Instantiate(pauseMenu);
                menu.Display();
                break;
            case TeeMessageType.OnPauseMenuDestroyed:
                this.SetTimeScale(1);
                break;
        }
    }

    private void Awake()
    {
        gamePlayUI = Resources.Load<SGamePlayUI>(UI_PATH + "GamePlay");
        pauseMenu = Resources.Load<SPauseMenu>(UI_PATH + "PauseMenu");
        player = Resources.Load<SPlayer>(PLAYER_PATH + "Sasuke");
        // wall = Resources.Load<GameObject>(WALL_PATH + "Wall");
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
        //AsyncOperation operation = SceneManager.LoadSceneAsync("Level1");
        yield return new WaitForSeconds(1f);
        gamePlayUI = Instantiate(gamePlayUI);
        SGameInstance.Instance.player = Instantiate(player);
        yield return new WaitForSeconds(1f);
        SGameInstance.Instance.player.SetupDependencies(DataController.GameStateData, gamePlayUI.playerGause);
        // wall = Instantiate(wall);
        SGameInstance.Instance.cinemachineCamera.Follow = SGameInstance.Instance.player.transform;

        alienController = new AlienController(player.transform);
        alienController.Init();
        alienController.ResolveGameStateData();
        alienController.StartSpawning();
        //alienController.CheckEvent(0); // Event Spawn boss

        GameInstance.gameEvent.OnBossDie += OnBossDie;
        GameInstance.gameEvent.OnAlienDie += OnAlienDie;

        timeCounter = new TimeCounter();
        timeCounter.OnEveryTotalSecondsCount = OnEveryTotalSecondsPassed;
        timeCounter.OnCounterStart = OnCounterStart;
        timeCounter.OnCounterEnd = OnCounterEnd;
        //timeCounter.OnEveryMinutesCount = OnEveryMinutesCount;


        timeCounter.StartCounting(0, 300);
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

    private void OnCounterStart()
    {
        //AlienProperties.addtionalPowerByMinutes = DataFactory.GetAddtionalPowerByMinute(0);
    }

    private void OnCounterEnd()
    {
        alienController.StopSpawning();
    }

    private void OnEveryTotalSecondsPassed(int totalSeconds)
    {
        //int timelineIndex = DataFactory.GetTimelineIndex(gameStateData.currentChapter, totalSeconds);
        int timelineIndex = 3;
        // gameStateData.currentTotalSeconds = totalSeconds;

        //CheckAndSpawnBossWarning(totalSeconds);
        if (timelineIndex != currentTimelineIndex) // change wave
        {
            currentTimelineIndex = timelineIndex;
            //alienController.ChangeZombieType(timelineIndex);
            //alienController.CheckSpawnSpeed(timelineIndex);
            //alienController.CheckEvent(timelineIndex);
            alienController.ChangeNumberOfAlienPerSpawn(timelineIndex);
            //GameInstance.gameEvent.OnWaveChange?.Invoke();
        }
        gamePlayUI.SetTime(totalSeconds);
    }
    private void OnEveryMinutesCount(int minutes)
    {
        //AlienProperties.addtionalPowerByMinutes = DataFactory.GetAddtionalPowerByMinute(minutes);
        //currentMinutes = minutes;
    }
}
