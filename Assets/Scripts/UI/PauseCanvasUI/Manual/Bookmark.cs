using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bookmark : MonoBehaviour
{
    [SerializeField]
    Sprite inactiveSprire;
    [SerializeField]
    Sprite activeSprite;

    Color32 inactiveTextColor = new Color32(149, 146, 140, 255);
    Color32 activeTextColor = new Color32(209, 154, 52, 255);

    public enum BOOKMARK_TYPE
    {
        SECTION_BOOKMARK,
        PAGE_BOOKMARK,
        PARAGRAPH_BOOKMARK
    }

    [SerializeField]
    BOOKMARK_TYPE type;
    [SerializeField]
    GameObject sectionOrPage;

    public GameObject SectionOrPage
    {
        get { return sectionOrPage; }
    }

    public void load()
    {
        switch (type)
        {
            case BOOKMARK_TYPE.PARAGRAPH_BOOKMARK:
                GetComponentInChildren<TextMeshProUGUI>().color = activeTextColor;
                GetComponentInParent<InfoSection>().changeRightPage(this);
                GameObject selector = GetComponentInParent<ParagraphPage>().selector;
                selector.transform.localPosition = new Vector3(
                    selector.transform.localPosition.x,
                    transform.parent.localPosition.y + gameObject.transform.localPosition.y,
                    selector.transform.localPosition.z);
                break;
            case BOOKMARK_TYPE.SECTION_BOOKMARK:
                GetComponentInParent<Manual>().changeSection(this);
                break;
        }
    }

    public void setInactive()
    {
        switch (type)
        {
            case BOOKMARK_TYPE.PARAGRAPH_BOOKMARK:
                GetComponentInChildren<TextMeshProUGUI>().color = inactiveTextColor;
                break;
        }
    }
}
