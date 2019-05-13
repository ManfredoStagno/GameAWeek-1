using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Camera cam;

    public float smoothSpeed = 0.125f;
    

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        cam.transform.LookAt(target);
    }
}
