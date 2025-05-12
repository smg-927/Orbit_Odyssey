using UnityEngine;

public class Nebula : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody spaceshipRb = other.gameObject.GetComponent<Rigidbody>();
            spaceshipRb.linearDamping = 50;  // 성운 안에 들어올 때 drag 증가
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody spaceshipRb = other.gameObject.GetComponent<Rigidbody>();
            spaceshipRb.linearDamping = 0f;  // 성운을 벗어날 때 drag 초기화
        }
    }
}
