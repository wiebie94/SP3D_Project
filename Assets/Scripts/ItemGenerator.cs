using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    private System.Random random;
    private static int randomSeed = 0;
    private bool isActive;
    private float inactiveTimer = 0f;
    private MeshRenderer ren;
    private BoxCollider coll;
    private AudioSource ItemSound;

    private void Start()
    {
        this.random = new System.Random(++randomSeed);
        this.ren = this.GetComponent<MeshRenderer>();
        this.coll = this.GetComponent<BoxCollider>();
        this.ItemSound = this.GetComponentInParent<AudioSource>();
    }

    /**
     * Is called when a racer passes the item-box.
     * Creates random number which is then passed to the racers script
     */
    private void OnTriggerEnter(Collider other)
    {
        ItemSound.Play();
        if (!other.gameObject.tag.Equals("Shield"))
        {
            other.gameObject.GetComponent<ItemScript>().collectItem(this.random.Next(0,5));
        }
        // Disable the collider and mark the item-box as inactive
        this.coll.enabled = false;
        isActive = false;
        inactiveTimer = 0f;
    }

    private void Update()
    {
        // If itembox is inactive, let it vanish over time
        if (!isActive)
        {
            inactiveTimer += Time.deltaTime;
            float currentVanishing = this.ren.material.GetFloat("_Vanishing");
            this.ren.material.SetFloat("_Vanishing", currentVanishing + (Time.deltaTime * 1.2f));
        }

        // After 3s, activate it again and make it visible
        if (inactiveTimer > 3.0f)
        {
            this.coll.enabled = true;
            this.ren.material.SetFloat("_Vanishing", 0.3f);
            isActive = true;
        }
    }
}
