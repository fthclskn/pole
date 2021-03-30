using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GetRope : MonoBehaviour
{
    private Rigidbody rigidbody;
    private GameObject attached;
    public bool haveAttached = false;
    private float lastPosZ;
    public float zVel;
    public GameObject fpsCamera;
    void Start()
    {
        lastPosZ = transform.position.z;
        rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (haveAttached)
        {
            attached.transform.position = transform.position;
            //zVel = 0;
           // StartCoroutine(camdelay());
           // CameraScr.Instance.currentView = CameraScr.Instance.views[0];

        }
        else
        {
           // haveAttached = false;
         //   zVel = 5;
            //fpsCamera.SetActive(false);
            //CameraScr.Instance.currentView = CameraScr.Instance.transform;
        }

        lastPosZ = transform.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            Vector3 vector3 = new Vector3(0,0f,0);
            rigidbody.AddForce(vector3, ForceMode.Impulse);
            attached = other.gameObject;
            haveAttached = true;


            Debug.Log("fdsl");
            
        }
    }

    /*IEnumerator camdelay()
    {
        yield return new WaitForSeconds(.75f);
        fpsCamera.SetActive(true);
        CameraScr.Instance.currentView = CameraScr.Instance.views[1];
        GameObject.Find("Main Camera").GetComponent<FollowScr>().enabled=true;
    }*/
}
