using UnityEngine;

public class Window_Ingame4 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void ExitButton()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("StageSelect");
    }
    public void RetryButton()
    {
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void NextButton()
    {
        GameManager.Instance.PlaySoundEffect("button1");

        if(GameManager.Instance.GameStage == 10)
        {
            GameManager.Instance.ChangeGameState("StageSelect");
        }
        else
        {
            GameManager.Instance.GameStage++;
            GameManager.Instance.ChangeScene("Ingame");
        }
    }
}
