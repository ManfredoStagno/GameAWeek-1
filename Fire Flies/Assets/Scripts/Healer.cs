using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public float timer = 2;
    public float puff = 10;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().isHealing = true;
            StartCoroutine(HealPlus(other.GetComponent<LightController>()));
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().isHealing = false;
            StopAllCoroutines();
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

    }
}
