using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Recipe
{

    public int currentLevel
    {
        get
        {
            switch (PotionListManager.potionsAvaliability[this])
            {
                case AVALIABILITY.LEVEL1: return 1;
                case AVALIABILITY.LEVEL2: return 2;
                case AVALIABILITY.LEVEL3: return 3;
                case AVALIABILITY.KNOWN_BLUEPRINT: return 0;
            }
            return -1;
        }
    }

    public enum POTION_TYPE
    {
        BASICALLY_KNOWN,
        BASICALLY_UNKNOWN
    }

    public enum AVALIABILITY
    {
        UNKNOWN_BLUEPRINT,
        KNOWN_BLUEPRINT,
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    [SerializeField]
    ConstellationManager.CONSTELLATION constellation1;
    [SerializeField]
    ConstellationManager.CONSTELLATION constellation2;

    public ConstellationManager.CONSTELLATION Constellation1
    {
        get
        {
            return constellation1;
        }
    }
    public ConstellationManager.CONSTELLATION Constellation2
    {
        get
        {
            return constellation2;
        }
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

    Color32 lvl1color = new Color32(31, 114, 176, 255);
    Color32 lvl2color = new Color32(162, 39, 97, 255);
    Color32 lvl3color = new Color32(150, 117, 24, 255);

    public Color32 IconColor
    {
        get
        {
            switch (Type)
            {
                case RECIPE_TYPE.POTION_BLUEPRINT:
                    return lvl1color;
                case RECIPE_TYPE.POTION_LEVEL_UP:
                    switch (currentLevel)
                    {
                        case 1:
                            return lvl2color;
                        case 2:
                            return lvl3color;
                    }
                    return new Color(183, 183, 183, 255);
                case RECIPE_TYPE.POTION_BREWING:
                    switch (currentLevel)
                    {
                        case 1:
                            return lvl1color;
                        case 2:
                            return lvl2color;
                        case 3:
                            return lvl3color;
                    }
                    return new Color(183, 183, 183, 255);
            }
            return new Color(183, 183, 183, 255);
        }
    }

    public override int Points
    {
        get
        {
            switch (Type)
            {
                case RECIPE_TYPE.POTION_BLUEPRINT:
                    return basePoints;
                case RECIPE_TYPE.POTION_LEVEL_UP:
                    switch (currentLevel)
                    {
                        case 1:
                            return basePointsLVL2;
                        case 2:
                            return basePointsLVL3;
                    }
                    return 0;
                case RECIPE_TYPE.POTION_BREWING:
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
            return 0;
        }
    }


    [SerializeField]
    int basePointsLVL2;

    [SerializeField]
    int basePointsLVL3;

    static double PotionCountlvl1
    {
        get
        {
            return 1.25;
        }
    }
    static double PotionCountlvl2
    {
        get
        {
            return 1.5;
        }
    }
    static double PotionCountlvl3
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

    public double nextLvLPotionCount
    {
        get
        {
            switch (currentLevel)
            {
                case 0:
                    return PotionCountlvl1;
                case 1:
                    return PotionCountlvl2;
                case 2:
                    return PotionCountlvl3;
            }
            return 0;
        }
    }

    [SerializeField]
    int price;

    public int Price
    {
        get
        {
            return price;
        }
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
