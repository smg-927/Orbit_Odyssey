using UnityEngine;

public class PlanetClickController : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayer;
    private GameObject selectedPlanet = null;
    DragableObj selectedPlanet_state = null;
    private Vector3 dragOffset;
    UIController uiController;

    private void Awake()
    {
        uiController = FindAnyObjectByType<UIController>();
    }

    public void ClickDown(Vector3 position)
    {
        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
            if (uiController == null)
            {
                Debug.LogError("UIController not found!");
                return;
            }
        }

        int clickableLayerMask = 1 << LayerMask.NameToLayer("Clickable");
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f, clickableLayerMask);

        //Debug.Log($"=== 충돌한 오브젝트 목록 ({hits.Length}개) ===");
        foreach (RaycastHit hit in hits)
        {
            Debug.Log($"- {hit.collider.name} (Layer: {hit.collider.gameObject.layer})");
        }

        if (hits.Length > 0)
        {
            GameManager.Instance.PlaySoundEffect("grab");
            selectedPlanet = hits[0].collider.gameObject;
            selectedPlanet_state = selectedPlanet.GetComponent<DragableObj>();
            if (selectedPlanet == null)
            {
                //Debug.LogWarning($"클릭한 오브젝트 {hits[0].collider.name}에 Planet 컴포넌트가 없습니다.");
            }
            else
            {
                //Debug.Log($"행성 선택됨: {selectedPlanet.name}");
            }
        }
        else
        {
            //Debug.Log("클릭 가능한 오브젝트가 없습니다.");
            selectedPlanet = null;
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
            if (!selectedPlanet_state.isOverlaped)
            {
                GameManager.Instance.PlaySoundEffect("drop");
                //Debug.Log($"행성 선택 해제: {selectedPlanet.name}");
                selectedPlanet = null;
            }
            else
            {
                Debug.Log("행성 삭제 - " + selectedPlanet.name);
                Debug.Log("삭제 행성 overlap 여부 - " + selectedPlanet_state.isOverlaped);
                GameManager.Instance.PlaySoundEffect("relocation");
                selectedPlanet_state = null;
                uiController.ReturnToInventory(selectedPlanet);
            }
        }
    }

    public void ReturnToInventoryByRay(Vector3 position)
    {
        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
            if (uiController == null)
            {
                Debug.LogError("UIController not found!");
                return;
            }
        }

        int clickableLayerMask = 1 << LayerMask.NameToLayer("Clickable");
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f, clickableLayerMask);

        if (hits.Length > 0)
        {
            GameObject target;
            target = hits[0].collider.transform.gameObject;
            uiController.ReturnToInventory(target);
        }
    }
}