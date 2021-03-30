using System;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private float pushForce;

    private HingeJoint _joint;
    private bool detached;
    private bool fly;
    

   
    void Update()
    {
        if (fly && !detached)
        {
            var x = GetComponent<BoxCollider>().bounds.max.y; 
            var y = GetComponent<PoleRenderer>().GetSize();

            if (x / y >= 0.97f)
            {
                Destroy(transform.parent.GetComponent<FixedJoint>());
                
                Vector3 dir = Quaternion.AngleAxis(30, transform.forward) * Vector3.forward; // ileri yönünde 30 derecelik açıyla * karakterin hangi yönde döndürdüğü axis
                transform.parent.GetComponent<Rigidbody>().AddForce(dir * 250f);
                
                 detached = true; // Detaches the transform from its parent.
            }
        }
        
    }
    

    public bool IsDetached()
    {
        return detached;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name.StartsWith("Platform") && null == _joint && !fly)
        {
            var contact = other.GetContact(0).normal;
            if (contact.z > 0)
            {
                Debug.Log("EMPTY!");
                // TODO: die
                return;
            }

            fly = true;
            _joint = gameObject.AddComponent<HingeJoint>();
            _joint.connectedBody = other.rigidbody;
            _joint.enableCollision = true;
            Jump();
            //GameObject.FindWithTag("Trail").GetComponent<TrailRenderer>().emitting = true;
            GetComponent<Animation>().Play("Bend");
        }
    }

    private void Jump()
    {
        var poleRenderer = GetComponent<PoleRenderer>();
        var rb = transform.parent.GetComponent<Rigidbody>();
        var oldVel = rb.velocity;
        oldVel.y = Mathf.Lerp(10f, 30f, poleRenderer.GetSize() / poleRenderer.GetMaxSize());
        rb.velocity = oldVel;
        
    }
}