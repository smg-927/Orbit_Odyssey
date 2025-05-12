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
    InventoryImporter inventoryImporter;
    CanvasGroup group_inventory;
    public Dictionary<string, int> inventoryPlanets = new Dictionary<string, int>();
    //List<GameObject> InventoryPlanets = new List<GameObject>();
    //Installed Planet
    List<GameObject> InstalledPlanets= new List<GameObject>();

    private void Awake()
    {
        group_inventory = transform.GetComponent<CanvasGroup>();
        inventoryImporter = transform.GetComponent<InventoryImporter>();
        SetInventoryObj();
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Retry");
            Retry();
        }*/
        
        if(GameManager.Instance.currentGameState != GameState.Mapping)
        {
            return;
        }

        if(draging && dragingObj != null)
        {
            if (Input.GetMouseButton(0))
            {
                SetAlphaForInventory(0);

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 20f;
                dragingObj.transform.position = worldPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetAlphaForInventory(1);

                // for(int i=0;i< InventoryPlanets.Count;i++)
                // {
                //     if(InventoryPlanets[i] == choosingObj)
                //     {
                //         //Delete in Inventory
                //         InventoryPlanets[i].GetComponent<Image>().enabled = false;
                //         InventoryPlanets.RemoveAt(i);
                //     }
                // }
                inventoryPlanets[dragingObj.name.Replace("(Clone)", "")]--;
                inventoryImporter.UpdateInventory(dragingObj.name.Replace("(Clone)", ""), inventoryPlanets[dragingObj.name.Replace("(Clone)", "")]);
                //Install the planet on space
                InstalledPlanets.Add(dragingObj);
                dragingObj = null;
                draging = false;
            }
        }

        
    }

    public void StartDragPlanet(GameObject ui_planet, GameObject prefab_planet)
    {
        if (GameManager.Instance.currentGameState != GameState.Mapping) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 20f;

        choosingObj = ui_planet;
        dragingObj = Instantiate(prefab_planet, worldPos, Quaternion.identity);
        draging = true;
    }

    public void SetAlphaForInventory(int alphaCase)
    {
        if(alphaCase == 0)
        {
            group_inventory.alpha = 0.2f;
            // for (int i = 0; i < InventoryPlanets.Count; i++)
            // {
            //     InventoryPlanets[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            // }
        }
        else
        {
            group_inventory.alpha = 1f;
            // for (int i = 0; i < InventoryPlanets.Count; i++)
            // {
            //     InventoryPlanets[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            // }
        }
    }

    public void Retry()
    {
        //Reset planets in inventory
        SetInventoryObj();
        inventoryImporter.UpdateInventory("Venus", inventoryPlanets["Venus"]);
        inventoryImporter.UpdateInventory("Mars", inventoryPlanets["Mars"]);
        inventoryImporter.UpdateInventory("Jupiter", inventoryPlanets["Jupiter"]);
        inventoryImporter.UpdateInventory("Saturn", inventoryPlanets["Saturn"]);
        // foreach (GameObject obj in InventoryPlanets)
        // {
        //     obj.GetComponent<Image>().enabled = true;
        // }
        //Destroy all installed planets
        foreach (GameObject obj in InstalledPlanets)
        {
            Destroy(obj);
        }
        InstalledPlanets.Clear();
        GameManager.Instance.ChangeGameState("Mapping");
    }

    void SetInventoryObj()
    {
        Inventory inventory = inventoryImporter.inventory;
        inventoryPlanets.Clear();
        inventoryPlanets.Add("Venus", inventory.Venus);
        inventoryPlanets.Add("Mars", inventory.Mars);
        inventoryPlanets.Add("Jupiter", inventory.Jupiter);
        inventoryPlanets.Add("Saturn", inventory.Saturn);
    }
}
