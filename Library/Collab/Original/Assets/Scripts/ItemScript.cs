using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    //Items
    public enum Item
    {
        None,
        Boost,
        Shoot,
        InvertSteering,
        Frost,
        Shield
    }

    //Prefabs
    public GameObject laserPrefab;
    public GameObject frostPrefab;
    public GameObject shieldPrefab;
    
    private GameObject shield;
    public Item currentItem = Item.None;
    private List<Item> items = new List<Item>();
    public List<GameObject> racers = new List<GameObject>();
    private GameObject gameLogic;
    public RacerMove _racerMove;
    private RaceLogic m_RaceLogic;
    private Rigidbody rb;
    private MeshRenderer ren;

    //ItemTimer and activation Variables
    private float boostTimer = 0f;
    private bool boostActive = false;
    private bool hitByLaser = false;
    private bool invertSteering = false;
    private float invertSteeringTimer = 0f;
    private bool freezeRacer = false;
    private float freezeTimer = 0f;
    private Vector3 currentVelocity;
    private bool shieldActive = false;
    private float shieldTimer = 0;
    private int shotCount;

    //Max Item Times in Seconds
    [Header("Item Settings")]
    public float maxBoostTime;      //1.5
    public float maxInvertTime;     //5
    public float maxFreezeTime;     //3
    public float maxShieldTime;
    public float shieldScale;
    public float dissolveSpeed;
    private float boostStrength = 100;

    private UICanvasHandler ui;

    private void Start()
    {
        this.gameLogic = this.GetComponent<InputHandler>().gameLogic;
        this.ui = GetComponent<UICanvasHandler>();

        this._racerMove = GetComponent<RacerMove>();
        this.m_RaceLogic = gameLogic.GetComponent<RaceLogic>();
        this.rb = GetComponent<Rigidbody>();
        this.ren = GetComponent<MeshRenderer>();
        
        items.Add(Item.Boost);
        items.Add(Item.Shoot);
        items.Add(Item.InvertSteering);
        items.Add(Item.Frost);
        items.Add(Item.Shield);
    }

    public void collectItem(int index)
    {
        if (currentItem == Item.None && !shieldActive)
        {
            currentItem = this.items[index];
            ui.changeItemIcon(currentItem);
            if (currentItem == Item.Shoot || currentItem == Item.Frost) this.shotCount = 3;
        }
    }

    public void useItem()
    {
        if(!isActiveAndEnabled) return;
        Debug.Log(currentItem.ToString());
        /*
         *  TEST
         */

        currentItem = Item.Shoot;
        
        switch (currentItem)
        {
            case Item.Boost:
                useBoost();
                break;
            case Item.Shoot:
                shoot(Item.Shoot);
                break;
            case Item.InvertSteering:
                invertEnemySteering();
                break;
            case Item.Frost:
                shoot(Item.Frost);
                break;
            case Item.Shield:
                protect();
                break;
        }

        Debug.Log(shotCount);
        if(shotCount == 0 ) currentItem = Item.None;
        ui.changeItemIcon(currentItem);
    }

    private void protect()
    {
        shieldActive = true;
    }

    private void invertEnemySteering()
    {
        this.invertSteering = true;
    }

    private void shoot(Item i)
    {
        GameObject laserProjectile;
        if (i == Item.Frost)
        { 
            laserProjectile = GameObject.Instantiate(frostPrefab, transform.position, transform.rotation);
            laserProjectile.GetComponent<Frost_Shot_Behavieour>().emitter = this.gameObject;
        }
        else
        {
            laserProjectile = GameObject.Instantiate(laserPrefab, transform.position, transform.rotation);
            laserProjectile.GetComponent<ShotBehavior>().emitter = this.gameObject;
        }

        this.shotCount--;
    }

    private void useBoost()
    {
        boostActive = true;
        this._racerMove.addBoost = true;
        // Den Booster Partikelemitter aktivieren
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void laserHit()
    {
        this.hitByLaser = true;
    }
    
    private void Update()
    {
        if (boostActive) boostTimer += Time.deltaTime;
        if (boostTimer > maxBoostTime)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
            this._racerMove.addBoost = false;
            this._racerMove.restoreMaxSpeed = true;
            boostActive = false;
            boostTimer = 0f;
        }

        if (hitByLaser)
        {
            if (findChildbyTag("Shield", this.gameObject) == null)
            {
                Material[] mat = (Material[])this.ren.materials.Clone();
                float currentVanishing = this.ren.materials[1].GetFloat("_Vanishing");
                if (currentVanishing > 0.8f)
                {
                    this.m_RaceLogic.respawnRacer(this.gameObject);
                    this.hitByLaser = false;
                    setMaterialsDissolve(0.0f);
                }
                else
                {
                    setMaterialsDissolve(currentVanishing +(Time.deltaTime * dissolveSpeed));
                }
            }
        }

        //Lenkung Invertieren Item
        if (invertSteering)
        {
            if (invertSteeringTimer > maxInvertTime)
                {
                    invertSteering = false;
                    invertSteeringTimer = 0f;
                    foreach(GameObject racer in this.racers)
                    {
                        if (!ReferenceEquals(this.gameObject, racer))
                        {
                            racer.GetComponent<RacerMove>().invertItem = false;
                        }
                    }
                }
                else
                {
                    invertSteeringTimer += Time.deltaTime;
                    // Die Lenkrichtung aller Gegner umkehren
                    foreach(GameObject racer in this.racers)
                    {
                        if (!ReferenceEquals(this.gameObject, racer) && findChildbyTag("Shield", racer.gameObject) == null)
                        {
                            racer.GetComponent<RacerMove>().invertItem = true;
                        }
                    }
                }
        }

        //Freeze Item
        if (freezeRacer)
        {
            if (findChildbyTag("Shield", this.gameObject) == null)
            {
                if (freezeTimer > maxFreezeTime)
                {
                    freezeRacer = false;
                    freezeTimer = 0f;
                    this._racerMove.enabled = true;
                    this.rb.velocity = currentVelocity;
                    findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount = 0;
                }
                else
                {
                    if (findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount < 0.6)
                    {
                        findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount += Time.deltaTime;
                    }
                    freezeTimer += Time.deltaTime;
                }
            }
        }

        //Schild Item
        if (shieldActive)
        {
            //Schild erstellen
            if (shieldTimer < maxShieldTime && findChildbyTag("Shield", this.gameObject) == null)
            {
                shield = Instantiate(shieldPrefab);
                shield.transform.SetParent(this.transform);
                shield.transform.position = this.transform.position;
                shield.transform.rotation = this.transform.rotation;
                shield.transform.localScale *= shieldScale;
                this.GetComponent<MeshRenderer>().enabled = false;
            }
            //Timer
            else if (shieldTimer < maxShieldTime)
            {
                shieldTimer += Time.deltaTime;
            }
           //Schild zerstoeren und Variablen reset
            else
            {
                shieldActive = false;
                shieldTimer = 0;
                Destroy(findChildbyTag("Shield", this.gameObject));
                this.ren.enabled = true;
            }
        }
    }

    private void setMaterialsDissolve(float f)
    {

        Debug.Log(dissolveSpeed);
        
        Material[] mat = (Material[])this.ren.materials.Clone();

        mat[0].SetFloat("_Vanishing", f);
        mat[1].SetFloat("_Vanishing", f);
        mat[2].SetFloat("_Vanishing", f);
        mat[3].SetFloat("_Vanishing", f);
        mat[4].SetFloat("_Vanishing", f);

        this.ren.materials = mat;
    }

    private void OnParticleCollision(GameObject other)
    {

        // Wenn die Partikelkollision nicht vom eigenen Booster kommt
        if (!ReferenceEquals(other.gameObject, this.transform.GetChild(1).gameObject))
        {
            this.laserHit();
        }
    }

    public void freeze()
    {
        currentVelocity = this.rb.velocity;
        this.rb.velocity = new Vector3(0,0,0);
        this._racerMove.enabled = false;
        freezeRacer = true;
    }
    
    //funktion die alle child objekte durchgeht und anhand eines layers zurückgibt
    public GameObject findChildbyTag(String tag, GameObject usedObject)
    {
        foreach (Transform child in usedObject.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
    
    public void resetItem()
    {
        this.shotCount = 0;
        this.currentItem = Item.None;
        this.ui.changeItemIcon(currentItem);
    }
}