using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingButton : MonoBehaviour
{
    public void cook()
    {
        Debug.Log("Cooking " + CookingModule.preparedRecipesCount);
    }
}
