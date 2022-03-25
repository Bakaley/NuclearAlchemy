using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeDrafter : MonoBehaviour
{
    static List<RecipeDrafter> drafters;

    public static void draftersRefresh()
    {
        foreach (RecipeDrafter recipeDrafter in drafters)
        {
            recipeDrafter.recipesList = new List<Recipe>();
            recipeDrafter.variant1 = null;
            recipeDrafter.variant2 = null;
            recipeDrafter.variant3 = null;
            recipeDrafter.refreshCounter();
        }
    }

    public enum DRAFT_TYPE
    {
        POTION_BREWING,
        POTION_LEVEL_UP,
        CONSUMABLE,
        INGREDIENT_BLUEPRINT,
        POTION_BLUEPRINT
    }

    Recipe variant1;
    Recipe variant2;
    Recipe variant3;

    [SerializeField]
    DRAFT_TYPE draft;

    public static void refreshDrafters(DRAFT_TYPE filter)
    {
        foreach(RecipeDrafter recipeDrafter in drafters)
        {
            if(recipeDrafter.draft == filter)
            {
                recipeDrafter.recipesList = new List<Recipe>();
                recipeDrafter.variant1 = null;
                recipeDrafter.variant2 = null;
                recipeDrafter.variant3 = null;
                recipeDrafter.refreshCounter();
            }
        }
    }
    public DRAFT_TYPE DraftType
    {
        get
        {
            return draft;
        }
    }

    TextMeshPro counter;

    private void Awake()
    {
        recipesList = new List<Recipe>();
        if (drafters == null) drafters = new List<RecipeDrafter>();
        drafters.Add(this);
    }

    private void Start()
    {
        if(DraftType != DRAFT_TYPE.POTION_BREWING)
        {
            counter = transform.parent.GetComponentInChildren<TextMeshPro>();
            refreshCounter();
        }

    }

    void refreshCounter()
    {
        if (counter) counter.text = Counter + "";
    }

    public int Counter
    {
        get
        {
            int n = 0;
            switch (draft)
            {
                case DRAFT_TYPE.INGREDIENT_BLUEPRINT:
                    foreach (Recipe recipe in DraftModule.pickedRecipes)
                    {
                        if (recipe.Type == Recipe.RECIPE_TYPE.NEW_INGREDIENT) n++;
                    }
                    return IngredientListManager.BlueprintsCount - n;
                case DRAFT_TYPE.POTION_LEVEL_UP:
                    foreach (Recipe recipe in DraftModule.pickedRecipes)
                    {
                        if (recipe.Type == Recipe.RECIPE_TYPE.POTION_LEVEL_UP) n++;
                    }
                    return PotionListManager.AvaliableToLevelUPCount - n;
                case DRAFT_TYPE.POTION_BLUEPRINT:
                    foreach (Recipe recipe in DraftModule.pickedRecipes)
                    {
                        if (recipe.Type == Recipe.RECIPE_TYPE.POTION_BLUEPRINT) n++;
                    }
                    return PotionListManager.AvaliableToLearnCount - n;
            }
            return 0;
        }
    }

    public List<Recipe> recipesList { get; private set; }

    public void beginDraft()
    {
        if (draft != DRAFT_TYPE.POTION_BREWING && UIManager.cookingMode) return;
        if (DraftModule.CurrentRecipes.Count >= 3)
        {
            UIManager.showHint("Сначала приготовьте уже выбранные рецепты)", 1f);
            return;
        }
        switch (draft)
        {
            case DRAFT_TYPE.POTION_BREWING:
                PotionListManager.LevelUPpingListRefresh();
                refreshDrafters(DRAFT_TYPE.POTION_LEVEL_UP);
                List<Recipe> constList1 = new List<Recipe>(PotionListManager.ConstellationList1);
                List<Recipe> constList2 = new List<Recipe>(PotionListManager.ConstellationList2);
                List<Recipe> constCombo = new List<Recipe>(PotionListManager.ConstellationListCombo);
                List<Recipe> basicList = new List<Recipe>(PotionListManager.BasicPotionList);
                foreach (Recipe recipe in DraftModule.pickedRecipes)
                {
                    if (constList1.Contains(recipe)) constList1.Remove(recipe);
                    if (constList2.Contains(recipe)) constList2.Remove(recipe);
                    if (constCombo.Contains(recipe)) constCombo.Remove(recipe);
                    if (basicList.Contains(recipe)) basicList.Remove(recipe);
                }

                //лист будет пустой, если созвездие 1 == NONE
                if(variant1 == null)
                {
                    if (constList1.Count != 0)
                    {
                        variant1 = constList1[Random.Range(0, constList1.Count)];
                        constList1.Remove(variant1);
                    }
                    else
                    {
                        variant1 = basicList[Random.Range(0, basicList.Count)];
                        basicList.Remove(variant1);
                    }
                }

                //лист будет пустой, если созвездие 2 == NONE
                if (variant2 == null)
                {
                    if (constList2.Count != 0)
                    {
                        variant2 = constList2[Random.Range(0, constList2.Count)];
                        constList2.Remove(variant2);
                    }
                    else
                    {
                        variant2 = basicList[Random.Range(0, basicList.Count)];
                        basicList.Remove(variant2);
                    }
                }

                //лист будет пустой, если одно из созвездий == NONE, или если все доступные зелья пикнуты
                //если проваливаемся в листе, пикаем из базовых
                if (variant3 == null)
                {
                    int n = Random.Range(0, 3);
                    if (n < constCombo.Count)
                    {
                        variant3 = constCombo[Random.Range(0, constCombo.Count)];
                        constCombo.Remove(variant3);
                    }
                    else
                    {
                        variant3 = basicList[Random.Range(0, basicList.Count)];
                        basicList.Remove(variant3);
                    }
                }
                break;

            case DRAFT_TYPE.INGREDIENT_BLUEPRINT:

                List<Recipe> constIngrList1 = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION1, Ingredient.AVALIABILITY.KNOWN_BLUEPRINT));
                List<Recipe> constIngrList2 = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION2, Ingredient.AVALIABILITY.KNOWN_BLUEPRINT));
                List<Recipe> basicIngrList = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION.NONE, Ingredient.AVALIABILITY.KNOWN_BLUEPRINT));
                if (ConstellationManager.CONSTELLATION1 == ConstellationManager.CONSTELLATION.NONE) constIngrList1 = basicIngrList;
                if (ConstellationManager.CONSTELLATION2 == ConstellationManager.CONSTELLATION.NONE) constIngrList2 = basicIngrList;
                foreach (Recipe recipe in DraftModule.pickedRecipes)
                {
                    if (constIngrList1.Contains(recipe)) constIngrList1.Remove(recipe);
                    if (constIngrList2.Contains(recipe)) constIngrList2.Remove(recipe);
                    if (basicIngrList.Contains(recipe)) basicIngrList.Remove(recipe);
                }
                foreach (Recipe recipe in recipesList)
                {
                    if (constIngrList1.Contains(recipe)) constIngrList1.Remove(recipe);
                    if (constIngrList2.Contains(recipe)) constIngrList2.Remove(recipe);
                    if (basicIngrList.Contains(recipe)) basicIngrList.Remove(recipe);
                }
                //первый список пуст, если все базовые изучены, или если все доступные пикнуты
                if (variant1 == null)
                {
                    if (basicIngrList.Count != 0)
                    {
                        variant1 = basicIngrList[Random.Range(0, basicIngrList.Count)];
                        basicIngrList.Remove(variant1);
                    }
                }

                //второй список пуст, если в созвездии все изучены, если в созвездии нет ингредиентов, или если все доступные пикнуты
                if (variant2 == null)
                {
                    if (constIngrList1.Count != 0)
                    {
                        variant2 = constIngrList1[Random.Range(0, constIngrList1.Count)];
                        constIngrList1.Remove(variant2);
                    }
                }

                //третий список пуст, если в созвездии все изучены, если в созвездии нет ингредиентов, или если все доступные пикнуты
                if (variant3 == null)
                {
                    if (constIngrList2.Count != 0)
                    {
                        variant3 = constIngrList2[Random.Range(0, constIngrList2.Count)];
                        constIngrList2.Remove(variant3);
                    }
                }


                if (variant1 == null)
                {
                    if (constIngrList1.Count != 0)
                    {
                        variant1 = constIngrList1[Random.Range(0, constIngrList1.Count)];
                        constIngrList1.Remove(variant1);
                    }
                    else if (constIngrList2.Count != 0)
                    {
                        variant1 = constIngrList2[Random.Range(0, constIngrList2.Count)];
                        constIngrList2.Remove(variant1);
                    }
                }

                if (variant2 == null)
                {
                    if (constIngrList2.Count != 0)
                    {
                        variant2 = constIngrList2[Random.Range(0, constIngrList2.Count)];
                        constIngrList2.Remove(variant2);
                    }
                    else if (basicIngrList.Count != 0)
                    {
                        variant2 = basicIngrList[Random.Range(0, basicIngrList.Count)];
                        basicIngrList.Remove(variant2);
                    }
                }

                if (variant3 == null)
                {
                    if (constIngrList1.Count != 0)
                    {
                        variant3 = constIngrList1[Random.Range(0, constIngrList1.Count)];
                        constIngrList1.Remove(variant3);
                    }
                    else if (basicIngrList.Count != 0)
                    {
                        variant3 = basicIngrList[Random.Range(0, basicIngrList.Count)];
                        basicIngrList.Remove(variant3);
                    }
                }


                break;

            case DRAFT_TYPE.CONSUMABLE:
                List<Recipe> constConsumableList1 = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION1, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
                List<Recipe> constConsumableList2 = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION2, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
                List<Recipe> basicConsumableList = new List<Recipe>(IngredientListManager.getIngredientBlueprints(ConstellationManager.CONSTELLATION.NONE, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
                if (ConstellationManager.CONSTELLATION1 == ConstellationManager.CONSTELLATION.NONE) constConsumableList1 = basicConsumableList;
                if (ConstellationManager.CONSTELLATION2 == ConstellationManager.CONSTELLATION.NONE) constConsumableList2 = basicConsumableList;


                foreach (Recipe recipe in DraftModule.pickedRecipes)
                {
                    if (constConsumableList1.Contains(recipe)) constConsumableList1.Remove(recipe);
                    if (constConsumableList2.Contains(recipe)) constConsumableList2.Remove(recipe);
                    if (basicConsumableList.Contains(recipe)) basicConsumableList.Remove(recipe);
                }
                foreach (Recipe recipe in recipesList)
                {
                    if (constConsumableList1.Contains(recipe)) constConsumableList1.Remove(recipe);
                    if (constConsumableList2.Contains(recipe)) constConsumableList2.Remove(recipe);
                    if (basicConsumableList.Contains(recipe)) basicConsumableList.Remove(recipe);
                }

                //первый список пуст, если все доступные пикнуты
                if (variant1 == null)
                {
                    if (basicConsumableList.Count != 0)
                    {
                        variant1 = basicConsumableList[Random.Range(0, basicConsumableList.Count)];
                        basicConsumableList.Remove(variant1);
                    }
                }

                //второй список пуст, если в созвездии нет изученных, если в созвездии нет ингредиентов, или если все доступные пикнуты
                if (variant2 == null)
                {
                    if (constConsumableList1.Count != 0)
                    {
                        variant2 = constConsumableList1[Random.Range(0, constConsumableList1.Count)];
                        constConsumableList1.Remove(variant2);
                    }
                }

                //третий список пуст, если в созвездии нет изученных, если в созвездии нет ингредиентов, или если все доступные пикнуты
                if (variant3 == null)
                {
                    if (constConsumableList2.Count != 0)
                    {
                        variant3 = constConsumableList2[Random.Range(0, constConsumableList2.Count)];
                        constConsumableList2.Remove(variant3);
                    }
                }

                if (variant1 == null)
                {
                    if (constConsumableList1.Count != 0)
                    {
                        variant1 = constConsumableList1[Random.Range(0, constConsumableList1.Count)];
                        constConsumableList1.Remove(variant1);
                    }
                    else if (constConsumableList2.Count != 0)
                    {
                        variant1 = constConsumableList2[Random.Range(0, constConsumableList2.Count)];
                        constConsumableList2.Remove(variant1);
                    }
                }

                if (variant2 == null)
                {
                    if (constConsumableList2.Count != 0)
                    {
                        variant2 = constConsumableList2[Random.Range(0, constConsumableList2.Count)];
                        constConsumableList2.Remove(variant2);
                    }
                    else if (basicConsumableList.Count != 0)
                    {
                        variant2 = basicConsumableList[Random.Range(0, basicConsumableList.Count)];
                        basicConsumableList.Remove(variant2);
                    }
                }

                if (variant3 == null)
                {
                    if (constConsumableList1.Count != 0)
                    {
                        variant3 = constConsumableList1[Random.Range(0, constConsumableList1.Count)];
                        constConsumableList1.Remove(variant3);
                    }
                    else if (basicConsumableList.Count != 0)
                    {
                        variant3 = basicConsumableList[Random.Range(0, basicConsumableList.Count)];
                        basicConsumableList.Remove(variant3);
                    }
                }
                break;


                //git test
            case DRAFT_TYPE.POTION_LEVEL_UP:
                refreshDrafters(DRAFT_TYPE.POTION_BREWING);
                List<Recipe> constlvlUPList1 = new List<Recipe>(PotionListManager.Const1ToLevelUP);
                List<Recipe> constlvlUPList2 = new List<Recipe>(PotionListManager.Const2ToLevelUP);
                List<Recipe> constlvlUPCombo = new List<Recipe>(PotionListManager.ConstComboToLevelUP);
                List<Recipe> basilvlUPcList = new List<Recipe>(PotionListManager.BasicToLevelUP);
                foreach (Recipe recipe in DraftModule.pickedRecipes)
                {
                    if (constlvlUPList1.Contains(recipe)) constlvlUPList1.Remove(recipe);
                    if (constlvlUPList2.Contains(recipe)) constlvlUPList2.Remove(recipe);
                    if (constlvlUPCombo.Contains(recipe)) constlvlUPCombo.Remove(recipe);
                    if (basilvlUPcList.Contains(recipe)) basilvlUPcList.Remove(recipe);
                }
                foreach (Recipe recipe in recipesList)
                {
                    if (constlvlUPList1.Contains(recipe)) constlvlUPList1.Remove(recipe);
                    if (constlvlUPList2.Contains(recipe)) constlvlUPList2.Remove(recipe);
                    if (constlvlUPCombo.Contains(recipe)) constlvlUPCombo.Remove(recipe);
                    if (basilvlUPcList.Contains(recipe)) basilvlUPcList.Remove(recipe);
                }
                //первый лист пустой, если созвездие не выбрано, если все зелья уже улучшены, или если все доступные пикнуты
                if (variant1 == null)
                {
                    if(constlvlUPList1.Count != 0)
                    {
                        variant1 = constlvlUPList1[Random.Range(0, constlvlUPList1.Count)];
                        constlvlUPList1.Remove(variant1);
                    }
                }
                //второй лист пустой, если созвездие не выбрано, если все зелья уже улучшены, или если все доступные пикнуты
                if (variant2 == null)
                {
                    if (constlvlUPList2.Count != 0)
                    {
                        variant2 = constlvlUPList2[Random.Range(0, constlvlUPList2.Count)];
                        constlvlUPList2.Remove(variant2);
                    }
                }
                //третий лист пустой, если созвездие не выбрано, если нету известных, если все уже улучшены, или если все доступные пикнуты
                if (variant3 == null)
                {
                    if (constlvlUPCombo.Count != 0)
                    {
                        variant3 = constlvlUPCombo[Random.Range(0, constlvlUPCombo.Count)];
                        constlvlUPCombo.Remove(variant3);
                    }
                }

                if (variant1 == null)
                {
                    if (constlvlUPList2.Count != 0)
                    {
                        variant1 = constlvlUPList2[Random.Range(0, constlvlUPList2.Count)];
                        constlvlUPList2.Remove(variant1);
                    }
                    else if (constlvlUPCombo.Count != 0)
                    {
                        variant1 = constlvlUPCombo[Random.Range(0, constlvlUPCombo.Count)];
                        constlvlUPCombo.Remove(variant1);
                    }
                    else if (basilvlUPcList.Count != 0)
                    {
                        variant1 = basilvlUPcList[Random.Range(0, basilvlUPcList.Count)];
                        basilvlUPcList.Remove(variant1);
                    }
                }

                if (variant2 == null)
                {
                    if (constlvlUPList1.Count != 0)
                    {
                        variant2 = constlvlUPList1[Random.Range(0, constlvlUPList1.Count)];
                        constlvlUPList1.Remove(variant2);
                    }
                    else if (constlvlUPCombo.Count != 0)
                    {
                        variant2 = constlvlUPCombo[Random.Range(0, constlvlUPCombo.Count)];
                        constlvlUPCombo.Remove(variant2);
                    }
                    else if (basilvlUPcList.Count != 0)
                    {
                        variant2 = basilvlUPcList[Random.Range(0, basilvlUPcList.Count)];
                        basilvlUPcList.Remove(variant2);
                    }
                }

                if (variant3 == null)
                {
                    if (basilvlUPcList.Count != 0)
                    {
                        variant3 = basilvlUPcList[Random.Range(0, basilvlUPcList.Count)];
                        basilvlUPcList.Remove(variant3);
                    }
                    else if (constlvlUPList1.Count != 0)
                    {
                        variant3 = constlvlUPList1[Random.Range(0, constlvlUPList1.Count)];
                        constlvlUPList1.Remove(variant3);
                    }
                    else if (constlvlUPList2.Count != 0)
                    {
                        variant3 = constlvlUPList2[Random.Range(0, constlvlUPList2.Count)];
                        constlvlUPList2.Remove(variant3);
                    }
                }

                //если зелье выпало в драфте, то его нельзя сварить
                if (variant1 != null) PotionListManager.LevelUPpingList.Add(variant1);
                if (variant2 != null) PotionListManager.LevelUPpingList.Add(variant2);
                if (variant3 != null) PotionListManager.LevelUPpingList.Add(variant3);
                break;

            case DRAFT_TYPE.POTION_BLUEPRINT:
                List<Recipe> constLearnList1 = new List<Recipe>(PotionListManager.Const1ToLearn);
                List<Recipe> constLearnList2 = new List<Recipe>(PotionListManager.Const2ToLearn);
                List<Recipe> constLearnCombo = new List<Recipe>(PotionListManager.ConstComboToLearn);
                foreach (Recipe recipe in DraftModule.pickedRecipes)
                {
                    if (constLearnList1.Contains(recipe)) constLearnList1.Remove(recipe);
                    if (constLearnList2.Contains(recipe)) constLearnList2.Remove(recipe);
                    if (constLearnCombo.Contains(recipe)) constLearnCombo.Remove(recipe);
                }
                foreach (Recipe recipe in recipesList)
                {
                    if (constLearnList1.Contains(recipe)) constLearnList1.Remove(recipe);
                    if (constLearnList2.Contains(recipe)) constLearnList2.Remove(recipe);
                    if (constLearnCombo.Contains(recipe)) constLearnCombo.Remove(recipe);
                }

                //первый лист пустой, если созвездие не выбрано, если нету чертежей, если все зелья уже известны, или если все доступные пикнуты
                if (variant1 == null)
                {
                    if (constLearnList1.Count != 0)
                    {
                        variant1 = constLearnList1[Random.Range(0, constLearnList1.Count)];
                        constLearnList1.Remove(variant1);
                    }
                }
                //второй лист пустой, если созвездие не выбрано, если нету чертежей, если все зелья уже известны, или если все доступные пикнуты
                if (variant2 == null)
                {
                    if (constLearnList2.Count != 0)
                    {
                        variant2 = constLearnList2[Random.Range(0, constLearnList2.Count)];
                        constLearnList2.Remove(variant2);
                    }
                }
                //третий лист пустой, если созвездие не выбрано, если нету чертежей, если все зелья уже известны, или если все доступные пикнуты
                if (variant3 == null)
                {
                    if (constLearnCombo.Count != 0)
                    {
                        variant3 = constLearnCombo[Random.Range(0, constLearnCombo.Count)];
                        constLearnCombo.Remove(variant3);
                    }
                }

                if (variant1 == null)
                {
                    if (constLearnList2.Count != 0)
                    {
                        variant1 = constLearnList2[Random.Range(0, constLearnList2.Count)];
                        constLearnList2.Remove(variant1);
                    }
                    else if (constLearnCombo.Count != 0)
                    {
                        variant1 = constLearnCombo[Random.Range(0, constLearnCombo.Count)];
                        constLearnCombo.Remove(variant1);
                    }
                }

                if (variant2 == null)
                {
                    if (constLearnList1.Count != 0)
                    {
                        variant2 = constLearnList1[Random.Range(0, constLearnList1.Count)];
                        constLearnList1.Remove(variant2);
                    }
                    else if (constLearnCombo.Count != 0)
                    {
                        variant2 = constLearnCombo[Random.Range(0, constLearnCombo.Count)];
                        constLearnCombo.Remove(variant2);
                    }

                }

                if (variant3 == null)
                {
                    if (constLearnList1.Count != 0)
                    {
                        variant3 = constLearnList1[Random.Range(0, constLearnList1.Count)];
                        constLearnList1.Remove(variant3);
                    }
                    else if (constLearnList2.Count != 0)
                    {
                        variant3 = constLearnList2[Random.Range(0, constLearnList2.Count)];
                        constLearnList2.Remove(variant3);
                    }
                }
                break;
        }
        recipesList = new List<Recipe>();
        recipesList.Add(variant1);
        recipesList.Add(variant2);
        recipesList.Add(variant3);
        DraftModule.beginDraft(this, recipesList);
    }

}
