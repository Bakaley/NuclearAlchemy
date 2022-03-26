using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField]
    GameObject ingredientPanelName;
    [SerializeField]
    GameObject ingredientNameSampler;
    [SerializeField]
    GameObject ingredientIconName;
    [SerializeField]
    GameObject buttonUpLeft;
    [SerializeField]
    GameObject buttonUpRight;
    [SerializeField]
    GameObject buttonDownLeft;
    [SerializeField]
    GameObject buttonDownRight;
    [SerializeField]
    GameObject ingredientJoystickTarget;
    [SerializeField]
    IngredientPlace placeUpLeft;
    [SerializeField]
    IngredientPlace placeUpRight;
    [SerializeField]
    IngredientPlace placeDownLeft;
    [SerializeField]
    IngredientPlace placeDownRight;
    [SerializeField]
    AddingBoard addingBoard;

    [SerializeField]
    GameObject ingedientManagerObject;

    [SerializeField]
    GameObject skipButton;

    IngredientListManager ingredientListManager;

    private static IngredientPlace targetedPlace;

    public static IngredientPanel staticInstance
    {
        get; private set;
    }

    public static Ingredient currentIngredient
    {
        get
        {
            return targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>();
        }
    }

    void Awake()
    {
        staticInstance = this;
        targetedPlace = placeUpLeft;
    }

    private void Start()
    {
        ingredientListManager = ingedientManagerObject.GetComponent<IngredientListManager>();
        changeTarget(placeUpLeft);

        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) skipButton.GetComponent<TextMeshProUGUI>().text = "Пропустить";
        else skipButton.GetComponent<TextMeshProUGUI>().text = "Skip";

        refreshIngredientsNoDelay();
    }

    void Update()
    {

    }

    public void chooseUpLeft()
    {
        CookingManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeUpLeft)
        {
            changeTarget(placeUpLeft);
            ingredientJoystickTarget.transform.SetParent(buttonUpLeft.transform, false);
        }
    }

    public void chooseUpRight()
    {

        CookingManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeUpRight)
        {
            changeTarget(placeUpRight);
            ingredientJoystickTarget.transform.SetParent(buttonUpRight.transform, false);
        }
    }

    public void chooseDownLeft()
    {

        CookingManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeDownLeft)
        {
            changeTarget(placeDownLeft);
            ingredientJoystickTarget.transform.SetParent(buttonDownLeft.transform, false);
        }
    }

    public void chooseDownRight()
    {

        CookingManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeDownRight)
        {
            changeTarget(placeDownRight);
            ingredientJoystickTarget.transform.SetParent(buttonDownRight.transform, false);
        }
    }

    void changeTarget(IngredientPlace place)
    {
        if (targetedPlace != place)
        {
            targetedPlace.untarget();
            targetedPlace = place;
            targetedPlace.target();
            updateIngredient();
        }
    }

    void updateIngredient()
    {
        addingBoard.ingredientFill(targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>());
        ingredientPanelName.GetComponentInChildren<TextMeshProUGUI>().text = targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>().IngredientName;
    }

    public static void refreshIngredientsWithDelay()
    {
        staticInstance.refresh();
        staticInstance.Invoke("updateIngredient", .25f);
    }

    public static void refreshIngredientsNoDelay()
    {
        if(staticInstance!= null)
        {
            staticInstance.refresh();
            staticInstance.Invoke("updateIngredient", .01f);
        }
    }

    public void refresh()
    {
        Destroy(placeUpLeft.ingredientPreview.gameObject);
        Destroy(placeUpRight.ingredientPreview.gameObject);
        Destroy(placeDownLeft.ingredientPreview.gameObject);
        Destroy(placeDownRight.ingredientPreview.gameObject);
        List<Ingredient> newIngredients = ingredientListManager.newIngredients;
        try
        {
            Instantiate(newIngredients[0].preview, placeUpLeft.transform);
        }
        catch
        {
            Debug.LogError(newIngredients[0].name);
        }
        try
        {
            Instantiate(newIngredients[1].preview, placeUpRight.transform);
        }
        catch
        {
            Debug.LogError(newIngredients[1].name);
        }
        try
        {
            Instantiate(newIngredients[2].preview, placeDownLeft.transform);
        }
        catch
        {
            Debug.LogError(newIngredients[2].name);
        }
        try
        {
            Instantiate(newIngredients[3].preview, placeDownRight.transform);
        }
        catch
        {
            Debug.LogError(newIngredients[3].name);
        }
    }
}
