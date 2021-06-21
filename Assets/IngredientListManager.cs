using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientListManager : MonoBehaviour
{
    List<GameObject> commonList;
    List<GameObject> uncommonList;
    // Start is called before the first frame update

    void Start()
    {
        commonList = new List<GameObject>(GetComponent<ListOfCommon>().unlockedIngredientList);
        Debug.Log(GetComponent<ListOfCommon>().unlockedIngredientList.Count);
    }

    public List <GameObject> newIngredients
    {
        get
        {
            List<GameObject> ingredients = new List<GameObject>();
            for (int i = 0; i <= 3; i++)
            {
                GameObject ingredient;
                ingredient = commonList[Random.Range(0, 50)];
                while (ingredients.Contains(ingredient))
                {
                    ingredient = commonList[Random.Range(0, 50)];
                }
                ingredients.Add(ingredient);
            }
            return ingredients;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
