using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormerTriggerControl : MonoBehaviour
{
    public GameObject NumberPlates;
    public GameObject NumberPlates2;
    public GameObject Closing;
    public GameObject ShadowHallway;

    private bool _entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_entered)
        {
            _entered = true;
            if (other.CompareTag("Player"))
            {
                GameManager.GM.FormerTriggerCount++;
                // If trigger is the third hallway
                if (GameManager.GM.FormerTriggerCount == 2)
                {
                    StartCoroutine(startGlitch(0.3f, 0.3f));
                    changeAllHallwaysNumberPlate();

                    // Also show closing early
                    Closing.SetActive(true);
                }
                if (GameManager.GM.FormerTriggerCount == 3)
                {
                    StartCoroutine(startGlitch(0.5f, 1f));
                    ShadowHallway.transform.position = transform.parent.parent.position;
                    ShadowHallway.GetComponentInChildren<ConnectionControl>().Closing.SetActive(true);
                    Closing.SetActive(true);

                    foreach (var temp in transform.parent.parent.gameObject.GetComponentsInChildren<Renderer>())
                    {
                        temp.enabled = false;
                    }
                }
            }
        }

    }

    IEnumerator startGlitch(float time, float amount)
    {
        Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = amount;
        Camera.main.GetComponent<Kino.AnalogGlitch>().verticalJump = amount;
        Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = amount;
        Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = amount;


        yield return new WaitForSeconds(time);
        Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = 0f;
        Camera.main.GetComponent<Kino.AnalogGlitch>().verticalJump = 0f;
        Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = 0f;
        Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0f;
    }

    private void changeAllHallwaysNumberPlate()
    {
        foreach (var hallway in GameManager.GM.HallwayAlternates)
        {
            hallway.GetComponentInChildren<FormerTriggerControl>().show2HideNumberPlate();
        }
    }

    public void show2HideNumberPlate()
    {
        NumberPlates.SetActive(false);
        NumberPlates2.SetActive(true);
    }
}
