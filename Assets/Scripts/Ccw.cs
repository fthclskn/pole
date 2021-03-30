using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Ccw : MonoBehaviour
{
    private Color slow;
    private Color play;
    private new Renderer renderer;
    
    
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        play = GetComponent<Renderer>().material.color;
        slow = Color.clear;
    }

    private void Update()
    {
        Color color = GetComponent<Renderer>().material.color;
        float timeScale = Time.timeScale;
        if (timeScale < 1f)
        {
            color = slow;
        }
        else
        {
            color = play;
        }
        
        if (renderer != null)
        {
            foreach (Material material in GetComponent<Renderer>().materials)
            {
                material.color = color;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GameManager.Instance.StopMoving = true;
            //todo Lose UI 
            //todo Lose Anim
            Debug.Log("door");
            //FindObjectOfType<PlayerMovement>().
        }
    }
}
