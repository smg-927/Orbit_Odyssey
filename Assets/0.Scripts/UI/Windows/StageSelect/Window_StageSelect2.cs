using UnityEngine;

public class Window_StageSelect2 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Stage1()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage2()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage3()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage4()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }

    public void Stage5()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }
    
}
