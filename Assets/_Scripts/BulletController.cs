using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();      
    }

  
    void Update()
    {
        
    }

    
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
    }
    
}
