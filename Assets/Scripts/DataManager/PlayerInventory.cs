using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory StaticInstance
    {
        get;
        private set;
    }
    private void Awake()
    {
        StaticInstance = this;
        potionsInventory = new Dictionary<Potion, int>();
        ingredientsInventory = new Dictionary<Ingredient, int>();
    }

    private void Start()
    {
        if (SaveLoadManager.saveObject != null)
        {
            Dictionary<string, int> savedPotions = SaveLoadManager.saveObject.potionsSave;
            List<Potion> keys = new List<Potion>(PotionListManager.potionsAvaliability.Keys);
            foreach (KeyValuePair<string, int> pair in savedPotions)
            {
                foreach (Potion potionRecipe in keys)
                {
                    if (potionRecipe.FileName == pair.Key)
                    {
                        potionsInventory.Add(potionRecipe, pair.Value);
                    }
                }
            }

            Dictionary<string, int> savedIngrs = SaveLoadManager.saveObject.potionsSave;
            List<Ingredient> keysIngr = new List<Ingredient>(IngredientListManager.ingredientsAvaliability.Keys);
            foreach (KeyValuePair<string, int> pair in savedIngrs)
            {
                foreach (Ingredient ingr in keysIngr)
                {
                    if (ingr.IngredientFileName == pair.Key) ingredientsInventory.Add(ingr, pair.Value);
                }
            }
        }

        foreach (Potion potion in PotionListManager.testFill)
        {
            //заполнить инвентарь случайными зельями
            //addPotion(potion, UnityEngine.Random.Range(1, 7));
        }
        
    }
    public Dictionary<Potion, int> potionsInventory
    {
        get; private set;
    }

    public Dictionary<Ingredient, int> ingredientsInventory
    {
        get; private set;
    }

    public void addPotion (Potion potionRecipe, int count)
    {
        if (potionsInventory.ContainsKey(potionRecipe)) potionsInventory[potionRecipe] += count;
        else potionsInventory.Add(potionRecipe, count);
        SaveLoadManager.Save();
        OnPotionsCountChange?.Invoke(this, EventArgs.Empty);
    }

    public void remove1Potion (Potion potionRecipe)
    {
        potionsInventory[potionRecipe]--;
        if (potionsInventory[potionRecipe] == 0) potionsInventory.Remove(potionRecipe);
        SaveLoadManager.Save();
        OnPotionsCountChange?.Invoke(this, EventArgs.Empty);

    }

    public void addIngredient(Ingredient ingredient, int count)
    {
        if (ingredientsInventory.ContainsKey(ingredient)) ingredientsInventory[ingredient] += count;
        else ingredientsInventory.Add(ingredient, count);
        SaveLoadManager.Save();
    }

    public void remove1Ingredient(Ingredient ingredient)
    {
        ingredientsInventory[ingredient]--;
        if (ingredientsInventory[ingredient] == 0) ingredientsInventory.Remove(ingredient);
        SaveLoadManager.Save();
    }

    public int PotionsAmount
    {
        get
        {
            int n = 0;
            foreach (KeyValuePair<Potion, int> pair in potionsInventory)
            {
                n += pair.Value;
            }
            return n;
        }
    }

    public event EventHandler OnPotionsCountChange;
}
