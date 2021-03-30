using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        //GameObject that we should follow
    public float distance;            //how far back?
    public float Smoothness;        //how far are we allowed to drift from the target position
    public float angleX;            //angle to pitch up on top of the target
    public float angleY;            //angle to yaw around the target
   
   /* private Transform m_transform_cache;    //cache for our transform component
   /* private Transform myTransform
    {//use this instead of transform
        get
        {//myTransform is guarunteed to return our transform component, but faster than just transform alone
            if (m_transform_cache == null)
            {//if we don't have it cached, cache it
                m_transform_cache = transform;
            }
            return m_transform_cache;
        }
    }*/
   
    //this runs when values are changed in the inspector
    void OnValidate()
    {
        if (target != null)
        {//we have a target, move the camera to target position for preview purposes
            Vector3 targetPos = GetTargetPos();
            
            transform.position = targetPos;
            
            transform.LookAt(target);
        }
    }
   
    
    void LateUpdate()
    {
   
        Vector3 targetPos = GetTargetPos();
        float t = Vector3.Distance(transform.position, targetPos) / Smoothness;
        transform.position = Vector3.Lerp(transform.position, targetPos, t * Time.deltaTime);
        transform.LookAt(target);
    }
   
   void OnDrawGizmos()
    {//this is for editor purpose only, shows the relationship between this script and target as a line
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }
   
    private Vector3 GetTargetPos()
    {
        Vector3 targetPos = new Vector3(0, 0, -distance);
        
        targetPos = Quaternion.Euler(angleX, angleY, 0) * targetPos;
        
        return target.position + (target.rotation * targetPos);
    }
 
}
