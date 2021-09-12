using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssencePanel : MonoBehaviour
{
    [SerializeField]
    GameObject WaterEssence;
    [SerializeField]
    GameObject FireEssence;
    [SerializeField]
    GameObject StoneEssence;
    [SerializeField]
    GameObject AirEssence;
    [SerializeField]
    GameObject BoneEssence;
    [SerializeField]
    GameObject MagicEssence;
    [SerializeField]
    GameObject PlantEssence;
    [SerializeField]
    GameObject MeatEssence;
    [SerializeField]
    GameObject CrystallEssence;
    [SerializeField]
    GameObject LightingEssence;

    public static Dictionary<Ingredient.ESSENSE, GameObject> essenceIcons
    {
        get; private set;
    }
    public static Dictionary<Ingredient.ESSENSE, int> essenceScores
    {
        get; private set;
    }

    public static void clearEssences()
    {
        essenceScores[Ingredient.ESSENSE.Water] = 0;
        essenceScores[Ingredient.ESSENSE.Fire] = 0;
        essenceScores[Ingredient.ESSENSE.Stone] = 0;
        essenceScores[Ingredient.ESSENSE.Air] = 0;
        essenceScores[Ingredient.ESSENSE.Mushroom] = 0;
        essenceScores[Ingredient.ESSENSE.Plant] = 0;
        essenceScores[Ingredient.ESSENSE.Animal] = 0;
        essenceScores[Ingredient.ESSENSE.Crystall] = 0;
        essenceScores[Ingredient.ESSENSE.Lighting] = 0;

        foreach (KeyValuePair<Ingredient.ESSENSE, int> pair in essenceScores)
        {
            essenceIcons[pair.Key].GetComponent<IDissolving>().disappear();
        }
    }

    void Awake()
    {
        essenceIcons = new Dictionary<Ingredient.ESSENSE, GameObject>();
        essenceScores = new Dictionary<Ingredient.ESSENSE, int>();

        essenceIcons.Add(Ingredient.ESSENSE.Water, WaterEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Fire, FireEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Stone, StoneEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Air, AirEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Mushroom, BoneEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Magic, MagicEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Plant, PlantEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Animal, MeatEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Crystall, CrystallEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Lighting, LightingEssence);

        essenceScores.Add(Ingredient.ESSENSE.Water, 0);
        essenceScores.Add(Ingredient.ESSENSE.Fire, 0);
        essenceScores.Add(Ingredient.ESSENSE.Stone, 0);
        essenceScores.Add(Ingredient.ESSENSE.Air, 0);
        essenceScores.Add(Ingredient.ESSENSE.Mushroom, 0);
        essenceScores.Add(Ingredient.ESSENSE.Magic, 0);
        essenceScores.Add(Ingredient.ESSENSE.Plant, 0);
        essenceScores.Add(Ingredient.ESSENSE.Animal, 0);
        essenceScores.Add(Ingredient.ESSENSE.Crystall, 0);
        essenceScores.Add(Ingredient.ESSENSE.Lighting, 0);
    }

    void Update()
    {

    }

    public void addEssence(Ingredient.ESSENSE essence)
    {
        essenceScores[essence]++;
        if(essenceScores[essence] == 1)
        {
            essenceIcons[essence].GetComponent<IDissolving>().appear();

           /* getNumberSpriteRenderer(essenceIcons[essence]).color = new Color32(154, 154, 154, 255);
            getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();*/
        }
       /* else if(essenceScores[essence] < 10)
        {
            Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + essenceScores[essence]) as Sprite;
            getNumberSpriteRenderer(essenceIcons[essence]).sprite = loadedNumber;
            getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();
        }
        else getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();*/
    }

    /*public static Image getNumberSpriteRenderer (GameObject icon)
    {
        foreach (Transform childTransform in icon.transform)
        {
            return childTransform.GetComponent<Image>();
        }
        return null;
    }*/
}