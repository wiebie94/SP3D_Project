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
    public bool isResolution;
    public Material optBackground;
    public Material normalBackground;

    private void Start()
    {
        GameObject.Find("Rounds").GetComponent<TextMesh>().text = "Laps : "  + GameOptions.laps;
        GameObject.Find("Resolution").GetComponent<TextMesh>().text = "Resolution : " + Screen.width + "x" + Screen.height;
    }


    void OnMouseUp()
    {
        //Start Game, change Scene
        if (isPlay)
        {
            SceneManager.LoadScene("GameScene");
        }

        /*
         * go to Options
         * enables and disables the Buttons and Pictures
         */
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
            GameObject.Find("Resolution").GetComponent<Renderer>().enabled = true;
            GameObject.Find("Resolution").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("ControllerInstructions").GetComponent<Renderer>().enabled = true;

            GameObject.Find("Background").GetComponent<MeshRenderer>().material = optBackground;
        }

        //Item Options
        if (isItems)
        {
            if (GameObject.Find("Items").GetComponent<TextMesh>().color == Color.green)
            {
                GameObject.Find("Items").GetComponent<TextMesh>().color = Color.red;
                GameObject.Find("Items").GetComponent<TextMesh>().text = "Items inactive";
                GameOptions.items = false;
            }
            else
            {
                GameObject.Find("Items").GetComponent<TextMesh>().color = Color.green;
                GameObject.Find("Items").GetComponent<TextMesh>().text = "Items active";
                GameOptions.items = true;
            }
        }

        if (isRounds)
        {
            if (GameOptions.laps < 6)
            {
                GameOptions.laps++;
                GameObject.Find("Rounds").GetComponent<TextMesh>().text = "Laps : " + GameOptions.laps;
                
            }
            else
            {
                GameObject.Find("Rounds").GetComponent<TextMesh>().text = "Laps : 1";
                GameOptions.laps = 1;
            }
            Debug.Log(GameOptions.laps);

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
            GameObject.Find("Resolution").GetComponent<Renderer>().enabled = false;
            GameObject.Find("Resolution").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("ControllerInstructions").GetComponent<Renderer>().enabled = false;

            GameObject.Find("Background").GetComponent<MeshRenderer>().material = normalBackground;
        }

        //Switching Resolutions
        if (isResolution)
        {
            switch (Screen.width)
            {
                case 1920:
                    Screen.SetResolution(1280, 720, true);
                    break;
                case 1280:
                    Screen.SetResolution(1024, 576, true);
                    break;
                case 1024:
                    Screen.SetResolution(768, 432, true);
                    break;
                case 768:
                    Screen.SetResolution(640, 360, true);
                    break;
                case 640:
                    Screen.SetResolution(1920, 1080, true);
                    break;
            }
            StartCoroutine(waitForFrame());
        }

        IEnumerator waitForFrame()
        {
            yield return null;
            yield return null;
            setRes();
        }

        void setRes()
        {
            GameObject.Find("Resolution").GetComponent<TextMesh>().text = "Resolution : " + Screen.width + "x" + Screen.height;
        }
        
        //Quit Game
        if(isQuit)
        {
            Application.Quit();
        }
    }
}
