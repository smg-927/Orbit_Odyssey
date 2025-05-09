using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Planet : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] GameObject prefab_planet;
    UIController uiController;

    private void Awake()
    {
        uiController = FindAnyObjectByType<UIController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        uiController.StartDragPlanet(this.gameObject, prefab_planet);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        uiController.inventoryPlanets[prefab_planet.name]--;
    }
}
