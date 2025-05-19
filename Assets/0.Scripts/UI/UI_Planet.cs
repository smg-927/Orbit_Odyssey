using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Planet : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject prefab_planet;
    UIController uiController;

    private void Awake()
    {
        uiController = FindAnyObjectByType<UIController>();
    }

    private void OnMouseDown()
    {
        Debug.Log("3D Object Clicked");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (uiController == null)
            {
                uiController = FindAnyObjectByType<UIController>();
                if (uiController == null)
                {
                    Debug.LogError("UIController not found!");
                    return;
                }
            }
            // ��Ŭ�� ó�� ����(�浹 �ִ� �� ���Ƽ� �ּ�ó���߽��ϴ� 5/18)
            /*if (uiController.inventoryPlanets[prefab_planet.name] <= 0)
            {
                Debug.Log("Not enough planets to drag");
                return;
            }*/

            //Debug.Log("Start Drag");
            uiController.StartDragPlanet(this.gameObject, prefab_planet);
        }
    }
}
