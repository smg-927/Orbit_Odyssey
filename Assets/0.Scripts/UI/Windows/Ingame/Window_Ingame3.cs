using UnityEngine;

public class Window_Ingame3 : Window
{
    public void ExitButton()
    {
        GameManager.Instance.ChangeGameState("StageSelect");
    }
    public void RetryButton()
    {
        GameManager.Instance.ChangeGameState("Mapping");
    }
}
