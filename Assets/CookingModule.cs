using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    GameObject recipePanel;
    [SerializeField]
    GameObject preparingDisslovingModule;
    [SerializeField]
    GameObject preparingPanel;
    [SerializeField]
    GameObject preparingInfoPanel;
    [SerializeField]
    GameObject draftWindow;

    [SerializeField]
    GameObject recipeTableSampler;

    static GameObject staticRecipeTableSampler;
    static Dictionary<Recipe, List<GameObject>> preparedRecipes;
    static Dictionary<Recipe, GameObject> recipeTables;

    public static List<Recipe> pickedRecipes
    {
        get
        {
            return new List<Recipe>(recipeTables.Keys);
        }
    }


    static RecipeDrafter openedDrafter;
    static CookingModule staticInstance;

    private void Awake()
    {
        preparedRecipes = new Dictionary<Recipe, List<GameObject>>();
        recipeTables = new Dictionary<Recipe, GameObject>();
        staticInstance = this;
    }

    public static void beginDraft(RecipeDrafter recipeDrafter, List<Recipe> recipes)
    {
        openedDrafter = recipeDrafter;
        staticInstance.draftWindow.GetComponent<DraftWindow>().generate(recipes);
    }

    public static void pick(Recipe recipe)
    {
        GameObject recipeTable = Instantiate(staticInstance.recipeTableSampler, staticInstance.recipePanel.transform);
        recipeTable.GetComponent<RecipeTable>().fillPage(recipe);
        DissolvingElement[] list = recipeTable.GetComponentsInChildren<DissolvingElement>();
        foreach (DissolvingElement elem in list)
        {
            elem.appear();
        }
        Destroy(openedDrafter.gameObject);
        openedDrafter = null;

        recipeTables.Add(recipe, recipeTable);
        preparedRecipes.Add(recipe, new List<GameObject>());
    }

    public static void prepareRecipe(RecipeTable table, Recipe recipe)
    {
        GameObject sampler = null;
        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                sampler = Instantiate(staticInstance.preparingPanel.GetComponent<PreparedPotionSamplers>().preparedPotionSampler, staticInstance.preparingPanel.transform);
                sampler.GetComponent<PreparedRecipe>().recipeIcon.GetComponent<SpriteRenderer>().sprite = recipe.gameObject.GetComponent<PotionRecipe>().Icon;
                sampler.GetComponent<PreparedRecipe>().recipe = recipe;
                sampler.GetComponent<PreparedRecipe>().counter.GetComponent<TextMeshProUGUI>().text = recipe.GetComponent<PotionRecipe>().PotionCount + "";
                break;
        }
        preparedRecipes[recipe].Add(sampler);

        if(preparedRecipesCount == 1)
        {
            DissolvingElement[] elems = staticInstance.preparingDisslovingModule.GetComponentsInChildren<DissolvingElement>(true);
            foreach (DissolvingElement elem in elems)
            {
                elem.appear();
            }
        }
        else
        {
            DissolvingElement[] elems = sampler.GetComponentsInChildren<DissolvingElement>(true);
            foreach (DissolvingElement elem in elems)
            {
                elem.appear();
            }
        }
        updateInfoPanel();
    }

    public static void unprepareRecipe(PreparedRecipe preparedRecipe)
    {
        preparedRecipes[preparedRecipe.recipe].Remove(preparedRecipe.gameObject);
        if (preparedRecipes[preparedRecipe.recipe].Count == 0) recipeTables[preparedRecipe.recipe].GetComponent<RecipeTable>().setTargeted(false);
        
        if (preparedRecipesCount == 0)
        {
            preparedRecipe.dissolveIn(.4f);
            DissolvingElement[] elems = staticInstance.preparingDisslovingModule.GetComponentsInChildren<DissolvingElement>(true);
            foreach (DissolvingElement elem in elems)
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
        foreach (Recipe recipe in pickedRecipes)
        {
            if (!recipeTables[recipe].GetComponent<RecipeTable>().recipeRequiermentCheck())
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
}
