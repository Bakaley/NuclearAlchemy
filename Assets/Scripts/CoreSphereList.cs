using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSphereList : MonoBehaviour
{
    public MeshRenderer[] meshRenderers
    {
        get
        {
            return GetComponentsInChildren<MeshRenderer>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
