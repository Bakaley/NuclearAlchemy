using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CookingModule : MonoBehaviour
{
    [SerializeField]
    int maximumPreparedRecipes;

    [SerializeField]
    string baseInfoString = "Столбцов: XXX; эссенций: YYY%";

    public static bool ableToNewPreparedRecipe
    {
        get
        {
            return preparedRecipesCount < staticInstance.maximumPreparedRecipes;
        }
    }

    public static int preparedRecipesCount {
        get
        {
                int n = 0;
                foreach (KeyValuePair<Recipe, List<GameObject>> pair in preparedRecipes)
                {
                    n += pair.Value.Count;
                }
                return n;
            }
        }


    [SerializeField]
    GameObject preparingDisslovingModule;
    [SerializeField]
    GameObject preparingPanel;
    [SerializeField]
    GameObject preparingInfoPanel;

    static GameObject staticRecipeTableSampler;
    static Dictionary<Recipe, List<GameObject>> preparedRecipes;

    static List<Recipe> recipesToAddOnAwake;


    static CookingModule staticInstance;

    private void Awake()
    {
        preparedRecipes = new Dictionary<Recipe, List<GameObject>>();
        staticInstance = this;
        if (recipesToAddOnAwake != null)
            foreach (Recipe recipe in recipesToAddOnAwake)
            {
                preparedRecipes.Add(recipe, new List<GameObject>());
            }
    }

    public static void addRecipe(Recipe recipe)
    {
        if (preparedRecipes != null)
        {
            preparedRecipes.Add(recipe, new List<GameObject>());
        }
        else
        {
            if (recipesToAddOnAwake == null) recipesToAddOnAwake = new List<Recipe>();
            recipesToAddOnAwake.Add(recipe);
        }
    }

    public static void prepareRecipe(Recipe recipe)
    {
        GameObject sampler = null;
        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotionSampler, staticInstance.preparingPanel.transform);
                sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Potion>().Icon;
                sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Potion>().IconColor;
                sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                sampler.GetComponent<PreparedRecipe>().counter.GetComponent<TextMeshProUGUI>().text = recipe.GetComponent<Potion>().PotionCount + "";
                break;
            case Recipe.RECIPE_TYPE.CONSUMABLE:
                sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedRecipeConsumableSampler, staticInstance.preparingPanel.transform);
                sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Ingredient>().IngredientIcon;
                sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Ingredient>().IngredientIconColor;
                sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                break;

            case Recipe.RECIPE_TYPE.POTION_LEVEL_UP:
                if (preparedRecipes[recipe].Count != 0)
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotionSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Potion>().Icon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Potion>().IconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                    sampler.GetComponent<PreparedRecipe>().counter.GetComponent<TextMeshProUGUI>().text = recipe.GetComponent<Potion>().nextLvLPotionCount + "";
                }
                else
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotLvlUpSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Potion>().Icon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Potion>().IconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                }
                break;
            case Recipe.RECIPE_TYPE.POTION_BLUEPRINT:
                if (preparedRecipes[recipe].Count != 0)
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotionSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Potion>().Icon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Potion>().IconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                    sampler.GetComponent<PreparedRecipe>().counter.GetComponent<TextMeshProUGUI>().text = recipe.GetComponent<Potion>().nextLvLPotionCount + "";
                }
                else
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotResearchSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Potion>().Icon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Potion>().IconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                }
                break;
            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                if (preparedRecipes[recipe].Count != 0)
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedRecipeConsumableSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Ingredient>().IngredientIcon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Ingredient>().IngredientIconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                    break;
                }
                else
                {
                    sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedIngrResearchSampler, staticInstance.preparingPanel.transform);
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().sprite = recipe.gameObject.GetComponent<Ingredient>().IngredientIcon;
                    sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<Image>().color = recipe.gameObject.GetComponent<Ingredient>().IngredientIconColor;
                    sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                }
                break;
        }
        preparedRecipes[recipe].Add(sampler);

        if(preparedRecipesCount == 1)
        {
            IDissolving[] elems = staticInstance.preparingDisslovingModule.GetComponentsInChildren<IDissolving>(true);
            foreach (IDissolving elem in elems)
            {
                elem.appear();
            }
        }
        else
        {
            IDissolving[] elems = sampler.GetComponentsInChildren<IDissolving>(true);
            foreach (IDissolving elem in elems)
            {
                elem.appear();
            }
        }
        updateInfoPanel();
    }

    public static void unprepareRecipe(PreparedRecipe preparedRecipe, bool redirecting = true)
    {
        //если отменяется единоразовая награда, то отменяется последняя из наград у этого рецепта
        if(preparedRecipe.RewardType == PreparedRecipe.REWARD_TYPE.ONETIME_REWARD && redirecting)
        {
            preparedRecipe = preparedRecipes[preparedRecipe.recipe][preparedRecipes[preparedRecipe.recipe].Count - 1].GetComponent<PreparedRecipe>();
        }
        preparedRecipes[preparedRecipe.recipe].Remove(preparedRecipe.gameObject);
        if (preparedRecipes[preparedRecipe.recipe].Count == 0) DraftModule.recipeTables[preparedRecipe.recipe].GetComponent<RecipeTable>().setTargeted(false);
        
        if (preparedRecipesCount == 0)
        {
            preparedRecipe.dissolveIn(.4f);
            IDissolving[] elems = staticInstance.preparingDisslovingModule.GetComponentsInChildren<IDissolving>(true);
            foreach (IDissolving elem in elems)
            {
                elem.disappear();
            }
        }
        else
        {
            preparedRecipe.dissolveIn(.4f);
            updateInfoPanel();
        }
    }

    static void updateInfoPanel()
    {
        string replacingString = staticInstance.baseInfoString;
        replacingString = replacingString.Replace("XXX", preparedRecipesCount + "");
        replacingString = replacingString.Replace("YYY", preparedRecipesCount * 25 + "");
        staticInstance.preparingInfoPanel.GetComponent<TextMeshProUGUI>().text = replacingString;
    }

    public static void updatePreparedPotions()
    {
        List<PreparedRecipe> recipesToUnprepare = new List<PreparedRecipe>();
        foreach (Recipe recipe in DraftModule.pickedRecipes)
        {
            DraftModule.recipeTables[recipe].GetComponent<RecipeTable>().crossingLinesUpdate();
            if (!DraftModule.recipeTables[recipe].GetComponent<RecipeTable>().recipeRequiermentCheck())
            {
                foreach (GameObject preparedRecipe in preparedRecipes[recipe])
                {
                    recipesToUnprepare.Add(preparedRecipe.GetComponent<PreparedRecipe>());
                }
            }
        }
        foreach(PreparedRecipe rec in recipesToUnprepare)
        {
            rec.unprepare();
        }
    }
    public static void cancelRecipe(Recipe recipe)
    {
        PotionListManager.LevelUPpingList.Remove(recipe);
        if(staticInstance != null)
        {
            List<GameObject> recipesToUnprepare = new List<GameObject>(preparedRecipes[recipe]);
            foreach (GameObject prepRecipe in recipesToUnprepare)
            {
                prepRecipe.GetComponent<PreparedRecipe>().unprepareWithNoredirecting();
            }
            preparedRecipes.Remove(recipe);
        }
        if (recipesToAddOnAwake != null && recipesToAddOnAwake.Contains(recipe)) recipesToAddOnAwake.Remove(recipe);
    }

    [SerializeField]
    GameObject inconsistenciesPanel, inconsistenciesCaption, inconsistencyContainer, inconsistencyStringSampler;
    public static void showInconsistencyPanel (List<string> inconsistancies)
    {
        staticInstance.inconsistenciesPanel.SetActive(true);
        string caption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Несоответствия" : "Inconsistencies";
        staticInstance.inconsistenciesCaption.GetComponent<TextMeshProUGUI>().text = caption;
        foreach (string incons in inconsistancies)
        {
            GameObject inconsGameObject = Instantiate(staticInstance.inconsistencyStringSampler, staticInstance.inconsistencyContainer.transform);
            inconsGameObject.GetComponent<TextMeshProUGUI>().text = incons;
        }
    }

    public static void hideInconsistencyPanel()
    {
        foreach (Transform child in staticInstance.inconsistencyContainer.transform)
        {
            if (child != staticInstance.inconsistencyContainer.transform) Destroy(child.gameObject);
        }
        staticInstance.inconsistenciesPanel.SetActive(false);
    }

    public void cook()
    {
        List<Recipe> toDestroy = new List<Recipe>();
        foreach (KeyValuePair<Recipe, List<GameObject>> pair in preparedRecipes)
        {
            foreach (GameObject preparedRecipe in pair.Value)
            {
                switch (preparedRecipe.GetComponent<PreparedRecipe>().RecipeType)
                {
                    case PreparedRecipe.RECIPE_TYPE.POTION_BLUEPRINT:
                        PotionListManager.researchPotion(preparedRecipe.GetComponent<PreparedRecipe>().recipe.GetComponent<Potion>());
                       
                        break;
                    case PreparedRecipe.RECIPE_TYPE.POTIONLEVELUP:
                        PotionListManager.levelUpPotion(preparedRecipe.GetComponent<PreparedRecipe>().recipe.GetComponent<Potion>());
                        break;
                    case PreparedRecipe.RECIPE_TYPE.POTION_BREWING:
                        Potion recipe = preparedRecipe.GetComponent<PreparedRecipe>().recipe.GetComponent<Potion>();
                        int bottles = (int)recipe.PotionCount;
                        int probability = (int)((recipe.PotionCount - bottles)*100);
                        int r = Random.Range(0, 100);
                        if (r < probability) bottles++;
                        PlayerInventory.addPotion(recipe, bottles);
                        break;
                    case PreparedRecipe.RECIPE_TYPE.INGREDIENT_BLUEPRINT:
                        IngredientListManager.researchIngredient(preparedRecipe.GetComponent<PreparedRecipe>().recipe.GetComponent<Ingredient>());
                        break;
                    case PreparedRecipe.RECIPE_TYPE.CONSUMABLE:
                        Ingredient consumable = preparedRecipe.GetComponent<PreparedRecipe>().recipe.GetComponent<Ingredient>();
                        PlayerInventory.addIngredient(consumable, 1);
                        break;
                }
                if (!toDestroy.Contains(preparedRecipe.GetComponent<PreparedRecipe>().recipe)) toDestroy.Add(preparedRecipe.GetComponent<PreparedRecipe>().recipe);
            }
        }

        foreach (Recipe rec in toDestroy)
        {
            DraftModule.cancelRecipe(rec);
        }
    }
}
