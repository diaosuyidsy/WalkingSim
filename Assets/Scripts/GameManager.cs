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
    public float NoiseCutoff;
    public float perCutoffGrowth = 1000f;
    public bool EndingShow = false;

    private int HallwayIndex = 0;
    private int count = 0;
    private bool StrangeThingShown = false;

    private void Awake()
    {
        GM = this;
    }

    private void Start()
    {
        NoiseMasterMixer.GetFloat("NoiseCutoff", out NoiseCutoff);
    }

    private void Update()
    {
        if (EndingShow)
        {
            RaycastHit[] hits;
            RaycastHit hit1;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit1))
            {
                if (hit1.transform.tag == "EndWall" && !StrangeThingShown)
                {
                    hit1.transform.gameObject.GetComponent<EndWallControl>().RevealOtherWall();
                    StrangeThingShown = true;
                }
            }

            hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "StrangeThing" && StrangeThingShown)
                {
                    hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Run_forward");
                }
            }
        }

    }

    public bool AddCount()
    {
        count++;
        // If we counted to Max, then ending comes
        return count == MaxCount;
    }

    public bool CompareCount(int offset)
    {
        return (count + offset) == MaxCount;
    }

    public void SetNoiseCutoff(float Cutoff)
    {
        NoiseMasterMixer.SetFloat("NoiseCutoff", Cutoff);
    }

    public void SetBlackScreen()
    {
        EndingScene.SetActive(true);
    }

}
