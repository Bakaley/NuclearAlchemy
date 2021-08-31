using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{

    [SerializeField]
    GameObject cancelMenuSampler;

    static Recipe recipeToCancel;

    public static PauseCanvas StaticInstance
    {
        get;
        private set;
    }

    private void Awake()
    {
        StaticInstance = this;
    }

    public static void CancelRecipe (Recipe recipe)
    {
        Pause();
        recipeToCancel = recipe;
        GameObject cancelMenu = Instantiate(StaticInstance.cancelMenuSampler);
    }

    static void Pause()
    {
        StaticInstance.gameObject.SetActive(true);
    }
}
