using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSetPiece : MonoBehaviour
{

    public GameObject father;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mr.enabled = false;
        }
    }
}
