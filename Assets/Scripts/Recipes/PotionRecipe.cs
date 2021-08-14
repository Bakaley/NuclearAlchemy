using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRecipe : Recipe
{

    [SerializeField]
    int currentLevel = 1;

    public enum POTION_TYPE
    {
        BASIC,
        TEMPERATURE,
        TEMPERATURE_PROGRESSING,
        AETHER,
        AETHER_PROGRESSING,
        SUPERNOVA,
        SUPERNOVA_PROGRESSING,
        VOIDS,
        VOIDS_PROGRESSING,
        COLORANTS,
        COLORANTS_PROGRESSING,
        BASIC_PLUS,
        BASIC_PLUS_PROGRESSING,
        TEMPERATURE_PLUS,
        AETHER_PLUS,
        SUPERNOVA_PLUS,
        VOIDS_PLUS,
        COLORANTS_PLUS,
        TEMPERATURE_AETHER,
        COLORANTS_AETHER,
        COLORANTS_SUPERNOVA,
        COLORANTS_VOIDS,
        TEMPERATURE_SUPERNOVA,
        AETHER_SUPERNOVA,
        VOIDS_SUPERNOVA,
        TEMPERATURE_VOIDS,
        AETHER_VOIDS,
        TEMPERATURE_COLORANTS,
    }

    [SerializeField]
    POTION_TYPE potionType;

    public POTION_TYPE Potion_Type
    {
        get
        {
            return potionType;
        }
    }

    [SerializeField]
    Sprite icon;

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    [SerializeField]
    protected Color iconColor;

    public override int Points
    {
        get
        {
            switch (currentLevel)
            {
                case 1:
                    return basePoints;
                case 2:
                    return basePointsLVL2;
                case 3:
                    return basePointsLVL3;
            }
            return 0;
        }
    }

    [SerializeField]
    int basePointsLVL2;

    [SerializeField]
    int basePointsLVL3;

    public static double PotionCountlvl1
    {
        get
        {
            return 1.25;
        }
    }
    public static double PotionCountlvl2
    {
        get
        {
            return 1.5;
        }
    }
    public static double PotionCountlvl3
    {
        get
        {
            return 1.75;
        }
    }


    public double PotionCount
    {
        get
        {
            switch (currentLevel)
            {
                case 1:
                    return PotionCountlvl1;
                case 2:
                    return PotionCountlvl2;
                case 3:
                    return PotionCountlvl3;
            }
            return 0;
        }
    }

    [SerializeField]
    int price;

    public int LunarCount
    {
        get
        {
            return price;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
