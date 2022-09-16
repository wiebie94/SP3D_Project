using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{

    public GameObject gameLogic;
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic");
    }

    // Respawns a player when they pass the collider

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("racer"))
        {
            gameLogic.GetComponent<RaceLogic>().respawnRacer(other.gameObject);
        }
    }
}
