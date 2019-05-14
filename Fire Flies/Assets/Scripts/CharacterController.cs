using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 50;
    public float decayTime = 25.0f;
    public float deadTime = 5f;

    private IEnumerator dieTimer;
    private Light light;

    private float fullLight;
    private float lightMultiplier;
    private float life;
    private float lifeBar;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        light = GetComponentInChildren<Light>();
        fullLight = light.intensity;
        life = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Fade();

        //if life regained stopcoroutine
        if (lifeBar < 1 && dieTimer != null)
        {
            StopCoroutine(dieTimer);
        }
        //Run coroutine
        if (lifeBar >= 1)
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

    void Fade()
    {
        life += Time.deltaTime;

        if (life > decayTime)
            life = decayTime;
        if (life < 0)
            life = 0;
        
        lifeBar = life / decayTime; 
        lightMultiplier = 1f - lifeBar;
       
        light.intensity = fullLight * lightMultiplier;
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
}
