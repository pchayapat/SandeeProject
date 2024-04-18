using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Vector3 pos = rb.position;
        rb.position += Vector3.left * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}