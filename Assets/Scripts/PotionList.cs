using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionList : MonoBehaviour
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

    public static Dictionary<GameObject, int> potionLevelDictionary;

    public static List<GameObject> standardBasicPotionList;

    List<GameObject> colorantList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_colorant1] == 0) list.Add(colorant1);
            else list.Add(progressing_colorant1);
            if (potionLevelDictionary[progressing_colorant2] == 0) list.Add(colorant2);
            else list.Add(progressing_colorant2);
            if (potionLevelDictionary[progressing_colorant3] == 0) list.Add(colorant3);
            else list.Add(progressing_colorant3);

            return list;
        }
    }

    List<GameObject> temperatureList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_temperature1] == 0) list.Add(temperature1);
            else list.Add(progressing_temperature1);
            if (potionLevelDictionary[progressing_temperature2] == 0) list.Add(temperature2);
            else list.Add(progressing_temperature2);
            if (potionLevelDictionary[progressing_temperature3] == 0) list.Add(temperature1);
            else list.Add(progressing_temperature3);

            return list;
        }
    }

    List<GameObject> aetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_aether1] == 0) list.Add(aether1);
            else list.Add(progressing_aether1);
            if (potionLevelDictionary[progressing_aether2] == 0) list.Add(aether2);
            else list.Add(progressing_aether2);
            if (potionLevelDictionary[progressing_aether3] == 0) list.Add(aether3);
            else list.Add(progressing_aether3);

            return list;
        }
    }
    List<GameObject> supernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_supernova1] == 0) list.Add(supernova1);
            else list.Add(progressing_supernova1);
            if (potionLevelDictionary[progressing_supernova2] == 0) list.Add(supernova2);
            else list.Add(progressing_supernova2);
            if (potionLevelDictionary[progressing_supernova3] == 0) list.Add(supernova3);
            else list.Add(progressing_supernova3);

            return list;
        }
    }
    List<GameObject> voidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_voids1] == 0) list.Add(voids1);
            else list.Add(progressing_voids1);
            if (potionLevelDictionary[progressing_voids2] == 0) list.Add(voids2);
            else list.Add(progressing_voids2);
            if (potionLevelDictionary[progressing_voids3] == 0) list.Add(voids3);
            else list.Add(progressing_voids3);

            return list;
        }
    }
    List<GameObject> lensingList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[progressing_colorant1] == 0) list.Add(colorant1);
            else list.Add(progressing_colorant1);
            if (potionLevelDictionary[progressing_colorant2] == 0) list.Add(colorant2);
            else list.Add(progressing_colorant2);
            if (potionLevelDictionary[progressing_colorant3] == 0) list.Add(colorant3);
            else list.Add(progressing_colorant3);

            return list;
        }
    }

    List<GameObject> colorantsTemperatureList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[temperature_colorant1] != 0) list.Add(temperature_colorant1);
            if (potionLevelDictionary[temperature_colorant2] != 0) list.Add(temperature_colorant2);
            if (potionLevelDictionary[temperature_colorant3] != 0) list.Add(temperature_colorant3);

            return list;
        }
    }

    List<GameObject> colorantsAetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[colorant_aether1] != 0) list.Add(colorant_aether1);
            if (potionLevelDictionary[colorant_aether2] != 0) list.Add(colorant_aether2);
            if (potionLevelDictionary[colorant_aether3] != 0) list.Add(colorant_aether3);

            return list;
        }
    }

    List<GameObject> colorantsSupernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[colorant_supernova1] != 0) list.Add(colorant_supernova1);
            if (potionLevelDictionary[colorant_supernova2] != 0) list.Add(colorant_supernova2);
            if (potionLevelDictionary[colorant_supernova3] != 0) list.Add(colorant_supernova3);

            return list;
        }
    }

    List<GameObject> colorantsVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[colorants_voids1] != 0) list.Add(colorants_voids1);
            if (potionLevelDictionary[colorants_voids2] != 0) list.Add(colorants_voids2);
            if (potionLevelDictionary[colorants_voids3] != 0) list.Add(colorants_voids3);

            return list;
        }
    }
    List<GameObject> colorantsPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[colorant_plus1] != 0) list.Add(colorant_plus1);
            if (potionLevelDictionary[colorant_plus2] != 0) list.Add(colorant_plus2);
            if (potionLevelDictionary[colorant_plus3] != 0) list.Add(colorant_plus3);

            return list;
        }
    }

    List<GameObject> temperatureAetherList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[temperature_aether1] != 0) list.Add(temperature_aether1);
            if (potionLevelDictionary[temperature_aether2] != 0) list.Add(temperature_aether2);
            if (potionLevelDictionary[temperature_aether3] != 0) list.Add(temperature_aether3);

            return list;
        }
    }
    List<GameObject> temperatureSupernovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[temperature_supernova1] != 0) list.Add(temperature_colorant1);
            if (potionLevelDictionary[temperature_supernova2] != 0) list.Add(temperature_supernova2);
            if (potionLevelDictionary[temperature_supernova3] != 0) list.Add(temperature_supernova3);

            return list;
        }
    }
    List<GameObject> temperatureVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[temperature_voids1] != 0) list.Add(temperature_voids1);
            if (potionLevelDictionary[temperature_voids2] != 0) list.Add(temperature_voids2);
            if (potionLevelDictionary[temperature_voids3] != 0) list.Add(temperature_voids3);

            return list;
        }
    }
    List<GameObject> temperaturePlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[temperature_plus1] != 0) list.Add(temperature_plus1);
            if (potionLevelDictionary[temperature_plus2] != 0) list.Add(temperature_plus2);
            if (potionLevelDictionary[temperature_plus3] != 0) list.Add(temperature_plus3);

            return list;
        }
    }

    List<GameObject> aetherSupenovaList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[aether_supernova1] != 0) list.Add(aether_supernova1);
            if (potionLevelDictionary[aether_supernova2] != 0) list.Add(aether_supernova2);
            if (potionLevelDictionary[aether_supernova3] != 0) list.Add(aether_supernova3);

            return list;
        }
    }
    List<GameObject> aetherVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[aether_voids1] != 0) list.Add(aether_voids1);
            if (potionLevelDictionary[aether_voids2] != 0) list.Add(aether_voids2);
            if (potionLevelDictionary[aether_voids3] != 0) list.Add(aether_voids3);

            return list;
        }
    }
    List<GameObject> aetherPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[aether_plus1] != 0) list.Add(aether_plus1);
            if (potionLevelDictionary[aether_plus2] != 0) list.Add(aether_plus2);
            if (potionLevelDictionary[aether_plus3] != 0) list.Add(aether_plus3);

            return list;
        }
    }

    List<GameObject> supenovaVoidsList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[voids_supernova1] != 0) list.Add(voids_supernova1);
            if (potionLevelDictionary[voids_supernova2] != 0) list.Add(voids_supernova2);
            if (potionLevelDictionary[voids_supernova3] != 0) list.Add(voids_supernova3);

            return list;
        }
    }
    List<GameObject> supenovaPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[supernova_plus1] != 0) list.Add(supernova_plus1);
            if (potionLevelDictionary[supernova_plus2] != 0) list.Add(supernova_plus2);
            if (potionLevelDictionary[supernova_plus3] != 0) list.Add(supernova_plus3);

            return list;
        }
    }

    List<GameObject> voidsPlusList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            if (potionLevelDictionary[voids_plus1] != 0) list.Add(voids_plus1);
            if (potionLevelDictionary[voids_plus2] != 0) list.Add(voids_plus2);
            if (potionLevelDictionary[voids_plus3] != 0) list.Add(voids_plus3);

            return list;
        }
    }

    //static PotionList staticPotionList;

    /*public static PotionList StaticInstance
    {
        get
        {
            return staticPotionList;
        }
    }*/


    public static List<GameObject> constellationList1
    {
        get; private set;
    }
    public static List<GameObject> constellationList2
    {
        get; private set;
    }
    public static List<GameObject> constellationListCombo
    {
        get; private set;
    }

    private void Awake()
    {
        //staticPotionList = this;

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

        potionLevelDictionary.Add(progressing_colorant1, 1);
        potionLevelDictionary.Add(progressing_colorant2, 0);
        potionLevelDictionary.Add(progressing_colorant3, 1);
        potionLevelDictionary.Add(progressing_temperature1, 0);
        potionLevelDictionary.Add(progressing_temperature2, 1);
        potionLevelDictionary.Add(progressing_temperature3, 0);
        potionLevelDictionary.Add(progressing_aether1, 0);
        potionLevelDictionary.Add(progressing_aether2, 1);
        potionLevelDictionary.Add(progressing_aether3, 0);
        potionLevelDictionary.Add(progressing_supernova1, 0);
        potionLevelDictionary.Add(progressing_supernova2, 1);
        potionLevelDictionary.Add(progressing_supernova3, 0);
        potionLevelDictionary.Add(progressing_voids1, 1);
        potionLevelDictionary.Add(progressing_voids2, 0);
        potionLevelDictionary.Add(progressing_voids3, 1);
        potionLevelDictionary.Add(progressing_standard_plus1, 1);
        potionLevelDictionary.Add(progressing_standard_plus2, 0);
        potionLevelDictionary.Add(progressing_standard_plus3, 1);

        potionLevelDictionary.Add(colorant_plus1, 1);
        potionLevelDictionary.Add(colorant_plus2, 0);
        potionLevelDictionary.Add(colorant_plus3, 1);
        potionLevelDictionary.Add(temperature_plus1, 0);
        potionLevelDictionary.Add(temperature_plus2, 1);
        potionLevelDictionary.Add(temperature_plus3, 0);
        potionLevelDictionary.Add(aether_plus1, 0);
        potionLevelDictionary.Add(aether_plus2, 1);
        potionLevelDictionary.Add(aether_plus3, 0);
        potionLevelDictionary.Add(supernova_plus1, 1);
        potionLevelDictionary.Add(supernova_plus2, 0);
        potionLevelDictionary.Add(supernova_plus3, 1);
        potionLevelDictionary.Add(voids_plus1, 1);
        potionLevelDictionary.Add(voids_plus2, 0);
        potionLevelDictionary.Add(voids_plus3, 1);
        potionLevelDictionary.Add(temperature_colorant1, 1);
        potionLevelDictionary.Add(temperature_colorant2, 0);
        potionLevelDictionary.Add(temperature_colorant3, 1);
        potionLevelDictionary.Add(temperature_aether1, 1);
        potionLevelDictionary.Add(temperature_aether2, 1);
        potionLevelDictionary.Add(temperature_aether3, 0);
        potionLevelDictionary.Add(colorant_aether1, 1);
        potionLevelDictionary.Add(colorant_aether2, 0);
        potionLevelDictionary.Add(colorant_aether3, 0);
        potionLevelDictionary.Add(temperature_supernova1, 0);
        potionLevelDictionary.Add(temperature_supernova2, 1);
        potionLevelDictionary.Add(temperature_supernova3, 0);
        potionLevelDictionary.Add(colorant_supernova1, 0);
        potionLevelDictionary.Add(colorant_supernova2, 1);
        potionLevelDictionary.Add(colorant_supernova3, 0);
        potionLevelDictionary.Add(aether_voids1, 1);
        potionLevelDictionary.Add(aether_voids2, 0);
        potionLevelDictionary.Add(aether_voids3, 0);
        potionLevelDictionary.Add(colorants_voids1, 0);
        potionLevelDictionary.Add(colorants_voids2, 1);
        potionLevelDictionary.Add(colorants_voids3, 0);
        potionLevelDictionary.Add(aether_supernova1, 1);
        potionLevelDictionary.Add(aether_supernova2, 0);
        potionLevelDictionary.Add(aether_supernova3, 1);
        potionLevelDictionary.Add(voids_supernova1, 0);
        potionLevelDictionary.Add(voids_supernova2, 1);
        potionLevelDictionary.Add(voids_supernova3, 0);
        potionLevelDictionary.Add(temperature_voids1, 0);
        potionLevelDictionary.Add(temperature_voids2, 1);
        potionLevelDictionary.Add(temperature_voids3, 0);
        
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
    }
    
    private void Start()
    {
        setupPotionLists();

    }

    public void setupPotionLists()
    {
        switch (ConstellationManager.CONSTELLATION1)
        {
            case ConstellationManager.CONSTELLATION.COLORANT:
                constellationList1 = colorantList;
                switch (ConstellationManager.CONSTELLATION2)
                {
                    case ConstellationManager.CONSTELLATION.TEMPERATURE:
                        constellationList2 = temperatureList;
                        constellationListCombo = colorantsTemperatureList;
                        break;
                    case ConstellationManager.CONSTELLATION.AETHER:
                        constellationList2 = aetherList;
                        constellationListCombo = colorantsAetherList;
                        break;
                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = colorantsSupernovaList;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = colorantsVoidsList;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = colorantsPlusList;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                constellationList1 = temperatureList;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.AETHER:
                        constellationList2 = aetherList;
                        constellationListCombo = temperatureAetherList;
                        break;
                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = temperatureSupernovaList;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = temperatureVoidsList;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = temperaturePlusList;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.AETHER:
                constellationList1 = aetherList;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.SUPERNOVA:
                        constellationList2 = supernovaList;
                        constellationListCombo = aetherSupenovaList;
                        break;
                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = aetherVoidsList;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = aetherPlusList;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                constellationList1 = supernovaList;
                switch (ConstellationManager.CONSTELLATION2)
                {

                    case ConstellationManager.CONSTELLATION.VOIDS:
                        constellationList2 = voidsList;
                        constellationListCombo = supenovaVoidsList;
                        break;
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = supenovaPlusList;
                        break;
                }
                break;

            case ConstellationManager.CONSTELLATION.VOIDS:
                constellationList1 = voidsList;
                switch (ConstellationManager.CONSTELLATION2)
                {
                    case ConstellationManager.CONSTELLATION.LENSING:
                        constellationList2 = lensingList;
                        constellationListCombo = voidsPlusList;
                        break;
                }
                break;
        }
    }
}
