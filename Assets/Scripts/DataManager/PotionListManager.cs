using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionListManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] potions;

    private void Awake()
    {
        staticInstance = this;
        potionsAvaliability = new Dictionary<Potion, Potion.AVALIABILITY>();
        LevelUPpingList = new List<Recipe>();

        foreach (GameObject potion in staticInstance.potions)
        {
            Potion recipe = potion.GetComponent<Potion>();
            if (recipe.Potion_Type == Potion.POTION_TYPE.BASICALLY_KNOWN) potionsAvaliability.Add(recipe, Potion.AVALIABILITY.LEVEL1);
            else potionsAvaliability.Add(recipe, Potion.AVALIABILITY.KNOWN_BLUEPRINT);
        }
    }

    private void Start()
    {
        
        if (SaveLoadManager.saveObject != null)
        {
            Dictionary<string, string> dict = SaveLoadManager.saveObject.potionsAvaliabilitySave;
            List<Potion> keys = new List<Potion>(potionsAvaliability.Keys);
            foreach(Potion key in keys)
            {
                Enum.TryParse(dict[key.FileName], out Potion.AVALIABILITY avaliability);
                potionsAvaliability[key] = avaliability;
            }
        }
    }

    public static List<Recipe> testFill
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
            {
                if (pair.Value == Potion.AVALIABILITY.LEVEL1) list.Add(pair.Key);
            }
            return list;
        }
    }

    public static List<Recipe> ConstellationList1
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if(ConstellationManager.CONSTELLATION1 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value != Potion.AVALIABILITY.UNKNOWN_BLUEPRINT &&
                        pair.Value != Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION1 && pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE))
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }

    public static List<Recipe> ConstellationList2
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if (ConstellationManager.CONSTELLATION2 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value != Potion.AVALIABILITY.UNKNOWN_BLUEPRINT &&
                        pair.Value != Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION2 && pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE))
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }
    public static List<Recipe> ConstellationListCombo
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if (ConstellationManager.CONSTELLATION1 != ConstellationManager.CONSTELLATION.NONE && ConstellationManager.CONSTELLATION2 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value != Potion.AVALIABILITY.UNKNOWN_BLUEPRINT &&
                        pair.Value != Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        pair.Key.Constellation1 == ConstellationManager.CONSTELLATION1 &&
                        pair.Key.Constellation2 == ConstellationManager.CONSTELLATION2)
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }

    public static List<Recipe> Const1ToLearn
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if (ConstellationManager.CONSTELLATION1 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value == Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION1 && pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE))
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }
    public static List<Recipe> Const2ToLearn
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if (ConstellationManager.CONSTELLATION2 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value == Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION2 && pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE))
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }
    public static List<Recipe> ConstComboToLearn
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            if (ConstellationManager.CONSTELLATION1 != ConstellationManager.CONSTELLATION.NONE && ConstellationManager.CONSTELLATION2 != ConstellationManager.CONSTELLATION.NONE)
            {
                foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
                {
                    if (pair.Value == Potion.AVALIABILITY.KNOWN_BLUEPRINT &&
                        pair.Key.Constellation1 == ConstellationManager.CONSTELLATION1 &&
                        pair.Key.Constellation2 == ConstellationManager.CONSTELLATION2)
                    {
                        list.Add(pair.Key);
                    }
                }
            }
            return list;
        }
    }

    public static List<Potion> PotionsToLearn
    {
        get
        {
            List<Potion> list = new List<Potion>();
            foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
            {
                if (pair.Value == Potion.AVALIABILITY.KNOWN_BLUEPRINT) list.Add(pair.Key);
            }
            return list;
        }
    }

    public static List<Recipe> BasicPotionList
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
            {
                if (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION.NONE &&
                    pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE)
                {
                    list.Add(pair.Key);
                }
            }
            return list;
        }
    }

    static PotionListManager staticInstance;

   

    public static Dictionary<Potion, Potion.AVALIABILITY> potionsAvaliability
    {
        get; private set;
    }

    //public static researchPotion()

    public static int AvaliableToLevelUPCount
    {

        get
        {
            int n = 0;
            foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
            {
                if (pair.Value == Potion.AVALIABILITY.LEVEL1 || pair.Value == Potion.AVALIABILITY.LEVEL1) n++;
            }
            return n;
        }
    }

    public static int AvaliableToLearnCount
    {

        get
        {
            int n = 0;
            foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in potionsAvaliability)
            {
                if (pair.Value == Potion.AVALIABILITY.KNOWN_BLUEPRINT) n++;
            }
            return n;
        }
    }

    public static List<Recipe> LevelUPpingList
    {
        get; private set;
    }

    //драфтер для левел апа доступен только ночью, а драфтер для зелья только днём
    //в финальной версии игры конфликт невозможен, но он возможен в демо-версии
    //эта функция - костыль для демо-версии
    public static void LevelUPpingListRefresh()
    {
        LevelUPpingList = new List<Recipe>();
    }


    public static List<Recipe> Const1ToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in ConstellationList1)
            {
                if (recipe.GetComponent<Potion>().currentLevel == 1 || recipe.GetComponent<Potion>().currentLevel == 2) list.Add(recipe);
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
                if (recipe.GetComponent<Potion>().currentLevel == 1 || recipe.GetComponent<Potion>().currentLevel == 2) list.Add(recipe);
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
                if (recipe.GetComponent<Potion>().currentLevel == 1 || recipe.GetComponent<Potion>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static List<Recipe> BasicToLevelUP
    {
        get
        {
            List<Recipe> list = new List<Recipe>();
            foreach (Recipe recipe in BasicPotionList)
            {
                if (recipe.GetComponent<Potion>().currentLevel == 1 || recipe.GetComponent<Potion>().currentLevel == 2) list.Add(recipe);
            }
            return list;
        }
    }

    public static void researchPotion(Potion potion)
    {
        potionsAvaliability[potion] = Potion.AVALIABILITY.LEVEL1;
    }

    public static void levelUpPotion(Potion potion)
    {
        if (potionsAvaliability[potion] == Potion.AVALIABILITY.LEVEL1) potionsAvaliability[potion] = Potion.AVALIABILITY.LEVEL2;
        else if (potionsAvaliability[potion] == Potion.AVALIABILITY.LEVEL2) potionsAvaliability[potion] = Potion.AVALIABILITY.LEVEL3;
    }
}
