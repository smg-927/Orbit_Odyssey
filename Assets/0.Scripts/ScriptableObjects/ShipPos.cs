using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShipStartPos", menuName = "Scriptable Objects/ShipStartPos")]
public class ShipStartPos : ScriptableObject
{
    public List<Vector2> list_shipStartPos;
}
