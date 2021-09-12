using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{

    [SerializeField]
    GameObject cancelMenuSampler, background, constellationPick, constellationConfirmMenuSampler, infoBookObject;

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
        GameObject cancelMenu = Instantiate(StaticInstance.cancelMenuSampler, StaticInstance.transform);
        cancelMenu.GetComponent<CancelMenu>().recipeToCancel = recipe;
    }

    public void ShowConstellationPickPanel()
    {
        Pause();
        constellationPick.SetActive(true);
    }

    public static bool Paused
    {
        get
        {
            return StaticInstance.background.activeSelf;
        }
    }
    public static void ShowConstellatioConfrirm(string caption)
    {
        Pause();
        GameObject menu = Instantiate(StaticInstance.constellationConfirmMenuSampler, StaticInstance.transform);
    }

    static void Pause()
    {
        StaticInstance.background.SetActive(true);
    }

    public static void Unpause()
    {
        StaticInstance.background.SetActive(false);
    }

    public static void ShowInfoBook()
    {
        Pause();
        StaticInstance.infoBookObject.SetActive(true);
    }

    public static void CloseInfoBook()
    {
        Unpause();
        StaticInstance.infoBookObject.SetActive(false);
    }
}
