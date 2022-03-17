using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    [SerializeField]
    GameObject sectionPlace;
    [SerializeField]
    GameObject activeBookmarkPlace;
    [SerializeField]
    GameObject inactiveBookmarks;
    [SerializeField]
    Bookmark defaultBookmark;

    Bookmark activeBookmark;
    GameObject activeSection;

    public void changeSection (Bookmark bookmark)
    {
        if(activeBookmark)
        {
            activeBookmark.transform.SetParent(inactiveBookmarks.transform);
            Destroy(activeSection.gameObject);
        }
        activeBookmark = bookmark;
        activeBookmark.gameObject.transform.SetParent(activeBookmarkPlace.transform);
        activeSection = Instantiate(activeBookmark.SectionOrPage, sectionPlace.transform);
    }
    
    private void OnEnable()
    {
        if (activeBookmark == null) changeSection(defaultBookmark);
        else changeSection(activeBookmark);
    }
}
