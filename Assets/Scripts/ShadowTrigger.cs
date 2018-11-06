using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTrigger : MonoBehaviour
{

    public GameObject NormalHallway;

    private bool _entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_entered)
        {
            _entered = true;
            if (other.CompareTag("Player"))
            {

                StartCoroutine(startGlitch(0.3f, 0.3f));

                foreach (var temp in NormalHallway.GetComponentsInChildren<Renderer>())
                {
                    temp.enabled = true;
                }
                transform.parent.parent.position = new Vector3(100f, 100f, 100f);
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
}
