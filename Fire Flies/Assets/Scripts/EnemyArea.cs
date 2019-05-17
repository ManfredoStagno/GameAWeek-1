using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    [HideInInspector]
    public bool playerDetected = false;
    [HideInInspector]
    public Transform player;

    void Start()
    {
        playerDetected = false;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            player = null;
            Debug.Log("NOT Chasing");
        }
    }
}
