using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    // public bool tooFar;
    private int layerMask;
    public float maxDistance;
    private Transform pc;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        // tooFar = false;
        layerMask = LayerMask.GetMask("RaycastTarget");
        if (maxDistance <= 0)
        {
            maxDistance = 3;
        }

        pc = transform.parent;
    }

    void Update()
    {
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
        {
            transform.position = pc.position + Vector3.ClampMagnitude(hit.point - pc.position, maxDistance);
        }
        
        
        
        //transform.position = pc.position + Vector3.ClampMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition)  - pc.position, maxDistance);
        //transform.position = new Vector3(transform.position.x, transform.position.y, pc.position.z-0.02f);
        
        
        Debug.DrawLine(transform.position, pc.transform.position, Color.red);
    }
}