using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceLogic : MonoBehaviour
{
    // A checkpoint stores the checkpoint-object and the rotation that the racer had when it passed the checkpoint
    private struct Checkpoint
    {
        public Checkpoint(GameObject checkPoint, Quaternion rotation)
        {
            CheckPoint = checkPoint;
            Rotation = rotation;
        }

        public GameObject CheckPoint { get; }
        public Quaternion Rotation { get; }
    }
    
    private float raceTimer = 0f;

    public GameObject mainCam;
    public GameObject menuCam;
    private float trackLength = 0.0f;                   // The track length (one lap); Is calculated at start
    private CheckPoint[] checkPointlist;                // A list of all checkpoints for distance calculations
    private float[] checkPointDistanceToStart;          // Stores each checkpoint's distance to the start/finish checkpoint, considering all checkpoints in between
    private SortedList<float, GameObject> positions;    // The current player ranking for one frame
    private float currentPosition = 1;
    public GameObject checkpoints;                      // Parent object of all checkpoints

    // Mapping racer <--> lap
    private Dictionary<GameObject, int> rundenZaehler = new Dictionary<GameObject, int>();

    // Mapping Racer <--> Last Checkpoint, used for respawn and consistency checking
    private Dictionary<GameObject, Checkpoint> checkPointMap = new Dictionary<GameObject, Checkpoint>();
    private bool raceStarted = false;
    public bool menuOpen = false;

    private void Update()
    {
        if (raceStarted) raceTimer += Time.deltaTime;
        updatePlayerPositions();
    }

    /**
     * Calculates the current player rankings
     * Player rankings are identified by calculating each player's distance from the end of the race:   A + B + (raceLaps - drivenLaps) * C
     * A = Player's distance to the next checkpoint
     * B = Distance of the track section from the checkpoint to the start checkpoint
     * C = track length (onelap)
     */
    private void updatePlayerPositions()
    {
        foreach (KeyValuePair<GameObject, int> racerState in this.rundenZaehler)
        {
            float distanceToFinish = 0f;
            // Calculating A
            int currentIndex = checkPointMap[racerState.Key].CheckPoint.GetComponent<CheckPoint>().index;
            int nextIndex = currentIndex + 1; // Index of next checkpoint
            if (nextIndex == checkPointlist.Length) nextIndex = 0;
            distanceToFinish += Vector3.Distance(racerState.Key.transform.position,
                checkPointlist[nextIndex].transform.position);
            
            // B
            distanceToFinish += checkPointDistanceToStart[nextIndex];
            
            // C
            distanceToFinish += (GameOptions.laps - racerState.Value) * trackLength;
            
            positions.Add(distanceToFinish, racerState.Key);

        }
        
        // Sort list => Current Player rankings
        int i = 1;
        foreach (KeyValuePair<float, GameObject> racerDistance in positions)
        {
            racerDistance.Value.GetComponent<UICanvasHandler>().setPosition(i);
            i++;
        }
        
        positions.Clear();
        
    }

    /**
     * Updates each players dictionary entries when they pass a checkpoint and updates their current lap when they pass checkpoint 0
     */
    public void nextCheckpoint(GameObject racer, GameObject checkPoint)
    {

        if (checkPoint.GetComponent<CheckPoint>().index == 0 &&
            (checkPointMap[racer].CheckPoint.GetComponent<CheckPoint>().index == -1 ||
             checkPointMap[racer].CheckPoint.GetComponent<CheckPoint>().index == 14))
        {

            checkPointMap[racer] = new Checkpoint(checkPoint, racer.transform.rotation);
            this.rundenZaehler[racer] = this.rundenZaehler[racer] + 1;
            racer.GetComponent<UICanvasHandler>().setLap(this.rundenZaehler[racer]);

            //stop the racer if he is finished
            if (rundenZaehler[racer] == GameOptions.laps+1)
            {
                racer.GetComponent<InputHandler>().resetInputValues();
                racer.GetComponent<InputHandler>().enabled = false;
                racer.transform.GetChild(2).GetChild(3).GetComponent<UnityEngine.UI.Text>().text = "Finished!";
                this.updateScoreboard(racer);
                this.checkRaceFinished();
            }
            
        }
        else
        {
            // If the player hasn't skipped a checkpoint, update his entry in the checkpoint dictionary
            if (checkPoint.GetComponent<CheckPoint>().index ==
                checkPointMap[racer].CheckPoint.GetComponent<CheckPoint>().index + 1)
            {
                //checkPointMap[racer] = checkPoint;
                checkPointMap[racer] = new Checkpoint(checkPoint, racer.transform.rotation);
            }
        }
    }

    
    private void updateScoreboard(GameObject racer)
    {

        String color;
        String timeNeeded;
        switch (racer.GetComponent<PlayerInput>().playerIndex)
        {
            case 0:
                color = "Blue";
                break;
            case 1:
                color = "Green";
                break;
            case 2:
                color = "Pink";
                break;
            case 3:
                color = "Yellow";
                break;
            default:
                color = "";
                break;
            
        }
        
        TimeSpan ts = TimeSpan.FromSeconds(raceTimer);
        timeNeeded = ts.ToString(@"mm\:ss\:ff");
        mainCam.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text +=
            ("\n" + "#" + (currentPosition++) + ": " + color);
        mainCam.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<UnityEngine.UI.Text>().text +=
            ("\n" + timeNeeded);

    }

    /**
     * Check if all players finished the race.
     * If they did, switch to scoreboard and restart-screen
     */
    private void checkRaceFinished()
    {
        bool raceFinished = true;
        foreach (KeyValuePair<GameObject, int> racerState in this.rundenZaehler)
        {
            if (racerState.Value < GameOptions.laps + 1) raceFinished = false;
        }

        if (raceFinished)
        {
            this.mainCam.SetActive(true);
            this.mainCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            this.mainCam.GetComponent<Camera>().cullingMask =GameObject.Find("PlayerManager").GetComponent<SetupRacer>().maskEverything;
                Transform txt = this.mainCam.transform.GetChild(0);
            txt.GetChild(0).gameObject.SetActive(false);    //info
            txt.GetChild(1).gameObject.SetActive(false);    //start
            txt.GetChild(3).gameObject.SetActive(true);     //restart
            txt.GetChild(4).gameObject.SetActive(true);     //Scoreboard

        }
    }

    /**
     * Insert players into the dictionaries when they join the game
     */
    public void addPlayer(GameObject racer)
        {
            this.rundenZaehler.Add(racer, 0);
            GameObject cP = new GameObject();
            cP.AddComponent<CheckPoint>();
            cP.GetComponent<CheckPoint>().index = -1;
            this.checkPointMap.Add(racer, new Checkpoint(cP, racer.transform.rotation));
        }

        /**
        * Respawn racer at the last checkpoint they passed 
        */
        public void respawnRacer(GameObject racer)
        {
            racer.GetComponent<RacerMove>().resetVelocity();
            racer.GetComponent<ItemScript>().resetItem();

            racer.transform.position = this.checkPointMap[racer].CheckPoint.transform.position;
            racer.transform.rotation = this.checkPointMap[racer].Rotation;

        }

        /**
         * Calculate the tracklength and each track section from checkpoint x to the finish/start checkpoint
         */
        private void Start()
        {
            if (!GameOptions.items)
            {
                GameObject.Find("Items").SetActive(false);
            }
            // tracklength
            this.checkPointlist = this.checkpoints.GetComponentsInChildren<CheckPoint>();
            this.positions = new SortedList<float, GameObject>();
            this.trackLength = calculateCheckpointDistanceToFinish(0);

            // track sections
            this.checkPointDistanceToStart = new float[checkPointlist.Length];
            checkPointDistanceToStart[0] = 0;
            for (int checkPointIndex = 1; checkPointIndex < checkPointDistanceToStart.Length; checkPointIndex++)
            {
                checkPointDistanceToStart[checkPointIndex] = calculateCheckpointDistanceToFinish(checkPointIndex);
            }
        }

        /**
         * helper function for calculating track section length
         */
        private float calculateCheckpointDistanceToFinish(int index1)
        {
            float length = 0f;
            int next;
            for (int i = index1; i < checkPointlist.Length; i++)
            {
                next = i + 1;
                if (i == checkPointlist.Length - 1) next = 0;
                length += Vector3.Distance(checkPointlist[i].transform.position,
                    checkPointlist[next].transform.position);
            }

            return length;
        }

        
        public void resetRaceProgress()
        {
            mainCam.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Text>().text =
                "";
            mainCam.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<UnityEngine.UI.Text>().text =
                "";
            raceTimer = 0f;
            currentPosition = 1;
            raceStarted = false;
            this.rundenZaehler.Clear();
            this.checkPointMap.Clear();
        }

        public void startRace()
        {
            this.raceStarted = true;
        }

        /**
         * Stops all racers and shows pause-menue
         */
        public void openMenuInRace()
        {
            if (raceStarted)
            {
                if (!menuOpen)
                {
                    foreach (KeyValuePair<GameObject, int> racerState in this.rundenZaehler)
                    {
                        racerState.Key.transform.GetChild(2).gameObject.SetActive(false);
                        racerState.Key.GetComponent<RacerMove>().stopVelocity = racerState.Key.GetComponent<Rigidbody>().velocity;
                        racerState.Key.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                        racerState.Key.GetComponent<ItemScript>()._racerMove.enabled = false;
                        racerState.Key.GetComponent<ItemScript>().enabled = false;
                    }
                    menuOpen = true;
                    menuCam.SetActive(true);
                }
                else
                {
                    resetRacerAndUI();
                }
            }
        }

        public void resetRacerAndUI()
        {
            menuOpen = false;
            menuCam.SetActive(false);
            foreach (KeyValuePair<GameObject, int> racerState in this.rundenZaehler)
            {
                racerState.Key.GetComponent<Rigidbody>().velocity = racerState.Key.GetComponent<RacerMove>().stopVelocity;
                racerState.Key.GetComponent<ItemScript>().enabled = true;
                racerState.Key.GetComponent<ItemScript>()._racerMove.enabled = true;
                racerState.Key.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
}
