using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject instructions;
    public GameObject[] points;
    public GameObject Door;
    public Transform[] DoorTargest;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door")
        {
            Animation anim = other.GetComponentInChildren<Animation>();
            instructions.SetActive(true);
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(false);
            }
            
            if (DetectShapes.instance.lineLength > 50f)
            {
                for (int i = 0; i < DoorTargest.Length; i++)
                {
                    Door.transform.position = Vector3.MoveTowards(Door.transform.position, DoorTargest[i].position,
                        40f * Time.timeScale*Time.deltaTime);
                }
               
                //anim.Play("Door");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            instructions.SetActive(false);
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(true);    
            }
            
        }
    }
}
