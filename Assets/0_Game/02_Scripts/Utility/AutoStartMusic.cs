using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AutoStartMusic : MonoBehaviour
{
    public StudioEventEmitter music;
    public float delay = 0;
    private float timer = 0;
    private bool done = false;

    private void Start()
    {
        if (delay == 0)
        {
            music.Play();
            done = true;
        }
    }

    private void Update()
    {
        if (!done)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                music.Play();
                done = true;
            }
        }
    }
}
