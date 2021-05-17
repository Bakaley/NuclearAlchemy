using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    Vector3 newPosition;
    float y0;
    float x0;
    public float amplitude = 0.01f;
    public float movingSpeed = 5;
    public float rotationSpeed = 0f;

    public float seedShift = 0;
    void Start()
    {

        y0 = transform.localPosition.y;
        x0 = transform.localPosition.x;
    }

    void Update()
    {      
        newPosition = new Vector3(amplitude * Mathf.Sin(movingSpeed/2 * Time.time + seedShift) + x0, amplitude * Mathf.Sin(movingSpeed * Time.time + seedShift) + y0, transform.localPosition.z);
        transform.localPosition = newPosition;
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }
}
