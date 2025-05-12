using UnityEngine;

public class Blackhole : Planet
{
    [SerializeField] public GameObject whitehole;

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Spaceship"))
        {
            GameManager.Instance.PlaySoundEffect("warp");
            other.gameObject.transform.position = whitehole.transform.position;
        }*/

        if (other.gameObject.CompareTag("Spaceship"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 incomingVelocity = rb.linearVelocity;
                other.transform.position = whitehole.transform.position;
                rb.linearVelocity = incomingVelocity;
                GameManager.Instance.PlaySoundEffect("warp");
            }
            else
            {
                other.transform.position = whitehole.transform.position;
            }

            other.gameObject.transform.position = whitehole.transform.position;
        }
    }
}