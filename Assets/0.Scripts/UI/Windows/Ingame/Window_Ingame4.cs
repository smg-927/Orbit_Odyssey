using UnityEngine;

public class Window_Ingame4 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void ExitButton()
    {
        GameManager.Instance.ChangeGameState("StageSelect");
    }
    public void RetryButton()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }
}
