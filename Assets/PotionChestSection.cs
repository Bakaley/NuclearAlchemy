using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChestSection : MonoBehaviour
{
    [SerializeField]
    GameObject potionCellSampler, potionBlueprintSampler;
    [SerializeField]
    GameObject leftContainer, rightContainer;

    private void OnEnable()
    {
        foreach (KeyValuePair<Potion, int> pair in PlayerInventory.potionsInventory)
        {
            GameObject potionCell = Instantiate(potionCellSampler, leftContainer.transform);
            potionCell.GetComponent<PotionCell>().fillPotionCell(pair.Key, pair.Value);
        }

        foreach (Recipe recipe in PotionListManager.PotionsToLearn)
        {
            GameObject potionCell = Instantiate(potionBlueprintSampler, rightContainer.transform);
            potionCell.GetComponent<PotionCell>().fillBlueprintCell(recipe);
        }
    }
}
