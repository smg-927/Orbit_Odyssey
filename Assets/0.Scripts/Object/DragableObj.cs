using UnityEngine;

public class DragableObj : MonoBehaviour
{
    public bool isOverlaped { get; set; } = false;
    private bool currentlyColliding = false;
    Rigidbody rb;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision(enter) name : " + collision.gameObject.name);
        isOverlaped = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collision(exit) name : " + collision.gameObject.name);
        isOverlaped = false;
    }*/

    private void LateUpdate()
    {
        if (!currentlyColliding)
        {
            isOverlaped = false;
        }
        currentlyColliding = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision(stay) name : " + collision.gameObject.name);
        currentlyColliding = true;
        isOverlaped = true;
    }
}
