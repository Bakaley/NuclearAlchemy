using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionBookmark : Bookmark
{
    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    Sprite activeSprite;

    Image bookmarkImage = null;
    Image symbol = null;

    private void Awake()
    {
        foreach (Image im in GetComponentsInChildren<Image>())
        {
            if (im.gameObject == gameObject) bookmarkImage = im;
            else symbol = im;
        }
    }

    public override void load()
    {
        GetComponentInParent<Manual>().changeSection(this);
        setActive();
    }

    public override void setActive()
    {
        bookmarkImage.sprite = activeSprite;
        symbol.color = activeColor;
        Debug.Log(bookmarkImage.GetComponent<Transform>());
        Debug.Log(transform.parent.GetComponentsInChildren<SectionBookmark>().Length);
        //активная закладка должна быть нарисована позже бекграунда книги, т.е. поверх него
        bookmarkImage.GetComponent<Transform>().SetSiblingIndex(transform.parent.GetComponentsInChildren<Transform>().Length - 1);
    }

    public override void setInactive()
    {
        if (bookmarkImage) bookmarkImage.sprite = inactiveSprite;
        if (symbol) symbol.color = inactiveColor;
        //неактивная закладка должна быть нарисована перед бекграундом книги
        bookmarkImage.GetComponent<Transform>().SetSiblingIndex(0);
    }
}
