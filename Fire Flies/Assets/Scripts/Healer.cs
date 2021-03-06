﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public float timer = 2;
    public float puff = 10;

    public PickUp[] pickUps;

    private AudioSource audioSource;

    public AudioClip healerAudioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(HealPlus(other.GetComponent<LightController>()));

            foreach (PickUp pickUp in pickUps)
            {
                pickUp.ResetValues();
            }

            audioSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StopAllCoroutines();

            audioSource.Stop();
        }
    }

    private IEnumerator HealPlus(LightController lc)
    {

        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");
        yield return new WaitForSeconds(timer);
        lc.life -= puff;
        Debug.Log("Puff");

    }
}
