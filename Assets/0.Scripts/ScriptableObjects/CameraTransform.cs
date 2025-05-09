using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public struct TransformData
{
    public Vector3 position;
    public Quaternion rotation;
}

[CreateAssetMenu(fileName = "CameraTransform", menuName = "Scriptable Objects/CameraTransform")]

public class CameraTransform : ScriptableObject
{
    public List<TransformData> cameraTransformList;
}
