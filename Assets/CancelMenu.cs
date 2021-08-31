using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelMenu : MonoBehaviour
{

    private void Awake()
    {
        staticInstance = this;
    }

    public static bool Opened
    {
        get
        {
            if (staticInstance == null) return false;
            return staticInstance.gameObject.activeInHierarchy;
        }
       
    }

    static CancelMenu staticInstance;

    public void YesButton()
    {
        Debug.Log("Yes");
    }

    public void NoButton()
    {
        Debug.Log("No");

    }
}
