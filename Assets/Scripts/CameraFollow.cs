using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    
    Vector3 offset = new Vector3(0f, 4.3f, -3f);
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, 0.3f);
        transform.position = delta;
    }
}
