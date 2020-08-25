using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealisticArrowRotation : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    // Update is called once per frame
    void Start()
    {
        UpdateRotation();
    }
    void Update()
    {
        if (rigidbody2D.simulated == true)
        {
            UpdateRotation();
        }
    }

    void UpdateRotation()
    {
        Vector2 v = rigidbody2D.velocity;
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);
    }
}
