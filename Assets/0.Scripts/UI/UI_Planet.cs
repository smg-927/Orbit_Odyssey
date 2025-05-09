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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Å¬¸¯");
        uiController.StartDragPlanet(this.gameObject, prefab_planet);
    }
}
