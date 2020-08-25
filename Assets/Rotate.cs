using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;
    public float maxRotation = 45f;
    // Update is called once per frame
    void Update()
    {
        // transform.Rotate ( Vector3.forward * ( speed * Time.deltaTime ) );
         transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }
}
