using Unity.VisualScripting;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    private Rigidbody rb;
    private Vector3 previousPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    public void GameStart()
    {
        rb.linearVelocity = new Vector3(1, 0, 0) * speed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = (currentPosition - previousPosition).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * speed);
        }

        previousPosition = currentPosition;
    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.ChangeGameState("GameOver");
        Destroy(gameObject);
    }
}
