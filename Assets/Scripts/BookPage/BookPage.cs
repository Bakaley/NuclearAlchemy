using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookPage : MonoBehaviour
{

    [SerializeField]
    GameObject potionBrewingSampler;
    [SerializeField]
    GameObject potionLevelUpSampler;
    [SerializeField]
    GameObject ingredientSampler;
    [SerializeField]
    GameObject potionBlueprintSampler;

    [SerializeField]
    GameObject recipeTypeString;
    [SerializeField]
    GameObject recipeNameString;
    [SerializeField]
    GameObject constellationString;
    [SerializeField]
    GameObject secondParamsBlock;

    [SerializeField]
    GameObject IngredientCell;
    [SerializeField]
    GameObject IngredientIcon;
    [SerializeField]
    GameObject blueprintIngrIcon;

    [SerializeField]
    GameObject rewardPlace;

    public Recipe recipe
    {
        get; private set;
    }

    public void fillPage(Recipe recipe)
    {
        this.recipe = recipe;

        PotionParamList potionParamList = GetComponentInChildren<PotionParamList>();
        string constellationCaption = "";
        string recipeNameCaption = "";
        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье": "Potion";
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.POTION_BLUEPRINT:
                constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Новое зелье" : "New potion";
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.POTION_LEVEL_UP:
                constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Улучшение зелья" : "Potion upgrade";
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Новый ингредиент" : "New ingredient";
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.GetComponent<Ingredient>().IngredientName : recipe.GetComponent<Ingredient>().EnglishName;
                break;
            case Recipe.RECIPE_TYPE.CONSUMABLE:
                constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Расходник" : "Consumable";
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.GetComponent<Ingredient>().IngredientName : recipe.GetComponent<Ingredient>().EnglishName;
                break;
        }
        recipeTypeString.GetComponent<TextMeshPro>().text = constellationCaption;
        recipeNameString.GetComponent<TextMeshPro>().text = recipeNameCaption;

        if(recipe.Type == Recipe.RECIPE_TYPE.POTION_LEVEL_UP) potionParamList.points.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().Points + "";
        else potionParamList.points.GetComponent<TextMeshPro>().text = recipe.Points + "";

        switch (recipe.Aspect)
        {
            case StatBoardView.Aspect.body:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.body;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.mind:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.mind;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.soul:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.soul;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.body_and_no_mind:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedMind;
                break;
            case StatBoardView.Aspect.body_and_no_soul:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedSoul;
                break;
            case StatBoardView.Aspect.mind_and_no_body:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedBody;
                break;
            case StatBoardView.Aspect.mind_and_no_soul:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedSoul;
                break;
            case StatBoardView.Aspect.soul_and_no_body:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.soul;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedBody;
                break;
            case StatBoardView.Aspect.soul_and_no_mind:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.soul;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.crossedMind;
                break;
            case StatBoardView.Aspect.mind_body:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.body;
                break;
            case StatBoardView.Aspect.mind_soul:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.soul;
                break;
            case StatBoardView.Aspect.body_soul:
                potionParamList.aspect1.GetComponent<SpriteRenderer>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<SpriteRenderer>().sprite = potionParamList.soul;
                break;
        }

        if (recipe.Temperature > 0)
        {
            potionParamList.param1.GetComponent<TextMeshPro>().text = "+" + recipe.Temperature;
            potionParamList.param1Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.temperaturePlus;
        }
        if (recipe.Temperature < 0)
        {
            potionParamList.param1.GetComponent<TextMeshPro>().text = recipe.Temperature + "";
            potionParamList.param1Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.temperatureMinus;
        }
        if (recipe.Aether != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshPro>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshPro>().text = recipe.Aether + "";
                potionParamList.param1Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.aether;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshPro>().text = recipe.Aether + "";
                potionParamList.param2Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.aether;
            }
        }
        if (recipe.Viscosity != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshPro>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshPro>().text = recipe.Viscosity + "";
                potionParamList.param1Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.viscosity;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshPro>().text = recipe.Viscosity + "";
                potionParamList.param2Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.viscosity;
            }
        }
        if (recipe.Voidness != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshPro>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshPro>().text = recipe.Voidness + "";
                potionParamList.param1Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.voidness;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshPro>().text = recipe.Voidness + "";
                potionParamList.param2Sprite.GetComponent<SpriteRenderer>().sprite = potionParamList.voidness;
            }
        }

        if (potionParamList.param1.GetComponent<TextMeshPro>().text == "0")
        {
            secondParamsBlock.SetActive(false);
        }

        if (potionParamList.param2.GetComponent<TextMeshPro>().text == "0")
        {
            potionParamList.param1.GetComponent<Transform>().localPosition = new Vector3(
                   potionParamList.param1.GetComponent<Transform>().localPosition.x,
                   (potionParamList.param1.GetComponent<Transform>().localPosition.y + potionParamList.param2.GetComponent<Transform>().localPosition.y) / 2,
                   potionParamList.param1.GetComponent<Transform>().localPosition.z);
            potionParamList.param1Sprite.GetComponent<Transform>().localPosition = new Vector3(
                   potionParamList.param1Sprite.GetComponent<Transform>().localPosition.x,
                   (potionParamList.param1Sprite.GetComponent<Transform>().localPosition.y + potionParamList.param2Sprite.GetComponent<Transform>().localPosition.y) / 2,
                   potionParamList.param1Sprite.GetComponent<Transform>().localPosition.z);
            potionParamList.param2.gameObject.SetActive(false);
            potionParamList.param2Sprite.gameObject.SetActive(false);

        }

        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                GameObject rewaredBrewGameobject = Instantiate(potionBrewingSampler, rewardPlace.transform);
                PotionBrewSampler potionBrewReward = rewaredBrewGameobject.GetComponent<PotionBrewSampler>();
                potionBrewReward.potionIcon.GetComponent<SpriteRenderer>().sprite = recipe.GetComponent<PotionRecipe>().Icon;
                potionBrewReward.potionIcon.GetComponent<SpriteRenderer>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                potionBrewReward.potionCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().PotionCount + "";
                potionBrewReward.lunarCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().LunarCount + "";
                setConstellationPotionString();
                break;

            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                fillIngredientReward();
                blueprintIngrIcon.SetActive(true);
                IngredientIcon.transform.localScale = new Vector3(1, 1, 1);
                break;

            case Recipe.RECIPE_TYPE.CONSUMABLE:
                fillIngredientReward();
                break;

            case Recipe.RECIPE_TYPE.POTION_LEVEL_UP:
                setConstellationPotionString();
                GameObject rewardLevelUPGameObject = Instantiate(potionLevelUpSampler, rewardPlace.transform);
                PotionLevelUpSampler potionLevelUpReward = rewardLevelUPGameObject.GetComponent<PotionLevelUpSampler>();
                potionLevelUpReward.potionIcon.GetComponent<SpriteRenderer>().sprite = recipe.GetComponent<PotionRecipe>().Icon;
                potionLevelUpReward.potionIcon.GetComponent<SpriteRenderer>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                potionLevelUpReward.oldCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().PotionCount + "";
                potionLevelUpReward.newCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().nextLvLPotionCount + "";
                break;

            case Recipe.RECIPE_TYPE.POTION_BLUEPRINT:
                GameObject rewaredPotBlueprintGameobject = Instantiate(potionBlueprintSampler, rewardPlace.transform);
                PotionBlueprintSampler potionBlueprintReward = rewaredPotBlueprintGameobject.GetComponent<PotionBlueprintSampler>();
                potionBlueprintReward.backgroundBlueprint.SetActive(true);
                potionBlueprintReward.potionIcon.transform.localScale = new Vector3(.75f, .75f, .75f);
                potionBlueprintReward.potionIcon.GetComponent<SpriteRenderer>().sprite = recipe.GetComponent<PotionRecipe>().Icon;
                potionBlueprintReward.potionIcon.GetComponent<SpriteRenderer>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                potionBlueprintReward.potionCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().PotionCount + "";
                potionBlueprintReward.lunarCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().LunarCount + "";
                setConstellationPotionString();
                break;
        }

        void setConstellationPotionString()
        {
            switch (recipe.GetComponent<PotionRecipe>().Potion_Type)
            {
                case PotionRecipe.POTION_TYPE.BASIC:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Базовые" : "Potion";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE:
                case PotionRecipe.POTION_TYPE.TEMPERATURE_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура" : "Temperature";
                    break;
                case PotionRecipe.POTION_TYPE.COLORANTS:
                case PotionRecipe.POTION_TYPE.COLORANTS_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители" : "Coloratns";
                    break;
                case PotionRecipe.POTION_TYPE.AETHER:
                case PotionRecipe.POTION_TYPE.AETHER_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир" : "Aether";
                    break;
                case PotionRecipe.POTION_TYPE.SUPERNOVA:
                case PotionRecipe.POTION_TYPE.SUPERNOVA_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые" : "Supernovas";
                    break;
                case PotionRecipe.POTION_TYPE.VOIDS:
                case PotionRecipe.POTION_TYPE.VOIDS_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Пустоты" : "Voids";
                    break;
                case PotionRecipe.POTION_TYPE.BASIC_PLUS:
                case PotionRecipe.POTION_TYPE.BASIC_PLUS_PROGRESSING:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Линзирование" : "Lensing";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE_COLORANTS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, температура" : "Colorants, temperature";
                    break;
                case PotionRecipe.POTION_TYPE.COLORANTS_AETHER:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, эфир" : "Colorants, aether";
                    break;
                case PotionRecipe.POTION_TYPE.COLORANTS_SUPERNOVA:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, сверхновые" : "Colorants, supernovas";
                    break;
                case PotionRecipe.POTION_TYPE.COLORANTS_VOIDS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, пустоты" : "Colorants, voids";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE_AETHER:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, эфир" : "Temperature, aether";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE_SUPERNOVA:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, сверхновые" : "Temperature, supernovas";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE_VOIDS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, пустоты" : "Temperature, voids";
                    break;
                case PotionRecipe.POTION_TYPE.AETHER_SUPERNOVA:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, сверхновые" : "Aether, supernovas";
                    break;
                case PotionRecipe.POTION_TYPE.AETHER_VOIDS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, пустоты" : "Aether, voids";
                    break;
                case PotionRecipe.POTION_TYPE.VOIDS_SUPERNOVA:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые, пустоты" : "Supernovas, voids";
                    break;
                case PotionRecipe.POTION_TYPE.COLORANTS_PLUS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, линзирование" : "Colorants, lensing";
                    break;
                case PotionRecipe.POTION_TYPE.TEMPERATURE_PLUS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, линзирование" : "Temperature, lensing";
                    break;
                case PotionRecipe.POTION_TYPE.AETHER_PLUS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, линзирование" : "Aether, lensing";
                    break;
                case PotionRecipe.POTION_TYPE.SUPERNOVA_PLUS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые, линзирование" : "Supernovas, lensing";
                    break;
                case PotionRecipe.POTION_TYPE.VOIDS_PLUS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Пустоты, линзирование" : "Voids, lensing";
                    break;
            }
            constellationString.SetActive(true);
            constellationString.GetComponent<TextMeshPro>().text = constellationCaption;
        }

        void fillIngredientReward()
        {
            GameObject ingrReward = Instantiate(ingredientSampler, rewardPlace.transform);
            IngredientSampler ingrSampler = ingrReward.GetComponent<IngredientSampler>();
            foreach (PreviewOrb orb in recipe.GetComponent<Ingredient>().preview.GetComponentsInChildren<PreviewOrb>())
            {
                Instantiate(orb.gameObject, ingrSampler.orbShift.transform);
            }

            if (recipe.GetComponent<Ingredient>().essence2 == Ingredient.ESSENSE.None)
            {
                ingrSampler.essence3.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().sprite;
                ingrSampler.essence3.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().color;
                ingrSampler.essence3.gameObject.transform.localPosition = new Vector3(ingrSampler.essence3.gameObject.transform.localPosition.x, (ingrSampler.essence3.gameObject.transform.localPosition.y + ingrSampler.essence1.gameObject.transform.localPosition.y) / 2, ingrSampler.essence3.gameObject.transform.localPosition.z);
                ingrSampler.essence3.gameObject.SetActive(true);
            }
            else if (recipe.GetComponent<Ingredient>().essence3 == Ingredient.ESSENSE.None)
            {
                ingrSampler.essence1.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().sprite;
                ingrSampler.essence1.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().color;
                ingrSampler.essence2.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence2].GetComponent<Image>().sprite;
                ingrSampler.essence2.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence2].GetComponent<Image>().color;

                ingrSampler.essence1.gameObject.transform.localPosition = new Vector3(ingrSampler.essence1.gameObject.transform.localPosition.x, (ingrSampler.essence1.gameObject.transform.localPosition.y + ingrSampler.essence3.gameObject.transform.localPosition.y) / 2, ingrSampler.essence1.gameObject.transform.localPosition.z);
                ingrSampler.essence2.gameObject.transform.localPosition = new Vector3(ingrSampler.essence2.gameObject.transform.localPosition.x, (ingrSampler.essence2.gameObject.transform.localPosition.y + ingrSampler.essence3.gameObject.transform.localPosition.y) / 2, ingrSampler.essence2.gameObject.transform.localPosition.z);

                ingrSampler.essence1.gameObject.SetActive(true);
                ingrSampler.essence2.gameObject.SetActive(true);
            }
            else
            {
                ingrSampler.essence1.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().sprite;
                ingrSampler.essence1.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence1].GetComponent<Image>().color;
                ingrSampler.essence2.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence2].GetComponent<Image>().sprite;
                ingrSampler.essence2.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence2].GetComponent<Image>().color;
                ingrSampler.essence3.GetComponent<Image>().sprite = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence3].GetComponent<Image>().sprite;
                ingrSampler.essence3.GetComponent<Image>().color = EssenceList.essenceIcons[recipe.GetComponent<Ingredient>().essence3].GetComponent<Image>().color;
                ingrSampler.essence1.gameObject.SetActive(true);
                ingrSampler.essence2.gameObject.SetActive(true);
                ingrSampler.essence3.gameObject.SetActive(true);
            }

            IngredientCell.SetActive(true);
            IngredientIcon.GetComponent<SpriteRenderer>().sprite = recipe.GetComponent<Ingredient>().IngredientIcon;
            IngredientIcon.GetComponent<SpriteRenderer>().color = recipe.GetComponent<Ingredient>().IngredientIconColor;


            switch (recipe.GetComponent<Ingredient>().Constellation)
            {
                case Ingredient.INGREDIENT_CONSTELLATION.ASPECT:
                case Ingredient.INGREDIENT_CONSTELLATION.RARE_ASPECT:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Базовые" : "Базовые";
                    break;
                case Ingredient.INGREDIENT_CONSTELLATION.COLORANTS:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители" : "Colorants";
                    break;
                case Ingredient.INGREDIENT_CONSTELLATION.TEMPERATURE:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура" : "Temperature";
                    break;
                case Ingredient.INGREDIENT_CONSTELLATION.AETHER:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир" : "Aether";
                    break;
                case Ingredient.INGREDIENT_CONSTELLATION.SUPERNOVA:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые" : "Supernovas";
                    break;
                case Ingredient.INGREDIENT_CONSTELLATION.VOID:
                    constellationCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Пустоты" : "Voids";
                    break;
            }

            constellationString.SetActive(true);
            constellationString.GetComponent<TextMeshPro>().text = constellationCaption;
        }

        Recipe.EssenceRequirement[] ess = recipe.essences;

        potionParamList.essence1.GetComponent<Image>().sprite = EssenceList.essenceIcons[ess[0].essence].GetComponent<Image>().sprite;
        potionParamList.essence1.GetComponent<Image>().color = EssenceList.essenceIcons[ess[0].essence].GetComponent<Image>().color;

        potionParamList.essence2.GetComponent<Image>().sprite = EssenceList.essenceIcons[ess[1].essence].GetComponent<Image>().sprite;
        potionParamList.essence2.GetComponent<Image>().color = EssenceList.essenceIcons[ess[1].essence].GetComponent<Image>().color;

        /*Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + ess[0].count) as Sprite;
        EssencePanel.getNumberSpriteRenderer(potionParamList.essence1).sprite = loadedNumber;
        loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + ess[1].count) as Sprite;
        EssencePanel.getNumberSpriteRenderer(potionParamList.essence2).sprite = loadedNumber;*/

    }

    public void activateElements()
    {
        IDissolving[] list = GetComponentsInChildren<IDissolving>();
        foreach (IDissolving elem in list)
        {
            elem.appear();
        }
    }

    public void pick()
    {
        DraftModule.clean();
        DraftModule.pick(recipe);
    }
}
