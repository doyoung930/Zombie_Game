using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] private float lightDecay = 0.1f;
    [SerializeField] private float angleDecay = 1f;
    [SerializeField] private float minimumAngle = 40f;
    
    Light myLight;
    
    private void Start()
    {
        myLight = GetComponent<Light>();
    }
    
    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }
    
    private void DecreaseLightIntensity()
    {
        myLight.intensity = lightDecay * Time.deltaTime;
    }
    
    private void DecreaseLightAngle()
    {
        if (myLight.intensity < minimumAngle)
        {
            return;
        }
        else
        {
            myLight.intensity -= angleDecay*Time.deltaTime;
        }
        
    }
}
