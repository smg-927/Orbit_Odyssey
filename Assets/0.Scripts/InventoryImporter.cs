using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryImporter : MonoBehaviour
{
    [SerializeField] public Inventory inventory;

    public Dictionary<string, GameObject> planetTexts = new Dictionary<string, GameObject>();

    public TextMeshProUGUI VenusText;
    public TextMeshProUGUI MarsText;
    public TextMeshProUGUI JupiterText;
    public TextMeshProUGUI SaturnText;

    void Start()
    {

        planetTexts.Add("Venus", VenusText.gameObject);
        planetTexts.Add("Mars", MarsText.gameObject);
        planetTexts.Add("Jupiter", JupiterText.gameObject);
        planetTexts.Add("Saturn", SaturnText.gameObject);

        planetTexts["Venus"].GetComponent<TextMeshProUGUI>().text = inventory.Venus.ToString();
        planetTexts["Mars"].GetComponent<TextMeshProUGUI>().text = inventory.Mars.ToString();
        planetTexts["Jupiter"].GetComponent<TextMeshProUGUI>().text = inventory.Jupiter.ToString();
        planetTexts["Saturn"].GetComponent<TextMeshProUGUI>().text = inventory.Saturn.ToString();
    }

    public void UpdateInventory(string text, int value)
    {
        planetTexts[text].GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
