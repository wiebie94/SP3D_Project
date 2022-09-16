using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyScript : MonoBehaviour
{
    private bool created = false;

    //dont destroy the Music when Switching the Scene
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
}
