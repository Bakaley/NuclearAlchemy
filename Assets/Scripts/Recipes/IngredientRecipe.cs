using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientRecipe : Recipe
{
    public override int Points
    {
        get
        {
            return basePoints;
        }
    }

    protected override void firstReward()
    {
        throw new System.NotImplementedException();
    }

    protected override void rewardsRemains()
    {
        throw new System.NotImplementedException();
    }
}
