using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Follower : MonoBehaviour
{

    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

   

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -20));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, 10f);
    }
}
