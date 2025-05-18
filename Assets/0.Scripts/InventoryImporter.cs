using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryImporter : MonoBehaviour
{
    public Inventory inventory{get; private set;}
    public Dictionary<string, GameObject> planetTexts = new Dictionary<string, GameObject>();
    public TextMeshProUGUI VenusText;
    public TextMeshProUGUI MarsText;
    public TextMeshProUGUI JupiterText;
    public TextMeshProUGUI NeptuneText;
    public TextMeshProUGUI SaturnText;

    void Awake()
    {
        inventory = Resources.Load<Inventory>("DataTable/Inventory/Inventory_stage" + GameManager.Instance.GameStage);
        if (inventory == null)
        {
            Debug.LogError("Inventory not found");
            return;
        }
    }
    void Start()
    {
        if(inventory == null)
        {
            Debug.LogError("Inventory not found");
            return;
        }
        planetTexts.Add("Venus", VenusText.gameObject);
        planetTexts.Add("Mars", MarsText.gameObject);
        planetTexts.Add("Jupiter", JupiterText.gameObject);
        planetTexts.Add("Neptune", NeptuneText.gameObject);
        planetTexts.Add("Saturn", SaturnText.gameObject);

        planetTexts["Venus"].GetComponent<TextMeshProUGUI>().text = inventory.Venus.ToString();
        planetTexts["Mars"].GetComponent<TextMeshProUGUI>().text = inventory.Mars.ToString();
        planetTexts["Jupiter"].GetComponent<TextMeshProUGUI>().text = inventory.Jupiter.ToString();
        planetTexts["Neptune"].GetComponent<TextMeshProUGUI>().text = inventory.Neptune.ToString();
        planetTexts["Saturn"].GetComponent<TextMeshProUGUI>().text = inventory.Saturn.ToString();
    }

    public void UpdateInventory(string text, int value)
    {
        planetTexts[text].GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
