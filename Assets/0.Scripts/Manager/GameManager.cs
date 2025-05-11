using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject spaceship;
    [SerializeField] private GameObject spaceshipPrefab;
    private CanvasGroup canvasGroup;
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
                MenuStart();
                break;
            case GameState.StageSelect:
                StageSelectStart();
                break;
            case GameState.Mapping:
                MappingStart();
                break;
            case GameState.Playing:
                PlayingStart();
                break;
            case GameState.Paused:
                PausedStart();
                break;
            case GameState.GameOver:
                GameOverStart();
                break;
        }
    }

<<<<<<< Updated upstream
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
            SceneController.Instance.LoadSceneAsync("Ingame");
        }
        else
        {
            MappingReady();
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

    public void MappingReady()
    {
        UIManager.Instance.NewSceneLoaded();

        if(spaceship != null)
        {
            Destroy(spaceship);
        }
        else
        {

        }

        if(cameraController == null)
        {
            cameraController = FindAnyObjectByType<CameraController>();
            if(cameraController == null)
            {
                Debug.LogError("CameraController가 없습니다.");
            }
        }

        if(canvasGroup == null)
        {
            canvasGroup = FindAnyObjectByType<CanvasGroup>();
            if(canvasGroup == null)
            {
                Debug.LogError("CanvasGroup이 없습니다.");
            }
        }
        canvasGroup.alpha = 1;
        spaceship = Instantiate(spaceshipPrefab, spaceshipPrefab.transform.position, Quaternion.identity);
        Time.timeScale = 0;
=======
    public void PlayButtonClick()
    {
        if (currentGameState == GameState.Playing)
        {
            ChangeGameState(GameState.Mapping.ToString());
        }
        else if (currentGameState == GameState.Mapping)
        {
            ChangeGameState(GameState.Playing.ToString());
        }
>>>>>>> Stashed changes
    }

    public void PlayingStart()
    {
        Time.timeScale = 1;
        spaceship.GetComponent<Spaceship>().GameStart();
        canvasGroup.alpha = 0;
    }
<<<<<<< Updated upstream
    private void PausedStart()
    {
=======

    private void MappingStart()
    {
        if(spaceship != null)
        {
            Destroy(spaceship);
        }
        canvasGroup.alpha = 1;
        spaceship = Instantiate(spaceshipPrefab, new Vector3(-10, 0, 20), Quaternion.identity);
        Time.timeScale = 0;
>>>>>>> Stashed changes
    }

    private void GameOverStart()
    {
    }


#endregion



    public void ChangeScene(string sceneName)
    {
        SceneController.Instance.LoadSceneAsync(sceneName);
    }

}
