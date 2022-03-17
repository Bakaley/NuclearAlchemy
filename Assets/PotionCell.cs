using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PotionCell : MonoBehaviour
{
    [SerializeField]
    Sprite emblemBasic;
    [SerializeField]
    Sprite emblemColorant;
    [SerializeField]
    Sprite emblemTemperature;
    [SerializeField]
    Sprite emblemAether;
    [SerializeField]
    Sprite emblemSupernova;
    [SerializeField]
    Sprite emblemVoid;
    [SerializeField]
    Sprite emblemLensing;

    [SerializeField]
    GameObject emblemPlace1;
    [SerializeField]
    GameObject emblemPlace2;
    [SerializeField]
    GameObject potionIcon;
    [SerializeField]
    GameObject potionName;
    [SerializeField]
    GameObject potionCount;
    [SerializeField]
    GameObject lunarCount;
    [SerializeField]
    GameObject potionCountCircle;

    public void fillPotionCell(Potion potion, int count)
    {
        potionName.GetComponent<TextMeshProUGUI>().text = potion.RecipeName;
        if (count == 1) potionCountCircle.SetActive(false);
        else potionCount.GetComponent<TextMeshProUGUI>().text = "x" + count;
        potionIcon.GetComponent<Image>().sprite = potion.Icon;

        emblemPlace1.GetComponent<Image>().sprite = getEmblemByConst(potion.Constellation1);

        if (potion.Constellation2 == ConstellationManager.CONSTELLATION.NONE)
        {
            emblemPlace1.transform.localPosition = new Vector3(
                (emblemPlace1.transform.localPosition.x + emblemPlace2.transform.localPosition.x) / 2,
                emblemPlace1.transform.localPosition.y,
                emblemPlace1.transform.localPosition.z);
            emblemPlace2.SetActive(false);
        }
        else
        {
            emblemPlace2.GetComponent<Image>().sprite = getEmblemByConst(potion.Constellation2);
        }

        lunarCount.GetComponent<TextMeshProUGUI>().text = potion.Price + "";
    }

    public void fillBlueprintCell(Recipe recipe)
    {
        Potion potion = recipe.GetComponent<Potion>();
        potionName.GetComponent<TextMeshProUGUI>().text = potion.RecipeName;
        potionIcon.GetComponent<Image>().sprite = potion.Icon;
        emblemPlace1.GetComponent<Image>().sprite = getEmblemByConst(potion.Constellation1);
        if (potion.Constellation2 == ConstellationManager.CONSTELLATION.NONE)
        {
            emblemPlace1.transform.localPosition = new Vector3(
                (emblemPlace1.transform.localPosition.x + emblemPlace2.transform.localPosition.x) / 2,
                emblemPlace1.transform.localPosition.y,
                emblemPlace1.transform.localPosition.z);
            emblemPlace2.SetActive(false);
        }
        else
        {
            emblemPlace2.GetComponent<Image>().sprite = getEmblemByConst(potion.Constellation2);
        }
    }

    Sprite getEmblemByConst(ConstellationManager.CONSTELLATION constellation)
    {
        switch (constellation)
        {
            case ConstellationManager.CONSTELLATION.TEMPERATURE:
                return emblemTemperature;
            case ConstellationManager.CONSTELLATION.COLORANT:
                return emblemColorant;
            case ConstellationManager.CONSTELLATION.AETHER:
                return emblemAether;
            case ConstellationManager.CONSTELLATION.SUPERNOVA:
                return emblemSupernova;
            case ConstellationManager.CONSTELLATION.VOIDS:
                return emblemVoid;
            case ConstellationManager.CONSTELLATION.LENSING:
                return emblemLensing;
        }
        return emblemBasic;
    }

}
