using UnityEngine;

public class Window_Ingame2 : Window
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UIManager.Instance.SetwindowWithoutclosing("Window1");
    }
    
}
