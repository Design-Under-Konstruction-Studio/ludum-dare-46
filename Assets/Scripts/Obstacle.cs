using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //public GameObject target;
    Rigidbody _rb;
    public bool direction = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //MoveAtPlayer();

        if (direction == true)
        {
            _rb.velocity = Vector3.up;
            
        }
        else
        {
            _rb.velocity = Vector3.down;
            
        }
    }

    void MoveAtPlayer()
    {
        _rb.velocity = Vector3.right;
    }
}
