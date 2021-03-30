using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeli : MonoBehaviour
{
    private float rotateSpeed = 50f; //rotation of the helicopter
    public float moveSpeed = 10f; //the speed the helicopter need to move towards the landPosition
    private Transform landPosition; //the position the helicopter needs to crash at
    private bool IsHitToHeli=false;
    public GameObject[] Deactive;
    private GameObject MainHeli;
    public GameObject Explosion;
    
    
    private int waypointIndex = 0;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float movespeed = 2f;
    public bool go = false;
    
    void Start()
    {
        MainHeli =GameObject.FindWithTag("Helicopter");
        landPosition = GameObject.Find("HeliCrashPoint").transform;

    }
    
    void Update()
    {
        Move();
        
        if (IsHitToHeli)
        {
            MainHeli.transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            Vector3 crashPosition = landPosition.position;
            Vector3 direction = (crashPosition - MainHeli.transform.position).normalized;
            MainHeli.transform.Translate(direction * moveSpeed * Time.deltaTime);
            FindObjectOfType<HeliMovement>().isHeliHit = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Helicopter")
        {
            IsHitToHeli = true;
            Debug.Log("ishit");
            Explosion.SetActive(true);
            StartCoroutine(DestroyingDelay());
        }
    }

    IEnumerator DestroyingDelay()
    {
        yield return new WaitForSeconds(2f);
        IsHitToHeli = false;
        foreach (var all in Deactive)
        {
            all.SetActive(false);
        }
        FindObjectOfType<HeliMovement>().isHeliHit = false;
//        FindObjectOfType<GetRope>().haveAttached = false;

    }
    private void Move()
    { 
        
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1 && go == false)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                movespeed * Time.deltaTime);
            Quaternion targetrotation = Quaternion.Euler(waypoints[waypointIndex].transform.eulerAngles.x,waypoints[waypointIndex].transform.eulerAngles.y, waypoints[waypointIndex].transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetrotation,movespeed*Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                // if (transform.position == waypoints[waypoints.Length-1].transform.position)
                
            }
        }
    }
}

