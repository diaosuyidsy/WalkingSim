using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public int MaxCount = 5;
    public GameObject FirstHallway;
    public GameObject[] HallwayAlternates;
    public GameObject EndingScene;
    public AudioClip[] Noises;
    public AudioMixer NoiseMasterMixer;

    private int HallwayIndex = 0;
    private int count = 0;

    private void Awake ()
    {
        GM = this;
    }

    private void Update ()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast (ray, out hit))
        {
            if (hit.transform.tag == "EndWall")
            {
                hit.transform.gameObject.GetComponent<EndWallControl> ().RevealOtherWall ();
            }

            if (hit.transform.tag == "StrangeThing")
            {
                EndingScene.SetActive (true);
            }
        }
    }

    public bool AddCount ()
    {
        count++;
        // If we counted to Max, then ending comes
        return count == MaxCount;
    }

    public void SetNoiseCutoff (float Cutoff)
    {
        NoiseMasterMixer.SetFloat ("NoiseCutoff", Cutoff);
    }

}
