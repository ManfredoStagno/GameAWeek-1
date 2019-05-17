using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    LightController lc;
    private IEnumerator dieTimer;

    public Transform myCamera;
    public Transform startCheckPoint;
    

    public float speed = 50;
    public float deadTime = 5f;
    public float myHoverMultiplier;
    public float myChompMultiplier;
    

    [HideInInspector]
    public bool isHealing = false;
    [HideInInspector]
    public bool isChomped;

    private Vector3 lastCheckpoint;
    private bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lc = GetComponent<LightController>();

        lc.fullLight = lc.light.intensity;
        lc.life = 0;

        lastCheckpoint = startCheckPoint.position;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            Multipliers(); 
        }
    }

    private void Update()
    {
        lc.Fade(isHealing);

        //if life regained stopcoroutine
        if (lc.lifeBar < 1 && dieTimer != null)
        {
            StopCoroutine(dieTimer);
        }
        //Run coroutine
        if (lc.lifeBar >= 1)
        {
            //return if coroutine active
            if (dieTimer != null)
                return;
            //StartCoroutine
            else
                dieTimer = DieCoroutine();
                StartCoroutine(dieTimer);
        }

        //Die if falling off
        if (transform.position.y < -42)
            Die();
    }

    void Move()
    {
        //Acquire input
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        movementDirection.x *= speed;
        movementDirection.z *= speed;

        MoveRelativeToCamera(movementDirection);
    }
    
    void Multipliers()
    {
        if (isChomped)
        {
            lc.lifeMultiplier = myChompMultiplier;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb.useGravity = false;
            lc.lifeMultiplier = myHoverMultiplier;
        }
        else
        {
            rb.useGravity = true;
            lc.lifeMultiplier = 1;
        }
    }

    void MoveRelativeToCamera(Vector3 inputMovement)
    {
        //Camera Relative movement
        Vector3 cameraForward = myCamera.forward;
        cameraForward.y = 0;
        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        Vector3 movement = cameraRelativeRotation * inputMovement;

        //MovePlayer relative to the camera
        rb.MovePosition(transform.position + movement * Time.deltaTime);

        //Rotation
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            transform.Translate(movement * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            if (other.GetComponent<PickUp>().pickedUp != true)
            {
                lc.life -= other.GetComponent<PickUp>().heal;
                other.GetComponent<PickUp>().pickedUp = true;
            }
        }

        if (other.CompareTag("Healer"))
        {
            isHealing = true;

            lastCheckpoint = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Healer"))
        {
            isHealing = false;
        }
    }

    void Die()
    {
        transform.position = lastCheckpoint;
        Debug.Log("You Dead");
    }

    IEnumerator DieCoroutine()
    {
        canMove = false;
        yield return new WaitForSeconds(deadTime);
        Die();
        canMove = true;
    }
    
}
