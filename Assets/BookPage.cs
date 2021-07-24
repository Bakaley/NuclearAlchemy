using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookPage : MonoBehaviour
{

   // [SerializeField]
    //TextMeshProUGUI

    public GameObject recipe;
    public void pick()
    {
        Debug.Log(recipe.name);
    }


}
