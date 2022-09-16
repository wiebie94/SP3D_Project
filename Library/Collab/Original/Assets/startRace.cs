using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startRace : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerManager;
    void Start()
    {
        
    }

    public void startGame()
    {
        Debug.Log("Startgame");
        this.playerManager.GetComponent<SetupRacer>().enableCameras();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
