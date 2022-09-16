using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    public bool isPlay;
    public bool isOptions;
    public bool isQuit;
    public bool isBack;
    public bool isItems;
    public bool isRounds;

    public bool itemsActive = true; //Sind Items aktiviert?
    public int rounds = 3;  //Aktuelle RundenZahö

    void OnMouseUp()
    {
        //Start Game
        if (isPlay)
        {
            SceneManager.LoadScene("GameScene");
        }

        //go to Options
        if (isOptions)
        {
            GameObject.Find("Name").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Play").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Options").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Quit").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;

            GameObject.Find("backToMain").GetComponent<Renderer>().enabled = true;
            GameObject.Find("backToMain").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Items").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Items").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Rounds").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Rounds").GetComponent<BoxCollider>().enabled = true;
        }

        //Item Options
        if (isItems)
        {
            if (GameObject.Find("Items").GetComponent<TextMesh>().color == Color.green)
            {
                GameObject.Find("Items").GetComponent<TextMesh>().color = Color.red;
                GameObject.Find("Items").GetComponent<TextMesh>().text = "Items inactive";
                itemsActive = false;
            }
            else
            {
                GameObject.Find("Items").GetComponent<TextMesh>().color = Color.green;
                GameObject.Find("Items").GetComponent<TextMesh>().text = "Items active";
                itemsActive = true;
            }
        }

        if (isRounds)
        {
            if (rounds < 6)
            {
                rounds++;
                GameObject.Find("Rounds").GetComponent<TextMesh>().text = "Rounds : " + rounds;
                
            }
            else
            {
                GameObject.Find("Rounds").GetComponent<TextMesh>().text = "Rounds : 1";
                rounds = 1;
            }
            
        }
        
        //back to Main Menu
        if (isBack)
        {
            GameObject.Find("Name").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Play").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Options").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Quit").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;

            GameObject.Find("backToMain").GetComponent<Renderer>().enabled = false;
            GameObject.Find("backToMain").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Items").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Items").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Rounds").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Rounds").GetComponent<BoxCollider>().enabled = false;
        }
        
        //Quit Game
        if(isQuit)
        {
            Application.Quit();
        }
    }
}
