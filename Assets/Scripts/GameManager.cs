using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public GameObject Player;
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
    private int turnTimes = 2;
    private float playerYRotation;
    private bool playerStartGlitch = false;
    private float maxDifference = 0f;

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
                    if (!hit1.transform.gameObject.GetComponent<EndWallControl>().BeingLookedAt)
                    {
                        turnTimes--;
                        hit1.transform.gameObject.GetComponent<EndWallControl>().LookedAtThisWall();
                        if (turnTimes == 0)
                        {
                            hit1.transform.gameObject.GetComponent<EndWallControl>().RevealOtherWall();
                            StrangeThingShown = true;
                            // Start the behavoir of glitch
                            TurnGlitchStart();
                        }
                    }
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

        if (playerStartGlitch)
        {
            float difference = Mathf.Abs(Player.transform.rotation.eulerAngles.y - playerYRotation);
            Debug.Log("PlayerYRotation: " + playerYRotation);
            Debug.Log(Player.transform.rotation.eulerAngles.y);
            Debug.Log(maxDifference);
            if (difference > maxDifference)
            {
                maxDifference = difference;
                float percent = maxDifference / 120f;
                Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = 0.3f * percent;
                Camera.main.GetComponent<Kino.AnalogGlitch>().verticalJump = 0.3f * percent;
                Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = 0.3f * percent;
                Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0.3f * percent;

            }
        }

    }

    private void TurnGlitchStart()
    {
        playerYRotation = Player.transform.rotation.eulerAngles.y;
        playerStartGlitch = true;
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
