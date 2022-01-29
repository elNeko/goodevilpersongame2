using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += transform.forward * 0.05f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += - transform.forward * 0.05f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Soul")
        {
            other.gameObject.GetComponent<SoulController>().tooFar = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("soul is too far");
        if (other.name == "Soul")
        {
            other.gameObject.GetComponent<SoulController>().tooFar = true;
        }
    }
}
