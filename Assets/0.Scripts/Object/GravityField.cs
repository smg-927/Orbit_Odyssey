using UnityEngine;
using System;
public class GravityField : MonoBehaviour
{
    private Planet planet;
    [SerializeField] float scale = 10;
    private void Start()
    {
        planet = transform.parent.GetComponent<Planet>();
        //float scale = 5 * planet.mass/20;
        //float multiplier = 2f;
        transform.localScale = new Vector3(scale,scale,scale);
        if(planet.isgoal)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null && GameManager.Instance.currentGameState == GameState.Playing)
            {
                Vector3 direction = (transform.position - other.transform.position).normalized;

                //250512 중력이 y축으로만 영향을 미칠 경우
                //Vector3 direction = (transform.position - other.transform.position).normalized;
                //direction.x = 0f;
                //direction.z = 0f;
                //direction = direction.normalized;

                // 중력 계산 (거리제곱에 반비례하는 버전)
                // float distance = Vector3.Distance(transform.position, other.transform.position);
                // float gravityForce = planet.mass * rb.mass / Mathf.Max(distance * distance, 0.01f); // 거리 제곱에 반비례

                //중력 계산 (거리와 상관없는 버전)
                //float gravityForce = planet.mass * rb.mass;

                // 중력 계산 (거리에 반비례 버전)
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float gravityForce = planet.mass * rb.mass / Mathf.Max(distance, 0.01f); // 거리에 반비례
                
                rb.AddForce(direction * gravityForce);
            }
        }
    }
}