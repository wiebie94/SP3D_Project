using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource hoverHum;
    void Start()
    {
        hoverHum = GetComponents<AudioSource>()[1];
    }

    /**
     * Set the hover-sound pitch depending on the input
     */
    void Update()
    {
        hoverHum.pitch = 1 + (Math.Abs(GetComponent<InputHandler>().acceleration) * 0.6f);
    }
}
