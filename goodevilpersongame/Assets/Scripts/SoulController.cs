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

        var point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        transform.position = pc.position + Vector3.ClampMagnitude(new Vector3(point.x, point.y, 0) - pc.position, maxDistance);
        
        Debug.DrawLine(transform.position, pc.transform.position, Color.red);
    }
}