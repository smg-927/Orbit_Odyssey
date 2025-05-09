using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Drag & Drop
    bool draging = false;
    GameObject choosingObj = null;
    GameObject dragingObj = null;

    //Inventory
    CanvasGroup group_inventory;
    List<GameObject> InventoryPlanets = new List<GameObject>();

    //Installed Planet
    List<GameObject> InstalledPlanets= new List<GameObject>();

    private void Awake()
    {
        group_inventory = transform.GetChild(0).GetComponent<CanvasGroup>();
        SetInventoryObj();
    }

    void Update()
    {
        if(draging && dragingObj != null)
        {
            if (Input.GetMouseButton(0))
            {
                SetAlphaForInventory(0);

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 0f;

                dragingObj.transform.position = worldPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetAlphaForInventory(1);

                for(int i=0;i< InventoryPlanets.Count;i++)
                {
                    if(InventoryPlanets[i] == choosingObj)
                    {
                        //Delete in Inventory
                        InventoryPlanets[i].GetComponent<Image>().enabled = false;
                        InventoryPlanets.RemoveAt(i);
                    }
                }
                //Install the planet on space
                InstalledPlanets.Add(dragingObj);
                dragingObj = null;
                draging = false;
            }
        }

        //Retry
        if(Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void StartDragPlanet(GameObject ui_planet, GameObject prefab_planet)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        choosingObj = ui_planet;
        dragingObj = Instantiate(prefab_planet, worldPos, Quaternion.identity);

        draging = true;
    }

    void SetAlphaForInventory(int alphaCase)
    {
        if(alphaCase == 0)
        {
            group_inventory.alpha = 0.2f;
            for (int i = 0; i < InventoryPlanets.Count; i++)
            {
                InventoryPlanets[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            group_inventory.alpha = 1f;
            for (int i = 0; i < InventoryPlanets.Count; i++)
            {
                InventoryPlanets[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void Retry()
    {
        //Reset planets in inventory
        SetInventoryObj();
        foreach (GameObject obj in InventoryPlanets)
        {
            obj.GetComponent<Image>().enabled = true;
        }

        //Destroy all installed planets
        foreach (GameObject obj in InstalledPlanets)
        {
            Destroy(obj);
        }
        InstalledPlanets.Clear();
    }

    void SetInventoryObj()
    {
        foreach (Transform child in group_inventory.transform)
        {
            InventoryPlanets.Add(child.gameObject);
        }
    }
}
