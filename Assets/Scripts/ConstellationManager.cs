using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ConstellationManager : MonoBehaviour
{

    public enum CONSTELLATION
    {
        NONE,
        COLORANT,
        TEMPERATURE,
        AETHER,
        SUPERNOVA,
        VOIDS,
        LENSING
    }

    public static CONSTELLATION CONSTELLATION1
    {
        get;
        private set;
    }
    public static CONSTELLATION CONSTELLATION2
    {
        get;
        private set;
    }

    private void Awake()
    {
        int n1 = UnityEngine.Random.Range(1, 7);
        CONSTELLATION1 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), n1);
        //rigging constellations
        //CONSTELLATION1 = CONSTELLATION.AETHER;
        Debug.Log(CONSTELLATION1);
        int n2 = UnityEngine.Random.Range(1, 7);
        CONSTELLATION2 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), UnityEngine.Random.Range(1, 7));
        //CONSTELLATION2 = CONSTELLATION.LENSING;
        while (CONSTELLATION1 == CONSTELLATION2) CONSTELLATION2 = (CONSTELLATION)Enum.ToObject(typeof(CONSTELLATION), UnityEngine.Random.Range(1, 7));
        Debug.Log(CONSTELLATION2);
        sortConstellations();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sortConstellations()
    {
        if((int)CONSTELLATION1 > (int)CONSTELLATION2)
        {
            CONSTELLATION constellation = CONSTELLATION1;
            CONSTELLATION1 = CONSTELLATION2;
            CONSTELLATION2 = constellation;
        }
    }
}
