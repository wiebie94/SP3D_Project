using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class InstantiateCam : MonoBehaviour
{
    public GameObject freeLookPrefab;
    public GameObject lookBackPrefab;
    public Camera camPrefab;
    public GameObject canvasPrefab;

    private List<Camera> cameras = new List<Camera>();
    private List<GameObject> racers = new List<GameObject>();


    public void onPlayerJoined(PlayerInput p)
    {

        // P1: 8
        // P2: 9
        // P3: 10
        // P4: 11

        p.gameObject.transform.position = new Vector3(-20, 200, -460);
        GameObject freeLookCam = Instantiate(freeLookPrefab);
        GameObject lookBackCam = Instantiate(lookBackPrefab);

        p.gameObject.GetComponent<InputHandler>().camBack = lookBackCam;
        p.gameObject.GetComponent<InputHandler>().camNormal = freeLookCam;

        this.racers.Add(p.gameObject);

        Camera c =Instantiate(camPrefab);

        this.cameras.Add(c);

        int numPlayers = racers.Count;

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
    }

    private void createCanvas(Camera c, GameObject gameObject)
    {
        GameObject canvas = Instantiate(canvasPrefab);
        canvas.GetComponent<Canvas>().worldCamera = c;
        canvas.transform.parent = gameObject.transform;
    }


    void Update()
    {
        updateSpeed();
    }

    private void updateSpeed()
    {
        foreach(GameObject racer in this.racers)
        {
            racer.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = racer.GetComponent<RacerMove>().speed.ToString();
        }
    }

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
}
