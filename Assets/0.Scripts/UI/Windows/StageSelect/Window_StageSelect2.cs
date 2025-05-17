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
            if (button != null && button.name != "Home")
            {
                string buttonName = button.name;
                if (buttonName.StartsWith("Stage"))
                {
                    string numberStr = buttonName.Replace("Stage", "");
                    if (int.TryParse(numberStr, out int stageNumber))
                    {
                        if (!stageButtons.ContainsKey(stageNumber))
                        {
                            stageButtons.Add(stageNumber, button);
                        }
                        else
                        {
                            Debug.LogWarning($"중복된 스테이지 번호가 발견되었습니다: Stage{stageNumber}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"스테이지 번호를 파싱할 수 없습니다: {buttonName}");
                    }
                }
            }
        }
    }

    void Start()
    {
        foreach(var button in stageButtons)
        {
            if(button.Key > GameManager.Instance.AvailableStage)
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
    }
    public void ReturnToMenu()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("Menu");
    }
}
