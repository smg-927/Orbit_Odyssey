using UnityEngine;

public class PlanetClickController : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayer;
    private Planet selectedPlanet;
    private Vector3 dragOffset;

    void Update()
    {
        if(GameManager.Instance.currentGameState == GameState.Mapping)
        {
            // 마우스 클릭 시 행성 선택
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in hits)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.layer == clickableLayer)
                    {
                        Debug.Log("hit.collider.gameObject.layer: " + hit.collider.gameObject.layer);
                        Planet planet = hit.collider.GetComponent<Planet>();
                        if (planet != null)
                        {
                            Debug.Log("Planet clicked!!!!: " + planet.gameObject.name);
                            selectedPlanet = planet;
                            break;
                        }
                    }
                }
            }
            // 마우스 드래그 중 행성 이동
            else if(Input.GetMouseButton(0) && selectedPlanet != null)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z = 20f;
                selectedPlanet.transform.position = newPosition;
            }
            // 마우스 버튼을 떼면 선택 해제
            else if(Input.GetMouseButtonUp(0))
            {
                selectedPlanet = null;
            }
        }
    }
}

