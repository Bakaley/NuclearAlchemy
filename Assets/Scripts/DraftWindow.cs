using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DraftWindow : MonoBehaviour
{

    [SerializeField]
    GameObject bookPageSampler;

    static GameObject page1 = null;
    static GameObject page2 = null;
    static GameObject page3 = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {
            if (Input.GetMouseButton(0) && !RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition, UIManager.cameraObject.GetComponent<Camera>()))
            {
                if (page1)
                {
                    Destroy(page1);
                    page1 = null;
                }
                if (page2)
                {
                    Destroy(page2);
                    page2 = null;
                }
                if (page3)
                {
                    Destroy(page3);
                    page3 = null;
                }
                opened = false;
            }
        }
    }

    public static bool opened {
        get;
        private set;
    }

    public void generate (List<Recipe> list)
    {

        opened = true;
        GetComponent<RectTransform>().sizeDelta = new Vector2(bookPageSampler.GetComponent<RectTransform>().rect.width * bookPageSampler.GetComponent<RectTransform>().localScale.x * list.Count, bookPageSampler.GetComponent<RectTransform>().rect.height * bookPageSampler.GetComponent<RectTransform>().localScale.y);

        foreach(Recipe recipe in list)
        {
            fillPageBook(recipe);
        }

        switch (list.Count)
        {
            case 1:
                page1.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
                page1.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
                page1.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                break;
            case 2:
                page1.GetComponent<RectTransform>().anchorMin = new Vector2(.0f, .5f);
                page1.GetComponent<RectTransform>().anchorMax = new Vector2(.0f, .5f);
                page1.GetComponent<RectTransform>().pivot = new Vector2(.0f, .5f);

                page2.GetComponent<RectTransform>().anchorMin = new Vector2(1f, .5f);
                page2.GetComponent<RectTransform>().anchorMax = new Vector2(1f, .5f);
                page2.GetComponent<RectTransform>().pivot = new Vector2(1f, .5f);
                break;
            case 3:
                page1.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
                page1.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
                page1.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);

                page2.GetComponent<RectTransform>().anchorMin = new Vector2(.0f, .5f);
                page2.GetComponent<RectTransform>().anchorMax = new Vector2(.0f, .5f);
                page2.GetComponent<RectTransform>().pivot = new Vector2(.0f, .5f);

                page3.GetComponent<RectTransform>().anchorMin = new Vector2(1f, .5f);
                page3.GetComponent<RectTransform>().anchorMax = new Vector2(1f, .5f);
                page3.GetComponent<RectTransform>().pivot = new Vector2(1f, .5f);
                break;
        }
    }

    void fillPageBook(Recipe recipe)
    {
        GameObject page = Instantiate(bookPageSampler, transform);
        if (page1 == null) page1 = page;
        else if (page2 == null) page2 = page;
        else if (page3 == null) page3 = page;

        page.GetComponent<BookPage>().fillPage(recipe);

        page.GetComponent<BookPage>().activateElements();
    }

    public static void clean()
    {
        if (page1) Destroy(page1.gameObject);
        if (page2) Destroy(page2.gameObject);
        if (page3) Destroy(page3.gameObject);
        opened = false;
    }

}
