using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryImporter : MonoBehaviour
{
    [SerializeField] public Inventory inventory;

    public Dictionary<string, GameObject> planetTexts = new Dictionary<string, GameObject>();

    public TextMeshProUGUI planet1Text;
    public TextMeshProUGUI planet2Text;
    public TextMeshProUGUI planet3Text;

    void Start()
    {

        planetTexts.Add("Planet 1", planet1Text.gameObject);
        planetTexts.Add("Planet 2", planet2Text.gameObject);
        planetTexts.Add("Planet 3", planet3Text.gameObject);

        planetTexts["Planet 1"].GetComponent<TextMeshProUGUI>().text = inventory.planet1.ToString();
        planetTexts["Planet 2"].GetComponent<TextMeshProUGUI>().text = inventory.planet2.ToString();
        planetTexts["Planet 3"].GetComponent<TextMeshProUGUI>().text = inventory.planet3.ToString();
    }

    public void UpdateInventory(string text, int value)
    {
        planetTexts[text].GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
