using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class RacerInput : MonoBehaviour
{
   RacerControl controls;
   [HideInInspector] public float steering;
   private float forwards;
   private float backwards;
   [HideInInspector] public float acceleration;
   [HideInInspector] public bool brake;
   Vector3 ac = new Vector3(0,0,0);

    //always called before start()
   void Awake()
   {

        controls = new RacerControl();
        controls.Racer.Move.performed += ctx => steering = ctx.ReadValue<Vector2>().x;
        controls.Racer.Accellerate.performed += ctx => forwards = ctx.ReadValue<float>();
        controls.Racer.Accellerate.performed += ctx => sum();
        controls.Racer.backwards.performed += ctx => backwards = ctx.ReadValue<float>()*-1;
        controls.Racer.backwards.performed += ctx => sum();
        controls.Racer.brake.performed += ctx => brake = ctx.ReadValue<bool>();

   }
 
   private void OnEnable()
   {
       controls.Racer.Enable();
   }
   private void OnDisable()
   {
       controls.Racer.Disable();
   }

   void sum(){
       acceleration = forwards + backwards;
   }
 
   void FixedUpdate()
   {
       //rb.AddForce(ac);
   }
}

