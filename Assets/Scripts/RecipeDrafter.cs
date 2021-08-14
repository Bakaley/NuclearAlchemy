using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDrafter : MonoBehaviour
{
    DraftWindow bookPageGenerator;

    public enum DRAFT_TYPE
    {
        POTION_BREWING,
        POTION_LEVEL_UP,
        CONSUMABLE,
        NEW_INGREDIENT,
    }

    Recipe variant1;
    Recipe variant2;
    Recipe variant3;

    [SerializeField]
    DRAFT_TYPE draft;


    public void beginDraft()
    {
        switch (draft)
        {
            case DRAFT_TYPE.POTION_BREWING:
                variant1 = PotionList.constellationList1[Random.Range(0, 3)].GetComponent<Recipe>();
                while (CookingModule.pickedRecipes.Contains(variant1)) variant1 = PotionList.constellationList1[Random.Range(0, 3)].GetComponent<Recipe>();
                variant2 = PotionList.constellationList2[Random.Range(0, 3)].GetComponent<Recipe>();
                while (CookingModule.pickedRecipes.Contains(variant2)) variant2 = PotionList.constellationList2[Random.Range(0, 3)].GetComponent<Recipe>();


                int n = Random.Range(0, 3);
                if (n < PotionList.constellationListCombo.Count)
                {
                    variant3 = PotionList.constellationListCombo[n].GetComponent<Recipe>();
                    while (CookingModule.pickedRecipes.Contains(variant3))
                    {
                        n = Random.Range(0, 3);
                        variant3 = PotionList.constellationListCombo[n].GetComponent<Recipe>();
                    }
                }
                else
                {
                    variant3 = PotionList.standardBasicPotionList[Random.Range(0, 9)].GetComponent<Recipe>();
                    while (CookingModule.pickedRecipes.Contains(variant3))
                    {
                        variant3 = PotionList.standardBasicPotionList[Random.Range(0, 9)].GetComponent<Recipe>();
                    }
                }
                break;
        }

        List<Recipe> recipesList = new List<Recipe>();
        recipesList.Add(variant1);
        recipesList.Add(variant2);
        recipesList.Add(variant3);
        CookingModule.beginDraft(this, recipesList);
    }

}
