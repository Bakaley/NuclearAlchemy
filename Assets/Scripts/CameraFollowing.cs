using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Vector3 startPositon;
    public Quaternion startRotation;

    public float maxFrontAngle = 62;
    public float maxBackAngle = 77;

    public Transform target;
    public float smoothSpeed = 0.125f;
    public float modifier = 10;

    // Start is called before the first frame update
    void Start()
    {
        startPositon = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {

        this.transform.position = new Vector3 (startPositon.x + target.position.x / modifier, startPositon.y, startPositon.z );
        this.transform.rotation = Quaternion.Euler(maxFrontAngle + 6 - target.transform.position.z, startRotation.y, startRotation.z);
    }
}
