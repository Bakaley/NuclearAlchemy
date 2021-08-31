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


    [SerializeField]
    GameObject[] ingredientsWithBlueprints;

    public static Dictionary<GameObject, Ingredient.INGREDIENT_STATE> blueprintsAvailability
    {
        get; private set;
    }
    static Dictionary<Ingredient.INGREDIENT_CONSTELLATION, ConstellationManager.CONSTELLATION> constellationDictionary;

    private void Awake()
    {
        constellationDictionary = new Dictionary<Ingredient.INGREDIENT_CONSTELLATION, ConstellationManager.CONSTELLATION>();
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.ASPECT, ConstellationManager.CONSTELLATION.NONE);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.RARE_ASPECT, ConstellationManager.CONSTELLATION.NONE);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.COLORANTS, ConstellationManager.CONSTELLATION.COLORANT);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.TEMPERATURE, ConstellationManager.CONSTELLATION.TEMPERATURE);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.AETHER, ConstellationManager.CONSTELLATION.AETHER);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.SUPERNOVA, ConstellationManager.CONSTELLATION.SUPERNOVA);
        constellationDictionary.Add(Ingredient.INGREDIENT_CONSTELLATION.VOID, ConstellationManager.CONSTELLATION.VOIDS);

        blueprintsAvailability = new Dictionary<GameObject, Ingredient.INGREDIENT_STATE>();

        foreach (GameObject ingr in ingredientsWithBlueprints)
        {

            int r = Random.Range(0, 4);
            if (r == 0) blueprintsAvailability.Add(ingr, Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT);
            else blueprintsAvailability.Add(ingr, Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT);

            /*if(ingr.GetComponent<Ingredient>().Constellation == Ingredient.INGREDIENT_CONSTELLATION.RARE_ASPECT) blueprintsAvailability.Add(ingr, Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT);
            else blueprintsAvailability.Add(ingr, Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT);*/
        }

        constellationKnownLists = new Dictionary<ConstellationManager.CONSTELLATION, List<Recipe>>();
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.NONE, new List<Recipe>());
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.COLORANT, colorantsList);
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.TEMPERATURE, temperatureList);
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.AETHER, aetherList);
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.SUPERNOVA, supernovaList);
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.VOIDS, voidsList);
        constellationKnownLists.Add(ConstellationManager.CONSTELLATION.LENSING, new List<Recipe>());

        constellationLearnedLists = new Dictionary<ConstellationManager.CONSTELLATION, List<Recipe>>();
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.NONE, new List<Recipe>());
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.COLORANT, colorantsLearnedList);
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.TEMPERATURE, temperatureLearnedList);
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.AETHER, aetherLearnedList);
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.SUPERNOVA, supernovaLearnedList);
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.VOIDS, voidsLearnedList);
        constellationLearnedLists.Add(ConstellationManager.CONSTELLATION.LENSING, new List<Recipe>());


    }

    public static Dictionary<ConstellationManager.CONSTELLATION, List<Recipe>> constellationKnownLists
    {
        get; private set;
    }

    public static Dictionary<ConstellationManager.CONSTELLATION, List<Recipe>> constellationLearnedLists
    {
        get; private set;
    }

    public static int BlueprintsCount
    {
        get
        {
            return basicBlueprintsList.Count + colorantsList.Count + temperatureList.Count + aetherList.Count + supernovaList.Count + voidsList.Count;
        }
    }

    public static List<Recipe> basicBlueprintsList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.NONE)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    public static List<Recipe> basicLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.NONE)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> colorantsList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.COLORANT)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> temperatureList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.TEMPERATURE)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> aetherList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.AETHER)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> supernovaList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.SUPERNOVA)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> voidsList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.KNOWN_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.VOIDS)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> colorantsLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.COLORANT)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> temperatureLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.TEMPERATURE)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> aetherLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.AETHER)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> supernovaLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.SUPERNOVA)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }

    static List<Recipe> voidsLearnedList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<GameObject, Ingredient.INGREDIENT_STATE> pair in blueprintsAvailability)
            {
                if (pair.Value == Ingredient.INGREDIENT_STATE.LEARNED_BLUEPRINT)
                {
                    if (constellationDictionary[pair.Key.GetComponent<Ingredient>().Constellation] == ConstellationManager.CONSTELLATION.VOIDS)
                    {
                        list.Add(pair.Key.GetComponent<Recipe>());
                    }
                }
            }
            return list;
        }
    }
}
