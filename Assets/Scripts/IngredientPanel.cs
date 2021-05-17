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


    Material dissolvingMaterial;

    private static IngredientPlace targetedPlace;

    public static Ingredient currentIngredient
    {
        get
        {
            return targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>();
        }
    }
    GameObject cellJoystick;

    void Awake()
    {
        targetedPlace = placeUpLeft;
    }

    private void Start()
    {
        changeIngredient(placeUpLeft);
    }

    void Update()
    {

    }

    public void chooseUpLeft()
    {
        UIManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeUpLeft)
        {
            changeIngredient(placeUpLeft);
            ingredientJoystickTarget.transform.SetParent(buttonUpLeft.transform, false);
        }
    }

    public void chooseUpRight()
    {
        UIManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeUpRight)
        {
            changeIngredient(placeUpRight);
            ingredientJoystickTarget.transform.SetParent(buttonUpRight.transform, false);
        }
    }

    public void chooseDownLeft()
    {
        UIManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeDownLeft)
        {
            changeIngredient(placeDownLeft);
            ingredientJoystickTarget.transform.SetParent(buttonDownLeft.transform, false);
        }
    }

    public void chooseDownRight()
    {
        UIManager.staticInstance.switchToAddingMode();
        if (targetedPlace != placeDownRight)
        {
            changeIngredient(placeDownRight);
            ingredientJoystickTarget.transform.SetParent(buttonDownRight.transform, false);
        }
    }

    void changeIngredient(IngredientPlace place)
    {
        if (targetedPlace != place)
        {
            targetedPlace.untarget();
            targetedPlace = place;
            addingBoard.ingredientFill(targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>());
            targetedPlace.target();

            ingredientPanelName.GetComponentInChildren<TextMeshProUGUI>().text = targetedPlace.ingredientPreview.ingredient.GetComponent<Ingredient>().IngredientName;
        }
    }

}
