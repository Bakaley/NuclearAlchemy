using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    void Start()
    {

    }
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

}
