using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    public CameraTransform camtransformlist;
    private int currentCameraIndex;
    void Start()
    {
        mainCamera = Camera.main;
        currentCameraIndex = camtransformlist.cameraTransformList.Count - 1;
        SwitchCamera();
    }
    public void SwitchCamera()
    {
        currentCameraIndex++;
        if (currentCameraIndex >= camtransformlist.cameraTransformList.Count)
        {
            currentCameraIndex = 0;
        }
        Debug.Log("Switching camera to index: " + currentCameraIndex);
        StartCoroutine(SwitchCameraCoroutine(currentCameraIndex));
    }

    IEnumerator SwitchCameraCoroutine(int index)
    {
        float strattime = Time.time;

        while (Time.time - strattime < 0.5f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camtransformlist.cameraTransformList[index].position, Time.deltaTime * 10f);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camtransformlist.cameraTransformList[index].rotation, Time.deltaTime * 10f);
            yield return null;
        }
        mainCamera.transform.position = camtransformlist.cameraTransformList[index].position;
        mainCamera.transform.rotation = camtransformlist.cameraTransformList[index].rotation;
    }
}
