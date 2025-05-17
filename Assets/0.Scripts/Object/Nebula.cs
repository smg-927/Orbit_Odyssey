using UnityEngine;

public class Nebula : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody spaceshipRb = other.gameObject.GetComponent<Rigidbody>();
            spaceshipRb.linearDamping = 0.4f;  // 성운 안에 들어올 때 drag 증가
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody spaceshipRb = other.gameObject.GetComponent<Rigidbody>();
            spaceshipRb.linearDamping = 0f;  // 성운을 벗어날 때 drag 초기화
        }
    }
}
