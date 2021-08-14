using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeTable : MonoBehaviour
{

    [SerializeField]
    GameObject recipeNameString;
    [SerializeField]
    GameObject secondParamsBlock;
    [SerializeField]
    public GameObject rewardIcon;
    [SerializeField]
    GameObject background;
    [SerializeField]
    Sprite targetedBackground;
    [SerializeField]
    Sprite defalutBackground;



    public Recipe recipe;

    public void fillPage(Recipe recipeObject)
    {
        this.recipe = recipeObject;

        PotionParamList potionParamList = GetComponent<PotionParamList>();
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
                rewardIcon.GetComponent<SpriteRenderer>().sprite = recipeObject.gameObject.GetComponent<PotionRecipe>().Icon;
                break;
        }

        Recipe.EssenceRequirement[] ess = recipe.essences;

        potionParamList.essence1.GetComponent<SpriteRenderer>().sprite = EssencePanel.essenceIcons[ess[0].essence].GetComponent<SpriteRenderer>().sprite;
        potionParamList.essence1.GetComponent<SpriteRenderer>().color = EssencePanel.essenceIcons[ess[0].essence].GetComponent<SpriteRenderer>().color;

        potionParamList.essence2.GetComponent<SpriteRenderer>().sprite = EssencePanel.essenceIcons[ess[1].essence].GetComponent<SpriteRenderer>().sprite;
        potionParamList.essence2.GetComponent<SpriteRenderer>().color = EssencePanel.essenceIcons[ess[1].essence].GetComponent<SpriteRenderer>().color;

        GetComponent<ButtonPreset>().enabled = true;
    }

    public void prepare()
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

    public static void updatePreparing()
    {
        //if (!recipeRequiermentCheck()) background.GetComponent<SpriteRenderer>().sprite = targetedBackground;
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
        if(targetOn) background.GetComponent<SpriteRenderer>().sprite = targetedBackground;
        else background.GetComponent<SpriteRenderer>().sprite = defalutBackground;
    }

}