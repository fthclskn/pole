using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraScr : MonoBehaviour
{
    public static CameraScr Instance { get; private set; }

    [SerializeField] private Camera _camera;
   // public Transform[] views;
    //public Transform currentView;
   // private float TransititionSpeed=2f;

    public Camera Camera => _camera;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //currentView = this.transform;
    }


   // private void LateUpdate()
   /* {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * TransititionSpeed);
        Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x,currentView.rotation.eulerAngles.x,Time.deltaTime*TransititionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y,currentView.rotation.eulerAngles.y,Time.deltaTime*TransititionSpeed),
        Mathf.LerpAngle(transform.rotation.eulerAngles.z,currentView.rotation.eulerAngles.z,Time.deltaTime*TransititionSpeed));
        transform.eulerAngles=currentAngle;
        
        
    }*/
}
