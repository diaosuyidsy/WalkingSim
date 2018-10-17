using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWallControl : MonoBehaviour
{
    public GameObject StrangeThing;
    public GameObject theOtherWall;

    private bool FirstTime = true;

    public void RevealOtherWall()
    {
        if (!FirstTime)
            return;
        FirstTime = false;
        theOtherWall.SetActive(false);
        StrangeThing.SetActive(true);
    }
}
