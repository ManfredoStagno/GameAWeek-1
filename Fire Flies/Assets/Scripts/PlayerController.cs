using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    LightController lc;

    public float speed = 50;
    public float deadTime = 5f;

    private IEnumerator dieTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lc = GetComponent<LightController>();

        lc.fullLight = lc.light.intensity;
        lc.life = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        lc.Fade(false);

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
    }

    void Move()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        movementDirection.x *= speed;
        movementDirection.z *= speed;
        rb.MovePosition(transform.position + movementDirection * Time.deltaTime);
        
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), 0.15F);
            transform.Translate(movementDirection * Time.deltaTime, Space.World);
        }
    }

    void Die()
    {
        //TODO: die method
        Debug.Log("You Dead");
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(deadTime);
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            if (other.GetComponent<PickUp>().pickedUp != true)
            {
                lc.life -= 5;
                other.GetComponent<PickUp>().pickedUp = true;
            }
        }
    }
}
