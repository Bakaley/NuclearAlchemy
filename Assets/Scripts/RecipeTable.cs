using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeTable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    [SerializeField]
    GameObject recipeNameString;
    [SerializeField]
    GameObject secondParamsBlock;
    [SerializeField]
    public GameObject rewardIcon;
    [SerializeField]
    GameObject backgroundIcon;
    [SerializeField]
    GameObject background, arrow;
    [SerializeField]
    Sprite targetedBackground;
    [SerializeField]
    Sprite defalutBackground;

    Recipe recipe;


    public void fillPage(Recipe recipeObject)
    {
        this.recipe = recipeObject;

        PotionParamList potionParamList = GetComponent<PotionParamList>();
        string recipeNameCaption = "";

        switch (recipe.Type)
        {
            case Recipe.RECIPE_TYPE.POTION_BREWING:
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.POTION_BLUEPRINT:
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.POTION_LEVEL_UP:
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.RecipeName : recipe.RecipeName;
                break;
            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.GetComponent<Ingredient>().IngredientName : recipe.GetComponent<Ingredient>().EnglishName;
                break;
            case Recipe.RECIPE_TYPE.CONSUMABLE:
                recipeNameCaption = GameSettings.CurrentLanguage == GameSettings.Language.RU ? recipe.GetComponent<Ingredient>().IngredientName : recipe.GetComponent<Ingredient>().EnglishName;
                break;
        }

        recipeNameString.GetComponent<TextMeshProUGUI>().text = recipeNameCaption;
        if (recipe.Type == Recipe.RECIPE_TYPE.POTION_LEVEL_UP) potionParamList.points.GetComponent<TextMeshProUGUI>().text = recipe.GetComponent<PotionRecipe>().Points + "";
        else potionParamList.points.GetComponent<TextMeshProUGUI>().text = recipe.Points + "";

        switch (recipe.Aspect)
        {
            case StatBoardView.Aspect.body:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.body;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.mind:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.mind;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.soul:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.soul;
                potionParamList.aspect1.GetComponent<Transform>().localPosition = new Vector3(
                    (potionParamList.aspect1.GetComponent<Transform>().localPosition.x + potionParamList.aspect2.GetComponent<Transform>().localPosition.x) / 2,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.y,
                    potionParamList.aspect1.GetComponent<Transform>().localPosition.z);
                potionParamList.aspect2.gameObject.SetActive(false);
                break;
            case StatBoardView.Aspect.body_and_no_mind:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedMind;
                break;
            case StatBoardView.Aspect.body_and_no_soul:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedSoul;
                break;
            case StatBoardView.Aspect.mind_and_no_body:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedBody;
                break;
            case StatBoardView.Aspect.mind_and_no_soul:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedSoul;
                break;
            case StatBoardView.Aspect.soul_and_no_body:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.soul;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedBody;
                break;
            case StatBoardView.Aspect.soul_and_no_mind:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.soul;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.crossedMind;
                break;
            case StatBoardView.Aspect.mind_body:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.body;
                break;
            case StatBoardView.Aspect.mind_soul:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.mind;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.soul;
                break;
            case StatBoardView.Aspect.body_soul:
                potionParamList.aspect1.GetComponent<Image>().sprite = potionParamList.body;
                potionParamList.aspect2.GetComponent<Image>().sprite = potionParamList.soul;
                break;
        }

        if (recipe.Temperature > 0)
        {
            potionParamList.param1.GetComponent<TextMeshProUGUI>().text = "+" + recipe.Temperature;
            potionParamList.param1Sprite.GetComponent<Image>().sprite = potionParamList.temperaturePlus;
        }
        if (recipe.Temperature < 0)
        {
            potionParamList.param1.GetComponent<TextMeshProUGUI>().text = recipe.Temperature + "";
            potionParamList.param1Sprite.GetComponent<Image>().sprite = potionParamList.temperatureMinus;
        }
        if (recipe.Aether != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshProUGUI>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshProUGUI>().text = recipe.Aether + "";
                potionParamList.param1Sprite.GetComponent<Image>().sprite = potionParamList.aether;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshProUGUI>().text = recipe.Aether + "";
                potionParamList.param2Sprite.GetComponent<Image>().sprite = potionParamList.aether;
            }
        }
        if (recipe.Viscosity != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshProUGUI>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshProUGUI>().text = recipe.Viscosity + "";
                potionParamList.param1Sprite.GetComponent<Image>().sprite = potionParamList.viscosity;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshProUGUI>().text = recipe.Viscosity + "";
                potionParamList.param2Sprite.GetComponent<Image>().sprite = potionParamList.viscosity;
            }
        }
        if (recipe.Voidness != 0)
        {
            if (potionParamList.param1.GetComponent<TextMeshProUGUI>().text == "0")
            {
                potionParamList.param1.GetComponent<TextMeshProUGUI>().text = recipe.Voidness + "";
                potionParamList.param1Sprite.GetComponent<Image>().sprite = potionParamList.voidness;
            }
            else
            {
                potionParamList.param2.GetComponent<TextMeshProUGUI>().text = recipe.Voidness + "";
                potionParamList.param2Sprite.GetComponent<Image>().sprite = potionParamList.voidness;
            }
        }

        if (potionParamList.param1.GetComponent<TextMeshProUGUI>().text == "0")
        {
            secondParamsBlock.SetActive(false);
        }

        if (potionParamList.param2.GetComponent<TextMeshProUGUI>().text == "0")
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
                rewardIcon.GetComponent<Image>().sprite = recipeObject.GetComponent<PotionRecipe>().Icon;
                rewardIcon.GetComponent<Image>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                break;
            case Recipe.RECIPE_TYPE.POTION_LEVEL_UP:
                rewardIcon.GetComponent<Image>().sprite = recipeObject.GetComponent<PotionRecipe>().Icon;
                rewardIcon.GetComponent<Image>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                arrow.SetActive(true);
                break;
            case Recipe.RECIPE_TYPE.NEW_INGREDIENT:
                backgroundIcon.SetActive(true);
                rewardIcon.transform.localScale = new Vector3(2f, 2f, 2f);
                rewardIcon.GetComponent<Image>().sprite = recipeObject.GetComponent<Ingredient>().IngredientIcon;
                rewardIcon.GetComponent<Image>().color = recipeObject.GetComponent<Ingredient>().IngredientIconColor;
                break;
            case Recipe.RECIPE_TYPE.CONSUMABLE:
                rewardIcon.transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
                rewardIcon.GetComponent<Image>().sprite = recipeObject.GetComponent<Ingredient>().IngredientIcon;
                rewardIcon.GetComponent<Image>().color = recipeObject.GetComponent<Ingredient>().IngredientIconColor;
                break;
            case Recipe.RECIPE_TYPE.POTION_BLUEPRINT:
                rewardIcon.GetComponent<Image>().sprite = recipeObject.GetComponent<PotionRecipe>().Icon;
                rewardIcon.GetComponent<Image>().color = recipe.GetComponent<PotionRecipe>().IconColor;
                backgroundIcon.SetActive(true);
                rewardIcon.transform.localScale = new Vector3(2f, 2f, 2f);
                break;
        }

        Recipe.EssenceRequirement[] ess = recipe.essences;

        potionParamList.essence1.GetComponent<Image>().sprite = EssenceList.essenceIcons[ess[0].essence].GetComponent<Image>().sprite;
        potionParamList.essence1.GetComponent<Image>().color = EssenceList.essenceIcons[ess[0].essence].GetComponent<Image>().color;

        potionParamList.essence2.GetComponent<Image>().sprite = EssenceList.essenceIcons[ess[1].essence].GetComponent<Image>().sprite;
        potionParamList.essence2.GetComponent<Image>().color = EssenceList.essenceIcons[ess[1].essence].GetComponent<Image>().color;
    }

    public void prepare()
    {
        if (UIManager.cookingMode)
        {
            if (CookingModule.ableToNewPreparedRecipe)
            {
                if (recipeRequiermentCheck())
                {
                    CookingModule.prepareRecipe(this, recipe);
                    setTargeted(true);
                }
            }
        }
    }

    public void unprepare()
    {
        background.GetComponent<SpriteRenderer>().sprite = defalutBackground;
    }

    public bool recipeRequiermentCheck()
    {
        StatBoardView stats = StatBoardView.staticInstance;
        if (stats.pointsCounter >= recipe.Points && aspectCheck(stats.potionAspect, recipe.Aspect) && secondParamsCheck(stats, recipe) && essenceCheck(recipe)) return true;
        return false;
    }

    bool aspectCheck(StatBoardView.Aspect stats, StatBoardView.Aspect recipe)
    {
        if (stats == recipe) return true;
        switch (recipe)
        {
            case StatBoardView.Aspect.body:
                if (stats == StatBoardView.Aspect.body_soul || stats == StatBoardView.Aspect.mind_body) return true;
                break;
            case StatBoardView.Aspect.mind:
                if (stats == StatBoardView.Aspect.mind_body || stats == StatBoardView.Aspect.mind_soul) return true;
                break;
            case StatBoardView.Aspect.soul:
                if (stats == StatBoardView.Aspect.body_soul || stats == StatBoardView.Aspect.mind_soul) return true;
                break;
            case StatBoardView.Aspect.body_and_no_mind:
                if (stats == StatBoardView.Aspect.body || stats == StatBoardView.Aspect.body_soul) return true;
                break;
            case StatBoardView.Aspect.body_and_no_soul:
                if (stats == StatBoardView.Aspect.body || stats == StatBoardView.Aspect.mind_body) return true;
                break;
            case StatBoardView.Aspect.mind_and_no_soul:
                if (stats == StatBoardView.Aspect.mind || stats == StatBoardView.Aspect.mind_body) return true;
                break;
            case StatBoardView.Aspect.mind_and_no_body:
                if (stats == StatBoardView.Aspect.mind || stats == StatBoardView.Aspect.mind_soul) return true;
                break;
            case StatBoardView.Aspect.soul_and_no_body:
                if (stats == StatBoardView.Aspect.soul || stats == StatBoardView.Aspect.mind_soul) return true;
                break;
            case StatBoardView.Aspect.soul_and_no_mind:
                if (stats == StatBoardView.Aspect.soul || stats == StatBoardView.Aspect.body_soul) return true;
                break;
        }
        return false;
    }

    bool secondParamsCheck(StatBoardView stats, Recipe recipe)
    {
        if (recipe.Temperature > 0)
        {
            if (stats.temperatureCounter < recipe.Temperature) return false;
        }
        if (recipe.Temperature < 0)
        {
            if (stats.temperatureCounter > recipe.Temperature) return false;
        }
        if (recipe.Aether != 0)
        {
            if (stats.aetherCoutner < recipe.Aether) return false;
        }
        if (recipe.Viscosity != 0)
        {
            if (stats.viscosityCounter < recipe.Viscosity) return false;
        }
        if (recipe.Voidness != 0)
        {
            if (stats.voidnessCounter < recipe.Voidness) return false;
        }
        return true;
    }

    bool essenceCheck(Recipe recipe)
    {
        foreach (Recipe.EssenceRequirement requirement in recipe.essences)
        {
            if (EssencePanel.essenceScores[requirement.essence] == 0) return false;
        }
        return true;
    }

    public void setTargeted(bool targetOn)
    {
        if(targetOn) background.GetComponent<Image>().sprite = targetedBackground;
        else background.GetComponent<Image>().sprite = defalutBackground;
    }

    float holdingTime = 0;
    bool holdingButton = false;



    public void OnPointerDown(PointerEventData eventData)
    {
        if (!UIManager.cookingMode)
        {
            holdingButton = true;
            holdingTime = 0;
        }
        else
        {
            if (recipeRequiermentCheck()) prepare();
            else
            {
                List<string> inconsistancies = new List<string>();
                if (!aspectCheck(StatBoardView.staticInstance.potionAspect, recipe.Aspect))
                {

                    string incons1 = "";
                    switch (recipe.Aspect)
                    {
                        case StatBoardView.Aspect.mind:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на разум" : "The potion must affect on mind";
                            break;
                        case StatBoardView.Aspect.body:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на тело" : "The potion must affect on body";
                            break;
                        case StatBoardView.Aspect.soul:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на душу" : "The potion must affect on soul";
                            break;
                        case StatBoardView.Aspect.mind_soul:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на разум и тело одновременно" : "The potion must affect on both mind and body";
                            break;
                        case StatBoardView.Aspect.mind_body:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на разум и тело одновременно" : "The potion must affect on both mind and body";
                            break;
                        case StatBoardView.Aspect.body_soul:
                            incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на тело и душу одновременно" : "The potion must affect on both body and soul";
                            break;
                        case StatBoardView.Aspect.mind_and_no_body:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.body:
                                case StatBoardView.Aspect.mind_body:
                                case StatBoardView.Aspect.body_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на тело" : "The potion must not affect on body";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на разум" : "The potion must affect on mind";
                                    break;
                            }
                            break;
                        case StatBoardView.Aspect.soul_and_no_body:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.body:
                                case StatBoardView.Aspect.mind_body:
                                case StatBoardView.Aspect.body_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на тело" : "The potion must not affect on body";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.mind:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на душу" : "The potion must affect on soul";
                                    break;
                            }
                            break;
                        case StatBoardView.Aspect.body_and_no_mind:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.mind:
                                case StatBoardView.Aspect.mind_body:
                                case StatBoardView.Aspect.mind_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на разум" : "The potion must not affect on mind";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на тело" : "The potion must affect on body";
                                    break;
                            }
                            break;
                        case StatBoardView.Aspect.soul_and_no_mind:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.mind:
                                case StatBoardView.Aspect.mind_body:
                                case StatBoardView.Aspect.mind_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на разум" : "The potion must not affect on mind";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.body:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на душу" : "The potion must affect on soul";
                                    break;
                            }
                            break;
                        case StatBoardView.Aspect.mind_and_no_soul:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.soul:
                                case StatBoardView.Aspect.mind_soul:
                                case StatBoardView.Aspect.body_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на душу" : "The potion must not affect on soul";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.body:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на разум" : "The potion must affect on mind";
                                    break;
                            }
                            break;
                        case StatBoardView.Aspect.body_and_no_soul:
                            switch (StatBoardView.staticInstance.potionAspect)
                            {
                                case StatBoardView.Aspect.soul:
                                case StatBoardView.Aspect.mind_soul:
                                case StatBoardView.Aspect.body_soul:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье не должно влиять на душу" : "The potion must not affect on soul";
                                    break;
                                case StatBoardView.Aspect.aspectless:
                                case StatBoardView.Aspect.mind:
                                    incons1 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Зелье должно влиять на тело" : "The potion must affect on body";
                                    break;
                            }
                            break;
                    }
                    inconsistancies.Add(incons1);
                }
                if(StatBoardView.staticInstance.pointsCounter < recipe.Points)
                {
                    string incons2 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Недостаточно силы (" + StatBoardView.staticInstance.pointsCounter + " из " + recipe.Points + ")" : "Not enough power (" + StatBoardView.staticInstance.pointsCounter+  " of " + recipe.Points + ")";
                    inconsistancies.Add(incons2);
                }
                if(!secondParamsCheck(StatBoardView.staticInstance, recipe)){
                    string incons3 = "";
                    if (recipe.Temperature > 0)
                    {
                        if (StatBoardView.staticInstance.temperatureCounter < recipe.Temperature) incons3 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Слишком низкая температура (+" + StatBoardView.staticInstance.temperatureCounter + " из +" + recipe.Temperature + ")" : "Temperature is too low (+" + StatBoardView.staticInstance.temperatureCounter + " of +" + recipe.Temperature + ")";
                        inconsistancies.Add(incons3);
                    }
                    if (recipe.Temperature < 0)
                    {
                        if (StatBoardView.staticInstance.temperatureCounter > recipe.Temperature) incons3 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Слишком высокая температура (" + StatBoardView.staticInstance.temperatureCounter + " из " + recipe.Temperature + ")" : "Temperature is too high (" + StatBoardView.staticInstance.temperatureCounter + " of " + recipe.Temperature + ")";
                        inconsistancies.Add(incons3);
                    }
                    if (recipe.Aether != 0 && incons3 == "")
                    {
                        if (StatBoardView.staticInstance.aetherCoutner < recipe.Aether) incons3 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Недостаточно эфира (" + StatBoardView.staticInstance.aetherCoutner + " из " + recipe.Aether + ")" : "Not enough aether (" + StatBoardView.staticInstance.aetherCoutner + " of " + recipe.Aether + ")";
                        inconsistancies.Add(incons3);
                    }
                    if (recipe.Viscosity != 0 && incons3 == "")
                    {
                        if (StatBoardView.staticInstance.viscosityCounter < recipe.Viscosity) incons3 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Недостаточно 3 и 4 уровней (" + StatBoardView.staticInstance.viscosityCounter + " из " + recipe.Viscosity + ")" : "Not enough 3 and 4 levels (" + StatBoardView.staticInstance.viscosityCounter + " of " + recipe.Viscosity + ")";
                        inconsistancies.Add(incons3);
                    }
                    if (recipe.Voidness != 0 && incons3 == "")
                    {
                        if (StatBoardView.staticInstance.voidnessCounter < recipe.Voidness) incons3 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Недостаточно пустот (" + StatBoardView.staticInstance.voidnessCounter + " из " + recipe.Voidness + ")" : "Not enough voids (" + StatBoardView.staticInstance.voidnessCounter + " of " + recipe.Voidness + ")";
                        inconsistancies.Add(incons3);
                    }
                }
                if (!essenceCheck(recipe))
                {
                    string incons4 = GameSettings.CurrentLanguage == GameSettings.Language.RU ? "Нет нужной эссенции": "No essence that needed";
                    inconsistancies.Add(incons4);
                }
                CookingModule.showInconsistencyPanel(inconsistancies);
            }
        }
    }

    private void Update()
    {
        if (!UIManager.cookingMode)
        {
            if (holdingButton) holdingTime += Time.deltaTime;
            if (holdingTime >= .25f && holdingButton)
            {
                holdingButton = false;
                PauseCanvas.CancelRecipe(recipe);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holdingButton = false;
        if (UIManager.cookingMode) CookingModule.hideInconsistencyPanel();
    }
}