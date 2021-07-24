using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Recipe : MonoBehaviour
{

    protected enum RECIPE_TYPE
    {
        POTION_BREWING,
        NEW_RECIPE,
        RECIPE_UPGRADE,
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public int Points
    {
        get;
    }

    abstract protected void firstReward();

    abstract protected void rewardsRemains();
}
