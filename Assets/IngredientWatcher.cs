using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientWatcher : MonoBehaviour
{
    [SerializeField]
    GameObject stringName, icon;
    Ingredient current;
    int number;
    [SerializeField]
    GameObject[] ingredients;
    List<Ingredient> list;

    [SerializeField]
    GameObject[] icons;

    private void Awake()
    {
        list = new List<Ingredient>();
        foreach (GameObject gameObject in ingredients)
        {
            if (gameObject.GetComponent<Ingredient>()) list.Add(gameObject.GetComponent<Ingredient>());
        }
        Debug.Log(list.Count);

        number = 0;
        current = list[number];
        refillBookPage();
    }

    private void Start()
    {
        int n = 0;
        foreach (GameObject gameObjectIcon in icons)
        {
            if (n >= list.Count) break;
            gameObjectIcon.GetComponentsInChildren<SpriteRenderer>()[1].sprite = list[n].IngredientIcon;
            gameObjectIcon.GetComponentsInChildren<SpriteRenderer>()[1].color = list[n].IngredientIconColor;
            n++;
        }
    }

    public void toLeft()
    {
        if(number-1 >= 0)
        {
            number--;
            current = list[number];
            refillBookPage();
        }
    }

    public void toRight()
    {
        if (number + 1 < list.Count)
        {
            number++;
            current = list[number];
            refillBookPage();
        }
    }

    void refillBookPage()
    {
        icon.GetComponent<SpriteRenderer>().sprite = current.IngredientIcon;
        icon.GetComponent<SpriteRenderer>().color = current.IngredientIconColor;
        stringName.GetComponent<TextMeshPro>().text = current.IngredientName;
    }
}
