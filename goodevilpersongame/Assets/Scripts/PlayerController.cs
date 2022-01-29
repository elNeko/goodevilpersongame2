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
    public float spring;
    private bool allowControl;
    private bool onGround;

    void Start()
    {
        soul = GetComponentInChildren<Transform>();
        groundPosition = transform.position.y;
        spring = GetComponent<SpringJoint>().spring;
        GetComponent<SpringJoint>().spring = 0;
        allowControl = true;
        onGround = true;
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

        if (allowControl)
        {
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += transform.forward * 0.05f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position += -transform.forward * 0.05f;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * thrust);
            }
        }

        if (soul == null)
        {
            soul = GameObject.Find("Soul").transform;
        }
    }

    public void GrappPull(Vector3 grappleTarget)
    {
        GetComponent<Rigidbody>().AddForce(-Vector3.Normalize(transform.position - grappleTarget) *
                                           GetComponent<PlayerController>().grabThrust);
        GetComponent<SpringJoint>().spring = spring;
        allowControl = false;
    }

    public void DeactivateGrapp()
    {
        GetComponent<SpringJoint>().spring = 0;
        allowControl = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}