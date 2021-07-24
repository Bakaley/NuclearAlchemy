using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject bookPageSampler;

    [SerializeField]
    GameObject potionBrewingSampler;
    [SerializeField]
    GameObject potionLevelUpSampler;
    [SerializeField]
    GameObject ingredientSampler;

    GameObject recipe1 = null;
    GameObject recipe2 = null;
    GameObject recipe3 = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            if (recipe1)
            {
                Destroy(recipe1);
                recipe1 = null;
            }
            if (recipe2)
            {
                Destroy(recipe2);
                recipe2 = null;
            }
            if (recipe3)
            {
                Destroy(recipe3);
                recipe3 = null;
            }
        }
    }

    public void generate (List<GameObject> list)
    {
       

        GetComponent<RectTransform>().sizeDelta = new Vector2(bookPageSampler.GetComponent<RectTransform>().rect.width * bookPageSampler.GetComponent<RectTransform>().localScale.x * list.Count, bookPageSampler.GetComponent<RectTransform>().rect.height * bookPageSampler.GetComponent<RectTransform>().localScale.y);

        foreach(GameObject recipe in list)
        {
            fillPageBook(recipe);
        }

        switch (list.Count)
        {
            case 1:
                recipe1.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
                recipe1.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
                recipe1.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                break;
            case 2:
                recipe1.GetComponent<RectTransform>().anchorMin = new Vector2(.0f, .5f);
                recipe1.GetComponent<RectTransform>().anchorMax = new Vector2(.0f, .5f);
                recipe1.GetComponent<RectTransform>().pivot = new Vector2(.0f, .5f);

                recipe2.GetComponent<RectTransform>().anchorMin = new Vector2(1f, .5f);
                recipe2.GetComponent<RectTransform>().anchorMax = new Vector2(1f, .5f);
                recipe2.GetComponent<RectTransform>().pivot = new Vector2(1f, .5f);
                break;
            case 3:
                recipe1.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
                recipe1.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
                recipe1.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);

                recipe2.GetComponent<RectTransform>().anchorMin = new Vector2(.0f, .5f);
                recipe2.GetComponent<RectTransform>().anchorMax = new Vector2(.0f, .5f);
                recipe2.GetComponent<RectTransform>().pivot = new Vector2(.0f, .5f);

                recipe3.GetComponent<RectTransform>().anchorMin = new Vector2(1f, .5f);
                recipe3.GetComponent<RectTransform>().anchorMax = new Vector2(1f, .5f);
                recipe3.GetComponent<RectTransform>().pivot = new Vector2(1f, .5f);
                break;
        }
    }

    void fillPageBook(GameObject recipe)
    {
        GameObject page = Instantiate(bookPageSampler, transform);
        if (recipe1 == null) recipe1 = page;
        else if (recipe2 == null) recipe2 = page;
        else if (recipe3 == null) recipe3 = page;
        page.GetComponent<BookPage>().recipe = recipe;
    }
}
