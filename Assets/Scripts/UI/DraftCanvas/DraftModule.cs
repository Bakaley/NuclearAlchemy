using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DraftModule : MonoBehaviour
{
    static RecipeDrafter openedDrafter;
    static DraftModule staticInstance;

    [SerializeField]
    GameObject recipePanel;
    [SerializeField]
    GameObject recipeTableSampler;

    [SerializeField]
    GameObject bookPageSampler, bookPageLimitSampler, container, recipeDrafterSampler;

    static GameObject page1 = null;
    static GameObject page2 = null;
    static GameObject page3 = null;

    private void Awake()
    {
        staticInstance = this;
        recipeTables = new Dictionary<Recipe, GameObject>();
        StartCoroutine(recipeSpawn());
    }

    public static Camera CookingCamera
    {
        get
        {
            return staticInstance.GetComponentInParent<Camera>();
        }
    }

    void Update()
    {
        if (opened)
        {
            if (Input.GetMouseButton(0) && !RectTransformUtility.RectangleContainsScreenPoint(container.GetComponent<RectTransform>(), Input.mousePosition, UIManager.UICamera))
            {
                if (page1)
                {
                    Destroy(page1);
                    page1 = null;
                }
                if (page2)
                {
                    Destroy(page2);
                    page2 = null;
                }
                if (page3)
                {
                    Destroy(page3);
                    page3 = null;
                }
                opened = false;
                openedDrafter = null;
            }
        }
    }

    static readonly int MAX_RECIPES = 3;

    public static List<RecipeTable> CurrentRecipes
    {
        get
        {
            List<RecipeTable> list = new List<RecipeTable>();
            foreach (Transform tr in staticInstance.recipePanel.transform)
            {
                if (tr.GetComponent<RecipeTable> ()) list.Add(tr.GetComponent<RecipeTable>());
            }
            return list;
        }
    }

    public static List<RecipeDrafter> CurrentDrafts
    {
        get
        {
            List<RecipeDrafter> list = new List<RecipeDrafter>();
            foreach (Transform tr in staticInstance.recipePanel.transform)
            {
                if (tr.GetComponent<RecipeDrafter>()) list.Add(tr.GetComponent<RecipeDrafter>());
            }
            return list;
        }
    }

    public static void cancelAllRecipes()
    {
        List<Recipe> recipes = new List<Recipe>(pickedRecipes);
        foreach (Recipe rec in recipes) cancelRecipe(rec);
    }

    public static bool opened
    {
        get;
        private set;
    }

    static void generate(List<Recipe> list, string limit = "")
    {
        opened = true;
        foreach (Recipe recipe in list)
        {
            if(recipe == null)
            {
                fillLimitBook(limit);
            }
            else fillPageBook(recipe);
        }
    }

    static void fillPageBook(Recipe recipe)
    {
        GameObject page = Instantiate(staticInstance.bookPageSampler, staticInstance.container.transform);
        if (page1 == null) page1 = page;
        else if (page2 == null) page2 = page;
        else if (page3 == null) page3 = page;

        page.GetComponent<BookPage>().fillPage(recipe);

        page.GetComponent<BookPage>().activateElements();
    }

    static void fillLimitBook (string limitText)
    {
        GameObject page = Instantiate(staticInstance.bookPageLimitSampler, staticInstance.container.transform);
        if (page1 == null) page1 = page;
        else if (page2 == null) page2 = page;
        else if (page3 == null) page3 = page;

        page.GetComponentInChildren<TextMeshProUGUI>().text = limitText;
        IDissolving[] dissolvingElements = page.GetComponentsInChildren<IDissolving>();
        foreach (IDissolving elem in dissolvingElements) elem.appear();
    }
    public static void clean()
    {
        if (page1) Destroy(page1.gameObject);
        if (page2) Destroy(page2.gameObject);
        if (page3) Destroy(page3.gameObject);
        opened = false;
    }

    public static Dictionary<Recipe, GameObject> recipeTables
    {
        get; private set;
    }

    public static List<Recipe> pickedRecipes
    {
        get
        {
            return new List<Recipe>(recipeTables.Keys);
        }
    }

    public static void addRecipeTable (Recipe recipe, GameObject recipeTable)
    {
        recipeTables.Add(recipe, recipeTable);
    }

    public static void beginDraft(RecipeDrafter recipeDrafter, List<Recipe> recipes)
    {
        openedDrafter = recipeDrafter;
        int n = 0;
        string limit = "";
        foreach(Recipe recipe in recipes)
        {
            if (recipe == null) n++;
        }
        if(n > 0)
        {
            switch (recipeDrafter.DraftType)
            {
                case RecipeDrafter.DRAFT_TYPE.INGREDIENT_BLUEPRINT:
                    if (recipeDrafter.Counter - (recipes.Count - n) == 0) limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Изучение новых ингредиентов недоступно в демо-версии." : "New blueprints can be found in dungeons, or purchased from merchants.";
                    else limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Оставшиеся чертежи находятся в других созведиях." : "The remaining blueprints are in other constellations.";
                    break;
                case RecipeDrafter.DRAFT_TYPE.CONSUMABLE:
                    limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Создание ингредиентов недоступно в демо-версии." : "Learn ingredient blueprints to be able to re-create them.";
                    break;
                case RecipeDrafter.DRAFT_TYPE.POTION_LEVEL_UP:
                    if (recipeDrafter.Counter - (recipes.Count - n) == 0) limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Все известные зелья уже улучшены." : "All known potions are already upgraded.";
                    else limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Остальные зелья, которые можно улучшить, находятся в других созвездиях." : "The rest of the potions, that can be upgraded, are in other constellations.";
                    break;
                case RecipeDrafter.DRAFT_TYPE.POTION_BLUEPRINT:
                    if (recipeDrafter.Counter - (recipes.Count - n) == 0) limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Вы выучили все рецепты зелий, которые у вас были." : "You learned all the potion recipes you had.";
                    else limit = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Вы уже знаете все рецепты зелий, которые привязаны к текущим созвездиям. Смените созвездия, чтобы увидеть оставшиеся неизученные рецепты." : "You already know all the potion recipes that are tied to the current constellations. Change constellations to see the remaining unlearned recipes.";
                    break;
            }
            generate(recipes, limit);
        }
        else generate(recipes);
    }
    public static void pick(Recipe recipe)
    {

        GameObject recipeTable = Instantiate(staticInstance.recipeTableSampler, staticInstance.recipePanel.transform);
        recipeTable.GetComponent<RecipeTable>().fillPage(recipe);
        IDissolving[] dissolvingList = recipeTable.GetComponentsInChildren<IDissolving>();
        foreach (IDissolving elem in dissolvingList)
        {
            elem.appear();
        }

        addRecipeTable(recipe, recipeTable);
        BottleModule.addRecipe(recipe);

        if(openedDrafter.DraftType == RecipeDrafter.DRAFT_TYPE.POTION_LEVEL_UP)
        {
            List<Recipe> list = openedDrafter.recipesList;
            list.Remove(recipe);
            foreach (Recipe rec in list)
            {
                PotionListManager.LevelUPpingList.Remove(rec);
            }
        }
        RecipeDrafter.refreshDrafters(openedDrafter.DraftType);

        if (recipe.Type == Recipe.RECIPE_TYPE.POTION_BREWING) Destroy(openedDrafter.gameObject);
        openedDrafter = null;
    }
    public static void cancelRecipe(Recipe recipe)
    {
        BottleModule.cancelRecipe(recipe);
        Destroy(recipeTables[recipe]);
        recipeTables.Remove(recipe);
    }

    public static bool isRecipePanelFull
    {
        get
        {
            return CurrentRecipes.Count + CurrentDrafts.Count >= MAX_RECIPES;
        }
    }

    IEnumerator recipeSpawn()
    {
        while (true)
        {
            if(!isRecipePanelFull) Instantiate(recipeDrafterSampler, recipePanel.transform);
            yield return new WaitForSeconds(1);
        }
    }
}
