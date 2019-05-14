using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

    public float frequency = 1;
    public float magnitude = 1;

    void FixedUpdate()
    {
        Vector3 sine = new Vector3(0, Mathf.Sin(Time.time * frequency) * Time.deltaTime * magnitude, 0);
        transform.position += sine;
    }
}
