using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    GameObject[] dissolvingArray = new GameObject[2];

    Dictionary<Ingredient.ESSENSE, GameObject> essenceIcons;
    Dictionary<Ingredient.ESSENSE, int> essenceScores;

    double dissolveTimer = 0;

    void Start()
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
        essenceIcons.Add(Ingredient.ESSENSE.Meat, MeatEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Crystall, CrystallEssence);
        essenceIcons.Add(Ingredient.ESSENSE.Lighting, LightingEssence);

        essenceScores.Add(Ingredient.ESSENSE.Water, 0);
        essenceScores.Add(Ingredient.ESSENSE.Fire, 0);
        essenceScores.Add(Ingredient.ESSENSE.Stone, 0);
        essenceScores.Add(Ingredient.ESSENSE.Air, 0);
        essenceScores.Add(Ingredient.ESSENSE.Mushroom, 0);
        essenceScores.Add(Ingredient.ESSENSE.Magic, 0);
        essenceScores.Add(Ingredient.ESSENSE.Plant, 0);
        essenceScores.Add(Ingredient.ESSENSE.Meat, 0);
        essenceScores.Add(Ingredient.ESSENSE.Crystall, 0);
        essenceScores.Add(Ingredient.ESSENSE.Lighting, 0);
    }

    void Update()
    {
        if (dissolveTimer >= 0)
        {
            dissolveTimer -= Time.deltaTime;
            foreach (GameObject essenceIcon in dissolvingArray)
                if(essenceIcon!= null)
                {
                    essenceIcon.GetComponent<SpriteRenderer>().material.SetFloat("Shading_Vector", essenceIcon.GetComponent<SpriteRenderer>().material.GetFloat("Shading_Vector") + Time.deltaTime*4);

                    if (essenceIcon.GetComponent<SpriteRenderer>().material.GetFloat("Shading_Vector") >= 1)
                    {
                        dissolveTimer = 0;
                        essenceIcon.GetComponent<SpriteRenderer>().material.SetFloat("Shading_Vector", 1);
                        
                    }
                }
        }
        //else gameObject.GetComponent<Material>().SetFloat("Shading_Vector", 1);
    }

    public void addEssence(Ingredient.ESSENSE essence)
    {
        essenceScores[essence]++;
        if(essenceScores[essence] == 1)
        {
            dissolvingArray[0] = essenceIcons[essence];
            dissolveTimer = .3;
            getNumberSpriteRenderer(essenceIcons[essence]).color = new Color32(154, 154, 154, 255);
            getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();
        }
        else if(essenceScores[essence] < 10)
        {
            Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + essenceScores[essence]) as Sprite;
            getNumberSpriteRenderer(essenceIcons[essence]).sprite = loadedNumber;
            getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();
        }
        else getNumberSpriteRenderer(essenceIcons[essence]).GetComponent<Animation>().Play();
    }

    public void addEssence(Ingredient.ESSENSE essence1, Ingredient.ESSENSE essence2)
    {
        essenceScores[essence1]++;
        if (essenceScores[essence1] == 1)
        {
            dissolvingArray[0] = essenceIcons[essence1];
            dissolveTimer = .3;
            getNumberSpriteRenderer(essenceIcons[essence1]).color = new Color32(154, 154, 154, 255);
            getNumberSpriteRenderer(essenceIcons[essence1]).GetComponent<Animation>().Play();
        }
        else if (essenceScores[essence1] < 10)
        {
            Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + essenceScores[essence1]) as Sprite;
            getNumberSpriteRenderer(essenceIcons[essence1]).sprite = loadedNumber;
            getNumberSpriteRenderer(essenceIcons[essence1]).GetComponent<Animation>().Play();
        }
        else getNumberSpriteRenderer(essenceIcons[essence1]).GetComponent<Animation>().Play();

        essenceScores[essence2]++;
        if (essenceScores[essence2] == 1)
        {
            dissolvingArray[1] = essenceIcons[essence2];
            dissolveTimer = .3;
            getNumberSpriteRenderer(essenceIcons[essence2]).color = new Color32(154, 154, 154, 255);
            getNumberSpriteRenderer(essenceIcons[essence2]).GetComponent<Animation>().Play();
        }
        else if (essenceScores[essence2] < 10)
        {
            Sprite loadedNumber = Resources.Load<Sprite>("Numbers/numberIcon" + essenceScores[essence2]) as Sprite;
            getNumberSpriteRenderer(essenceIcons[essence2]).sprite = loadedNumber;
            getNumberSpriteRenderer(essenceIcons[essence2]).GetComponent<Animation>().Play();
        }
        else getNumberSpriteRenderer(essenceIcons[essence2]).GetComponent<Animation>().Play();
    }

    SpriteRenderer getNumberSpriteRenderer (GameObject icon)
    {
        foreach (Transform childTransform in icon.transform)
        {
            return childTransform.GetComponent<SpriteRenderer>();
        }
        return null;
    }
}