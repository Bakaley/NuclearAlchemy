using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceList : MonoBehaviour
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

    private void Awake()
    {
        essenceIcons = new Dictionary<Ingredient.ESSENSE, GameObject>();

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
    }
}
