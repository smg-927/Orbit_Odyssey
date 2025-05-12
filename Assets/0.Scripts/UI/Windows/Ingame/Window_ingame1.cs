using UnityEngine;

public class Window_ingame1 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayButton()
    {
        if(GameManager.Instance.currentGameState == GameState.Mapping)
        {
            GameManager.Instance.PlaySoundEffect("button2");
            GameManager.Instance.PlaySoundEffect("shipworking");
            GameManager.Instance.ChangeGameState("Playing");
        }
        else
        {
            GameManager.Instance.PlaySoundEffect("button2");
            GameManager.Instance.ChangeGameState("Mapping");
        }
    }

    public void CameraButton()
    {
        if(GameManager.Instance.currentGameState == GameState.Playing)
        {
            GameManager.Instance.PlaySoundEffect("button2");
            GameManager.Instance.cameraController.SwitchCamera();
        }
    }

    public void RetryButton()
    {
        if (GameManager.Instance.currentGameState == GameState.Mapping)
        {
            GameManager.Instance.PlaySoundEffect("relocation");
            GameManager.Instance.Retry();
        }
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("Menu");
    }
}
