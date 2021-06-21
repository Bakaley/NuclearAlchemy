using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListOfIngredeints : MonoBehaviour
{
    //true = unlocked, false = locked
    protected Dictionary<GameObject, bool> unlockedIngredients;
    // Start is called before the first frame update


    protected virtual void Awake()
    {
        unlockedIngredients = new Dictionary<GameObject, bool>();
    }
    protected virtual void Start()
    {

    }

    public List<GameObject> unlockedIngredientList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            foreach (KeyValuePair<GameObject, bool> entry in unlockedIngredients)
            {
                if (entry.Value) list.Add(entry.Key);
            }
            return list;
        }
    }
}
