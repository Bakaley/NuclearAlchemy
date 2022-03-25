using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    [SerializeField]
    GameObject sectionPlace;
    [SerializeField]
    SectionBookmark defaultTopBookmark;

    SectionBookmark activeSectionBookmark = null;
    GameObject currentSection;

    public void changeSection (SectionBookmark sectionBookmark)
    {
        if(sectionBookmark != activeSectionBookmark)
        {
            if(activeSectionBookmark) activeSectionBookmark.setInactive();
            activeSectionBookmark = sectionBookmark;
            activeSectionBookmark.setActive();
            if(currentSection) Destroy(currentSection);
            currentSection = Instantiate(activeSectionBookmark.SectionOrPage, sectionPlace.transform);
        }
    }
    
    private void OnEnable()
    {
        if (activeSectionBookmark == null) changeSection(defaultTopBookmark);
    }
}
