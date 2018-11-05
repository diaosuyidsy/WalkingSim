using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeThingControl : MonoBehaviour
{
    public GameObject theOtherStrangeThing;

    private void FixedUpdate()
    {
        transform.LookAt(GameObject.FindWithTag("Player").transform);
    }

    public void StartBlackScreen()
    {
        GameManager.GM.SetBlackScreen();
    }

    public void OnShownThis()
    {
        foreach (Collider c in theOtherStrangeThing.GetComponents<Collider>())
        {
            c.enabled = false;
        }
    }


}
