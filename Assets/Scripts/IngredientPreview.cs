using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPreview : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer essense1;
    [SerializeField]
    public SpriteRenderer essense2;
    [SerializeField]
    public SpriteRenderer essense3;

    [SerializeField]
    Material defaultEssenceMaterial;
    [SerializeField]
    Material targetedEssenceMaterial;

    [SerializeField]
    Sprite ingredientIcon;
    [SerializeField]
    Color ingredientIconColor;


    public Material DefaultEssenceMaterial
    {
        get
        {
            return defaultEssenceMaterial;
        }
    }

    public Material TargetedEssenceMaterial
    {
        get
        {
            return targetedEssenceMaterial;
        }
    }

    [SerializeField]
    public GameObject ingredient;
    
    // Start is called before the first frame update
    void Start()
    {

    }
}
