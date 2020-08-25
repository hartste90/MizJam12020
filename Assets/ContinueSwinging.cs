using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSwinging : MonoBehaviour
{
    public float veloictyThreshold = .1f;
    public float forceAdded = 1f;
    public HingeJoint2D hingeJoint;
    public Rigidbody2D rigidbody2D;
    
    public LineRenderer line;
    void Start()
    {
        line.SetPosition(0, transform.parent.position);
    }
    void Update()
    {
        if (rigidbody2D.velocity.magnitude <= veloictyThreshold)
        {
            Debug.Log("Applying force down");
            rigidbody2D.AddForce(Vector2.down * forceAdded, ForceMode2D.Impulse);
        }

        line.SetPosition(1, transform.position);
    }
}
