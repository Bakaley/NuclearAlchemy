using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCauldron : MonoBehaviour
{
    public GameObject panel;

    public void open()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
