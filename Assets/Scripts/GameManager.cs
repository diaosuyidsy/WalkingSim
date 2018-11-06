using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
    public AudioSource GlitchFinalSoundEffect;
    public AudioSource GlitchCasualSoundEffect;
    public AudioSource FarawayRingEffect;
    public int FormerTriggerCount = 0;

    private int HallwayIndex = 0;
    private int count = 0;
    private bool StrangeThingShown = false;
    private int turnTimes = 2;
    private bool playerStartGlitch = false;
    private bool playedSound = false;
    private GameObject theRevealedMonster;
    public bool stopTurningUpVolumn = false;

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
                            theRevealedMonster = hit1.transform.gameObject.GetComponent<EndWallControl>().StrangeThing;
                            StrangeThingShown = true;
                            // Start the behavoir of glitch
                            TurnGlitchStart();
                            // Also, start the ringing behind the player
                            FarawayRingEffect.Play();
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
                    // Start Glitch Sound Effect
                    if (!playedSound)
                    {
                        playedSound = true;
                        GlitchFinalSoundEffect.Play();
                    }
                }
            }
        }

        if (playerStartGlitch)
        {
            Vector3 targetDir = theRevealedMonster.transform.position - Player.transform.position;
            float angleBetween = Vector3.Angle(Player.transform.forward, targetDir);
            float percent = 1 - (Mathf.Abs(angleBetween) / 180f);
            Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = 0.3f * percent;
            Camera.main.GetComponent<Kino.AnalogGlitch>().verticalJump = 0.3f * percent;
            Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = 0.3f * percent;
            Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0.3f * percent;
        }

    }

    private void TurnGlitchStart()
    {

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
        StartCoroutine(SwitchToMenu());
    }

    IEnumerator SwitchToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MenuScene");
    }
}
