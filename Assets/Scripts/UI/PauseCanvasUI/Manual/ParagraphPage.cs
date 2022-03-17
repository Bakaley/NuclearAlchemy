using UnityEngine;
using UnityEditor;

public class ParagraphPage : MonoBehaviour
{
    [SerializeField]
    Bookmark defaultParagraph;
    [SerializeField]
    public GameObject selector;

    private void OnEnable()
    {
        selector.GetComponent<DissolvingSprite>().appear();
        defaultParagraph.load();
        Invoke("setSelector", .1f);
    }

    void setSelector()
    {
        selector.transform.localPosition = new Vector3(
           selector.transform.localPosition.x,
           defaultParagraph.transform.parent.localPosition.y + defaultParagraph.gameObject.transform.localPosition.y,
           selector.transform.localPosition.z);
    }
}
