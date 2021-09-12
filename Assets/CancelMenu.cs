using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelMenu : MonoBehaviour
{

    public Recipe recipeToCancel;

    public void confirm()
    {
        DraftModule.cancelRecipe(recipeToCancel);
        PauseCanvas.Unpause();
        Destroy(gameObject);
    }

    public void decline()
    {
        PauseCanvas.Unpause();
        Destroy(gameObject);
    }
}
