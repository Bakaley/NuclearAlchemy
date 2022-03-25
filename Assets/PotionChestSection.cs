using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionChestSection : MonoBehaviour
{
    [SerializeField]
    GameObject potionCellSampler, potionBlueprintSampler;
    [SerializeField]
    GameObject leftContainer, rightContainer;
    [SerializeField]
    GameObject titleString;

    private void Awake()
    {
        if (filterDictionary == null)
        {
            filterDictionary = new Dictionary<FILTER_MODE, ConstellationManager.CONSTELLATION>();
            filterDictionary.Add(FILTER_MODE.COLORANTS, ConstellationManager.CONSTELLATION.COLORANT);
            filterDictionary.Add(FILTER_MODE.TEMPERATURE, ConstellationManager.CONSTELLATION.TEMPERATURE);
            filterDictionary.Add(FILTER_MODE.AETHER, ConstellationManager.CONSTELLATION.AETHER);
            filterDictionary.Add(FILTER_MODE.SUPERNOVA, ConstellationManager.CONSTELLATION.SUPERNOVA);
            filterDictionary.Add(FILTER_MODE.VOIDS, ConstellationManager.CONSTELLATION.VOIDS);
            filterDictionary.Add(FILTER_MODE.LENSING, ConstellationManager.CONSTELLATION.LENSING);
        }
    }
    private void OnEnable()
    {        
        PlayerInventory.StaticInstance.OnPotionsCountChange += inventoryChangeHandler;
        TextRefresh();
    }

    void inventoryChangeHandler (object inventory, EventArgs e)
    {
        TextRefresh();
    }

    void TextRefresh()
    {
        string str = titleString.GetComponent<TextMeshProUGUI>().text;
        string firstPart = str.Substring(0, str.IndexOf('['));
        titleString.GetComponent<TextMeshProUGUI>().text = firstPart + "[" + PlayerInventory.StaticInstance.PotionsAmount + "/MAX]";
    }

    private void OnDisable()
    {
        PlayerInventory.StaticInstance.OnPotionsCountChange -= inventoryChangeHandler;
    }

    public void Filter(FILTER_MODE filter)
    {
        containersRefresh(filter);
        OnFilterChanged?.Invoke(this, filter);
    }

    public enum FILTER_MODE
    {
        ALL,
        BASIC,
        COLORANTS,
        TEMPERATURE,
        AETHER,
        SUPERNOVA,
        VOIDS,
        LENSING
    }

    public event EventHandler<FILTER_MODE> OnFilterChanged;

    void containersRefresh (FILTER_MODE filter)
    {
        foreach (Transform t in leftContainer.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<PotionCell>()) Destroy(t.gameObject);
        }

        foreach (KeyValuePair<Potion, int> pair in PlayerInventory.StaticInstance.potionsInventory)
        {
            if (filter == FILTER_MODE.BASIC)
            {
                if (pair.Key.Constellation1 == ConstellationManager.CONSTELLATION.NONE &&
                    pair.Key.Constellation2 == ConstellationManager.CONSTELLATION.NONE)
                {
                    createPotionChestCell(pair.Key, pair.Value);
                }
            }
            else if (filter == FILTER_MODE.ALL) createPotionChestCell(pair.Key, pair.Value);
            else if (pair.Key.Constellation1 == filterDictionary[filter] ||
                    pair.Key.Constellation2 == filterDictionary[filter])
                    createPotionChestCell(pair.Key, pair.Value);
        }

        foreach (Transform t in rightContainer.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<PotionCell>()) Destroy(t.gameObject);
        }

        foreach (Potion potion in PotionListManager.PotionsToLearn)
        {
            if (filter == FILTER_MODE.BASIC)
            {
                if (potion.Constellation1 == ConstellationManager.CONSTELLATION.NONE &&
                    potion.Constellation2 == ConstellationManager.CONSTELLATION.NONE)
                {
                    createPotionBlueprintCell(potion);
                }
            }
            else if (filter == FILTER_MODE.ALL) createPotionBlueprintCell(potion);
            else if (potion.Constellation1 == filterDictionary[filter] ||
                    potion.Constellation2 == filterDictionary[filter])
                createPotionBlueprintCell(potion);
        }
    }

    static Dictionary<FILTER_MODE, ConstellationManager.CONSTELLATION> filterDictionary;

    void createPotionChestCell(Potion potion, int count)
    {
        GameObject potionCell = Instantiate(potionCellSampler, leftContainer.transform);
        potionCell.GetComponent<PotionCell>().fillPotionChestCell(potion, count);
    }

    void createPotionBlueprintCell(Potion potion)
    {
        GameObject potionCell = Instantiate(potionBlueprintSampler, rightContainer.transform);
        potionCell.GetComponent<PotionCell>().fillPotionBlueprintCell(potion);
    }
}
