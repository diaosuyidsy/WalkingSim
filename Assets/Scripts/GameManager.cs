using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public int MaxCount = 5;
    public GameObject FirstHallway;
    public GameObject[] HallwayAlternates;
    public GameObject EndingScene;
    private int HallwayIndex = 0;
    private int count = 0;

    private void Awake ()
    {
        GM = this;
    }

    private void Update ()
    {
        // Test
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

    public void ChangeWall (Vector3 Pos, float Rot)
    {
        // Get the current Hallway Alternates that gets moved
        var thisWall = HallwayAlternates[HallwayIndex];
        thisWall.SetActive (true);
        thisWall.transform.position = Pos;
        thisWall.transform.eulerAngles = new Vector3 (0, Rot, 0);
        // Then change the hallway index to for next access of Hallway Alternates
        Debug.Log (HallwayIndex);
        if (HallwayIndex == 3)
            Destroy (FirstHallway);
        HallwayIndex++;
        if (HallwayIndex > HallwayAlternates.Length - 1)
            HallwayIndex = 0;
    }

}
