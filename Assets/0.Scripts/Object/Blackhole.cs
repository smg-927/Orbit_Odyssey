using UnityEngine;

public class Blackhole : Planet
{
    [SerializeField] public GameObject whitehole;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Spaceship"))
        {
            other.gameObject.transform.position = whitehole.transform.position;
        }
    }
}