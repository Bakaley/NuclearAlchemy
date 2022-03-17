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

    public enum AFFILIATION
    {
        ASPECT,
        RARE_ASPECT,
        COLORANTS,
        TEMPERATURE,
        SUPERNOVA,
        AETHER,
        VOID
    }

    public static Dictionary<AFFILIATION, ConstellationManager.CONSTELLATION> affiliationConstellationDictionary
    {
        get; private set;
    }

    public ConstellationManager.CONSTELLATION CONSTELLATION
    {
        get
        {
            switch (affiliation)
            {
                case AFFILIATION.COLORANTS: return ConstellationManager.CONSTELLATION.COLORANT;
                case AFFILIATION.TEMPERATURE: return ConstellationManager.CONSTELLATION.TEMPERATURE;
                case AFFILIATION.AETHER: return ConstellationManager.CONSTELLATION.AETHER;
                case AFFILIATION.SUPERNOVA: return ConstellationManager.CONSTELLATION.SUPERNOVA;
                case AFFILIATION.VOID: return ConstellationManager.CONSTELLATION.VOIDS;
            }
            return ConstellationManager.CONSTELLATION.NONE;
        }
    }

    [SerializeField]
    public GameObject preview;
    [SerializeField]
    public ESSENSE essence1;
    [SerializeField]
    public ESSENSE essence2;
    [SerializeField]
    public ESSENSE essence3;

    public enum AVALIABILITY
    {
        UNKNOWN_WITH_NO_BLUEPRINT,
        LEARNED_WITH_NO_BLUEPRINT,
        UNKNOWN_BLUEPRINT,
        KNOWN_BLUEPRINT,
        LEARNED_BLUEPRINT,
    }

    public AVALIABILITY state
    {
        get
        {
            return IngredientListManager.ingredientsAvaliability[this];
        }
    }

    [SerializeField]
    RARITY rarity;
    [SerializeField]
    AFFILIATION affiliation;

    public RARITY Rarity { get { return rarity; } }
    public AFFILIATION Affliliation { get { return affiliation; } }

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
