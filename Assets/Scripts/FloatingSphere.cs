using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSphere : MonoBehaviour
{
    Vector3 newPosition;
    float y0;
    public float amplitude = 0.05f;
    public float movingSpeed = 2;
    public float rotationSpeed = 10f;
    void Start()
    {
        y0 = transform.localPosition.y;

    }
    void Update()
    {
       // newPosition = transform.up;
        newPosition = new Vector3(transform.localPosition.x, amplitude * Mathf.Sin(movingSpeed * Time.time) + y0 + amplitude/2, transform.localPosition.z);
        transform.localPosition = newPosition;
        transform.Rotate(Time.deltaTime * rotationSpeed, 0, 0);
    }   

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
      
    }

}
