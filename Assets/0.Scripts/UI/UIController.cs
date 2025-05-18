using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlanetClickController planetClickController;
    //Drag & Drop
    bool draging = false;
    GameObject choosingObj = null;
    GameObject dragingObj = null;
    DragableObj dragingObj_state = null;

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
    }

    private void Start()
    {
        SetInventoryObj();
    }

    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Mapping)
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

                if(!dragingObj_state.isOverlaped)
                {
                    // 설치 가능
                    RemoveFromInventory();
                    InstalledPlanets.Add(dragingObj);

                    dragingObj = null;
                    dragingObj_state = null;
                    draging = false;
                }
                else
                {
                    // 겹침, 설치 불가
                    GameManager.Instance.PlaySoundEffect("relocation");
                    dragingObj_state = null;
                    Destroy(dragingObj);
                    draging = false;
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                //좌클릭 드래그 - 배치된 행성 위치 수정
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                planetClickController.ClickDown(mousePos);
            }
            else if(Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                planetClickController.ClickStay(mousePos);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                planetClickController.ClickUp();
            }
            else if(Input.GetMouseButtonDown(1))
            {
                //우클릭 - 배치된 행성 인벤토리로 넣기
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                planetClickController.ReturnToInventoryByRay(mousePos);
            }
        }

        
    }
    public void StartDragPlanet(GameObject ui_planet, GameObject prefab_planet)
    {
        if (GameManager.Instance.currentGameState != GameState.Mapping) return;
        if (inventoryPlanets[prefab_planet.name] == 0) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 20f;

        GameManager.Instance.PlaySoundEffect("grab");
        choosingObj = ui_planet;
        dragingObj = Instantiate(prefab_planet, worldPos, Quaternion.identity);
        dragingObj_state = dragingObj.GetComponent<DragableObj>();
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
        Debug.Log("Venus: " + inventoryPlanets["Venus"]);
        Debug.Log("Mars: " + inventoryPlanets["Mars"]);
        Debug.Log("Jupiter: " + inventoryPlanets["Jupiter"]);
        Debug.Log("Neptune: " + inventoryPlanets["Neptune"]);
        Debug.Log("Saturn: " + inventoryPlanets["Saturn"]);
        inventoryImporter.UpdateInventory("Venus", inventoryPlanets["Venus"]);
        inventoryImporter.UpdateInventory("Mars", inventoryPlanets["Mars"]);
        inventoryImporter.UpdateInventory("Jupiter", inventoryPlanets["Jupiter"]);
        inventoryImporter.UpdateInventory("Saturn", inventoryPlanets["Saturn"]);
        inventoryImporter.UpdateInventory("Neptune", inventoryPlanets["Neptune"]);

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
        SetInventoryObj();
    }

    void SetInventoryObj()
    {
        Debug.Log("SetInventoryObj");
        Inventory inventory = inventoryImporter.inventory;
        if(inventory == null)
        {
            Debug.LogError("Inventory is null");
            return;
        }
        inventoryPlanets.Clear();
        inventoryPlanets.Add("Venus", inventory.Venus);
        inventoryPlanets.Add("Mars", inventory.Mars);
        inventoryPlanets.Add("Jupiter", inventory.Jupiter);
        inventoryPlanets.Add("Saturn", inventory.Saturn);
        inventoryPlanets.Add("Neptune", inventory.Neptune);
    }

    public void SetPlanetGravityEffectOff()
    {
        foreach (GameObject obj in InstalledPlanets)
        {
            obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void SetPlanetGravityEffectOn()
    {
        foreach (GameObject obj in InstalledPlanets)
        {
            obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void RemoveFromInventory()
    {
        GameManager.Instance.PlaySoundEffect("drop");
        inventoryPlanets[dragingObj.name.Replace("(Clone)", "")]--;
        inventoryImporter.UpdateInventory(dragingObj.name.Replace("(Clone)", ""), inventoryPlanets[dragingObj.name.Replace("(Clone)", "")]);
    }

    public void ReturnToInventory(GameObject targetObj)
    {
        string planetName = targetObj.name.Replace("(Clone)", "");
        
        for (int i=0; i< InstalledPlanets.Count; i++)
        {
            if (InstalledPlanets[i] == targetObj)
            {
                InstalledPlanets.RemoveAt(i);
                break;
            }
        }
        Destroy(targetObj);
        GameManager.Instance.PlaySoundEffect("relocation");
        inventoryPlanets[planetName]++;
        inventoryImporter.UpdateInventory(targetObj.name.Replace("(Clone)", ""), inventoryPlanets[targetObj.name.Replace("(Clone)", "")]);
    }
}
