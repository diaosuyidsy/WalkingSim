using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWallControl : MonoBehaviour
{
    public GameObject StrangeThing;
    public GameObject theOtherWall;
    public bool BeingLookedAt = false;

    private bool FirstTime = true;

    public void RevealOtherWall()
    {
        if (!FirstTime)
            return;
        FirstTime = false;
        theOtherWall.SetActive(false);
        StrangeThing.SetActive(true);
        // Disable the other Strange Thing colliders
        StrangeThing.GetComponent<StrangeThingControl>().OnShownThis();
    }

    public void LookedAtThisWall()
    {
        BeingLookedAt = true;
        theOtherWall.GetComponent<EndWallControl>().BeingLookedAt = false;
    }
}
