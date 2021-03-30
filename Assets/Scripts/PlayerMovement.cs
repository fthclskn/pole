using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeedZ = 200f;
    public float moveSpeedY = 0;
    public float moveSpeedX = 0;
    public Rigidbody rb;
    public bool OnHeli=false;
    public bool IsAttached=false;
    private float jumpSpeed;
    private float ZaxisMove;
    public Transform target;
    public float step;
    private bool check;
    public Transform Heli;

    

    private void Awake()
    {
        
    }

    /*
    private int waypointIndex = 0;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    public bool go = false;
    */
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
        if (OnHeli)
        {
            

            if (!IsAttached)
            {
               // transform.position = Vector3.MoveTowards(transform.position, target.position, step*Time.deltaTime);
               

               StartCoroutine(HeliJumpDelay());
               rb.velocity= new Vector3(0,0,0);
               
                
            }

            if (IsAttached)
            {    
                rb.velocity=new Vector3(0,0,0);
                this.transform.parent = Heli.transform;
            }

            
           // rb.velocity= new Vector3(0,moveSpeedY,3);

        }
       
    }

    private void FixedUpdate()
    {
        if (!OnHeli)
        {
            moveSpeedY = FindObjectOfType<DetectShapes>().verticalVelocity ;
            moveSpeedX = FindObjectOfType<DetectShapes>().HorizontalVelocity;
            moveSpeedZ =FindObjectOfType<DetectShapes>().ZaxisVelocity;
            rb.velocity= new Vector3(moveSpeedX,moveSpeedY,moveSpeedZ);
        }

        
    }

    IEnumerator HeliJumpDelay()
    {
        yield return new WaitForSeconds(0.45f);
       // FindObjectOfType<DetectShapes>().anim.SetTrigger("next");
        Vector3 desiredPosition = transform.position = Vector3.MoveTowards(transform.position, target.position, step*Time.deltaTime);
        Vector3 moveDelta = new Vector3(desiredPosition.x,desiredPosition.y,desiredPosition.z)-transform.position;
        transform.position = desiredPosition;
        jumpSpeed = 2f;
        ZaxisMove = 4f;
        check = false;
    }

    private void Move()
    { 
        /*
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1 && go == false)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);
            Quaternion targetrotation = Quaternion.Euler(waypoints[waypointIndex].transform.eulerAngles.x,waypoints[waypointIndex].transform.eulerAngles.y, waypoints[waypointIndex].transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetrotation,moveSpeed*Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                // if (transform.position == waypoints[waypoints.Length-1].transform.position)
                
            }
        }*/
    }
   
}
