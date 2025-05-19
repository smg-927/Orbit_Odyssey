using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Window_StageSelect2 : Window
{
    private Dictionary<int, Button> stageButtons = new Dictionary<int, Button>();
    private List<GameObject> dottedLines = new List<GameObject>();
    [SerializeField] private float dotSpacing = 50f; // 점 사이의 간격
    [SerializeField] private GameObject dotPrefab; // 점 프리팹
    [SerializeField] private Color dotColor = new Color(1f, 1f, 1f, 0.5f); // 점 색상

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

    private GameObject CreateDot()
    {
        GameObject dot = Instantiate(dotPrefab, transform);
        dot.transform.SetParent(transform, false);
        dot.GetComponent<Image>().color = dotColor;
        dot.GetComponent<Image>().raycastTarget = false;
        return dot;
    }

    private void DrawDottedLine(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        int dotCount = Mathf.CeilToInt(distance / dotSpacing);
        Vector2 direction = (end - start).normalized;

        for (int i = 0; i < dotCount; i++)
        {
            float t = (float)i / (dotCount - 1);
            Vector2 position = Vector2.Lerp(start, end, t);
            
            GameObject dot = CreateDot();
            dot.GetComponent<RectTransform>().anchoredPosition = position;
            dottedLines.Add(dot);
            
            // 점을 버튼들 뒤로 보내기
            dot.transform.SetSiblingIndex(1);
        }
    }

    private void DrawDottedLines()
    {
        // 기존 점선 제거
        foreach (var line in dottedLines)
        {
            if (line != null)
                Destroy(line);
        }
        dottedLines.Clear();

        // 버튼들을 스테이지 번호 순서대로 정렬
        var sortedButtons = stageButtons.OrderBy(x => x.Key).ToList();

        // 연속된 버튼들 사이에 점선 생성
        for (int i = 0; i < sortedButtons.Count - 1; i++)
        {
            var currentButton = sortedButtons[i].Value;
            var nextButton = sortedButtons[i + 1].Value;

            RectTransform currentRect = currentButton.GetComponent<RectTransform>();
            RectTransform nextRect = nextButton.GetComponent<RectTransform>();

            DrawDottedLine(currentRect.anchoredPosition, nextRect.anchoredPosition);
        }
    }

    void Start()
    {
        foreach(var button in stageButtons)
        {
            if(button.Key > GameManager.Instance.AvailableStage)
            {
                button.Value.transform.GetComponent<Image>().sprite = button.Value.transform.GetChild(0).GetComponent<Image>().sprite;
                button.Value.interactable = false;
            }
            else
            {
                button.Value.transform.GetComponent<Image>().sprite = button.Value.transform.GetChild(1).GetComponent<Image>().sprite;
                button.Value.interactable = true;
            }
        }

        // 점선 그리기
        DrawDottedLines();
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

    public void ReturnToStageSelect()
    {
        GameManager.Instance.PlaySoundEffect("button2");
        GameManager.Instance.ChangeGameState("StageSelect");
    }
}
