using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject spaceship;
    [SerializeField] private GameObject spaceshipPrefab;

    private UIController uiController;
    public CameraController cameraController{get; private set;}
    public GameState currentGameState{get; private set;} = GameState.Menu;
    public int AvailableStage {get; private set;} = 1;
    public int GameStage { get; set; } = 1;

    public int BackdoorinputforDebug = 0;

    //Audio Sources
    public Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    public Dictionary<string, AudioSource> bgmSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Transform audioParent = transform.Find("Audio Sources");
        foreach(Transform child in audioParent)
        {
            String audioName = child.name.Replace("audio_", "");
            audioSources.Add(audioName, child.GetComponent<AudioSource>());
        }

        Transform bgmParent = transform.Find("Audio Sources_BGM");
        foreach (Transform child in bgmParent)
        {
            String audioName = child.name.Replace("bgm_", "");
            bgmSources.Add(audioName, child.GetComponent<AudioSource>());
        }
    }

    void Start()
    {
        ChangeGameState(GameState.Menu.ToString());
    }
    private void Update()
    {
        //Debug.Log(currentGameState);
        if(Input.GetKeyDown(KeyCode.F1))
        {
            BackdoorinputforDebug++;
            if(BackdoorinputforDebug > 7)
            {
                AvailableStage = 10;
            }
        }
    }
    public void ChangeGameState(string newGameState)
    {
        currentGameState = (GameState)Enum.Parse(typeof(GameState), newGameState);
        switch(currentGameState)
        {
            case GameState.Menu:
                Debug.Log("GameStateChange : Menu");
                MenuStart();
                break;
            case GameState.StageSelect:
                Debug.Log("GameStateChange : StageSelect");
                StageSelectStart();
                break;
            case GameState.Mapping:
                Debug.Log("GameStateChange : Mapping");
                MappingStart();
                break;
            case GameState.Playing:
                Debug.Log("GameStateChange : Playing");
                PlayingStart();
                break;
            case GameState.Paused:
                Debug.Log("GameStateChange : Paused");
                PausedStart();
                break;
            case GameState.GameOver:
                Debug.Log("GameStateChange : GameOver");
                GameOverStart();
                break;
            case GameState.Win:
                Debug.Log("GameStateChange : Win");
                WinStart();
                break;
        }
    }
    
#region 게임상태 이동 관련
    public void MenuStart()
    {
        SceneController.Instance.LoadSceneAsync("MainScreen");
        SetBGM("main", true);
        SetBGM("Ingame", false);
    }
    public void StageSelectStart()
    {
        SceneController.Instance.LoadSceneAsync("StageSelect");
        SetBGM("main", true);
        SetBGM("Ingame", false);
    }
    private void MappingStart()
    {
        if(SceneManager.GetActiveScene().name != "Ingame")
        {
            //Debug.Log("Move to Ingame");
            SceneController.Instance.LoadSceneAsync("Ingame");
            SetBGM("main", false);
            SetBGM("Ingame", true);
        }
        else
        {
            MappingReady(false);
        }
    }
    public void MenuReady()
    {
        UIManager.Instance.NewSceneLoaded();
    }
    public void StageSelectReady()
    {
        UIManager.Instance.NewSceneLoaded();
    }
    public void MappingReady(bool isSceneLoaded)
    {
        if(isSceneLoaded)
        {
            UIManager.Instance.NewSceneLoaded();
            GameObject stageprefab = Resources.Load<GameObject>("Prefabs/StagePrefabs/Stage" + GameStage);
            if(stageprefab == null)
            {
                Debug.LogError("Stage" + GameStage + "이 없습니다.");
            }
            Instantiate(stageprefab, stageprefab.transform.position, stageprefab.transform.rotation);
        }

        if(spaceship != null)
        {
            Destroy(spaceship);
        }
        spaceship = Instantiate(spaceshipPrefab, spaceshipPrefab.transform.position, spaceshipPrefab.transform.rotation);

        if (cameraController == null)
        {
            cameraController = FindAnyObjectByType<CameraController>();
            if(cameraController == null)
            {
                Debug.LogError("CameraController가 없습니다.");
            }
        }

        cameraController.CameraInitialize();

        if(uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
            if(uiController == null)
            {
                Debug.LogError("UIController가 없습니다.");
            }
        }

        UIManager.Instance.HideWindow("Window3");
        UIManager.Instance.HideWindow("Window4");

        uiController.SetAlphaForInventory(1);
        if(!isSceneLoaded)
        {
            uiController.SetPlanetGravityEffectOn();
        }
    }

    public void PlayingStart()
    {
        Time.timeScale = 1;
        spaceship.GetComponent<Spaceship>().GameStart();

        if(uiController == null) 
        {
            uiController = FindAnyObjectByType<UIController>();
            if(uiController == null)
            {
                Debug.LogError("UIController가 없습니다.");
            }
        }
        uiController.SetAlphaForInventory(0);
        uiController.SetPlanetGravityEffectOff();
    }
    private void PausedStart()
    {
        //어떠한 연출 필요
    }

    private void GameOverStart()
    {
        UIManager.Instance.SetwindowWithoutclosing("Window3");
    }

    private void WinStart()
    {
        if(GameStage == AvailableStage)
        {
            AvailableStage++;
        }
        UIManager.Instance.SetwindowWithoutclosing("Window4");
    }

#endregion
    public void ChangeScene(string sceneName)
    {
        SceneController.Instance.LoadSceneAsync(sceneName);
        GameManager.Instance.currentGameState = GameState.Mapping;
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);

        uiController.Retry();
    }

    public void Retry()
    {
        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
            if (uiController == null)
            {
                Debug.LogError("UIController가 없습니다.");
            }
        }
        uiController.Retry();
    }

    public void PlaySoundEffect(String name)
    {
        audioSources[name].PlayOneShot(audioSources[name].clip);
    }

    public void SetBGM(String name, bool b)
    {
        if(b)
        {
            if(!bgmSources[name].isPlaying)
                bgmSources[name].Play();
        }
        else
        {
            bgmSources[name].Stop();
        }
    }
}
