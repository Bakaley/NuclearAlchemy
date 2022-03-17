using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientListManager : MonoBehaviour
{
    static List<Ingredient> commonList;
    static List<Ingredient> uncommonList;
    static List<Ingredient> colorantsList;
    static List<Ingredient> aetherList;
    static List<Ingredient> temperatureList;
    static List<Ingredient> supernovaList;
    static List<Ingredient> voidsList;
    // Start is called before the first frame update

    [SerializeField]
    GameObject[] commonIngredients;
    [SerializeField]
    GameObject[] uncommonIngredients;
    [SerializeField]
    GameObject[] colorantsIngredients;
    [SerializeField]
    GameObject[] temperatureIngredients;
    [SerializeField]
    GameObject[] aetherIngredients;
    [SerializeField]
    GameObject[] supernovaIngredients;
    [SerializeField]
    GameObject[] voidsIngredients;

    private void Awake()
    {

        commonList = new List<Ingredient>();
        foreach (GameObject ingr in commonIngredients) commonList.Add(ingr.GetComponent<Ingredient>());
        uncommonList = new List<Ingredient>();
        foreach (GameObject ingr in uncommonIngredients) uncommonList.Add(ingr.GetComponent<Ingredient>());
        colorantsList = new List<Ingredient>();
        foreach (GameObject ingr in colorantsIngredients) colorantsList.Add(ingr.GetComponent<Ingredient>());
        aetherList = new List<Ingredient>();
        foreach (GameObject ingr in aetherIngredients) aetherList.Add(ingr.GetComponent<Ingredient>());
        temperatureList = new List<Ingredient>();
        foreach (GameObject ingr in temperatureIngredients) temperatureList.Add(ingr.GetComponent<Ingredient>());
        supernovaList = new List<Ingredient>();
        foreach (GameObject ingr in supernovaIngredients) supernovaList.Add(ingr.GetComponent<Ingredient>());
        voidsList = new List<Ingredient>();
        foreach (GameObject ingr in voidsIngredients) voidsList.Add(ingr.GetComponent<Ingredient>());

        staticInstance = this;

        ingredientsAvaliability = new Dictionary<Ingredient, Ingredient.AVALIABILITY>();
        foreach (Ingredient ingr in commonList) ingredientsAvaliability.Add(ingr, Ingredient.AVALIABILITY.LEARNED_WITH_NO_BLUEPRINT);

        foreach (Ingredient ingr in uncommonList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (Ingredient ingr in colorantsList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (Ingredient ingr in temperatureList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (Ingredient ingr in aetherList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (Ingredient ingr in supernovaList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (Ingredient ingr in voidsList)
        {
            ingredientsAvaliability.Add(ingr.GetComponent<Ingredient>(), Ingredient.AVALIABILITY.UNKNOWN_WITH_NO_BLUEPRINT);
        }
        foreach (GameObject ingr in ingredientsWithBlueprints)
        {
            ingredientsAvaliability[ingr.GetComponent<Ingredient>()] = Ingredient.AVALIABILITY.UNKNOWN_BLUEPRINT;
        }

    }

    void Start()
    {
        
        if (SaveLoadManager.saveObject != null)
        {
            Dictionary<string, string> dict = SaveLoadManager.saveObject.ingredientsAvaliabilitySave;
            List<Ingredient> keys = new List<Ingredient>(ingredientsAvaliability.Keys);
            foreach (Ingredient key in keys)
            {
                Enum.TryParse(dict[key.IngredientFileName], out Ingredient.AVALIABILITY avaliability);
                ingredientsAvaliability[key] = avaliability;
            }
        }

        refreshLists();
    }

    public static void refreshLists()
    {
        List<Ingredient> firstList = null;

        switch (ConstellationManager.CONSTELLATION1)
        {
            case ConstellationManager.CONSTELLATION.COLORANT:
                firstList = colorantsList;
                break;
            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                firstList = temperatureList;
                break;
            case ConstellationManager.CONSTELLATION.AETHER:
                firstList = aetherList;
                break;
            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                firstList = supernovaList;
                break;
            case ConstellationManager.CONSTELLATION.VOIDS:
                firstList = voidsList;
                break;
            case ConstellationManager.CONSTELLATION.LENSING:
            case ConstellationManager.CONSTELLATION.NONE:
                firstList = new List<Ingredient>();
                break;
        }

        List<Ingredient> secondList = null;

        switch (ConstellationManager.CONSTELLATION2)
        {
            case ConstellationManager.CONSTELLATION.COLORANT:
                secondList = colorantsList;
                break;
            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                secondList = temperatureList;
                break;
            case ConstellationManager.CONSTELLATION.AETHER:
                secondList = aetherList;
                break;
            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                secondList = supernovaList;
                break;
            case ConstellationManager.CONSTELLATION.VOIDS:
                secondList = voidsList;
                break;
            case ConstellationManager.CONSTELLATION.LENSING:
            case ConstellationManager.CONSTELLATION.NONE:
                secondList = new List<Ingredient>();
                break;
        }

        uncommonList = new List<Ingredient>();
        foreach (GameObject ingr in staticInstance.uncommonIngredients) uncommonList.Add(ingr.GetComponent<Ingredient>());
        uncommonList.AddRange(firstList);
        uncommonList.AddRange(secondList);

    }

    public List <Ingredient> newIngredients
    {
        get
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 0; i <= 3; i++)
            {
                Ingredient ingredient;
                int ingredientNumber = UnityEngine.Random.Range(0, 100);
                if(ingredientNumber < uncommonList.Count) ingredient = uncommonList[ingredientNumber];
                else ingredient = commonList[UnityEngine.Random.Range(0, 50)];

                while (ingredients.Contains(ingredient))
                {
                    ingredientNumber = UnityEngine.Random.Range(0, 100);
                    if (ingredientNumber < uncommonList.Count) ingredient = uncommonList[ingredientNumber];
                    else ingredient = commonList[UnityEngine.Random.Range(0, 50)];
                }

                ingredients.Add(ingredient);
            }
            return ingredients;
        }
    }


    [SerializeField]
    GameObject[] ingredientsWithBlueprints;

    public static Dictionary<Ingredient, Ingredient.AVALIABILITY> ingredientsAvaliability
    {
        get; private set;
    }  

    static IngredientListManager staticInstance;


    public static List<Ingredient> getIngredients(Ingredient.AFFILIATION affiliation, Ingredient.AVALIABILITY avaliability)
    {
        List<Ingredient> list = new List<Ingredient>();
        foreach (KeyValuePair<Ingredient, Ingredient.AVALIABILITY> pair in ingredientsAvaliability) {
            if (pair.Key.Affliliation == affiliation && pair.Value == avaliability) list.Add(pair.Key);
        }
        return list;
    }

    public static List<Ingredient> getIngredients(ConstellationManager.CONSTELLATION constellation, Ingredient.AVALIABILITY avaliability)
    {
        if (constellation == ConstellationManager.CONSTELLATION.NONE) return new List<Ingredient>();
        //линзирование не может занимать слот первого созвездия и возвращает ингредиенты из слота первого созвездия, если этот слот != NONE
        if (constellation == ConstellationManager.CONSTELLATION.LENSING)
        {
            if (ConstellationManager.CONSTELLATION1 == ConstellationManager.CONSTELLATION.NONE) return getIngredients(ConstellationManager.CONSTELLATION1, avaliability);
        }

        List<Ingredient> list = new List<Ingredient>();
        foreach (KeyValuePair<Ingredient, Ingredient.AVALIABILITY> pair in ingredientsAvaliability)
        {
            if (pair.Key.CONSTELLATION == constellation && pair.Value == avaliability) list.Add(pair.Key);
        }
        return list;
    }

    public static List<Recipe> getIngredientBlueprints(ConstellationManager.CONSTELLATION constellation, Ingredient.AVALIABILITY avaliability)
    {
        List<Recipe> list = new List<Recipe>();
        foreach (KeyValuePair<Ingredient, Ingredient.AVALIABILITY> pair in ingredientsAvaliability)
        {
            if (pair.Key.CONSTELLATION == constellation && pair.Value == avaliability)
            {
                list.Add(pair.Key.GetComponent<Recipe>());
            }
        }
        return list;
    }
    
    public static int BlueprintsCount
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<Ingredient, Ingredient.AVALIABILITY> pair in ingredientsAvaliability)
            {
                if (pair.Value == Ingredient.AVALIABILITY.KNOWN_BLUEPRINT) list.Add(pair.Key.GetComponent<Recipe>());
            }
            return list.Count;
        }
    }

    public static void researchIngredient(Ingredient ingr)
    {
        if (ingr.GetComponent<Recipe>()) ingredientsAvaliability[ingr] = Ingredient.AVALIABILITY.LEARNED_BLUEPRINT;
        else ingredientsAvaliability[ingr] = Ingredient.AVALIABILITY.LEARNED_WITH_NO_BLUEPRINT;
    }

}
