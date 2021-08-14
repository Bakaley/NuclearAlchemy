using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingBoard : MonoBehaviour
{
    [SerializeField]
    IngredientPanel ingredientPanel;

    Ingredient currnetIngredient;

    [SerializeField]
    GameObject orbShift;


    void updateOrbList()
    {
        orbs = new Orb[length, height];
        foreach (Transform child in orbShift.transform)
        {
            if (child.tag == "Orb")
            {
                orbs[(int)child.transform.localPosition.x, (int)child.transform.localPosition.y] = child.gameObject.GetComponent<Orb>();
            }
        }
    }

    public static int length { get; private set; } = 4;
    public static int height { get; private set; } = 4;

    public Orb[,] orbs = new Orb[length, height];

    int maxLeftX;
    int maxRightX;

    public GameObject OrbShift
    {
        get
        {
            return orbShift;
        }
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
        List<Orb> orbs = new List<Orb>(orbShift.GetComponentsInChildren<Orb>());
        foreach (Orb orb in orbs)
        {
            Destroy(orb.gameObject);
        }
    }
    
    public void ingredientFill(Ingredient ingredient)
    {
        addingBoardClear();
        currnetIngredient = ingredient;

        maxLeftX = MixingBoard.Length;
        maxRightX = -1;

        float seed = Random.Range(-100f, 100f);
        foreach (Orb orb in ingredient.GetComponentsInChildren<Orb>())
        {
            GameObject _orb = Instantiate(orb.gameObject, orb.transform.localPosition, Quaternion.identity);
            _orb.transform.SetParent(orbShift.transform, false);
            _orb.GetComponent<Orb>().movingSeed = seed;
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
                foreach (Orb orb in orbShift.GetComponentsInChildren<Orb>())
                {
                    orb.transform.localPosition = new Vector3(orb.transform.localPosition.x - 1, orb.transform.localPosition.y, orb.transform.localPosition.z);
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
            if (maxRightX != MixingBoard.Length - 1)
            {
                foreach (Orb orb in orbShift.GetComponentsInChildren<Orb>())
                {
                    orb.transform.localPosition = new Vector3(orb.transform.localPosition.x + 1, orb.transform.localPosition.y, orb.transform.localPosition.z);
                }
                maxRightX++;
                maxLeftX++;
            }
        }
        updateOrbList();
    }
    public void refreshIngredients()
    {
        ingredientPanel.refreshIngredientsWithDelay();
    }
}
