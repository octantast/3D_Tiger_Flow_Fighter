using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GeneralController general;
    private float thisindex;

    public int currentPlatform;
    void Start()
    {
        thisindex = Random.Range(1, 1.5f);
    }

    public void Update()
    {
        if (!general.paused)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0, -0.1f), general.ui.wavespeed * thisindex * Time.deltaTime);
            if(transform.localPosition.z <= 0)
            {
                general.waveHere(gameObject, this);
                this.enabled = false;
            }
        }
    }
}
