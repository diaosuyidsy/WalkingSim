using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPositionSetup : MonoBehaviour
{

    public Transform[] Walls;

    void Start()
    {
        float temp = 0f;
        for (int i = 0; i < Walls.Length; i++)
        {
            temp += Walls[i].position.z;
        }
        temp = temp / Walls.Length;
        Debug.Log(temp);
        transform.position = new Vector3(transform.position.x, transform.position.y, temp);
    }
}
