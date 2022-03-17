using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationPickMenu : MonoBehaviour
{
    [SerializeField]
    GameObject starField, circle;

    static ConstellationPickMenu staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    private void OnEnable()
    {
        starField.GetComponent<Animation>().Play();

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.GetComponent<IDissolving>() != null && child.tag != "ConstellationCircle")
            {
                child.GetComponent<IDissolving>().appear();
            }
        }
    }

    public void cancel ()
    {
        ConstellationCircle[] circles = GetComponentsInChildren<ConstellationCircle>();
        foreach (ConstellationCircle circle in circles) circle.unselect();
        gameObject.SetActive(false);
        PauseCanvas.Unpause();
    }

    public void applyWithCheck()
    {
        if (!MixingBoard.isEmpty || DraftModule.pickedRecipes.Count != 0)
        {
            string caption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Смена созвездий очистит котёл и отменит выбранные рецепты.\nПродолжить?" : "Constellations changing will clear the cauldron and cancel all the selected recipes.\nContinue?";
            PauseCanvas.ShowConstellatioConfrirm(caption);
        }
        else apply();
    }

    public static void apply()
    {
        ConstellationManager.setConstellations(ConstellationCircle.pickedConstellations);
        staticInstance.gameObject.SetActive(false);
        PauseCanvas.Unpause();
    }
}
