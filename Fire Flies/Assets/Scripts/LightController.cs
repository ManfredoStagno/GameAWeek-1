using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject lightObject;

    [HideInInspector]
    public Light light;
    [HideInInspector]
    public Transform lightTransform;

    public float decayTime = 25.0f;

    public float frequency = 1;
    public float magnitude = 0.5f;

    [HideInInspector]
    public float hoverMultiplier = 1;

    [HideInInspector]
    public float fullLight;
    [HideInInspector]
    public float lightMultiplier;
    [HideInInspector]
    public float life;
    [HideInInspector]
    public float lifeBar;


    void OnEnable()
    {
        light = lightObject.GetComponent<Light>();
        lightTransform = lightObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Floating();
    }

    public void Floating()
    {
        Vector3 sine = new Vector3(0, Mathf.Sin(Time.time * frequency) * Time.deltaTime * magnitude, 0);
        lightTransform.position += sine;
    }

    public void Fade(bool healing)
    {
        if(!healing)
            life += Time.deltaTime * hoverMultiplier;
        if(healing)
            life -= Time.deltaTime;

        if (life > decayTime)
            life = decayTime;
        if (life < 0)
            life = 0;

        lifeBar = life / decayTime;

        lightMultiplier = 1f - lifeBar;        

        light.intensity = fullLight * lightMultiplier;
    }
}
