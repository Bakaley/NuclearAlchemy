using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    Vector3 newPosition;
    float y0;
    float x0;
    [SerializeField]
    float amplitude = 0.01f;
    [SerializeField]
    float movingSpeed = 5;
    [SerializeField]
    float rotationSpeedX = 0f;
    [SerializeField]
    float rotationSpeedY = 0f;
    [SerializeField]
    float rotationSpeedZ = 0f;

    [SerializeField]
    UnityEngine.Space rotationSpace = Space.World;

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
        transform.Rotate(Time.deltaTime * rotationSpeedX, Time.deltaTime * rotationSpeedY, Time.deltaTime * rotationSpeedZ, rotationSpace);
    }
}
