using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class ParagraphBookmark : Bookmark
{

    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        load();
    }

    override public void load()
    {
        GetComponentInParent<InfoSection>().changePage(this);
        setActive();
    }

    public override void setActive()
    {
        text.color = activeColor;
        GameObject selector = GetComponentInParent<ParagraphPage>().selector;
        selector.transform.localPosition = new Vector3(
            selector.transform.localPosition.x,
            transform.parent.localPosition.y + gameObject.transform.localPosition.y,
            selector.transform.localPosition.z);
    }

    public override void setInactive()
    {
        text.color = inactiveColor;
    }
}
