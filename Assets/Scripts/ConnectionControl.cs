using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionControl : MonoBehaviour
{

    public Transform ConnectionRight;
    public Transform ConnectionLeft;
    public GameObject EndingHolder;
    public GameObject ConnectionRightWall;
    public GameObject ConnectionLeftWall;
    public GameObject Closing;
    public bool FirstEntry = true;
    private float NoiseRaiseTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // If not first time entry, don't do anything
            if (FirstEntry)
            {
                FirstEntry = false;

                // We need to set all other hallways' FirstEntry true
                setOtherHallwaysFirstEntryTrue();

                // Connect other hallways
                ConnectTwoHallways();

                // Show the closing so that player cannot walk back
                if (Closing != null)
                    Closing.SetActive(true);

                // If this is true, then need to display the ending
                if (GameManager.GM.AddCount())
                {
                    Debug.Log("Set Ending True");
                    GameManager.GM.EndingShow = true;
                    EndingHolder.SetActive(true);
                }
                else
                {
                    if (GameManager.GM.CompareCount(1))
                    {
                        GameManager.GM.NoiseMasterMixer.SetFloat("NoiseCutoff", 500f);
                    }
                    else
                        // If not, then turn up the volume
                        StartCoroutine(RaiseNoiseCutoff());
                }
            }
        }
    }

    IEnumerator RaiseNoiseCutoff()
    {
        while (NoiseRaiseTime > 0f)
        {
            NoiseRaiseTime -= Time.deltaTime;
            GameManager.GM.NoiseMasterMixer.SetFloat("NoiseCutoff", GameManager.GM.NoiseCutoff);
            GameManager.GM.NoiseCutoff += Time.deltaTime * 200f;
            yield return null;
        }
    }


    void ConnectTwoHallways()
    {
        ConnectionRightWall.transform.position = ConnectionRight.position;
        ConnectionLeftWall.transform.position = ConnectionLeft.position;
        if (transform.parent.parent.gameObject != GameManager.GM.FirstHallway)
            GameManager.GM.FirstHallway.SetActive(false);
    }

    public void setOtherHallwaysFirstEntryTrue()
    {
        for (int i = 0; i < GameManager.GM.HallwayAlternates.Length; i++)
        {
            if (GameManager.GM.HallwayAlternates[i] != transform.parent.gameObject)
            {
                GameManager.GM.HallwayAlternates[i].GetComponentInChildren<ConnectionControl>().FirstEntry = true;
                // Also set the closing to false
                GameManager.GM.HallwayAlternates[i].GetComponentInChildren<ConnectionControl>().Closing.SetActive(false);
            }
        }
    }
}
