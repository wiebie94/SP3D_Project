using System;
using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{
	public GameObject emitter;
	public int laserSpeed = 1000;
	private float lifeTimer;
	void Start ()
	{
		this.lifeTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * laserSpeed;
		lifeTimer += Time.deltaTime;
		if (lifeTimer > 20.0f)
		{
			Destroy(this.gameObject);
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		
		if (!other.gameObject.tag.Equals("checkpoint") && !ReferenceEquals(other.gameObject, this.emitter))
		{
			if (other.gameObject.tag.Equals("racer"))
			{
				other.gameObject.GetComponent<ItemScript>().laserHit();
				Debug.Log("enemy racer hit");
			}
			
			// Laser zerstören (destroy)
			Destroy(this.gameObject);
		}
	}
}
