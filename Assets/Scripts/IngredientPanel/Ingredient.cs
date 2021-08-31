using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    
    public enum ESSENSE
    {
        None,
        Fire,
        Water,
        Air,
        Stone,
        Mushroom,
        Magic,
        Plant,
        Animal,
        Crystall,
        Lighting,
    }

    public enum RARITY
    {
        COMMON, 
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public enum INGREDIENT_CONSTELLATION
    {
        ASPECT,
        RARE_ASPECT,
        COLORANTS,
        TEMPERATURE,
        SUPERNOVA,
        AETHER,
        VOID
    }

    [SerializeField]
    public GameObject preview;
    [SerializeField]
    public ESSENSE essence1;
    [SerializeField]
    public ESSENSE essence2;
    [SerializeField]
    public ESSENSE essence3;

    public enum INGREDIENT_STATE
    {
        UNKNOWN_WITH_NO_BLUEPRINT,
        LEARNED_WITH_NO_BLUEPRINT,
        UNKNOWN_BLUEPRINT,
        KNOWN_BLUEPRINT,
        LEARNED_BLUEPRINT,
    }

    public INGREDIENT_STATE state
    {
        get
        {
            return IngredientListManager.blueprintsAvailability[gameObject];
        }
    }

    [SerializeField]
    RARITY rarity;
    [SerializeField]
    INGREDIENT_CONSTELLATION constellation;

    public RARITY Rarity { get { return rarity; } }
    public INGREDIENT_CONSTELLATION Constellation { get { return constellation; } }

    [SerializeField]
    public string IngredientName, EnglishName;
    [SerializeField]
    public string IngredientFileName;
    [SerializeField]
    public string IngredientDescription;
    [SerializeField]
    public Sprite IngredientIcon;
    [SerializeField]
    public Color IngredientIconColor;

    [SerializeField]
    public bool locked;

    public ESSENSE[] essenceList
    {
        get
        {
            return new ESSENSE[] { essence1, essence2, essence3 };
        }
    }

    public List<Orb> orbs
    {
        get {
            if (orbs == null)
            {
                orbs = new List<Orb>(GetComponentsInChildren<Orb>());
            }
            return orbs;
        }
        private set
        {
         //   orbs = value;
        }
    }

    void Start()
    {
        orbs = new List<Orb>(GetComponentsInChildren<Orb>());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
