using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    private int layerMaskWall;
    private int layerMaskWallNr;
    public float maxDistance;
    private Transform pc;
    RaycastHit hit;
    private Vector3 grappleTarget;
    public bool wallCollided;

    // Start is called before the first frame update
    void Start()
    {
        layerMaskWall = LayerMask.GetMask("Wall");
        layerMaskWallNr = LayerMask.NameToLayer("Wall");
        if (maxDistance <= 0)
        {
            maxDistance = 3;
        }

        pc = transform.parent;
        wallCollided = false;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            var point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.position = pc.position +
                                 Vector3.ClampMagnitude(new Vector3(point.x, point.y, 0) - pc.position, maxDistance);
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (wallCollided)
            {
                grappleTarget = transform.position;
                SwitchParentChild(pc.transform, transform);

                pc.GetComponent<PlayerController>().GrappPull(grappleTarget);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            SwitchParentChild(transform, pc.transform);
            pc.GetComponent<PlayerController>().DeactivateGrapp();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == layerMaskWallNr)
        {
            wallCollided = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == layerMaskWallNr)
        {
            wallCollided = false;
        }
    }

    public void SwitchParentChild(Transform parent, Transform child)
    {
        child.parent = null;
        parent.SetParent(child);
    }
}