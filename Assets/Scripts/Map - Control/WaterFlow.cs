using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float speed;
    Rigidbody rb;

    public bool back;
    public bool right;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Vector3 pos = rb.position;
        if(back == true && right == false){
            Back();
        }
        if(back == false && right == true){
            Right();
        }
        //rb.position += Vector3.back * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }

    public void Back(){
        rb.position += Vector3.back * speed * Time.fixedDeltaTime;
    }
    public void Right(){
        rb.position += Vector3.right * speed * Time.fixedDeltaTime;
    }
}
