using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSection : MonoBehaviour
{

    [SerializeField]
    GameObject plankSampler;
    [SerializeField]
    Sprite uncommonPlank;
    [SerializeField]
    Sprite rareplank;
    [SerializeField]
    Sprite epicPlank;
    [SerializeField]
    Sprite legendaryPlank;

    static readonly int FULL_LIST_MAX_SIZE = 17;
    static readonly int HALF_LIST_MAX_SIZE = 8;

    [SerializeField]
    GameObject standardFullList1;
    [SerializeField]
    GameObject standardHalfList1;
    [SerializeField]
    GameObject standardHalfList2;
    [SerializeField]
    GameObject standardFullList2;

    [SerializeField]
    GameObject const1FullList;
    [SerializeField]
    GameObject const1HalfList;
    [SerializeField]
    GameObject const2FullList;
    [SerializeField]
    GameObject const2HalfList;

    [SerializeField]
    GameObject infoblockStandard;
    [SerializeField]
    GameObject infoblockConstellations;

    private void OnEnable()
    {

        List<Ingredient> standardList = new List<Ingredient>(IngredientListManager.getIngredients(Ingredient.AFFILIATION.RARE_ASPECT, Ingredient.AVALIABILITY.LEARNED_WITH_NO_BLUEPRINT));
        standardList.AddRange(IngredientListManager.getIngredients(Ingredient.AFFILIATION.RARE_ASPECT, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
        standardList.Sort((first, second) => {
        var comaprison = first.Rarity.CompareTo(second.Rarity);
        if (comaprison != 0)
            return -comaprison;
        else return first.IngredientName.CompareTo(second.IngredientName);
        });
        infoblockStandard.GetComponentInChildren<TextMeshProUGUI>().text = infoblockStandard.GetComponentInChildren<TextMeshProUGUI>().text.Replace("XXX", standardList.Count + "");
        while (standardList.Count < 50) standardList.Add(null);

        List<Ingredient> constlist1 = new List<Ingredient>(IngredientListManager.getIngredients(ConstellationManager.CONSTELLATION1, Ingredient.AVALIABILITY.LEARNED_WITH_NO_BLUEPRINT));
        constlist1.AddRange(IngredientListManager.getIngredients(ConstellationManager.CONSTELLATION1, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
        constlist1.Sort((first, second) => {
            var comaprison = first.Rarity.CompareTo(second.Rarity);
            if (comaprison != 0)
                return -comaprison;
            else return first.IngredientName.CompareTo(second.IngredientName);
        });
        infoblockConstellations.GetComponentInChildren<TextMeshProUGUI>().text = infoblockConstellations.GetComponentInChildren<TextMeshProUGUI>().text.Replace("XXX", constlist1.Count + "");
        while (constlist1.Count < 25) constlist1.Add(null);

        List<Ingredient> constlist2 = new List<Ingredient>(IngredientListManager.getIngredients(ConstellationManager.CONSTELLATION2, Ingredient.AVALIABILITY.LEARNED_WITH_NO_BLUEPRINT));
        constlist2.AddRange(IngredientListManager.getIngredients(ConstellationManager.CONSTELLATION2, Ingredient.AVALIABILITY.LEARNED_BLUEPRINT));
        constlist2.Sort((first, second) => {
            var comaprison = first.Rarity.CompareTo(second.Rarity);
            if (comaprison != 0)
                return -comaprison;
            else return first.IngredientName.CompareTo(second.IngredientName);
        });
        infoblockConstellations.GetComponentInChildren<TextMeshProUGUI>().text = infoblockConstellations.GetComponentInChildren<TextMeshProUGUI>().text.Replace("YYY", constlist2.Count + "");
        while (constlist2.Count < 25) constlist2.Add(null);


        for (int i = 0; i < standardList.Count; i++)
        {
            GameObject plank;
            if (i < FULL_LIST_MAX_SIZE) {
                plank = Instantiate(plankSampler, standardFullList1.transform);
            }
            else if (i >= FULL_LIST_MAX_SIZE && i < FULL_LIST_MAX_SIZE + HALF_LIST_MAX_SIZE)
            {
                plank = Instantiate(plankSampler, standardHalfList1.transform);
            }
            else if (i >= FULL_LIST_MAX_SIZE + HALF_LIST_MAX_SIZE && i < FULL_LIST_MAX_SIZE + HALF_LIST_MAX_SIZE + HALF_LIST_MAX_SIZE)
            {
                plank = Instantiate(plankSampler, standardHalfList2.transform);
            }
            else
            {
                plank = Instantiate(plankSampler, standardFullList2.transform);
            }
            if(standardList[i] != null)
            {
                plank.GetComponent<Image>().sprite = getPlankSprite(standardList[i].Rarity);
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) plank.GetComponentInChildren<TextMeshProUGUI>().text = standardList[i].IngredientName + "";
                else plank.GetComponent<TextMeshProUGUI>().text = standardList[i].EnglishName + "";
                plank.GetComponent<IngredientPlank>().ingredient = standardList[i];
            }
        }

        for (int i = 0; i < constlist1.Count; i++)
        {
            GameObject plank;
            if (i < FULL_LIST_MAX_SIZE)
            {
                plank = Instantiate(plankSampler, const1FullList.transform);
            }
            else
            {
                plank = Instantiate(plankSampler, const1HalfList.transform);
            }
            if (constlist1[i] != null)
            {
                plank.GetComponent<Image>().sprite = getPlankSprite(constlist1[i].Rarity);
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) plank.GetComponentInChildren<TextMeshProUGUI>().text = constlist1[i].IngredientName + "";
                else plank.GetComponent<TextMeshProUGUI>().text = constlist1[i].EnglishName + "";
                plank.GetComponent<IngredientPlank>().ingredient = constlist1[i];
            }
        }

        for (int i = 0; i < constlist2.Count; i++)
        {
            GameObject plank;
            if (i < FULL_LIST_MAX_SIZE)
            {
                plank = Instantiate(plankSampler, const2FullList.transform);
            }
            else
            {
                plank = Instantiate(plankSampler, const2HalfList.transform);
            }
            if (constlist2[i] != null)
            {
                plank.GetComponent<Image>().sprite = getPlankSprite(constlist2[i].Rarity);
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) plank.GetComponentInChildren<TextMeshProUGUI>().text = constlist2[i].IngredientName + "";
                else plank.GetComponent<TextMeshProUGUI>().text = constlist2[i].EnglishName + "";
                plank.GetComponent<IngredientPlank>().ingredient = constlist2[i];
            }
        }
    }

    Sprite getPlankSprite (Ingredient.RARITY rarity)
    {
        switch (rarity)
        {
            case Ingredient.RARITY.RARE: return rareplank;
            case Ingredient.RARITY.EPIC: return epicPlank;
            case Ingredient.RARITY.LEGENDARY: return legendaryPlank;
        }
        return uncommonPlank;
    }

    //`List<Ingredient> sort(List<Ingredient> list)

}
