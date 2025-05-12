using UnityEngine;

public class Window_mainscreen2 : Window
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    public void StageSelect()
    {
        GameManager.Instance.ChangeGameState("StageSelect");
    }

    public void GamePlay()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void ExitButton()
    {
        Application.Quit();
        //추후 변경 필요
    }
}
