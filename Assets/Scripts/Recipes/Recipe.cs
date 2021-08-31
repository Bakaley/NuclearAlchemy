using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class Recipe : MonoBehaviour
{

    public enum RECIPE_TYPE
    {
        POTION_BREWING,
        POTION_BLUEPRINT,
        POTION_LEVEL_UP,
        NEW_INGREDIENT,
        CONSUMABLE,
        RESEARCH,
        WINEMAKING,
        CORE_CREATION,
        FERTILIZER_CREATION,
        QUINTESSECNE_CREATION
    }

    [SerializeField]
    protected RECIPE_TYPE type;

    [SerializeField]
    protected string recipeName;

    [SerializeField]
    protected int basePoints;

    [SerializeField]
    protected StatBoardView.Aspect aspect;

    [SerializeField]
    protected int temperature;

    [SerializeField]
    protected int aether;

    [SerializeField]
    protected int visctosity;

    [SerializeField]
    protected int voidness;



    //поля с аксессорами не могут быть SerializeField, поэтому для каждого делаем своё поле с get

    public RECIPE_TYPE Type
    {
        get
        {
            if (GetComponent<Ingredient>() && GetComponent<Ingredient>().state == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT) return RECIPE_TYPE.CONSUMABLE;
            else if (GetComponent<PotionRecipe>() && PotionListManager.LevelUPpingList.Contains(GetComponent<PotionRecipe>())) return RECIPE_TYPE.POTION_LEVEL_UP;
            else if (GetComponent<PotionRecipe>() && PotionListManager.potionLevelDictionary[gameObject] == 0) return RECIPE_TYPE.POTION_BLUEPRINT;
            return type;
        }
    }

    public string RecipeName
    {
        get
        {
            return recipeName;
        }
    }

    public StatBoardView.Aspect Aspect
    {
        get
        {
            return aspect;
        }
    }

    public int Temperature
    {
        get
        {
            return temperature;
        }
    }

    public int Aether
    {
        get
        {
            return aether;
        }
    }

    public int Viscosity
    {
        get
        {
            return visctosity;
        }
    }

    public int Voidness
    {
        get
        {
            return voidness;
        }
    }


    abstract public int Points
    {
        get;
    }


    public struct EssenceRequirement
    {
        public Ingredient.ESSENSE essence;
        public int count;
        public EssenceRequirement(Ingredient.ESSENSE ess, int n)
        {
            essence = ess;
            count = n;
        }

    }

    public EssenceRequirement essence1;
    public EssenceRequirement essence2;

    public EssenceRequirement[] essences
    {
        get
        {
            if (essence1.essence == Ingredient.ESSENSE.None || essence2.essence == Ingredient.ESSENSE.None)
            {
                int n1 = UnityEngine.Random.Range(1, 11);
                essence1 = new EssenceRequirement((Ingredient.ESSENSE)Enum.ToObject(typeof(Ingredient.ESSENSE), n1), UnityEngine.Random.Range(1, 3));
                int n2 = UnityEngine.Random.Range(1, 11);
                essence2 = new EssenceRequirement((Ingredient.ESSENSE)Enum.ToObject(typeof(Ingredient.ESSENSE), n2), UnityEngine.Random.Range(1, 3));
                while (essence1.essence == essence2.essence) essence2 = new EssenceRequirement((Ingredient.ESSENSE)Enum.ToObject(typeof(Ingredient.ESSENSE), UnityEngine.Random.Range(1, 11)), UnityEngine.Random.Range(1, 3));
            }
            return new EssenceRequirement[] { essence1, essence2 };
        }
    }

    abstract protected void firstReward();

    abstract protected void rewardsRemains();
}
