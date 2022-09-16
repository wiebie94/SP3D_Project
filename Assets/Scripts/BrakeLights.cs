using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls the light intensity and materials
 */
public class BrakeLights : MonoBehaviour
{
    
    private InputHandler input;

    private Renderer ren;

    private Material[] noBrake;
    private Material[] brake;

    public Light plLeft;
    public Light plRight;

    private Color ledBaseColor;

    public int plIntensityBrake;
    public int plIntensityNoBrake;

    void Start()
    {
        input = this.GetComponent<InputHandler>();
        this.ren = GetComponent<MeshRenderer>();
        this.ledBaseColor = this.ren.materials[2].color;
    }

    
    void Update()
    {
        
        Material[] mat = (Material[])this.ren.materials.Clone();
        // Change brakelight (pointlight) intensity and emission intensity
        if(input.acceleration < 0)
        {
            mat[0].SetColor("_EmissionColor", new Color(0.8f, 0, 0) * 15);
            plLeft.intensity = plIntensityBrake;
            plRight.intensity = plIntensityBrake;
        }
        else
        {
            mat[0].SetColor("_EmissionColor", new Color(0.8f, 0, 0) * 9);
            plLeft.intensity = plIntensityNoBrake;
            plRight.intensity = plIntensityNoBrake;
        }

        // Change thruster emission intensity depending on the input
        
        // Em = 2 + accl*2
        
        float emission = 2 + input.acceleration * 2;
        //mat[2].SetColor("_EmissionColor", new Color(0.0784f, 0.5882f, 0.749f) * (emission*5));
        mat[2].SetColor("_EmissionColor", this.ledBaseColor * (emission*5));
        mat[3].SetColor("_EmissionColor", this.ledBaseColor * (emission*5));
        this.ren.materials = mat;
    }
}
