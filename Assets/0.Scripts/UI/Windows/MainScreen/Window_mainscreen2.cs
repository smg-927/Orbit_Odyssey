using UnityEngine;

public class Window_mainscreen2 : Window
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    public void StageSelect()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("StageSelect");
    }

    public void GamePlay()
    {
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void ExitButton()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        Application.Quit();
        //추후 변경 필요
    }
}
