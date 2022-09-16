using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int index;
    public GameObject gameLogic;
    private RaceLogic m_RaceLogic;

    private void Start()
    {
        this.m_RaceLogic = gameLogic.GetComponent<RaceLogic>();
    }

    /**
     * Is called when a racer passes the checkpoint
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("racer"))
        {
            // Report checkpoint-passing to the racelogic
            this.m_RaceLogic.nextCheckpoint(other.gameObject, this.gameObject);
        }
    }
}
