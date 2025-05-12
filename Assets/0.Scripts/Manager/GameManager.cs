using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using NUnit.Framework;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject spaceship;
    [SerializeField] private GameObject spaceshipPrefab;

    private UIController uiController;
    public CameraController cameraController{get; private set;}
    public GameState currentGameState{get; private set;} = GameState.Menu;

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
    }

    void Start()
    {
        ChangeGameState(GameState.Menu.ToString());
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
        }
    }

#region 게임상태 이동 관련
    public void MenuStart()
    {
        SceneController.Instance.LoadSceneAsync("MainScreen");
    }

    public void StageSelectStart()
    {
        SceneController.Instance.LoadSceneAsync("StageSelect");
    }
    private void MappingStart()
    {
        if(SceneManager.GetActiveScene().name != "Ingame")
        {
            Debug.Log("Move to Ingame");
            SceneController.Instance.LoadSceneAsync("Ingame");
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
        }

        if(spaceship != null)
        {
            Destroy(spaceship);
        }
        spaceship = Instantiate(spaceshipPrefab, spaceshipPrefab.transform.position, Quaternion.identity);

        if(cameraController == null)
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
        uiController.SetAlphaForInventory(1);
        Time.timeScale = 0;
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
    }
    private void PausedStart()
    {
        //어떠한 연출 필요
    }

    private void GameOverStart()
    {
        UIManager.Instance.SetwindowWithoutclosing("Window3");
    }

#endregion
    public void ChangeScene(string sceneName)
    {
        SceneController.Instance.LoadSceneAsync(sceneName);
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);

        uiController.Retry();
    }

    public void Retry()
    {
        uiController.Retry();
    }

}
