using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [HideInInspector]
    public LightController lc;
    [HideInInspector]
    public bool pickedUp = false;

    public float heal = 5;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        lc = GetComponent<LightController>();

        lc.fullLight = lc.light.intensity;
        lc.life = 0;
    }

    
    void Update()
    {
        if (pickedUp)
            lc.Fade(false);
    }

    public void ResetValues()
    {
        pickedUp = false;
        lc.light.intensity = lc.fullLight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp)
        {
            if (other.CompareTag("Player"))
            {
                audioSource.Play();
            }
        }
    }
}
