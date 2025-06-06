using UnityEngine;

public class Window_Ingame3 : Window
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
}
