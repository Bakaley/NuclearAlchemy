using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreparedRecipe : MonoBehaviour
{
    [SerializeField]
    public GameObject recipeIcon;
    [SerializeField]
    public GameObject counter;
    [SerializeField]
    RECIPE_TYPE recipeType;

    public Recipe recipe;

    public enum REWARD_TYPE
    {
        ONETIME_REWARD,
        REPEATED_REWARD
    }

    public enum RECIPE_TYPE
    {
        POTION_BREWING,
        CONSUMABLE,
        POTIONLEVELUP,
        INGREDIENT_BLUEPRINT,
        POTION_BLUEPRINT
    }

    public REWARD_TYPE RewardType
    {
        get
        {
            if (recipeType == RECIPE_TYPE.CONSUMABLE || recipeType == RECIPE_TYPE.POTION_BREWING)
                return REWARD_TYPE.REPEATED_REWARD;
            else return REWARD_TYPE.ONETIME_REWARD;
        }
    }

    public RECIPE_TYPE RecipeType
    {
        get
        {
            return recipeType;
        }
    }

    public void unprepare()
    {
        BottleModule.unprepareRecipe(this);
    }

    public void unprepareWithNoredirecting()
    {
        BottleModule.unprepareRecipe(this, false);
    }

    public void dissolveIn(float time)
    {
        GetComponent<GraphicRaycaster>().enabled = false;
        GetComponent<ButtonPreset>().enabled = false;
        IDissolving[] elems = GetComponentsInChildren<IDissolving>(true);
        foreach (IDissolving elem in elems)
        {
            elem.disappear();
        }
        Invoke("destroy", time);
    }

    private void destroy()
    {
        Destroy(this.gameObject);
    }
}
