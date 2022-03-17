using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AddingBoard : MonoBehaviour
{
    [SerializeField]
    IngredientPanel ingredientPanel;

    Ingredient currnetIngredient;

    [SerializeField]
    GameObject orbShift;

    public OrbBox[] orbBoxes
    {
        get; private set;
    }

    void updateOrbList()
    {
        orbs = new Orb[Width, Height];
        orbBoxes = GetComponentsInChildren<OrbBox>();

        foreach (OrbBox orbBox in orbBoxes)
        {
            orbs[(int)Math.Round(orbBox.transform.localPosition.x), (int)Math.Round(orbBox.transform.localPosition.y)] = orbBox.GetComponent<OrbBox>().Orb;
        }
    }

    public static int Width { get; private set; } = 4;
    public static int Height { get; private set; } = 4;

    public Orb[,] orbs = new Orb[Width, Height];

    int maxLeftX;
    int maxRightX;

    public GameObject OrbShift
    {
        get
        {
            return orbShift;
        }
    }

    private void Awake()
    {
        orbBoxes = new OrbBox[Height * Width];
    }

    // Start is called before the first frame update
    void Start()
    {
        currnetIngredient = IngredientPanel.currentIngredient;
        ingredientFill(currnetIngredient);
    }

    // Update is called once per frame
    void Update()
    {
        updateOrbList();
    }

    public void addingBoardClear()
    {
        List<OrbBox> orbs = new List<OrbBox>(orbShift.GetComponentsInChildren<OrbBox>());
        foreach (OrbBox orb in orbs)
        {
            Destroy(orb.gameObject);
        }
    }
    
    public void ingredientFill(Ingredient ingredient)
    {
        addingBoardClear();
        currnetIngredient = ingredient;

        maxLeftX = MixingBoard.Width;
        maxRightX = -1;

        float seed = UnityEngine.Random.Range(-100f, 100f);
        foreach (OrbBox orbBox in ingredient.GetComponentsInChildren<OrbBox>())
        {
            GameObject _orb = Instantiate(orbBox.gameObject, orbBox.transform.localPosition, Quaternion.identity);
            _orb.transform.SetParent(orbShift.transform, false);
            _orb.GetComponentInChildren<Orb>().movingSeed = seed;
            maxLeftX = Mathf.Min(maxLeftX, (int)_orb.transform.localPosition.x);
            maxRightX = Mathf.Max(maxRightX, (int)_orb.transform.localPosition.x);
        }
        updateOrbList();
    }

    public void moveIngredientLeft()
    {
        if(MixingBoard.deployDelay <= 0)
        {
            MixingBoard.ingredientMovementDelay = .2f;
            if (maxLeftX != 0)
            {
                foreach (OrbBox orbBox in orbShift.GetComponentsInChildren<OrbBox>())
                {
                    orbBox.transform.localPosition = new Vector3(orbBox.transform.localPosition.x - 1, orbBox.transform.localPosition.y, orbBox.transform.localPosition.z);
                }
                maxRightX--;
                maxLeftX--;
            }
        }
        updateOrbList();
    }

    public void moveIngredientRight()
    {
        if (MixingBoard.deployDelay <= 0)
        {
            MixingBoard.ingredientMovementDelay = .2f;
            if (maxRightX != MixingBoard.Width - 1)
            {
                foreach (OrbBox orbBox in orbShift.GetComponentsInChildren<OrbBox>())
                {
                    orbBox.transform.localPosition = new Vector3(orbBox.transform.localPosition.x + 1, orbBox.transform.localPosition.y, orbBox.transform.localPosition.z);
                }
                maxRightX++;
                maxLeftX++;
            }
        }
        updateOrbList();
    }
    public void refreshIngredients()
    {
        IngredientPanel.refreshIngredientsWithDelay();
    }
}
