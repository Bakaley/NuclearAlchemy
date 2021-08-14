using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookPage : MonoBehaviour
{

    [SerializeField]
    GameObject potionBrewingSampler;
    [SerializeField]
    GameObject potionLevelUpSampler;
    [SerializeField]
    GameObject ingredientSampler;

    [SerializeField]
    GameObject recipeTypeString;
    [SerializeField]
    GameObject recipeNameString;
    [SerializeField]
    GameObject constellationString;
    [SerializeField]
    GameObject secondParamsBlock;

    [SerializeField]
    GameObject rewardPlace;

    [SerializeField]
    GameObject WaterEssence;
    [SerializeField]
    GameObject FireEssence;
    [SerializeField]
    GameObject StoneEssence;
    [SerializeField]
    GameObject AirEssence;
    [SerializeField]
    GameObject Mushroom;
    [SerializeField]
    GameObject MagicEssence;
    [SerializeField]
    GameObject PlantEssence;
    [SerializeField]
    GameObject AnimalEssence;
    [SerializeField]
    GameObject CrystallEssence;
    [SerializeField]
    GameObject LightingEssence;

    public Recipe recipe
    {
        get; private set;
    }

    public void fillPage(Recipe recipe)
    {
        this.recipe = recipe;

        PotionParamList potionParamList = GetComponentInChildren<PotionParamList>();
        string str = "";
        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье": "Potion";
                break;
            case Recipe.RECIPE_TYPE.NEW_RECIPE:
                str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Новое зелье" : "New potion";
                break;
            case Recipe.RECIPE_TYPE.RECIPE_UPGRADE:
                str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Улучшение зелья" : "Potion improvement";
                break;
            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Новый ингредиент" : "New ingredient";
                break;
            case Recipe.RECIPE_TYPE.CONSUMABLE:
                str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Расходник" : "Consumable";
                break;
        }
        recipeTypeString.GetComponent<TextMeshPro>().text = str;
        recipeNameString.GetComponent<TextMeshPro>().text = recipe.RecipeName;

        potionParamList.points.GetComponent<TextMeshPro>().text = recipe.Points + "";

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

                GameObject reward = Instantiate(potionBrewingSampler, rewardPlace.transform);
                PotionBrewSampler potionBrewReward = reward.GetComponent<PotionBrewSampler>();
                potionBrewReward.potionIcon.GetComponent<SpriteRenderer>().sprite = recipe.GetComponent<PotionRecipe>().Icon;
                potionBrewReward.potionCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().PotionCount + "";
                potionBrewReward.lunarCount.GetComponent<TextMeshPro>().text = recipe.GetComponent<PotionRecipe>().LunarCount + "";

                constellationString.SetActive(true);
                switch (recipe.GetComponent<PotionRecipe>().Potion_Type)
                {
                    case PotionRecipe.POTION_TYPE.BASIC:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Базовые" : "Potion";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE:
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура" : "Temperature";
                        break;
                    case PotionRecipe.POTION_TYPE.COLORANTS:
                    case PotionRecipe.POTION_TYPE.COLORANTS_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители" : "Coloratns";
                        break;
                    case PotionRecipe.POTION_TYPE.AETHER:
                    case PotionRecipe.POTION_TYPE.AETHER_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир" : "Aether";
                        break;
                    case PotionRecipe.POTION_TYPE.SUPERNOVA:
                    case PotionRecipe.POTION_TYPE.SUPERNOVA_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые" : "Supernovas";
                        break;
                    case PotionRecipe.POTION_TYPE.VOIDS:
                    case PotionRecipe.POTION_TYPE.VOIDS_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Пустоты" : "Voids";
                        break;
                    case PotionRecipe.POTION_TYPE.BASIC_PLUS:
                    case PotionRecipe.POTION_TYPE.BASIC_PLUS_PROGRESSING:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Линзирование" : "Lensing";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_COLORANTS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, температура" : "Colorants, temperature";
                        break;
                    case PotionRecipe.POTION_TYPE.COLORANTS_AETHER:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, эфир" : "Colorants, aether";
                        break;
                    case PotionRecipe.POTION_TYPE.COLORANTS_SUPERNOVA:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, сверхновые" : "Colorants, supernovas";
                        break;
                    case PotionRecipe.POTION_TYPE.COLORANTS_VOIDS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, пустоты" : "Colorants, voids";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_AETHER:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, эфир" : "Temperature, aether";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_SUPERNOVA:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, сверхновые" : "Temperature, supernovas";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_VOIDS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, пустоты" : "Temperature, voids";
                        break;
                    case PotionRecipe.POTION_TYPE.AETHER_SUPERNOVA:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, сверхновые" : "Aether, supernovas";
                        break;
                    case PotionRecipe.POTION_TYPE.AETHER_VOIDS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, пустоты" : "Aether, voids";
                        break;
                    case PotionRecipe.POTION_TYPE.VOIDS_SUPERNOVA:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые, пустоты" : "Supernovas, voids";
                        break;
                    case PotionRecipe.POTION_TYPE.COLORANTS_PLUS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Красители, линзирование" : "Colorants, lensing";
                        break;
                    case PotionRecipe.POTION_TYPE.TEMPERATURE_PLUS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Температура, линзирование" : "Temperature, lensing";
                        break;
                    case PotionRecipe.POTION_TYPE.AETHER_PLUS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Эфир, линзирование" : "Aether, lensing";
                        break;
                    case PotionRecipe.POTION_TYPE.SUPERNOVA_PLUS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Сверхновые, линзирование" : "Supernovas, lensing";
                        break;
                    case PotionRecipe.POTION_TYPE.VOIDS_PLUS:
                        str = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Пустоты, линзирование" : "Voids, lensing";
                        break;

                }
                constellationString.GetComponent<TextMeshPro>().text = str;
                break;
        }

        Recipe.EssenceRequirement[] ess = recipe.essences;

        potionParamList.essence1.GetComponent<SpriteRenderer>().sprite = EssencePanel.essenceIcons[ess[0].essence].GetComponent<SpriteRenderer>().sprite;
        potionParamList.essence1.GetComponent<SpriteRenderer>().color = EssencePanel.essenceIcons[ess[0].essence].GetComponent<SpriteRenderer>().color;

        potionParamList.essence2.GetComponent<SpriteRenderer>().sprite = EssencePanel.essenceIcons[ess[1].essence].GetComponent<SpriteRenderer>().sprite;
        potionParamList.essence2.GetComponent<SpriteRenderer>().color = EssencePanel.essenceIcons[ess[1].essence].GetComponent<SpriteRenderer>().color;

        /*Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + ess[0].count) as Sprite;
        EssencePanel.getNumberSpriteRenderer(potionParamList.essence1).sprite = loadedNumber;
        loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + ess[1].count) as Sprite;
        EssencePanel.getNumberSpriteRenderer(potionParamList.essence2).sprite = loadedNumber;*/

    }

    public void activateElements()
    {
        DissolvingElement[] list = GetComponentsInChildren<DissolvingElement>();
        foreach (DissolvingElement elem in list)
        {
            elem.appear();
        }
    }

    public void pick()
    {
        DraftWindow.clean();
        CookingModule.pick(recipe);
    }
}
