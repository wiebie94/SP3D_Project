using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasHandler : MonoBehaviour
{
    
    public Sprite itemNone;
    public Sprite itemBoost;
    public Sprite itemInvert;
    public Sprite itemLaser;
    public Sprite itemFrost;
    public Sprite itemShield;

    // UI Elements
    private Image itemIcon;
    private Text currentSpeed;
    private Text position;
    private Text currentLap;
    private GameObject invertIcon;

    // Components
    private RacerMove movement;
    void Start()
    {
        this.movement = GetComponent<RacerMove>();
        
        this.itemIcon = this.transform.GetChild(2).GetChild(2).GetComponent<Image>();
        this.position = this.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
        this.currentSpeed = this.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>();
        this.currentLap = transform.GetChild(2).GetChild(3).GetComponent<Text>();
        this.invertIcon = transform.GetChild(2).GetChild(6).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        updateSpeed();
        transform.GetChild(2).GetChild(6).gameObject.SetActive(movement.invertItem);
    }

    public void changeItemIcon(ItemScript.Item currentItem)
    {
        switch (currentItem)
        {
            case ItemScript.Item.None:
                itemIcon.sprite = itemNone;
                break;
            case ItemScript.Item.Boost:
                itemIcon.sprite = itemBoost;
                break;
            case ItemScript.Item.Shoot:
                itemIcon.sprite = itemLaser;
                break;
            case ItemScript.Item.Frost:
                itemIcon.sprite = itemFrost;
                break;
            case ItemScript.Item.InvertSteering:
                itemIcon.sprite = itemInvert;
                break;
            case ItemScript.Item.Shield:
                itemIcon.sprite = itemShield;
                break;
                
        }
    }

    private void updateSpeed()
    {
        currentSpeed.text = ((Math.Abs((int)movement.speed))/2).ToString() + " km/h";
    }

    public void setPosition(int pos)
    {
        this.position.text = "#" + pos;
    }

    public void setLap(int lap)
    {
        this.currentLap.text = lap + "/" + GameOptions.laps;
    }

    public void resetUI()
    {
        movement.speed = 0;
        currentSpeed.text = "0 km/h";
        currentLap.text = "0/" + GameOptions.laps;
        itemIcon.sprite = itemNone;
    }

}
