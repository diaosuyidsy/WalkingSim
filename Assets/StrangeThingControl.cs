using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeThingControl : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.LookAt(GameObject.FindWithTag("Player").transform);
    }

    public void StartBlackScreen()
    {
        GameManager.GM.SetBlackScreen();
    }

}
