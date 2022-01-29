using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform soul;
    public float maxDistance;
    public float thrust = 1f;
    public float grabThrust = 20f;
    private float groundPosition;
    public float drag;

    void Start()
    {
        soul = GetComponentInChildren<Transform>();
        groundPosition = transform.position.y;
        drag = GetComponent<Rigidbody>().drag;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            transform.position = soul.position +
                                 Vector3.ClampMagnitude(
                                     new Vector3(transform.position.x, transform.position.y, 0) - soul.position,
                                     maxDistance);
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += transform.forward * 0.05f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += -transform.forward * 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= groundPosition)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * thrust);
        }

        if (soul == null)
        {
            soul = GameObject.Find("Soul").transform;
        }
    }

    public void GrappPull(Vector3 grappleTarget)
    {
        Debug.Log("character is pulled");
        GetComponent<Rigidbody>().AddForce(-Vector3.Normalize(transform.position - grappleTarget)* GetComponent<PlayerController>().grabThrust);
        Debug.Log(Vector3.Normalize(transform.position - grappleTarget));
        
        Debug.DrawLine(transform.position, grappleTarget, Color.black);
    }
}