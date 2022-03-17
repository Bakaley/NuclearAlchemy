using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoWindow : MonoBehaviour
{
    public void Close()
    {
        Destroy(gameObject);
    }
}
