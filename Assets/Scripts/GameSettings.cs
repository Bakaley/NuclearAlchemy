using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public enum Language
    {
        EN,
        RU
    }

    static Language currentLanguage = Language.RU;

    public static Language CurrentLanguage
    {
        get
        {
            return currentLanguage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
