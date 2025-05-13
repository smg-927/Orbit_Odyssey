using UnityEngine;

public class PlanetClickController : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayer;
    private Planet selectedPlanet;
    private Vector3 dragOffset;

    public void ClickDown(Vector3 position)
    {
        int uiLayer = clickableLayer;
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100);
        
        Debug.Log($"=== 충돌한 오브젝트 목록 ({hits.Length}개) ===");
        foreach (RaycastHit hit in hits)
        {
            Debug.Log($"- {hit.collider.name} (Layer: {hit.collider.gameObject.layer}, Distance: {hit.distance:F2})");
        }
        Debug.Log("========================");

        if (hits.Length > 0)
        {
            selectedPlanet = hits[0].collider.GetComponent<Planet>();
            if(selectedPlanet == null)
            {
                Debug.Log("Planet이 없습니다.");
            }
        }
    }
    public void ClickStay(Vector3 position)
    {
        if(selectedPlanet != null)
        {
            Debug.Log("selectedPlanet : " + selectedPlanet.name);
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