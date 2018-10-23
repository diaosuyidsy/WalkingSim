using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionControl : MonoBehaviour
{
    //public Transform ConnectionPrePos1;
    //public Transform ConnectionPrePos2;
    public Transform ConnectionRight;
    public Transform ConnectionLeft;
    public GameObject EndingHolder;
    public GameObject ConnectionRightWall;
    public GameObject ConnectionLeftWall;
    public GameObject Closing;
    public bool FirstEntry = true;

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            // If not first time entry, don't do anything
            if (FirstEntry)
            {
                FirstEntry = false;

                // We need to set all other hallways' FirstEntry true
                setOtherHallwaysFirstEntryTrue ();

                // Connect other hallways
                ConnectTwoHallways ();

                // Show the closing so that player cannot walk back
                if (Closing != null)
                    Closing.SetActive (true);

                // If this is true, then need to display the ending
                if (GameManager.GM.AddCount ())
                {
                    Debug.Log ("Set Ending True");
                    EndingHolder.SetActive (true);
                }
            }
        }
    }

    void ConnectTwoHallways ()
    {
        ConnectionRightWall.transform.position = ConnectionRight.position;
        ConnectionLeftWall.transform.position = ConnectionLeft.position;
        if (transform.parent.parent.gameObject != GameManager.GM.FirstHallway)
            GameManager.GM.FirstHallway.SetActive (false);
    }


    void Connect (Vector3 Pos, Transform PrePos)
    {
        // Get the correct Rotation
        var connectionZ = Pos.z - PrePos.position.z;
        var connectionX = Pos.x - PrePos.position.x;
        var angle = Mathf.Atan2 (connectionZ, connectionX);
        var angleInDegree = Mathf.Rad2Deg * angle;
        angleInDegree *= -1;

        GameManager.GM.ChangeWall (Pos, angleInDegree);
    }

    public void setOtherHallwaysFirstEntryTrue ()
    {
        for (int i = 0; i < GameManager.GM.HallwayAlternates.Length; i++)
        {
            if (GameManager.GM.HallwayAlternates[i] != transform.parent.gameObject)
            {
                GameManager.GM.HallwayAlternates[i].GetComponentInChildren<ConnectionControl> ().FirstEntry = true;
                // Also set the closing to false
                GameManager.GM.HallwayAlternates[i].GetComponentInChildren<ConnectionControl> ().Closing.SetActive (false);
            }
        }
    }
}
