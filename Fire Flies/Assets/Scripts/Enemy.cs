using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    EnemyArea enemyArea;
    LightController lc;

    public Transform[] target;
    public float speed;

    private int current;

    private float startIntensity;
    public float chompCeiling;
    public float chompSpeed;

    private bool upChomp = true;
    private bool isChomping = false;
    private bool isChasing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyArea = GetComponentInParent<EnemyArea>();
        lc = GetComponentInChildren<LightController>();
        startIntensity = lc.light.intensity;
        
    }

    void Update()
    {
   

        if (enemyArea.playerDetected || isChasing)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, enemyArea.player.position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        }
        else
        {
            if (transform.position != target[current].position)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
            else
            {
                current = (current + 1) % target.Length;
            }

        }

        if (isChomping)
        {
            Chomp();
        }
        
    }

    void Chomp()
    {
        if (upChomp)
        {
            lc.light.intensity += chompSpeed;
            if (lc.light.intensity >= startIntensity + chompCeiling)
                upChomp = false;
        }
        else
        {
            lc.light.intensity -= chompSpeed;
            if (lc.light.intensity <= startIntensity)
                upChomp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            isChomping = true;
            other.GetComponent<PlayerController>().isChomped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            isChomping = false;
            other.GetComponent<PlayerController>().isChomped = false;
        }
    }
}
