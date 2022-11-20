using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class darkPresence : MonoBehaviour
{
    private float time=0;
    private Light2D worldLight;
    private float initialLightLevel;
    public bool transition;
    [Range(0, 1)]
    public float lightLevel;

    // Start is called before the first frame update
    void Start()
    {
        worldLight=GameObject.Find("GlobalLight 2D").GetComponent<Light2D>();
        initialLightLevel = worldLight.intensity;
        if(initialLightLevel> lightLevel)
        {
            if(!transition)
            {
                worldLight.intensity = lightLevel;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (initialLightLevel > lightLevel)
        {
            if (time < 1 && transition)
            {
                time += Time.deltaTime;
                worldLight.intensity = Mathf.Lerp(initialLightLevel, lightLevel, time);
            }
        }
    }
}
