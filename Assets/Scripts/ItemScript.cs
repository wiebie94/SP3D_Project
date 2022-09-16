using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    //Available Items
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
    public float maxBoostTime;
    public float maxInvertTime;
    public float maxFreezeTime;
    public float maxShieldTime;
    public float shieldScale;
    public float dissolveSpeed;
    private float boostStrength = 100;

    private UICanvasHandler ui;

    
    //Audio sound effects
    public AudioSource laserSound;
    public AudioSource freezeSound;
    public AudioSource dissolveSound;
    public AudioSource boostSound;
    
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

    /**
     * Collect the item which corresponds to the passed value by the item-box
     */
    public void collectItem(int index)
    {
        if (currentItem == Item.None && !shieldActive)
        {
            currentItem = this.items[index];
            ui.changeItemIcon(currentItem);
            if (currentItem == Item.Shoot || currentItem == Item.Frost) this.shotCount = 3;
        }
    }

    /**
     * Is executed when use-item-button is pressed
     * Depending on the item, a function is called
     */
    public void useItem()
    {
        if(!isActiveAndEnabled) return;
        
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

        if(shotCount == 0 ) currentItem = Item.None;
        ui.changeItemIcon(currentItem);
    }

    private void protect()
    {
        this.shieldActive = true;
    }

    private void invertEnemySteering()
    {
        this.invertSteering = true;
    }

    /**
     * Instantiate a projectile (frost or laser depending on the current item)
     */
    private void shoot(Item i)
    {
        
        laserSound.Play();
        GameObject laserProjectile;
        if (i == Item.Frost)
        { 
            laserProjectile = GameObject.Instantiate(frostPrefab, transform.position, transform.rotation);
            // Set emitter so that the projectile doesn't register a collision with the racer firing the laser
            laserProjectile.GetComponent<Frost_Shot_Behavieour>().emitter = this.gameObject;
        }
        else
        {
            laserProjectile = GameObject.Instantiate(laserPrefab, transform.position, transform.rotation);
            laserProjectile.GetComponent<ShotBehavior>().emitter = this.gameObject;
        }

        // Decrease shot count by one
        this.shotCount--;
    }

    private void useBoost()
    {
        boostSound.Play();
        boostActive = true;
        this._racerMove.addBoost = true;
        // Activate boost particle emitter
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void laserHit()
    {
        dissolveSound.Play();
        this.hitByLaser = true;
    }
    
    /**
     * For each item that's active over a timespan, a bool variable is set
     */
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

        // If this racers was recently hit by a laser
        if (hitByLaser)
        {
            // If it doesn't have a shield, dissolve it and trigger the respawn 
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
            // Loop over all enemies. Invert their steering if they do not have a shield
                else
                {
                    invertSteeringTimer += Time.deltaTime;
                    foreach(GameObject racer in this.racers)
                    {
                        if (!ReferenceEquals(this.gameObject, racer) && findChildbyTag("Shield", racer.gameObject) == null)
                        {
                            racer.GetComponent<RacerMove>().invertItem = true;
                        }
                    }
                }
        }

        //If this racer was hit by the frost item
        if (freezeRacer)
        {
            if (findChildbyTag("Shield", this.gameObject) == null)
            {
                if (freezeTimer > maxFreezeTime)
                {
                    // If frost duration exceeded the max timer, restore the rigidbody's velocity and reactivate the movement-component
                    freezeRacer = false;
                    freezeTimer = 0f;
                    this._racerMove.enabled = true;
                    this.rb.velocity = currentVelocity;
                    findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount = 0;
                }
                else
                {
                    // Gradually add frost effect to the players camera
                    if (findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount < 0.6)
                    {
                        findChildbyTag("MainCamera", this.gameObject).GetComponent<FrostEffect>().FrostAmount += Time.deltaTime;
                    }
                    freezeTimer += Time.deltaTime;
                }
            }
        }

        
        if (shieldActive)
        {
            //Instantiate shield
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
           //Destroy shield if timer exceeded
            else
            {
                shieldActive = false;
                shieldTimer = 0;
                Destroy(findChildbyTag("Shield", this.gameObject));
                this.ren.enabled = true;
            }
        }
    }

    /**
     * Set the shader vanishing value for all materials on the racer
     * Is used for dissolve-effect that is triggered by laser or booster hits 
     */
    private void setMaterialsDissolve(float f)
    {
        
        Material[] mat = (Material[])this.ren.materials.Clone();

        mat[0].SetFloat("_Vanishing", f);
        mat[1].SetFloat("_Vanishing", f);
        mat[2].SetFloat("_Vanishing", f);
        mat[3].SetFloat("_Vanishing", f);
        mat[4].SetFloat("_Vanishing", f);

        this.ren.materials = mat;
    }

    /**
     * Is called when the racer hits an enemie's booster.
     * Triggers the dissolve effect and respawn
     */
    private void OnParticleCollision(GameObject other)
    {
        
        if (!ReferenceEquals(other.gameObject, this.transform.GetChild(1).gameObject))
        {
            this.laserHit();
        }
    }

    /**
     * Store the current velocity and disable movement
     */
    public void freeze()
    {
        freezeSound.Play();
        currentVelocity = this.rb.velocity;
        this.rb.velocity = new Vector3(0,0,0);
        this._racerMove.enabled = false;
        freezeRacer = true;
    }
    
    
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