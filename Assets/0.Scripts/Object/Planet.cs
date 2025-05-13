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
                Destroy(collision.gameObject);
                GameManager.Instance.PlaySoundEffect("clear");
                GameManager.Instance.ChangeGameState("Win");
            }
            else
            {
                GameManager.Instance.PlaySoundEffect("destroy");
                collision.gameObject.GetComponent<Spaceship>().Die();
            }
        }
    }

    
}
