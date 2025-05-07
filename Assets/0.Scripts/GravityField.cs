using UnityEngine;
using System;
public class GravityField : MonoBehaviour
{
    private Planet planet;
    private SphereCollider gravityField;

    private void Start()
    {
        planet = transform.parent.GetComponent<Planet>();
        gravityField = GetComponent<SphereCollider>();
        gravityField.radius = (float)(0.5 * Math.Sqrt(planet.mass / 2));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (transform.position - other.transform.position).normalized;
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float gravityForce = planet.mass * rb.mass / Mathf.Max(distance * distance, 0.01f); // 거리 제곱에 반비례
                rb.AddForce(direction * gravityForce);
            }
        }
    }
}