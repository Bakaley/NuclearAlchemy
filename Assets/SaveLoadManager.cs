using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class SaveLoadManager : MonoBehaviour
{
    static readonly string fileSaveName = "/save.json";

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + fileSaveName))
        {
            string str = File.ReadAllText(Application.persistentDataPath + fileSaveName);
            saveObject = JsonConvert.DeserializeObject<SaveLoadFile>(str);
        }
    }

    public static SaveLoadFile saveObject
    {
        get; private set;
    }

    public static void Save()
    {
        SaveLoadFile saveLoadFile = new SaveLoadFile();

        foreach (KeyValuePair<Potion, Potion.AVALIABILITY> pair in PotionListManager.potionsAvaliability)
        {
            saveLoadFile.potionsAvaliabilitySave.Add(pair.Key.FileName, pair.Value + "");
        }

        foreach (KeyValuePair<Ingredient, Ingredient.AVALIABILITY> pair in IngredientListManager.ingredientsAvaliability)
        {
            saveLoadFile.ingredientsAvaliabilitySave.Add(pair.Key.IngredientFileName, pair.Value + "");
        }

        foreach (KeyValuePair<Potion, int> pair in PlayerInventory.potionsInventory)
        {
            saveLoadFile.potionsSave.Add(pair.Key.FileName, pair.Value);
        }

        foreach (KeyValuePair<Ingredient, int> pair in PlayerInventory.ingredientsInventory)
        {
            saveLoadFile.ingredientsSave.Add(pair.Key.IngredientFileName, pair.Value);
        }
        string str = JsonConvert.SerializeObject(saveLoadFile);
        File.WriteAllText(Application.persistentDataPath + fileSaveName, str);
    }

    public class SaveLoadFile
    {
        //сериализуемые поля должны быть public
        public Dictionary<string, string> potionsAvaliabilitySave
        {
            get; private set;
        }
        public Dictionary<string, string> ingredientsAvaliabilitySave
        {
            get; private set;
        }
        public Dictionary<string, int> potionsSave
        {
            get; private set;
        }
        public Dictionary<string, int> ingredientsSave
        {
            get; private set;
        }
        public SaveLoadFile()
        {
            potionsAvaliabilitySave = new Dictionary<string, string>();
            ingredientsAvaliabilitySave = new Dictionary<string, string>();
            potionsSave = new Dictionary<string, int>();
            ingredientsSave = new Dictionary<string, int>();
        }
    }
}
