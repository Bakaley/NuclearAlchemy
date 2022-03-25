using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterBookmark : Bookmark
{
    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    Sprite activeSprite;

    Image bookmarkImage = null;
    TextMeshProUGUI text = null;

    private void Awake()
    {
        bookmarkImage = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }


    public override void load()
    {
        GetComponentInParent<InfoSection>().changeChapter(this);
        setActive();
    }

    public override void setActive()
    {
        bookmarkImage.sprite = activeSprite;
        text.color = activeColor;
        bookmarkImage.GetComponent<Transform>().SetSiblingIndex(transform.parent.GetComponentsInChildren<SectionBookmark>().Length - 1);
    }

    public override void setInactive()
    {
        if (bookmarkImage) bookmarkImage.sprite = inactiveSprite;
        if (text) text.color = inactiveColor;
    }
}
