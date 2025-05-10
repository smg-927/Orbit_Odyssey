using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject spaceship;
    [SerializeField] private GameObject spaceshipPrefab;
    [SerializeField] private CanvasGroup canvasGroup;
    private GameState currentGameState = GameState.Menu;

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
        ChangeGameState(GameState.Mapping.ToString());
    }

    public void ChangeGameState(string newGameState)
    {
        currentGameState = (GameState)Enum.Parse(typeof(GameState), newGameState);
        switch(currentGameState)
        {
            case GameState.Menu:
                break;
            case GameState.Mapping:
                MappingStart();
                break;
            case GameState.Playing:
                PlayingStart();
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void PlayingStart()
    {
        Time.timeScale = 1;
        spaceship.GetComponent<Spaceship>().GameStart();
        canvasGroup.alpha = 0;
    }

    public void MappingStart()
    {
        spaceship = Instantiate(spaceshipPrefab, new Vector3(-10, 0, 20), Quaternion.identity);
        Time.timeScale = 0;
    }


    public void ChangeScene(string sceneName)
    {
        SceneController.Instance.LoadSceneAsync(sceneName);
    }

}
