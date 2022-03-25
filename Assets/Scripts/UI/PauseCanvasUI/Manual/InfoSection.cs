using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSection : MonoBehaviour
{
    [SerializeField]
    ChapterBookmark defaultRightBookmark;

    [SerializeField]
    GameObject leftPagePlace;
    [SerializeField]
    GameObject rightPagePlace;

    ParagraphBookmark activeParagraphBookmark = null;
    ChapterBookmark activeChapterBookmark = null;

    GameObject currentLeftPage;
    GameObject currentRightPage;

    public void changeChapter (ChapterBookmark chapterBookmark)
    {
        if (activeChapterBookmark != chapterBookmark)
        {
            if (activeChapterBookmark) activeChapterBookmark.setInactive();
            activeChapterBookmark = chapterBookmark;
            activeChapterBookmark.setActive();
            if (currentLeftPage) Destroy(currentLeftPage);
            currentLeftPage = Instantiate(activeChapterBookmark.SectionOrPage, leftPagePlace.transform);
        }
    }
    public void changePage(ParagraphBookmark paragraphBookmark)
    {
        if (activeParagraphBookmark != paragraphBookmark)
        {
            if (activeParagraphBookmark) activeParagraphBookmark.setInactive();
            activeParagraphBookmark = paragraphBookmark;
            activeParagraphBookmark.setActive();
            if (currentRightPage) Destroy(currentRightPage);
            currentRightPage = Instantiate(activeParagraphBookmark.SectionOrPage, rightPagePlace.transform);
        }
    }

    private void OnEnable()
    {
        if (activeChapterBookmark == null) changeChapter(defaultRightBookmark);

    }

}
