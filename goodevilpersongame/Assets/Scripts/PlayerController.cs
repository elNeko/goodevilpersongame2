using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform soul;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += transform.right * 0.05f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += - transform.right * 0.05f;
        }

        if (soul == null)
        {
            soul = GameObject.Find("Soul").transform;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Soul")
        {
            other.gameObject.GetComponent<SoulController>().tooFar = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Soul")
        {
            other.gameObject.GetComponent<SoulController>().tooFar = true;
        }
    }
    */
}
