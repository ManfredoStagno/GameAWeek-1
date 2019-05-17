using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    public float distance = 10.0f;
    public Vector3 offset;
    
    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    public float minClamp = -30.0f;
    public float maxClamp = 50.0f;

    private void Start()
    {

    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;
        currentY = Mathf.Clamp(currentY, minClamp, maxClamp);
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * dir;
        transform.LookAt(target.position + offset);
    }
}
