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
        Meat,
        Crystall,
        Lighting,
    }

    public enum RARITY
    {
        COMMON, 
        UNCOMMON,
        RARE,
        VERY_RARE,
        LEGENDARY
    }

    public enum CONSTELLATION
    {
        ASPECT,
        //NEORGANIC,
        TEMPERATURE,
        SUPERNOVA,
        //GOLD_AND_BLACK,
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
    [SerializeField]
    public ESSENSE essence4;

    [SerializeField]
    RARITY rarity;
    [SerializeField]
    CONSTELLATION constellation;

    public RARITY Rarity { get { return rarity; } }
    public CONSTELLATION Constellation { get { return constellation; } }

    [SerializeField]
    public string IngredientName;
    [SerializeField]
    public string IngredientDescription;

    public ESSENSE[] essenceList
    {
        get
        {
            return new ESSENSE[] { essence1, essence2, essence3, essence4 };
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
