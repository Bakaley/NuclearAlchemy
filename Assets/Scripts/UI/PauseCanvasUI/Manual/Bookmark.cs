using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Bookmark : MonoBehaviour
{
    [SerializeField]
    protected Color32 activeColor = new Color32(219, 164, 65, 255);

    [SerializeField]
    protected Color32 inactiveColor = new Color32(37, 175, 255, 255);


    [SerializeField]
    GameObject sectionOrPage;

    public GameObject SectionOrPage
    {
        get { return sectionOrPage; }
    }

    abstract public void load();
    abstract public void setActive();
    abstract public void setInactive();
}
