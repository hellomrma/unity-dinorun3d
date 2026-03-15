using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector3 mapSize;
    public float GetMapSize()
    {
        return mapSize.z;
    }
}
