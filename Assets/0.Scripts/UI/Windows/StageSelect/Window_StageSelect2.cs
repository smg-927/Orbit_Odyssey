using UnityEngine;

public class Window_StageSelect2 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Stage1()
    {
        GameManager.Instance.GameStage = 1; // 추후 변경경
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage2()
    {
        GameManager.Instance.GameStage = 4;
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage3()
    {
        GameManager.Instance.GameStage = 4;
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage4()
    {
        GameManager.Instance.GameStage = 4;
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage5()
    {
        GameManager.Instance.GameStage = 4;
        GameManager.Instance.PlaySoundEffect("button1");
        GameManager.Instance.ChangeGameState("Mapping");
    }
    
    public void ReturnToMenu()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("Menu");
    }
}
