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

    private void Awake()
    {
        shipStartPos = Resources.Load<ShipStartPos>("DataTable/ShipStartPos/ShipStartPos");
        Vector2 startPos = shipStartPos.list_shipStartPos[GameManager.Instance.GameStage - 1];
        transform.position = new Vector3(startPos.x, startPos.y, 20);
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    public void GameStart()
    {
        rb.AddForce(new Vector3(1, 0, 0) * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        Move();
        CheckOutOfBounds();
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
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        previousPosition = currentPosition;
    }

    private void CheckOutOfBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        
        // 화면 밖으로 나갔는지 확인 (x, y 좌표가 0보다 작거나 1보다 큰 경우)
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || 
            viewportPosition.y < 0 || viewportPosition.y > 1)
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
