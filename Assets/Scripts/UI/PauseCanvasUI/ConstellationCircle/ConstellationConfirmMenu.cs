using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationConfirmMenu : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Canvas>().sortingLayerName = "Pause";
    }
    public void confirm()
    {
        ConstellationPickMenu.apply();
        Destroy(gameObject);
    }

    public void decline()
    {
        Destroy(gameObject);
    }
}
