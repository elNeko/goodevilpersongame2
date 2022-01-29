using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    public bool tooFar;
    public Collider coll;
    private int layerMask;
    public float maxDistance;
    private Transform pc;

    // Start is called before the first frame update
    void Start()
    {
        tooFar = false;
        layerMask = LayerMask.GetMask("RaycastTarget");
        if (maxDistance <= 0)
        {
            maxDistance = 3;
        }

        pc = transform.parent;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
        {
            if (!tooFar)
            {
                transform.position = new Vector3(Mathf.Clamp(hit.point.x, pc.position.x - maxDistance, pc.position.x + maxDistance) ,
                    Mathf.Clamp(hit.point.y, pc.position.y - maxDistance, pc.position.y + maxDistance) , transform.position.z);
            }
            
            /*
            transform.position = new Vector3(
                Vector3.ClampMagnitude(new Vector3(0,0,0), 3);
                Mathf.Clamp(hit.point.x, pc.position.x - maxDistance, pc.position.x + maxDistance),
                Mathf.Clamp(hit.point.y, pc.position.y - maxDistance, pc.position.y + maxDistance),
                transform.position.z);
            */
            transform.position = pc.position + Vector3.ClampMagnitude(hit.point - pc.position, 7);
        }
    }
}