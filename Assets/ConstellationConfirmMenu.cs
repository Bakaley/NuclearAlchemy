using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationConfirmMenu : MonoBehaviour
{
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
