using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionChestFilterBookmark : MonoBehaviour
{

    [SerializeField]
    PotionChestSection.FILTER_MODE mode;
    [SerializeField]
    Sprite activeForm, inactiveForm;
    [SerializeField]
    PotionChestSection parent;

    private void Start()
    {
        if (mode == PotionChestSection.FILTER_MODE.ALL) applyFilter();
        parent.OnFilterChanged += FilterChangeHadler;
    }

    public void applyFilter()
    {
        GetComponent<Image>().sprite = activeForm;
        GetComponent<Transform>().SetSiblingIndex(transform.parent.GetComponentsInChildren<PotionChestFilterBookmark>().Length-1);
        parent.Filter(mode);
    }

    void FilterChangeHadler(object sender, PotionChestSection.FILTER_MODE newMode)
    {
        if (newMode != mode)
        {
            GetComponent<Image>().sprite = inactiveForm;
        }
    }

}
