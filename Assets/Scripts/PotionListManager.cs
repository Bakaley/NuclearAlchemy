using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionListManager : MonoBehaviour
{
    [SerializeField]
    public GameObject basic1;
    [SerializeField]
    public GameObject basic2;
    [SerializeField]
    public GameObject basic3;
    [SerializeField]
    public GameObject basic4;
    [SerializeField]
    public GameObject basic5;
    [SerializeField]
    public GameObject basic6;
    [SerializeField]
    public GameObject basic7;
    [SerializeField]
    public GameObject basic8;
    [SerializeField]
    public GameObject basic9;

    [SerializeField]
    public GameObject progressing_standard_plus1;
    [SerializeField]
    public GameObject progressing_standard_plus2;
    [SerializeField]
    public GameObject progressing_standard_plus3;
    [SerializeField]
    public GameObject standard_plus1;
    [SerializeField]
    public GameObject standard_plus2;
    [SerializeField]
    public GameObject standard_plus3;

    [SerializeField]
    public GameObject progressing_colorant1;
    [SerializeField]
    public GameObject progressing_colorant2;
    [SerializeField]
    public GameObject progressing_colorant3;
    [SerializeField]
    public GameObject progressing_temperature1;
    [SerializeField]
    public GameObject progressing_temperature2;
    [SerializeField]
    public GameObject progressing_temperature3;
    [SerializeField]
    public GameObject progressing_aether1;
    [SerializeField]
    public GameObject progressing_aether2;
    [SerializeField]
    public GameObject progressing_aether3;
    [SerializeField]
    public GameObject progressing_supernova1;
    [SerializeField]
    public GameObject progressing_supernova2;
    [SerializeField]
    public GameObject progressing_supernova3;
    [SerializeField]
    public GameObject progressing_voids1;
    [SerializeField]
    public GameObject progressing_voids2;
    [SerializeField]
    public GameObject progressing_voids3;

    [SerializeField]
    public GameObject colorant1;
    [SerializeField]
    public GameObject colorant2;
    [SerializeField]
    public GameObject colorant3;
    [SerializeField]
    public GameObject temperature1;
    [SerializeField]
    public GameObject temperature2;
    [SerializeField]
    public GameObject temperature3;
    [SerializeField]
    public GameObject aether1;
    [SerializeField]
    public GameObject aether2;
    [SerializeField]
    public GameObject aether3;
    [SerializeField]
    public GameObject supernova1;
    [SerializeField]
    public GameObject supernova2;
    [SerializeField]
    public GameObject supernova3;
    [SerializeField]
    public GameObject voids1;
    [SerializeField]
    public GameObject voids2;
    [SerializeField]
    public GameObject voids3;

    [SerializeField]
    public GameObject colorant_plus1;
    [SerializeField]
    public GameObject colorant_plus2;
    [SerializeField]
    public GameObject colorant_plus3;
    [SerializeField]
    public GameObject temperature_plus1;
    [SerializeField]
    public GameObject temperature_plus2;
    [SerializeField]
    public GameObject temperature_plus3;
    [SerializeField]
    public GameObject aether_plus1;
    [SerializeField]
    public GameObject aether_plus2;
    [SerializeField]
    public GameObject aether_plus3;
    [SerializeField]
    public GameObject supernova_plus1;
    [SerializeField]
    public GameObject supernova_plus2;
    [SerializeField]
    public GameObject supernova_plus3;
    [SerializeField]
    public GameObject voids_plus1;
    [SerializeField]
    public GameObject voids_plus2;
    [SerializeField]
    public GameObject voids_plus3;

    [SerializeField]
    public GameObject temperature_colorant1;
    [SerializeField]
    public GameObject temperature_colorant2;
    [SerializeField]
    public GameObject temperature_colorant3;

    [SerializeField]
    public GameObject temperature_aether1;
    [SerializeField]
    public GameObject temperature_aether2;
    [SerializeField]
    public GameObject temperature_aether3;


    [SerializeField]
    public GameObject colorant_aether1;
    [SerializeField]
    public GameObject colorant_aether2;
    [SerializeField]
    public GameObject colorant_aether3;


    [SerializeField]
    public GameObject temperature_supernova1;
    [SerializeField]
    public GameObject temperature_supernova2;
    [SerializeField]
    public GameObject temperature_supernova3;

    [SerializeField]
    public GameObject colorant_supernova1;
    [SerializeField]
    public GameObject colorant_supernova2;
    [SerializeField]
    public GameObject colorant_supernova3;

    [SerializeField]
    public GameObject aether_voids1;
    [SerializeField]
    public GameObject aether_voids2;
    [SerializeField]
    public GameObject aether_voids3;

    [SerializeField]
    public GameObject colorants_voids1;
    [SerializeField]
    public GameObject colorants_voids2;
    [SerializeField]
    public GameObject colorants_voids3;

    [SerializeField]
    public GameObject aether_supernova1;
    [SerializeField]
    public GameObject aether_supernova2;
    [SerializeField]
    public GameObject aether_supernova3;

    [SerializeField]
    public GameObject voids_supernova1;
    [SerializeField]
    public GameObject voids_supernova2;
    [SerializeField]
    public GameObject voids_supernova3;

    [SerializeField]
    public GameObject temperature_voids1;
    [SerializeField]
    public GameObject temperature_voids2;
    [SerializeField]
    public GameObject temperature_voids3;


    static List<GameObject> constellationList1;
    static List<GameObject> constellationList2;
    static List<GameObject> constellationListCombo;

    static List<GameObject> constellationLearn1;
    static List<GameObject> constellationLearn2;
    static List<GameObject> constellationLearnCombo;

    static PotionListManager staticInstance;

    private void Awake()
    {
        staticInstance = this;

        //уровень = -1, если зелье невыучено, и чертёж неизвестен
        //уроввень = 0, если зелье невыучено, но известен чертёж
        //уровень = 1 - зелье известно, уровень сложности - 1, кол-во зелий с одного действия - 1.25
        //уровень = 2 - зелье известно, уровень сложности - 2, кол-во зелий с одного действия - 1.5
        //уровень = 3 - зелье известно, уровень сложности - 3, кол-во зелий с одного действия - 1.75

        potionLevelDictionary = new Dictionary<GameObject, int>();

        potionLevelDictionary.Add(basic1, 1);
        potionLevelDictionary.Add(basic2, 1);
        potionLevelDictionary.Add(basic3, 1);
        potionLevelDictionary.Add(basic4, 1);
        potionLevelDictionary.Add(basic5, 1);
        potionLevelDictionary.Add(basic6, 1);
        potionLevelDictionary.Add(basic7, 1);
        potionLevelDictionary.Add(basic8, 1);
        potionLevelDictionary.Add(basic9, 1);
        /*
        potionLevelDictionary.Add(colorant1, 0);
        potionLevelDictionary.Add(colorant2, 0);
        potionLevelDictionary.Add(colorant3, 0);
        potionLevelDictionary.Add(temperature1, 0);
        potionLevelDictionary.Add(temperature2, 0);
        potionLevelDictionary.Add(temperature3, 0);
        potionLevelDictionary.Add(aether1, 0);
        potionLevelDictionary.Add(aether2, 0);
        potionLevelDictionary.Add(aether3, 0);
        potionLevelDictionary.Add(supernova1, 0);
        potionLevelDictionary.Add(supernova2, 0);
        potionLevelDictionary.Add(supernova3, 0);
        potionLevelDictionary.Add(voids1, 0);
        potionLevelDictionary.Add(voids2, 0);
        potionLevelDictionary.Add(voids3, 0);
        potionLevelDictionary.Add(standard_plus1, 0);
        potionLevelDictionary.Add(standard_plus2, 0);
        potionLevelDictionary.Add(standard_plus3, 0);

        potionLevelDictionary.Add(progressing_colorant1, -0);
        potionLevelDictionary.Add(progressing_colorant2, 0);
        potionLevelDictionary.Add(progressing_colorant3, 0);
        potionLevelDictionary.Add(progressing_temperature1, -0);
        potionLevelDictionary.Add(progressing_temperature2, 0);
        potionLevelDictionary.Add(progressing_temperature3, 0);
        potionLevelDictionary.Add(progressing_aether1, -0);
        potionLevelDictionary.Add(progressing_aether2, 0);
        potionLevelDictionary.Add(progressing_aether3, 0);
        potionLevelDictionary.Add(progressing_supernova1, -0);
        potionLevelDictionary.Add(progressing_supernova2, 0);
        potionLevelDictionary.Add(progressing_supernova3, 0);
        potionLevelDictionary.Add(progressing_voids1, -0);
        potionLevelDictionary.Add(progressing_voids2, 0);
        potionLevelDictionary.Add(progressing_voids3, 0);
        potionLevelDictionary.Add(progressing_standard_plus1, -0);
        potionLevelDictionary.Add(progressing_standard_plus2, 0);
        potionLevelDictionary.Add(progressing_standard_plus3, 0);

        potionLevelDictionary.Add(colorant_plus1, -0);
        potionLevelDictionary.Add(colorant_plus2, 0);
        potionLevelDictionary.Add(colorant_plus3, 0);
        potionLevelDictionary.Add(temperature_plus1, -0);
        potionLevelDictionary.Add(temperature_plus2, 0);
        potionLevelDictionary.Add(temperature_plus3, 0);
        potionLevelDictionary.Add(aether_plus1, -0);
        potionLevelDictionary.Add(aether_plus2, 0);
        potionLevelDictionary.Add(aether_plus3, 0);
        potionLevelDictionary.Add(supernova_plus1, -0);
        potionLevelDictionary.Add(supernova_plus2, 0);
        potionLevelDictionary.Add(supernova_plus3, 0);
        potionLevelDictionary.Add(voids_plus1, -0);
        potionLevelDictionary.Add(voids_plus2, 0);
        potionLevelDictionary.Add(voids_plus3, 0);
        potionLevelDictionary.Add(temperature_colorant1, -0);
        potionLevelDictionary.Add(temperature_colorant2, 0);
        potionLevelDictionary.Add(temperature_colorant3, 0);
        potionLevelDictionary.Add(temperature_aether1, -0);
        potionLevelDictionary.Add(temperature_aether2, 0);
        potionLevelDictionary.Add(temperature_aether3, 0);
        potionLevelDictionary.Add(colorant_aether1, -0);
        potionLevelDictionary.Add(colorant_aether2, 0);
        potionLevelDictionary.Add(colorant_aether3, 0);
        potionLevelDictionary.Add(temperature_supernova1, -0);
        potionLevelDictionary.Add(temperature_supernova2, 0);
        potionLevelDictionary.Add(temperature_supernova3, 0);
        potionLevelDictionary.Add(colorant_supernova1, -0);
        potionLevelDictionary.Add(colorant_supernova2, 0);
        potionLevelDictionary.Add(colorant_supernova3, 0);
        potionLevelDictionary.Add(aether_voids1, -0);
        potionLevelDictionary.Add(aether_voids2, 0);
        potionLevelDictionary.Add(aether_voids3, 0);
        potionLevelDictionary.Add(colorants_voids1, -0);
        potionLevelDictionary.Add(colorants_voids2, 0);
        potionLevelDictionary.Add(colorants_voids3, 0);
        potionLevelDictionary.Add(aether_supernova1, -0);
        potionLevelDictionary.Add(aether_supernova2, 0);
        potionLevelDictionary.Add(aether_supernova3, 0);
        potionLevelDictionary.Add(voids_supernova1, -0);
        potionLevelDictionary.Add(voids_supernova2, 0);
        potionLevelDictionary.Add(voids_supernova3, 0);
        potionLevelDictionary.Add(temperature_voids1, -0);
        potionLevelDictionary.Add(temperature_voids2, 0);
        potionLevelDictionary.Add(temperature_voids3, 0);*/

        potionLevelDictionary.Add(colorant1, 1);
        potionLevelDictionary.Add(colorant2, 1);
        potionLevelDictionary.Add(colorant3, 1);
        potionLevelDictionary.Add(temperature1, 1);
        potionLevelDictionary.Add(temperature2, 1);
        potionLevelDictionary.Add(temperature3, 1);
        potionLevelDictionary.Add(aether1, 1);
        potionLevelDictionary.Add(aether2, 1);
        potionLevelDictionary.Add(aether3, 1);
        potionLevelDictionary.Add(supernova1, 1);
        potionLevelDictionary.Add(supernova2, 1);
        potionLevelDictionary.Add(supernova3, 1);
        potionLevelDictionary.Add(voids1, 1);
        potionLevelDictionary.Add(voids2, 1);
        potionLevelDictionary.Add(voids3, 1);
        potionLevelDictionary.Add(standard_plus1, 1);
        potionLevelDictionary.Add(standard_plus2, 1);
        potionLevelDictionary.Add(standard_plus3, 1);

        potionLevelDictionary.Add(progressing_colorant1, -1);
        potionLevelDictionary.Add(progressing_colorant2, 0);
        potionLevelDictionary.Add(progressing_colorant3, 1);
        potionLevelDictionary.Add(progressing_temperature1, -1);
        potionLevelDictionary.Add(progressing_temperature2, 0);
        potionLevelDictionary.Add(progressing_temperature3, 1);
        potionLevelDictionary.Add(progressing_aether1, -1);
        potionLevelDictionary.Add(progressing_aether2, 0);
        potionLevelDictionary.Add(progressing_aether3, 1);
        potionLevelDictionary.Add(progressing_supernova1, -1);
        potionLevelDictionary.Add(progressing_supernova2, 0);
        potionLevelDictionary.Add(progressing_supernova3, 1);
        potionLevelDictionary.Add(progressing_voids1, -1);
        potionLevelDictionary.Add(progressing_voids2, 0);
        potionLevelDictionary.Add(progressing_voids3, 1);
        potionLevelDictionary.Add(progressing_standard_plus1, -1);
        potionLevelDictionary.Add(progressing_standard_plus2, 0);
        potionLevelDictionary.Add(progressing_standard_plus3, 1);

        potionLevelDictionary.Add(colorant_plus1, -1);
        potionLevelDictionary.Add(colorant_plus2, 0);
        potionLevelDictionary.Add(colorant_plus3, 1);
        potionLevelDictionary.Add(temperature_plus1, -1);
        potionLevelDictionary.Add(temperature_plus2, 0);
        potionLevelDictionary.Add(temperature_plus3, 1);
        potionLevelDictionary.Add(aether_plus1, -1);
        potionLevelDictionary.Add(aether_plus2, 0);
        potionLevelDictionary.Add(aether_plus3, 1);
        potionLevelDictionary.Add(supernova_plus1, -1);
        potionLevelDictionary.Add(supernova_plus2, 0);
        potionLevelDictionary.Add(supernova_plus3, 1);
        potionLevelDictionary.Add(voids_plus1, -1);
        potionLevelDictionary.Add(voids_plus2, 0);
        potionLevelDictionary.Add(voids_plus3, 1);
        potionLevelDictionary.Add(temperature_colorant1, -1);
        potionLevelDictionary.Add(temperature_colorant2, 0);
        potionLevelDictionary.Add(temperature_colorant3, 1);
        potionLevelDictionary.Add(temperature_aether1, -1);
        potionLevelDictionary.Add(temperature_aether2, 0);
        potionLevelDictionary.Add(temperature_aether3, 1);
        potionLevelDictionary.Add(colorant_aether1, -1);
        potionLevelDictionary.Add(colorant_aether2, 0);
        potionLevelDictionary.Add(colorant_aether3, 1);
        potionLevelDictionary.Add(temperature_supernova1, -1);
        potionLevelDictionary.Add(temperature_supernova2, 0);
        potionLevelDictionary.Add(temperature_supernova3, 1);
        potionLevelDictionary.Add(colorant_supernova1, -1);
        potionLevelDictionary.Add(colorant_supernova2, 0);
        potionLevelDictionary.Add(colorant_supernova3, 1);
        potionLevelDictionary.Add(aether_voids1, -1);
        potionLevelDictionary.Add(aether_voids2, 0);
        potionLevelDictionary.Add(aether_voids3, 1);
        potionLevelDictionary.Add(colorants_voids1, -1);
        potionLevelDictionary.Add(colorants_voids2, 0);
        potionLevelDictionary.Add(colorants_voids3, 1);
        potionLevelDictionary.Add(aether_supernova1, -1);
        potionLevelDictionary.Add(aether_supernova2, 0);
        potionLevelDictionary.Add(aether_supernova3, 1);
        potionLevelDictionary.Add(voids_supernova1, -1);
        potionLevelDictionary.Add(voids_supernova2, 0);
        potionLevelDictionary.Add(voids_supernova3, 1);
        potionLevelDictionary.Add(temperature_voids1, -1);
        potionLevelDictionary.Add(temperature_voids2, 0);
        potionLevelDictionary.Add(temperature_voids3, 1);

        standardBasicPotionList = new List<GameObject>();

        standardBasicPotionList.Add(basic1);
        standardBasicPotionList.Add(basic2);
        standardBasicPotionList.Add(basic3);
        standardBasicPotionList.Add(basic4);
        standardBasicPotionList.Add(basic5);
        standardBasicPotionList.Add(basic6);
        standardBasicPotionList.Add(basic7);
        standardBasicPotionList.Add(basic8);
        standardBasicPotionList.Add(basic9);

        LevelUPpingList = new List<Recipe>();
    }

    private void Start()
    {
        setupPotionLists();
    }

    static void setupPotionLists()
    {
        switch (ConstellationManager.CONSTELLATION1)
        {
            case ConstellationManager.CONSTELLATION.NONE:
                constellationList1 = new List<GameObject>();
                constellationLearn1 = new List<GameObject>();
                constellationListCombo = new List<GameObject>();
                constellationLearnCombo = new List<GameObject>();
                switch (ConstellationManager.CONSTELLATION2)
                {
                    case ConstellationManager.CONSTELLATION.NONE:
                        constellationList2 = new List<GameObject>();
                        constellationLearn2 = new List<GameObject>();
                        break;
                    case ConstellationManager.CONSTELLATION.COLORANT:
                        constellationList2 = colorantList;
                        constellationLearn2 = colorantLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.TEMPERATURE:
                        constellationList2 = temperatureList;
                        constellationLearn2 = temperatureLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.AETHER:
                        constellationList2 = aetherList;
                        constellationLearn2 = aetherLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationLearn2 = supernovaLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationLearn2 = voidsLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationLearn2 = lensingLearning;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.COLORANT:
                constellationList1 = colorantList;
                constellationLearn1 = colorantLearning;
                switch (ConstellationManager.CONSTELLATION2)
                {
                    case ConstellationManager.CONSTELLATION.TEMPERATURE:
                        constellationList2 = temperatureList;
                        constellationListCombo = colorantsTemperatureList;
                        constellationLearn2 = temperatureLearning;
                        constellationLearnCombo = colorantsTemperatureLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.AETHER:
                        constellationList2 = aetherList;
                        constellationListCombo = colorantsAetherList;
                        constellationLearn2 = aetherLearning;
                        constellationLearnCombo = colorantsAetherLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = colorantsSupernovaList;
                        constellationLearn2 = supernovaLearning;
                        constellationLearnCombo = colorantsSupernovaLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = colorantsVoidsList;
                        constellationLearn2 = voidsLearning;
                        constellationLearnCombo = colorantsVoidsLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = colorantsPlusList;
                        constellationLearn2 = lensingLearning;
                        constellationLearnCombo = colorantsPlusLearning;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                constellationList1 = temperatureList;
                constellationLearn1 = temperatureLearning;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.AETHER:
                        constellationList2 = aetherList;
                        constellationListCombo = temperatureAetherList;
                        constellationLearn2 = aetherLearning;
                        constellationLearnCombo = temperatureAetherLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = temperatureSupernovaList;
                        constellationLearn2 = supernovaLearning;
                        constellationLearnCombo = temperatureSupernovaLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = temperatureVoidsList;
                        constellationLearn2 = voidsLearning;
                        constellationLearnCombo = temperatureVoidsLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = temperaturePlusList;
                        constellationLearn2 = lensingLearning;
                        constellationLearnCombo = temperaturePlusLearning;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.AETHER:
                constellationList1 = aetherList;
                constellationLearn1 = aetherLearning;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = aetherSupenovaList;
                        constellationLearn2 = supernovaLearning;
                        constellationLearnCombo = aetherSupenovaLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = aetherVoidsList;
                        constellationLearn2 = voidsLearning;
                        constellationLearnCombo = aetherVoidsLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = aetherPlusList;
                        constellationLearn2 = lensingLearning;
                        constellationLearnCombo = aetherPlusLearning;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                constellationList1 = supernovaList;
                constellationLearn1 = supernovaLearning;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = supernovaVoidsList;
                        constellationLearn2 = voidsLearning;
                        constellationLearnCombo = supernovaVoidsLearning;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = supernovaPlusList;
                        constellationLearn2 = lensingLearning;
                        constellationLearnCombo = supernovaPlusLearning;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.VOIDS:
                constellationList1 = voidsList;
                constellationLearn1 = voidsLearning;
                switch (ConstellationManager.CONSTELLATION2)
                {
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = voidsPlusList;
                        constellationLearn2 = lensingLearning;
                        constellationLearnCombo = voidsPlusLearning;
                        break;
                }
                break;
        }
    }
    public static Dictionary<GameObject, int> potionLevelDictionary
    {
        get; private set;
    }

    static List<GameObject> standardBasicPotionList;

    public static int AvaliableToLevelUPCount
    {

        get
        {
            int n = 0;
            foreach (KeyValuePair<GameObject, int> pair in potionLevelDictionary)
            {
                if (pair.Value == 1 || pair.Value == 2) n++;
            }
            return n;
        }
    }

    public static int AvaliableToLearnCount
    {

        get
        {
            int n = 0;
            foreach (KeyValuePair<GameObject, int> pair in potionLevelDictionary)
            {
                if (pair.Value == 0) n++;
            }
            return n;
        }
    }

    public static List<Recipe> LevelUPpingList
    {
        get; private set;
    }

    static List<GameObject> colorantList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_colorant1] > 0) list.Add(staticInstance.progressing_colorant1);
            list.Add(staticInstance.colorant1);
            if (potionLevelDictionary[staticInstance.progressing_colorant2] > 0) list.Add(staticInstance.progressing_colorant2);
            list.Add(staticInstance.colorant2);
            if (potionLevelDictionary[staticInstance.progressing_colorant3] > 0) list.Add(staticInstance.progressing_colorant3);
            list.Add(staticInstance.colorant3);

            return list;
        }
    }

    static List<GameObject> colorantLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_colorant1] == 0) list.Add(staticInstance.progressing_colorant1);
            if (potionLevelDictionary[staticInstance.progressing_colorant2] == 0) list.Add(staticInstance.progressing_colorant2);
            if (potionLevelDictionary[staticInstance.progressing_colorant3] == 0) list.Add(staticInstance.progressing_colorant3);
            return list;
        }
    }
    static List<GameObject> temperatureList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_temperature1] > 0) list.Add(staticInstance.progressing_temperature1);
            list.Add(staticInstance.temperature1);
            if (potionLevelDictionary[staticInstance.progressing_temperature2] > 0) list.Add(staticInstance.progressing_temperature2);
            list.Add(staticInstance.temperature2);
            if (potionLevelDictionary[staticInstance.progressing_temperature3] > 0) list.Add(staticInstance.progressing_temperature3);
            list.Add(staticInstance.temperature3);

            return list;
        }
    }

    static List<GameObject> temperatureLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_temperature1] == 0) list.Add(staticInstance.progressing_temperature1);
            if (potionLevelDictionary[staticInstance.progressing_temperature2] == 0) list.Add(staticInstance.progressing_temperature2);
            if (potionLevelDictionary[staticInstance.progressing_temperature3] == 0) list.Add(staticInstance.progressing_temperature3);
            return list;
        }
    }

    static List<GameObject> aetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_aether1] > 0) list.Add(staticInstance.progressing_aether1);
            list.Add(staticInstance.aether1);
            if (potionLevelDictionary[staticInstance.progressing_aether2] > 0) list.Add(staticInstance.progressing_aether2);
            list.Add(staticInstance.aether2);
            if (potionLevelDictionary[staticInstance.progressing_aether3] > 0) list.Add(staticInstance.progressing_aether3);
            list.Add(staticInstance.aether3);

            return list;
        }
    }

    static List<GameObject> aetherLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_aether1] == 0) list.Add(staticInstance.progressing_aether1);
            if (potionLevelDictionary[staticInstance.progressing_aether2] == 0) list.Add(staticInstance.progressing_aether2);
            if (potionLevelDictionary[staticInstance.progressing_aether3] == 0) list.Add(staticInstance.progressing_aether3);
            return list;
        }
    }
    static List<GameObject> supernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_supernova1] > 0) list.Add(staticInstance.progressing_supernova1);
            list.Add(staticInstance.supernova1);
            if (potionLevelDictionary[staticInstance.progressing_supernova2] > 0) list.Add(staticInstance.progressing_supernova2);
            list.Add(staticInstance.supernova2);
            if (potionLevelDictionary[staticInstance.progressing_supernova3] > 0) list.Add(staticInstance.progressing_supernova3);
            list.Add(staticInstance.supernova3);

            return list;
        }
    }

    static List<GameObject> supernovaLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_supernova1] == 0) list.Add(staticInstance.progressing_supernova1);
            if (potionLevelDictionary[staticInstance.progressing_supernova2] == 0) list.Add(staticInstance.progressing_supernova2);
            if (potionLevelDictionary[staticInstance.progressing_supernova3] == 0) list.Add(staticInstance.progressing_supernova3);
            return list;
        }
    }
    static List<GameObject> voidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_voids1] > 0) list.Add(staticInstance.progressing_voids1);
            list.Add(staticInstance.voids1);
            if (potionLevelDictionary[staticInstance.progressing_voids2] > 0) list.Add(staticInstance.progressing_voids2);
            list.Add(staticInstance.voids2);
            if (potionLevelDictionary[staticInstance.progressing_voids3] > 0) list.Add(staticInstance.progressing_voids3);
            list.Add(staticInstance.voids3);

            return list;
        }
    }

    static List<GameObject> voidsLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_voids1] == 0) list.Add(staticInstance.progressing_voids1);
            if (potionLevelDictionary[staticInstance.progressing_voids2] == 0) list.Add(staticInstance.progressing_voids2);
            if (potionLevelDictionary[staticInstance.progressing_voids3] == 0) list.Add(staticInstance.progressing_voids3);
            return list;
        }
    }
    static List<GameObject> lensingList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_standard_plus1] > 0) list.Add(staticInstance.progressing_standard_plus1);
            list.Add(staticInstance.standard_plus1);
            if (potionLevelDictionary[staticInstance.progressing_standard_plus2] > 0) list.Add(staticInstance.progressing_standard_plus2);
            list.Add(staticInstance.standard_plus2);
            if (potionLevelDictionary[staticInstance.progressing_standard_plus3] > 0) list.Add(staticInstance.progressing_standard_plus3);
            list.Add(staticInstance.standard_plus3);

            return list;
        }
    }

    static List<GameObject> lensingLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.progressing_standard_plus1] == 0) list.Add(staticInstance.progressing_standard_plus1);
            if (potionLevelDictionary[staticInstance.progressing_standard_plus2] == 0) list.Add(staticInstance.progressing_standard_plus2);
            if (potionLevelDictionary[staticInstance.progressing_standard_plus3] == 0) list.Add(staticInstance.progressing_standard_plus3);
            return list;
        }
    }

    static List<GameObject> colorantsTemperatureList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_colorant1] > 0) list.Add(staticInstance.temperature_colorant1);
            if (potionLevelDictionary[staticInstance.temperature_colorant2] > 0) list.Add(staticInstance.temperature_colorant2);
            if (potionLevelDictionary[staticInstance.temperature_colorant3] > 0) list.Add(staticInstance.temperature_colorant3);

            return list;
        }
    }

    static List<GameObject> colorantsTemperatureLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_colorant1] == 0) list.Add(staticInstance.temperature_colorant1);
            if (potionLevelDictionary[staticInstance.temperature_colorant2] == 0) list.Add(staticInstance.temperature_colorant2);
            if (potionLevelDictionary[staticInstance.temperature_colorant3] == 0) list.Add(staticInstance.temperature_colorant3);
            return list;
        }
    }

    static List<GameObject> colorantsAetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_aether1] > 0) list.Add(staticInstance.colorant_aether1);
            if (potionLevelDictionary[staticInstance.colorant_aether2] > 0) list.Add(staticInstance.colorant_aether2);
            if (potionLevelDictionary[staticInstance.colorant_aether3] > 0) list.Add(staticInstance.colorant_aether3);

            return list;
        }
    }

    static List<GameObject> colorantsAetherLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_aether1] == 0) list.Add(staticInstance.colorant_aether1);
            if (potionLevelDictionary[staticInstance.colorant_aether2] == 0) list.Add(staticInstance.colorant_aether2);
            if (potionLevelDictionary[staticInstance.colorant_aether3] == 0) list.Add(staticInstance.colorant_aether3);
            return list;
        }
    }

    static List<GameObject> colorantsSupernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_supernova1] > 0) list.Add(staticInstance.colorant_supernova1);
            if (potionLevelDictionary[staticInstance.colorant_supernova2] > 0) list.Add(staticInstance.colorant_supernova2);
            if (potionLevelDictionary[staticInstance.colorant_supernova3] > 0) list.Add(staticInstance.colorant_supernova3);

            return list;
        }
    }

    static List<GameObject> colorantsSupernovaLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_supernova1] == 0) list.Add(staticInstance.colorant_supernova1);
            if (potionLevelDictionary[staticInstance.colorant_supernova2] == 0) list.Add(staticInstance.colorant_supernova2);
            if (potionLevelDictionary[staticInstance.colorant_supernova3] == 0) list.Add(staticInstance.colorant_supernova3);
            return list;
        }
    }

    static List<GameObject> colorantsVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorants_voids1] > 0) list.Add(staticInstance.colorants_voids1);
            if (potionLevelDictionary[staticInstance.colorants_voids2] > 0) list.Add(staticInstance.colorants_voids2);
            if (potionLevelDictionary[staticInstance.colorants_voids3] > 0) list.Add(staticInstance.colorants_voids3);

            return list;
        }
    }

    static List<GameObject> colorantsVoidsLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorants_voids1] == 0) list.Add(staticInstance.colorants_voids1);
            if (potionLevelDictionary[staticInstance.colorants_voids2] == 0) list.Add(staticInstance.colorants_voids2);
            if (potionLevelDictionary[staticInstance.colorants_voids3] == 0) list.Add(staticInstance.colorants_voids3);
            return list;
        }
    }
    static List<GameObject> colorantsPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_plus1] > 0) list.Add(staticInstance.colorant_plus1);
            if (potionLevelDictionary[staticInstance.colorant_plus2] > 0) list.Add(staticInstance.colorant_plus2);
            if (potionLevelDictionary[staticInstance.colorant_plus3] > 0) list.Add(staticInstance.colorant_plus3);

            return list;
        }
    }

    static List<GameObject> colorantsPlusLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.colorant_plus1] == 0) list.Add(staticInstance.colorant_plus1);
            if (potionLevelDictionary[staticInstance.colorant_plus2] == 0) list.Add(staticInstance.colorant_plus2);
            if (potionLevelDictionary[staticInstance.colorant_plus3] == 0) list.Add(staticInstance.colorant_plus3);
            return list;
        }
    }

    static List<GameObject> temperatureAetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_aether1] > 0) list.Add(staticInstance.temperature_aether1);
            if (potionLevelDictionary[staticInstance.temperature_aether2] > 0) list.Add(staticInstance.temperature_aether2);
            if (potionLevelDictionary[staticInstance.temperature_aether3] > 0) list.Add(staticInstance.temperature_aether3);

            return list;
        }
    }

    static List<GameObject> temperatureAetherLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_aether1] == 0) list.Add(staticInstance.temperature_aether1);
            if (potionLevelDictionary[staticInstance.temperature_aether2] == 0) list.Add(staticInstance.temperature_aether2);
            if (potionLevelDictionary[staticInstance.temperature_aether3] == 0) list.Add(staticInstance.temperature_aether3);
            return list;
        }
    }
    static List<GameObject> temperatureSupernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_supernova1] > 0) list.Add(staticInstance.temperature_supernova1);
            if (potionLevelDictionary[staticInstance.temperature_supernova2] > 0) list.Add(staticInstance.temperature_supernova2);
            if (potionLevelDictionary[staticInstance.temperature_supernova3] > 0) list.Add(staticInstance.temperature_supernova3);

            return list;
        }
    }
    static List<GameObject> temperatureSupernovaLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_supernova1] == 0) list.Add(staticInstance.temperature_supernova1);
            if (potionLevelDictionary[staticInstance.temperature_supernova2] == 0) list.Add(staticInstance.temperature_supernova2);
            if (potionLevelDictionary[staticInstance.temperature_supernova3] == 0) list.Add(staticInstance.temperature_supernova3);
            return list;
        }
    }
    static List<GameObject> temperatureVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_voids1] > 0) list.Add(staticInstance.temperature_voids1);
            if (potionLevelDictionary[staticInstance.temperature_voids2] > 0) list.Add(staticInstance.temperature_voids2);
            if (potionLevelDictionary[staticInstance.temperature_voids3] > 0) list.Add(staticInstance.temperature_voids3);

            return list;
        }
    }

    static List<GameObject> temperatureVoidsLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_voids1] == 0) list.Add(staticInstance.temperature_voids1);
            if (potionLevelDictionary[staticInstance.temperature_voids2] == 0) list.Add(staticInstance.temperature_voids2);
            if (potionLevelDictionary[staticInstance.temperature_voids3] == 0) list.Add(staticInstance.temperature_voids3);
            return list;
        }
    }
    static List<GameObject> temperaturePlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_plus1] > 0) list.Add(staticInstance.temperature_plus1);
            if (potionLevelDictionary[staticInstance.temperature_plus2] > 0) list.Add(staticInstance.temperature_plus2);
            if (potionLevelDictionary[staticInstance.temperature_plus3] > 0) list.Add(staticInstance.temperature_plus3);

            return list;
        }
    }

    static List<GameObject> temperaturePlusLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.temperature_plus1] == 0) list.Add(staticInstance.temperature_plus1);
            if (potionLevelDictionary[staticInstance.temperature_plus2] == 0) list.Add(staticInstance.temperature_plus2);
            if (potionLevelDictionary[staticInstance.temperature_plus3] == 0) list.Add(staticInstance.temperature_plus3);
            return list;
        }
    }

    static List<GameObject> aetherSupenovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_supernova1] > 0) list.Add(staticInstance.aether_supernova1);
            if (potionLevelDictionary[staticInstance.aether_supernova2] > 0) list.Add(staticInstance.aether_supernova2);
            if (potionLevelDictionary[staticInstance.aether_supernova3] > 0) list.Add(staticInstance.aether_supernova3);

            return list;
        }
    }

    static List<GameObject> aetherSupenovaLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_supernova1] == 0) list.Add(staticInstance.aether_supernova1);
            if (potionLevelDictionary[staticInstance.aether_supernova2] == 0) list.Add(staticInstance.aether_supernova2);
            if (potionLevelDictionary[staticInstance.aether_supernova3] == 0) list.Add(staticInstance.aether_supernova3);
            return list;
        }
    }
    static List<GameObject> aetherVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_voids1] > 0) list.Add(staticInstance.aether_voids1);
            if (potionLevelDictionary[staticInstance.aether_voids2] > 0) list.Add(staticInstance.aether_voids2);
            if (potionLevelDictionary[staticInstance.aether_voids3] > 0) list.Add(staticInstance.aether_voids3);

            return list;
        }
    }

    static List<GameObject> aetherVoidsLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_voids1] == 0) list.Add(staticInstance.aether_voids1);
            if (potionLevelDictionary[staticInstance.aether_voids2] == 0) list.Add(staticInstance.aether_voids2);
            if (potionLevelDictionary[staticInstance.aether_voids3] == 0) list.Add(staticInstance.aether_voids3);
            return list;
        }
    }
    static List<GameObject> aetherPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_plus1] > 0) list.Add(staticInstance.aether_plus1);
            if (potionLevelDictionary[staticInstance.aether_plus2] > 0) list.Add(staticInstance.aether_plus2);
            if (potionLevelDictionary[staticInstance.aether_plus3] > 0) list.Add(staticInstance.aether_plus3);

            return list;
        }
    }

    static List<GameObject> aetherPlusLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.aether_plus1] == 0) list.Add(staticInstance.aether_plus1);
            if (potionLevelDictionary[staticInstance.aether_plus2] == 0) list.Add(staticInstance.aether_plus2);
            if (potionLevelDictionary[staticInstance.aether_plus3] == 0) list.Add(staticInstance.aether_plus3);
            return list;
        }
    }

    static List<GameObject> supernovaVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.voids_supernova1] > 0) list.Add(staticInstance.voids_supernova1);
            if (potionLevelDictionary[staticInstance.voids_supernova2] > 0) list.Add(staticInstance.voids_supernova2);
            if (potionLevelDictionary[staticInstance.voids_supernova3] > 0) list.Add(staticInstance.voids_supernova3);

            return list;
        }
    }
    static List<GameObject> supernovaVoidsLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.voids_supernova1] == 0) list.Add(staticInstance.voids_supernova1);
            if (potionLevelDictionary[staticInstance.voids_supernova2] == 0) list.Add(staticInstance.voids_supernova2);
            if (potionLevelDictionary[staticInstance.voids_supernova3] == 0) list.Add(staticInstance.voids_supernova3);
            return list;
        }
    }

    static List<GameObject> supernovaPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.supernova_plus1] > 0) list.Add(staticInstance.supernova_plus1);
            if (potionLevelDictionary[staticInstance.supernova_plus2] > 0) list.Add(staticInstance.supernova_plus2);
            if (potionLevelDictionary[staticInstance.supernova_plus3] > 0) list.Add(staticInstance.supernova_plus3);

            return list;
        }
    }

    static List<GameObject> supernovaPlusLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.supernova_plus1] == 0) list.Add(staticInstance.supernova_plus1);
            if (potionLevelDictionary[staticInstance.supernova_plus2] == 0) list.Add(staticInstance.supernova_plus2);
            if (potionLevelDictionary[staticInstance.supernova_plus3] == 0) list.Add(staticInstance.supernova_plus3);
            return list;
        }
    }

    static List<GameObject> voidsPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.voids_plus1] > 0) list.Add(staticInstance.voids_plus1);
            if (potionLevelDictionary[staticInstance.voids_plus2] > 0) list.Add(staticInstance.voids_plus2);
            if (potionLevelDictionary[staticInstance.voids_plus3] > 0) list.Add(staticInstance.voids_plus3);

            return list;
        }
    }

    static List<GameObject> voidsPlusLearning
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[staticInstance.voids_plus1] == 0) list.Add(staticInstance.voids_plus1);
            if (potionLevelDictionary[staticInstance.voids_plus2] == 0) list.Add(staticInstance.voids_plus2);
            if (potionLevelDictionary[staticInstance.voids_plus3] == 0) list.Add(staticInstance.voids_plus3);
            return list;
        }
    }

    public static List<Recipe> ConstellationList1
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject potion in constellationList1) list.Add(potion.gameObject.GetComponent<Recipe>());
            return list;
        }
    }

    public static List<Recipe> ConstellationList2
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject potion in constellationList2) list.Add(potion.gameObject.GetComponent<Recipe>());
            return list;
        }
    }

    public static List<Recipe> ConstellationListCombo
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject potion in constellationListCombo) list.Add(potion.gameObject.GetComponent<Recipe>());
            return list;
        }
    }

    public static List<Recipe> BasicList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject potion in standardBasicPotionList) list.Add(potion.gameObject.GetComponent<Recipe>());
            return list;
        }
    }

    public static List<Recipe> Const1ToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in ConstellationList1)
            {
                if (recipe.GetComponent<PotionRecipe>().currentLevel == 1 || recipe.GetComponent<PotionRecipe>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static List<Recipe> Const2ToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in ConstellationList2)
            {
                if (recipe.GetComponent<PotionRecipe>().currentLevel == 1 || recipe.GetComponent<PotionRecipe>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static List<Recipe> ConstComboToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in ConstellationListCombo)
            {
                if (recipe.GetComponent<PotionRecipe>().currentLevel == 1 || recipe.GetComponent<PotionRecipe>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static List<Recipe> BasicToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in BasicList)
            {
                if (recipe.GetComponent<PotionRecipe>().currentLevel == 1 || recipe.GetComponent<PotionRecipe>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static List<Recipe> Const1ToLearn
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject obj in constellationLearn1)
            {
                list.Add(obj.GetComponent<Recipe>());
            }
            return list;
        }
    }

    public static List<Recipe> Const2ToLearn
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject obj in constellationLearn2)
            {
                list.Add(obj.GetComponent<Recipe>());
            }
            return list;
        }
    }

    public static List<Recipe> ConstComboToLearn
    {
        get
        {
            setupPotionLists();
            List<Recipe> list = new List<Recipe>();
            foreach (GameObject obj in constellationLearnCombo)
            {
                list.Add(obj.GetComponent<Recipe>());
            }
            return list;
        }
    }

}
