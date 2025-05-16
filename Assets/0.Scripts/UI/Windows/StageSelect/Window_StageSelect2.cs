using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Window_StageSelect2 : Window
{
    private Dictionary<int, Button> stageButtons = new Dictionary<int, Button>();
    protected override void Awake()
    {
        base.Awake();
        foreach(Transform child in transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                int stageNumber = int.Parse(button.name.Replace("Stage", ""));
                stageButtons.Add(stageNumber, button);
            }
        }
    }

    void Start()
    {
        foreach(var button in stageButtons)
        {
            if(GameManager.Instance.AvailableStage > button.Key)
            {
                button.Value.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                button.Value.interactable = false;
            }
            else
            {
                button.Value.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                button.Value.interactable = true;
            }
        }
    }

    public void Stage1()
    {
        GotoStage(1);
    }

    public void Stage2()
    {
        GotoStage(2);
    }

    public void Stage3()
    {
        GotoStage(3);
    }

    public void Stage4()
    {
        GotoStage(4);
    }

    public void Stage5()
    {
        GotoStage(5);
    }
    
    public void Stage6()
    {
        GotoStage(6);
    }

    public void Stage7()
    {
        GotoStage(7);
    }

    public void Stage8()
    {
        GotoStage(8);
    }

    public void Stage9()
    {
        GotoStage(9);
    }

    public void Stage10()
    {
        GotoStage(10);
    }
    
    private void GotoStage(int stage)
    {
        if(GameManager.Instance.AvailableStage >= stage)
        {
            GameManager.Instance.GameStage = stage;
            GameManager.Instance.PlaySoundEffect("button1");
            GameManager.Instance.ChangeGameState("Mapping");
        }
        else
        {
            //거부
        }
    }
    public void ReturnToMenu()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("Menu");
    }
}
