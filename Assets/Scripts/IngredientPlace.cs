using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPlace : MonoBehaviour
{
    public IngredientPanel ingredientPanel;
    public IngredientPreview ingredientPreview;

    [SerializeField]
    bool inventoryFlag;

    Image image;

    [SerializeField]
    Sprite defaultSprite;
    [SerializeField]
    Sprite targetedSprite;


    public void target()
    {
        image.sprite = targetedSprite;
        ingredientPreview.essense1.material = ingredientPreview.TargetedEssenceMaterial;
        ingredientPreview.essense2.material = ingredientPreview.TargetedEssenceMaterial;
    }

    public void untarget()
    {
        image.sprite = defaultSprite;
        ingredientPreview.essense1.material = ingredientPreview.DefaultEssenceMaterial;
        ingredientPreview.essense2.material = ingredientPreview.DefaultEssenceMaterial;
    }

    void Start()
    {
        ingredientPreview = GetComponentInChildren<IngredientPreview>();
        ingredientPanel = GetComponentInParent<IngredientPanel>();
        image = GetComponentInChildren<Button>().GetComponent<Image>();
    }

    void Update()
    {
        
    }
}
