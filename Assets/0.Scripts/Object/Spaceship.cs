using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] float rotationSpeed = 15;
    private Rigidbody rb;
    private Vector3 previousPosition;
    private ShipStartPos shipStartPos;
    private BoxCollider collider;

    GameObject particle1, particle2;

    private void Awake()
    {
        shipStartPos = Resources.Load<ShipStartPos>("DataTable/ShipStartPos/ShipStartPos");
        Vector2 startPos = shipStartPos.list_shipStartPos[GameManager.Instance.GameStage - 1];
        transform.position = new Vector3(startPos.x, startPos.y, 20);
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;

        particle1 = transform.Find("TrailEffect 1").gameObject;
        particle2 = transform.Find("TrailEffect 2").gameObject;
    }

    private void Start()
    {
        particle1.SetActive(false);
        particle2.SetActive(false);
    }

    public void GameStart()
    {
        particle1.SetActive(true);
        particle2.SetActive(true);
        rb.AddForce(new Vector3(1, 0, 0) * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        Move();
        CheckOutOfBounds();

        if (GameManager.Instance.currentGameState == GameState.Playing) collider.enabled = true;
        else collider.enabled = false;
    }

    private void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = (currentPosition - previousPosition).normalized;

        /*if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * speed);
        }*/

        //Z축 중심으로만 회전하도록 제한
        Vector3 XYDirection = new Vector3(direction.x, direction.y, 0);
        if (XYDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(XYDirection.y, XYDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(-angle, 90f, 0);
            //rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed));
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }

        previousPosition = currentPosition;
    }

    private void CheckOutOfBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        
        // 화면 밖으로 나갔는지 확인 (x, y 좌표가 0보다 작거나 1보다 큰 경우)
        if (viewportPosition.x < -0.1f || viewportPosition.x > 1.1f || 
            viewportPosition.y < -0.1f || viewportPosition.y > 1.1f)
        {
            GameManager.Instance.PlaySoundEffect("destroy");
            Die();
        }
    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.ChangeGameState("GameOver");
        Destroy(gameObject);
    }
}
