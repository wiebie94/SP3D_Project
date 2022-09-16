using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;
using UnityEngine.Serialization;

public class SetupRacer : MonoBehaviour
{
    public GameObject freeLookPrefab;
    public GameObject lookBackPrefab;
    public Camera camPrefab;
    public GameObject canvasPrefab;
    public GameObject countdownCanvas;
    public GameObject mainCam;
    public GameObject gameLogic;

    private List<Camera> cameras = new List<Camera>();
    public List<GameObject> racers = new List<GameObject>();
    
    public int maskEverything;

    public List<Material> ledMaterials;

    public void onPlayerJoined(PlayerInput p)
    {

        positionRacer(p.gameObject);
        
        // Instantiate and assign cameras to the player
        GameObject freeLookCam = Instantiate(freeLookPrefab);
        GameObject lookBackCam = Instantiate(lookBackPrefab);

        p.gameObject.GetComponent<InputHandler>().camBack = lookBackCam;
        p.gameObject.GetComponent<InputHandler>().camNormal = freeLookCam;
        p.gameObject.GetComponent<InputHandler>().gameLogic = this.gameLogic;

        this.racers.Add(p.gameObject);

        Camera c =Instantiate(camPrefab);

        this.cameras.Add(c);

        cameras[racers.Count - 1].gameObject.SetActive(false);
        
        int numPlayers = racers.Count;
        
        // Configure the (virtual) cameras to look at the player
        freeLookCam.GetComponent<CinemachineFreeLook>().Follow = p.gameObject.transform;
        freeLookCam.GetComponent<CinemachineFreeLook>().LookAt = p.gameObject.transform;

        lookBackCam.GetComponent<CinemachineVirtualCamera>().Follow = p.gameObject.transform;
        lookBackCam.GetComponent<CinemachineVirtualCamera>().LookAt = p.gameObject.transform;

        freeLookCam.layer = LayerMask.NameToLayer("P" + numPlayers);
        freeLookCam.GetComponent<CinemachineInputProvider>().PlayerIndex = p.playerIndex;

        lookBackCam.layer = LayerMask.NameToLayer("P" + numPlayers);

        int oldMask = c.cullingMask;
        int newMask = oldMask;

        switch (numPlayers)
        {
            case 1:
                newMask = oldMask & ~(1 << 9);
                newMask = newMask & ~(1 << 10);
                newMask = newMask & ~(1 << 11);
                break;

            case 2:
                newMask = oldMask & ~(1 << 8);
                newMask = newMask & ~(1 << 10);
                newMask = newMask & ~(1 << 11);
                break;
            case 3:
                newMask = oldMask & ~(1 << 8);
                newMask = newMask & ~(1 << 9);
                newMask = newMask & ~(1 << 11);
                break;
            case 4:
                newMask = oldMask & ~(1 << 8);
                newMask = newMask & ~(1 << 9);
                newMask = newMask & ~(1 << 10);
                break;
        }

        c.cullingMask = newMask;

        arrangeSplitscreen();
        createCanvas(c, p.gameObject);

        freeLookCam.transform.parent = p.gameObject.transform;
        lookBackCam.transform.parent = p.gameObject.transform;

        // Set the individual color for the racer (booster and backlight) based on the index
        Material individualPlayerColor = this.ledMaterials[p.playerIndex];
        Material[] mat = (Material[])p.gameObject.GetComponent<MeshRenderer>().materials.Clone();
        mat[2] = individualPlayerColor;
        mat[3] = individualPlayerColor;
        p.gameObject.GetComponent<MeshRenderer>().materials = mat;
        ParticleSystem.MainModule ps = p.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
        ps.startColor = new Color(individualPlayerColor.color.r, individualPlayerColor.color.g,
            individualPlayerColor.color.b);
        p.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Light>().color = individualPlayerColor.color;

        p.gameObject.GetComponent<InputHandler>().enabled = false;
        p.gameObject.GetComponent<RacerMove>().enabled = false;

        p.gameObject.GetComponent<ItemScript>().racers = this.racers;

        c.transform.parent = p.gameObject.transform;
        addPlayerToGameLogic(p.gameObject);
    }

    private void addPlayerToGameLogic(GameObject racer)
    {
        this.gameLogic.GetComponent<RaceLogic>().addPlayer(racer);
    }

