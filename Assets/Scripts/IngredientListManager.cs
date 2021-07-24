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
        List<GameObject> firstList = null;
        
        switch (ConstellationManager.CONSTELLATION1)
        {
            case ConstellationManager.CONSTELLATION.COLORANT:
                firstList = GetComponent<ListOfColorants>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                firstList = GetComponent<ListOfTemperature>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.AETHER:
                firstList = GetComponent<ListOfAether>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                firstList = GetComponent<ListOfSuperovas>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.VOIDS:
                firstList = GetComponent<ListOfVoids>().unlockedIngredientList;
                break;
        }

        List<GameObject> secondList = null;

        switch (ConstellationManager.CONSTELLATION2)
        {
            case ConstellationManager.CONSTELLATION.COLORANT:
                secondList = GetComponent<ListOfColorants>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                secondList = GetComponent<ListOfTemperature>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.AETHER:
                secondList = GetComponent<ListOfAether>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                secondList = GetComponent<ListOfSuperovas>().unlockedIngredientList;
                break;
            case ConstellationManager.CONSTELLATION.VOIDS:
                secondList = GetComponent<ListOfVoids>().unlockedIngredientList;
                break;
        }

        commonList = new List<GameObject>(GetComponent<ListOfCommon>().unlockedIngredientList);
        uncommonList = new List<GameObject>(GetComponent<ListOfRareAspect>().unlockedIngredientList);
        if(firstList != null && firstList.Count != 0) foreach(GameObject gameObject in firstList) uncommonList.Add(gameObject);
        if(secondList != null && secondList.Count != 0)  foreach (GameObject gameObject in secondList) uncommonList.Add(gameObject);

    }

    public List <GameObject> newIngredients
    {
        get
        {
            List<GameObject> ingredients = new List<GameObject>();
            for (int i = 0; i <= 3; i++)
            {
                GameObject ingredient;
                int ingredientNumber = Random.Range(0, 100);
                if(ingredientNumber < uncommonList.Count) ingredient = uncommonList[ingredientNumber];
                else ingredient = commonList[Random.Range(0, 50)];

                while (ingredients.Contains(ingredient))
                {
                    ingredientNumber = Random.Range(0, 100);
                    if (ingredientNumber < uncommonList.Count) ingredient = uncommonList[ingredientNumber];
                    else ingredient = commonList[Random.Range(0, 50)];
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
