using UnityEngine;
using TMPro;
public class InventoryImporter : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public TextMeshProUGUI planet1Text;
    public TextMeshProUGUI planet2Text;
    public TextMeshProUGUI planet3Text;
    public TextMeshProUGUI wormholeText;

    void Start()
    {
        planet1Text.text = "Planet 1: " + inventory.planet1.ToString();
        planet2Text.text = "Planet 2: " + inventory.planet2.ToString();
        planet3Text.text = "Planet 3: " + inventory.planet3.ToString();
        wormholeText.text = "Wormhole: " + inventory.wormhole.ToString();
    }
}
