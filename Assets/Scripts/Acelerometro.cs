using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerometro : MonoBehaviour
{
   
    private Rigidbody rb;
    private float speed = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration;
        tilt = Quaternion.Euler(90,0,0)*tilt;
        rb.AddForce(tilt*speed);
    }
}
