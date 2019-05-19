using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    [HideInInspector]
    public bool playerDetected = false;
    
    public Transform player;

    private AudioSource audioSource;

    void Start()
    {
        playerDetected = false;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.GetComponent<Transform>();
            Debug.Log("Chasing");
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            //player = null;
            Debug.Log("NOT Chasing");
            audioSource.Stop();
        }
    }
}
