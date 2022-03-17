using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolvingSprite : MonoBehaviour, IDissolving
{
    static double dissolvingTimer = .5f;
    //public static Material defaultSpriteMaterial;
    [SerializeField]
    Material dissolvingMaterialSample;
    [SerializeField]
    float defaultEdgesParam;
    Material material;

    [SerializeField]
    bool random;
    [SerializeField]
    Material variant2;
    [SerializeField]
    Material variant3;
    [SerializeField]
    Material variant4;

    List<Material> variants;

    [SerializeField]
    float timeModifier = 1;

    Button[] buttonlist;
    SpriteRenderer[] spritelist;
    Image[] imagelist;

    bool dissolving = false;

    double currentDissolvingTimer = 0;


    void initialize()
    {
        buttonlist = GetComponentsInChildren<Button>(true);
        spritelist = GetComponentsInChildren<SpriteRenderer>(true);
        imagelist = GetComponentsInChildren<Image>(true);

        variants = new List<Material>();

        if (random)
        {
            variants.Add(dissolvingMaterialSample);
            variants.Add(variant2);
            variants.Add(variant3);
            variants.Add(variant4);
            dissolvingMaterialSample = variants[Random.Range(0, variants.Count)];
        }

        material = Instantiate<Material>(dissolvingMaterialSample);

        if (imagelist != null)
            foreach (Image image in imagelist)
            {
                image.material = material;
            }

        if (spritelist != null)
        {
            foreach (SpriteRenderer spriteRenderer in spritelist)
            {
                spriteRenderer.material = material;
            }
        }
    }

    void Awake()
    {
        if(buttonlist== null) initialize();
    }

    void Update()
    {
        if (currentDissolvingTimer > 0)
        {
            
            currentDissolvingTimer -= Time.deltaTime;
            if (dissolving)
            {
                material.SetFloat("_Level", material.GetFloat("_Level") + Time.deltaTime * 3 / timeModifier);
                if (material.GetFloat("_Level") > 1)
                {
                    material.SetFloat("_Edges", 0f);
                    material.SetFloat("_Level", 1f);
                }
            }

            else
            {
                material.SetFloat("_Level", material.GetFloat("_Level") - Time.deltaTime * 3 / timeModifier);
                if (material.GetFloat("_Level") < 0)
                {
                    material.SetFloat("_Edges", 0f);
                }
            }
        }
    }

    public void appear()
    {
        if(buttonlist == null) initialize();
        foreach (Transform transform in GetComponentInChildren<Transform>(true))
        {
            transform.gameObject.SetActive(true);
        }

        dissolving = false;
        foreach (Button button in buttonlist)
        {
            button.interactable = true;
        }
        material.SetFloat("_Edges", defaultEdgesParam);
        material.SetFloat("_Level", 1f);
        currentDissolvingTimer = dissolvingTimer * timeModifier;
    }

    public void disappear()
    {
        if (buttonlist == null) initialize();
        dissolving = true;
        foreach (Button button in buttonlist)
        {button.interactable = false;
        }
        material.SetFloat("_Edges", defaultEdgesParam);
        material.SetFloat("_Level", 0f);
        currentDissolvingTimer = dissolvingTimer * timeModifier;
    }

    void deactivate()
    {
        foreach (Transform transform in GetComponentInChildren<Transform>(true))
        {
            transform.gameObject.SetActive(true);
        }
    }
}
