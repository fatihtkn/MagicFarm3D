using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Vector3 targetDistance;
    public Transform target;
    public float trackSpeed;


    private void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, target.position + targetDistance, Time.deltaTime * trackSpeed);

       
        
        

    }
}
