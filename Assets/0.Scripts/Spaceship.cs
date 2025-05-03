using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(1, 0, 0) * speed;
    }
    
}