    /**
     * Creates the UI overlay for each player
     */
    private void createCanvas(Camera c, GameObject gameObject)
    {
        GameObject canvas = Instantiate(canvasPrefab);
        canvas.GetComponent<Canvas>().worldCamera = c;
        canvas.GetComponent<Canvas>().planeDistance = 0.7f;
        canvas.transform.SetParent(gameObject.transform, false);
        canvas.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = "0/" + GameOptions.laps;
    }

    /**
     * Dynamically arrange splitscreen based on the amount of players
     */
    private void arrangeSplitscreen()
    {
        if(racers.Count == 2)
        {
            cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 1);
            cameras[1].GetComponent<Camera>().rect = new Rect(0, -0.5f, 1, 1);
        }

        if(racers.Count == 3)
        {
            cameras[0].GetComponent<Camera>().rect = new Rect(-0.5f, 0.5f, 1, 1);
            cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 1, 1);
            cameras[2].GetComponent<Camera>().rect = new Rect(0.25f, -0.5f, 0.5f, 1);
        }

        if (racers.Count == 4)
        {
            cameras[0].GetComponent<Camera>().rect = new Rect(-0.5f, 0.5f, 1, 1);
            cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 1, 1);
            cameras[2].GetComponent<Camera>().rect = new Rect(-0.5f, -0.5f, 1, 1);
            cameras[3].GetComponent<Camera>().rect = new Rect(0.5f, -0.5f, 1, 1);
        }
    }

    /**
     * At race start, activates all player cameras.
     * Adjusts UI arrangement if there's only two player (different screen ratio)
     */
    public void enableCameras()
    {
        if (racers.Count == 2)
        {
            foreach (GameObject racer in racers)
            {
                racer.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-654, 62);
                racer.transform.GetChild(2).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-886, -312);
                racer.transform.GetChild(2).GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(769, -160);
                racer.transform.GetChild(2).GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(426, 88);
                racer.transform.GetChild(2).GetChild(6).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 174);
            }
        }
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(true);
        }
        this.mainCam.SetActive(false);
        StartCoroutine(countdown(3));
    }

    /**
     * Shows countdown canvas and enables input if the race started
     */
    IEnumerator countdown(int i)
    {
        int count = i;
        this.countdownCanvas.SetActive(true);
        
        while (count > 0)
        {
            this.countdownCanvas.GetComponent<UnityEngine.UI.Text>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        this.countdownCanvas.SetActive(false);
        this.gameLogic.GetComponent<RaceLogic>().startRace();
        allowInput();
    }

    public void allowInput()
    {
        foreach (GameObject racer in racers)
        {
            racer.GetComponent<InputHandler>().enabled = true;
            racer.gameObject.GetComponent<RacerMove>().enabled = true;
        }
    }

    public void restartRace()
    {
        this.mainCam.SetActive(true);
        this.mainCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        this.mainCam.GetComponent<Camera>().cullingMask =GameObject.Find("PlayerManager").GetComponent<SetupRacer>().maskEverything;
        
        gameLogic.GetComponent<RaceLogic>().resetRacerAndUI();
        
        this.gameLogic.GetComponent<RaceLogic>().resetRaceProgress();
        foreach (GameObject racer in racers)
        {
            racer.GetComponent<UICanvasHandler>().resetUI();
            this.gameLogic.GetComponent<RaceLogic>().addPlayer(racer);
            racer.GetComponent<InputHandler>().enabled = false;
            racer.GetComponent<RacerMove>().enabled = false;
            racer.GetComponent<ItemScript>().resetItem();

            positionRacer(racer);
        }
        startGame();
    }

    /**
     * Positions the new player based on their index
     */
    private void positionRacer(GameObject racer)
    {
        racer.transform.rotation = Quaternion.identity;
        
        switch (racer.GetComponent<PlayerInput>().playerIndex)
        {
            case 0:
                racer.transform.position = new Vector3(-20f, 10f, 270f);
                break;
            case 1:
                racer.transform.position = new Vector3(23f, 10f, 270f);
                break;
            case 2:
                racer.transform.position = new Vector3(-20f, 10f, 220f);
                break;
            case 3:
                racer.transform.position = new Vector3(23f, 10f, 220f);
                break;
        }
        racer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void startGame()
    {
        maskEverything = mainCam.GetComponent<Camera>().cullingMask;
        if (racers.Count > 0)
        {
            this.mainCam.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            this.mainCam.GetComponent<Camera>().cullingMask = 0;
            StartCoroutine(waiter());
        }
        
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForEndOfFrame();
        enableCameras();
    }
}
