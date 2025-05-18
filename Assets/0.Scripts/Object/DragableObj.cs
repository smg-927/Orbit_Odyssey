using UnityEngine;

public class DragableObj : MonoBehaviour
{
    public bool isOverlaped { get; set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        isOverlaped = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOverlaped = false;
    }
}
