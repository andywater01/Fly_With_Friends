using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public int speed;
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward.normalized * speed * Time.deltaTime, ForceMode.Force);
    }
}
