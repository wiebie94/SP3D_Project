using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class startRace : MonoBehaviour
{

    public GameObject playerManager;

    public void startGame()
    {

        playerManager.GetComponent<SetupRacer>().startGame();
        
    }

    IEnumerator waiter()
    {
        yield return new WaitForEndOfFrame();
        this.playerManager.GetComponent<SetupRacer>().enableCameras();
    }
    
    public void backToMenu()
    {
        Destroy(GameObject.Find("BackgroundMusik"));
        SceneManager.LoadScene("Menu");
        
    }
}
