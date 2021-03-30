using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _gameObject.transform.position.z - 10f);
    }
}
