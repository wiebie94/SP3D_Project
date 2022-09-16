using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Colors the Buttons in the Menu green on hover
 */
public class MouseHover : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMesh>().color = Color.magenta;
    }

    void OnMouseOver()
    {
        GetComponent<TextMesh>().color = Color.green;
    }

    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = Color.magenta;
    }
}
