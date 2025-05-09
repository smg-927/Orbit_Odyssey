using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] public float mass = 30f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spaceship"))
        {
            collision.gameObject.GetComponent<Spaceship>().Die();
        }
    }
}
