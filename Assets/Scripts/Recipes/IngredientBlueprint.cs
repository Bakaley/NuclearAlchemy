using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBlueprint : Recipe
{
    public override int Points
    {
        get
        {
            return basePoints;
        }
    }
}
