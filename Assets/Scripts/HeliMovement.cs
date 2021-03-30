using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMovement : MonoBehaviour
{
    private Rigidbody rb;
    //public float forwardAcceleration=2.5f, forwardSpeed=25f;
    private float activeForwardSpeed,rollInput;
    private float rot;
    private Animator anim;
    public bool isHeliHit=false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (FindObjectOfType<GetRope>().haveAttached==true)
        {
            rb.useGravity = true;
            //activeForwardSpeed =
              //  Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration * Time.deltaTime);
            //transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            if (!isHeliHit)
            {
                Debug.Log("sa" +
                          "as");
                rb.velocity= new Vector3(0,0,5f);
            }

            if (isHeliHit)
            {
                rb.velocity=new Vector3(0,0,0);
            }
            
           
        }
        else
        {
            rb.useGravity = false;
        }
    }
}
