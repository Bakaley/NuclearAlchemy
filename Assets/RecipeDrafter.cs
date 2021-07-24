using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDrafter : MonoBehaviour
{
    [SerializeField]
    GameObject draftWindow;

    BookPageGenerator bookPageGenerator;

    public enum DRAFT_TYPE
    {
        POTION_BREWING,
        POTION_LEVEL_UP,
        CONSUMABLE,
        NEW_INGREDIENT,
    }

    GameObject variant1;
    GameObject variant2;
    GameObject variant3;

    static RecipeDrafter openedDrafter;

    [SerializeField]
    DRAFT_TYPE draft;

    public static void pick(GameObject recipe)
    {
        openedDrafter.replace(recipe);
    }

    // Start is called before the first frame update
    void Start()
    {
        bookPageGenerator = draftWindow.GetComponent<BookPageGenerator>();
    }

    void beginDraft()
    {
        openedDrafter = this;
        switch (draft)
        {
            case DRAFT_TYPE.POTION_BREWING:
                variant1 = PotionList.constellationList1[Random.Range(0, 3)];
                variant2 = PotionList.constellationList2[Random.Range(0, 3)];

                int n = Random.Range(0, 3);
                if (n < PotionList.constellationListCombo.Count)
                    variant3 = PotionList.constellationListCombo[n];
                else variant3 = PotionList.standardBasicPotionList[Random.Range(0, 9)];

               // Debug.Log(variant1.GetComponent<Recipe>().name);
              // Debug.Log(variant2.GetComponent<Recipe>().name);
               // Debug.Log(variant3.GetComponent<Recipe>().name);

                break;
        }

        int n2 = Random.Range(1, 4);
        List<GameObject> recipesList = new List<GameObject>();
        if (n2 == 1)
        {
            recipesList.Add(variant1);
        }
        if (n2 == 2)
        {
            recipesList.Add(variant1);
            recipesList.Add(variant2);
        }
        if (n2 == 3)
        {
            recipesList.Add(variant1);
            recipesList.Add(variant2);
            recipesList.Add(variant3);
        }

        bookPageGenerator.generate(recipesList);
    }


    void replace(GameObject recipe)
    {
        Debug.Log(recipe.name);
    }
}
