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
        if(uiController.inventoryPlanets[prefab_planet.name] > 0)
        {
            uiController.inventoryPlanets[prefab_planet.name]--;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (uiController.inventoryPlanets[prefab_planet.name] <= 0)
        {
            Debug.Log("Not enough planets to drag");
            return;
        }
        Debug.Log("Start Drag");
        uiController.StartDragPlanet(this.gameObject, prefab_planet);
    }
}
