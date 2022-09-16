using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.InputSystem.Controls;

/**
 * Component which handles events fired by Unity's new Input System.
 * These values can be read by other components on this gameobject
 */
public class InputHandler : MonoBehaviour
{
	[HideInInspector] public float steering;
	private float forwards;
	private float backwards;
	[HideInInspector] public float acceleration;
	[HideInInspector] public bool brake;
	[HideInInspector] public float cam;
	[HideInInspector] public float menunav;

	public GameObject camNormal;
	public GameObject camBack;

	public GameObject gameLogic;
	

	public void OnMove(InputAction.CallbackContext ctx)
	{
        if (ctx.performed)
        {
	        steering = ctx.ReadValue<Vector2>().x;
        }
        if(ctx.canceled)
        {
			steering = 0;
		}

	}

	public void onAccellerate(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			forwards = ctx.ReadValue<float>();
		}
		if (ctx.canceled)
		{
			forwards = 0;
		}
	}

	public void onBackwards(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			backwards = ctx.ReadValue<float>() * -1;
		}
		if (ctx.canceled)
		{
			backwards = 0;
		}
	}

	public void onRotateCam(InputAction.CallbackContext ctx)
    {
		if (ctx.performed)
		{
			cam = ctx.ReadValue<Vector2>().x;
		}
		if (ctx.canceled)
		{
			cam = 0;
		}
	}

	public void onLookBack(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			this.camBack.GetComponent<CinemachineVirtualCamera>().Priority = 60;
		}
		if (ctx.canceled)
		{
			this.camBack.GetComponent<CinemachineVirtualCamera>().Priority = 10;
		}
	}

	public void onUseItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
			this.GetComponent<ItemScript>().useItem();
        }
    }

	public void onRespawn(InputAction.CallbackContext ctx)
	{
		if (ctx.performed){
			this.gameLogic.GetComponent<RaceLogic>().respawnRacer(this.gameObject);
		}
	}

	public void onMenuNav(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			menunav = ctx.ReadValue<Vector2>().y;
		}
		if(ctx.canceled)
		{
			menunav = 0;
		}
	}

	public void onOpenMenuInRace(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			this.gameLogic.GetComponent<RaceLogic>().openMenuInRace();
		}
	}

	public void resetInputValues()
	{
		steering = 0;
		forwards = 0;
		backwards = 0;
		acceleration = 0;
		brake = false;
	}

	void sum()
	{
		acceleration = forwards + backwards;
	}

    void Update()
    {
		sum();
    }

}
