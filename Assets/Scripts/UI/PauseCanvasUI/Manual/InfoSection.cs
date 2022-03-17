using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSection : MonoBehaviour
{
    [SerializeField]
    Bookmark defaultParagraphBookmark;

    [SerializeField]
    GameObject leftPagePlace;
    [SerializeField]
    GameObject rightPagePlace;

    [SerializeField]
    Bookmark activeParagraph;
    [SerializeField]
    Bookmark activeBookmark;

    GameObject currentLeftPage;
    GameObject currentRightPage;

    void changeLeftPage (Bookmark bookmark)
    {
        if(activeBookmark != bookmark)
        {
            if (currentLeftPage)
            {
                activeBookmark.setInactive();
                Destroy(currentLeftPage);
            }
            currentLeftPage = Instantiate(bookmark.SectionOrPage, leftPagePlace.transform);
            activeBookmark = bookmark;
        }

    }
    public void changeRightPage(Bookmark bookmark)
    {
        if(activeParagraph != bookmark)
        {
            if (currentLeftPage)
            {
                activeParagraph.setInactive();
                Destroy(currentRightPage);
            }
            currentRightPage = Instantiate(bookmark.SectionOrPage, rightPagePlace.transform);
            activeParagraph = bookmark;
        }
    }

    private void OnEnable()
    {
        changeLeftPage(defaultParagraphBookmark);
    }

}
