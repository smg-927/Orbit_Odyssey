using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] public float mass = 30f;
    [SerializeField] public bool isgoal = false;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spaceship"))
        {
            if(isgoal)
            {
                GameManager.Instance.ChangeGameState("Win");
            }
            else
            {
                collision.gameObject.GetComponent<Spaceship>().Die();
            }
        }
    }
}
