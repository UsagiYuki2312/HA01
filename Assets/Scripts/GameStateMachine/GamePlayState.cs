using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GamePlayState : State
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
    private void Awake()
    {
        Debug.Log("Awake Run");
        gamePlayUI = Resources.Load<SGamePlayUI>(UI_PATH + "GamePlay");
        player = Resources.Load<SPlayer>(PLAYER_PATH + "Player");
        wall = Resources.Load<GameObject>(WALL_PATH + "Wall");
        boss = Resources.Load<GameObject>(BOSS_PATH + "Boss");
    }

    void Start()
    {
        StartCoroutine(RunLoading());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Run");
            gamePlayUI = Instantiate(gamePlayUI);
            player = Instantiate(player);
            SGameInstance.Instance.player = player;
            wall = Instantiate(wall);
            boss = Instantiate(boss);
        }

    }

    IEnumerator RunLoading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level1");
        yield return new WaitForSeconds(1f);
        gamePlayUI = Instantiate(gamePlayUI);
        player = Instantiate(player);
        SGameInstance.Instance.player = player;
        wall = Instantiate(wall);
        boss = Instantiate(boss);
        SGameInstance.Instance.cinemachineCamera.Follow = SGameInstance.Instance.player.transform;

        alienController = new AlienController(player.transform, 1);
        alienController.Init();
        alienController.StartSpawning();
    }

}
