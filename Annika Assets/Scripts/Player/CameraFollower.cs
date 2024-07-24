using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Vector3 Offset = new Vector3(0f, 0f, -10f);
    float smoothtime = 1;
    Vector3 velocity = Vector3.zero;

    public Transform Target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition , ref velocity , smoothtime);
    }
}
