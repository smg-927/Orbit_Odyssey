using Unity.VisualScripting;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;
    private Vector3 previousPosition;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
        direction = new Vector3(1, 0, 0);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * speed, Time.deltaTime * speed);
    }
}
