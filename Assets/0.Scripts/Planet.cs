using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float mass = 30f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (transform.position - other.transform.position).normalized;
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float gravityForce = mass * rb.mass / Mathf.Max(distance * distance, 0.01f); // 거리 제곱에 반비례
                rb.AddForce(direction * gravityForce);
            }
        }
    }
}
