using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPushPlayer : MonoBehaviour
{
    public float ForceForPushingPlayerInZ = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            Vector3 vector3 = transform.position * ForceForPushingPlayerInZ;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(vector3, ForceMode.Impulse);
        }
    }

}
