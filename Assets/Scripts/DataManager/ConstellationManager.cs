using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ConstellationManager : MonoBehaviour
{

    public enum CONSTELLATION
    {
        NONE,
        COLORANT,
        TEMPERATURE,
        AETHER,
        SUPERNOVA,
        VOIDS,
        LENSING
    }


    public static CONSTELLATION CONSTELLATION1
    {
        get;
        private set;
    }
    public static CONSTELLATION CONSTELLATION2
    {
        get;
        private set;
    }

    public static bool CONTAINS(CONSTELLATION constellation)
    {
        return (CONSTELLATION1 == constellation || CONSTELLATION2 == constellation);
    }

    public static void setConstellations (List<CONSTELLATION> list)
    {
        CONSTELLATION1 = list[0];
        CONSTELLATION2 = list[1];
        sortConstellations();
        RecipeDrafter.draftersRefresh();
        IngredientListManager.refreshLists();
        StatBoardView.blocksRefresh();
        IngredientPanel.refreshIngredientsNoDelay();
        MixingBoard.cleanUpBoard();
        DraftModule.cancelAllRecipes();
    }

    private void Awake()
    {
        int n1 = UnityEngine.Random.Range(0, 7);
        CONSTELLATION1 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), n1);
        //rigging constellations
        CONSTELLATION1 = CONSTELLATION.NONE;
        int n2 = UnityEngine.Random.Range(0, 7);
        CONSTELLATION2 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), UnityEngine.Random.Range(0, 7));
        CONSTELLATION2 = CONSTELLATION.NONE;
        while (CONSTELLATION1 == CONSTELLATION2)
        {
            if (CONSTELLATION1 == CONSTELLATION.NONE) break;
            CONSTELLATION2 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), UnityEngine.Random.Range(0, 7));
        }
        sortConstellations();

    }

    static void sortConstellations()
    {
        if((int)CONSTELLATION1 > (int)CONSTELLATION2)
        {
            CONSTELLATION constellation = CONSTELLATION1;
            CONSTELLATION1 = CONSTELLATION2;
            CONSTELLATION2 = constellation;
        }
    }
}
