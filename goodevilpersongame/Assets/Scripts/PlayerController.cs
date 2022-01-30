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
    public float velocity;
    private Rigidbody _rb;

    void Start()
    {
        soul = GetComponentInChildren<Transform>();
        groundPosition = transform.position.y;
        spring = GetComponent<SpringJoint>().spring;
        GetComponent<SpringJoint>().spring = 0;
        allowControl = true;
        onGround = false;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        onGround = Physics.Raycast(_rb.position, -Vector3.up, 1f);

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
                GetComponent<Rigidbody>()
                    .MovePosition(transform.position + (transform.right * Time.fixedDeltaTime * velocity));
            }

            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody>()
                    .MovePosition(transform.position + (-transform.right * Time.fixedDeltaTime * velocity));
            }

            if (Input.GetKey(KeyCode.Space) && onGround)
            {
                Debug.Log("jump!");
                _rb.velocity = new Vector3(0, 10, 0);
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