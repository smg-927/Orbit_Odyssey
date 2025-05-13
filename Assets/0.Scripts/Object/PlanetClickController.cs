using UnityEngine;

public class PlanetClickController : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayer;
    private Planet selectedPlanet;
    private Vector3 dragOffset;

    public void ClickDown(Vector3 position)
    {
        int clickableLayerMask = 1 << LayerMask.NameToLayer("Clickable");
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayerMask))
        {
            selectedPlanet = hit.collider.GetComponent<Planet>();
        }
    }
    public void ClickStay(Vector3 position)
    {
        if(selectedPlanet != null)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(position);
            newPosition.z = 20f;
            selectedPlanet.transform.position = newPosition;
        }
    }
    public void ClickUp()
    {
        if(selectedPlanet != null)
        {
            selectedPlanet = null;
        }
    }
}