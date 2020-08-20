using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 10f * Time.deltaTime);
    }
}
