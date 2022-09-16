using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost_Shot_Behavieour : MonoBehaviour
{
    public GameObject emitter;
    public int laserSpeed = 1000;
    private float lifeTimer;
    void Start ()
    {
        this.lifeTimer = 0;
    }
	
    // Move the projectile and destroy it when is lifetime exceeds the limit
    void Update () {
        transform.position += transform.forward * Time.deltaTime * laserSpeed;
        lifeTimer += Time.deltaTime;
        if (lifeTimer > 20.0f)
        {
            Destroy(this.gameObject);
        }

    }
	
    // When the projectile hits something
    private void OnTriggerEnter(Collider other)
    {
		
        if (!other.gameObject.tag.Equals("checkpoint") && !ReferenceEquals(other.gameObject, this.emitter))
        {

            // If the hit object is a racer, execute the freeze function
            if (other.gameObject.tag.Equals("racer"))
            {
                other.gameObject.GetComponent<ItemScript>().freeze();
                Debug.Log("enemy racer hit");
            }
            
            Destroy(this.gameObject);
        }
    }

}
