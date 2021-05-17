using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolvingUIElement : MonoBehaviour
{
    static double dissolvingTimer = .5f;
    //public static Material defaultSpriteMaterial;
    [SerializeField]
    Material dissolvingMaterialSample;
    [SerializeField]
    float defaultEdgesParam;
    Material material;

    [SerializeField]
    float timeModifier = 1;

    Button[] buttonlist;
    SpriteRenderer[] spritelist;
    Image[] imagelist;

    bool dissolving = false;

    double currentDissolvingTimer = 0;

    void Awake()
    {
        buttonlist = GetComponentsInChildren<Button>(true);
        spritelist = GetComponentsInChildren<SpriteRenderer>(true);
        imagelist = GetComponentsInChildren<Image>(true);

        material = Instantiate<Material>(dissolvingMaterialSample);
    }

    private void Start()
    {
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

    void Update()
    {
        if (currentDissolvingTimer >= 0)
        {
            currentDissolvingTimer -= Time.deltaTime;
            if (dissolving)
            {
                material.SetFloat("_Level", material.GetFloat("_Level") + Time.deltaTime*3 /timeModifier);
                if (material.GetFloat("_Level") >= 1)
                {
                    material.SetFloat("_Edges", 0f);
                    gameObject.SetActive(false);
                    material.SetFloat("_Level", 1f);
                }
            }

            else
            {
                material.SetFloat("_Level", material.GetFloat("_Level") - Time.deltaTime*3 / timeModifier);
                if (material.GetFloat("_Level") >= 1.001)
                {
                    gameObject.SetActive(true);
                    material.SetFloat("_Edges", 0f);
                }
            }

        }
        else material.SetFloat("_Edges", 0f);
    }

    public void dissolve()
    {
        dissolving = true;
        if (buttonlist != null) foreach(Button button in buttonlist)
        {
            button.interactable = false;
        }
           
        material.SetFloat("_Edges", defaultEdgesParam);
        material.SetFloat("_Level", 0f);
        currentDissolvingTimer = dissolvingTimer * timeModifier;
        
    }

    public void appear()
    {
        dissolving = false;
        gameObject.SetActive(true);
        foreach (Button button in buttonlist)
        {
            button.interactable = true;
        }
        material.SetFloat("_Edges", defaultEdgesParam);
        material.SetFloat("_Level", 1f);
        currentDissolvingTimer = dissolvingTimer * timeModifier;
    }

    void deactivate()
    {
        gameObject.SetActive(false);
    }
  

}
