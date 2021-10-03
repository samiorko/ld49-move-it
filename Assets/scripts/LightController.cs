using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Material breakingLight;
    public Material normalLight;
    
    public Color breakingColor;
    public Color normalColor;

    public List<Renderer> brakeRenderers;
    public List<Light> brakeLights;
    public List<Light> backingLights;

    public float normalLightIntensity;
    public float normalLightRange;
    
    public float breakingLightIntensity;
    public float breakingLightRange;
    
    private float _lastBrakeTime;
    private AudioSource _brakeSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _brakeSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Car.Instance.braking)
        {
            _lastBrakeTime = Time.time;
        }
        
        
        _brakeSound.enabled = Car.Instance.backing;
        
        var isBraking  = Time.time - _lastBrakeTime < 0.1f;
            
        foreach (var light in brakeLights)
        {
            if (isBraking)
            {
               light.color = breakingColor;
               light.intensity = breakingLightIntensity;
               light.range = breakingLightRange;
            }
            else
            {
               light.color = normalColor;
               light.intensity = normalLightIntensity;
               light.range = normalLightRange;
            }
        }

        foreach (var light in backingLights)
        {
            light.enabled = Car.Instance.backing;
        }

        foreach (var renderer in brakeRenderers)
        {
            if (isBraking)
            {
                renderer.material = breakingLight;
            }
            else
            {
                renderer.material = normalLight;
            }
        }
    }
}
