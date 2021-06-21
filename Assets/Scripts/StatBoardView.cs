using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StatBoardView : MonoBehaviour
{

    MixingBoard board;

    [SerializeField]
    GameObject textScoreCounter;
    [SerializeField]
    GameObject textTemperatureCounter;
    [SerializeField]
    GameObject textAetherCounter;
    [SerializeField]
    GameObject textPlasmicityCouter;
    [SerializeField]
    GameObject textVoidCounter;

    int scoreCounter;
    int mindCounter;
    int bodyCounter;
    int soulCounter;
    int temperatureCounter;
    int aetherCoutner;
    int plasmicityCounter;
    int voidnessCounter;

    [SerializeField]
    SpriteRenderer left;
    [SerializeField]
    SpriteRenderer right;
    [SerializeField]
    SpriteRenderer middle;

    [SerializeField]
    Sprite aspectlessSprite;
    [SerializeField]
    Sprite mindSprite;
    [SerializeField]
    Sprite bodySprite;
    [SerializeField]
    Sprite soulSprite;
    [SerializeField]
    Sprite amassSprite;


    public enum FILTER_TYPE
    {
        NONE,
        POINTS,
        ASPECT,
        AETHERNESS,
        TEMPERATURE,
        VOIDNESS,
        VISCOSITY
    }

    public enum Aspect
    {
        aspectless,
        mind,
        body,
        soul,
        mind_body,
        mind_soul,
        body_soul,
        amass
    }

    public Aspect potionAspect;

    // Start is called before the first frame update
    void Start()
    {
        board = MixingBoard.StaticInstance;
    }

    // Update is called once per frame
    void Update()
    {
        int oldScoreCounter = scoreCounter;
        scoreCounter = 0;

        int oldTemperature = temperatureCounter;
        temperatureCounter = 0;

        int oldAether = aetherCoutner;
        aetherCoutner = 0;

        int oldPlasmicity = plasmicityCounter;
        plasmicityCounter = 0;

        int oldVoidness = voidnessCounter;
        voidnessCounter = 0;


        Aspect oldAspect = potionAspect;
        mindCounter = 0;
        bodyCounter = 0;
        soulCounter = 0;

        foreach (Transform child in board.OrbShift.transform)
        {
            if (child.gameObject.CompareTag("Orb"))
            {
                Orb orb = child.gameObject.GetComponent<Orb>();
                if(orb)
                {
                    scoreCounter += orb.pointsImpact;
                    if (orb.Level == 3)
                    {
                        if (orb.type == Orb.ORB_TYPES.MIND_ASPECT) mindCounter += orb.GetComponent<AspectOrb>().aspectImpact;
                        if (orb.type == Orb.ORB_TYPES.BODY_ASPECT) bodyCounter += orb.GetComponent<AspectOrb>().aspectImpact;
                        if (orb.type == Orb.ORB_TYPES.SOUL_ASPECT) soulCounter += orb.GetComponent<AspectOrb>().aspectImpact;
                    }
                    temperatureCounter += orb.temperatureCountImpact;
                    aetherCoutner += orb.aetherImpact;
                    plasmicityCounter += orb.viscosityImpact;
                    voidnessCounter += orb.voidnessImpact;
                }
            }
        }

        if (mindCounter < 0) mindCounter = 0;
        if (bodyCounter < 0) bodyCounter = 0;
        if (soulCounter < 0) soulCounter = 0;

        if (oldScoreCounter != scoreCounter)
        {
            textScoreCounter.GetComponent<TextMeshProUGUI>().text = scoreCounter + "";
            textScoreCounter.GetComponent<Animation>().Play();
        }

        if(oldTemperature != temperatureCounter)
        {
            textTemperatureCounter.GetComponent<TextMeshProUGUI>().text = temperatureCounter + "";
            textTemperatureCounter.GetComponent<Animation>().Play();
        }

        if (oldAether != aetherCoutner)
        {
            textAetherCounter.GetComponent<TextMeshProUGUI>().text = aetherCoutner + "";
            textAetherCounter.GetComponent<Animation>().Play();
        }

        if (oldPlasmicity != plasmicityCounter)
        {
            textPlasmicityCouter.GetComponent<TextMeshProUGUI>().text = plasmicityCounter + "";
            textPlasmicityCouter.GetComponent<Animation>().Play();
        }

        if (oldVoidness != voidnessCounter)
        {
            textVoidCounter.GetComponent<TextMeshProUGUI>().text = voidnessCounter + "";
            textVoidCounter.GetComponent<Animation>().Play();
        }

        if (mindCounter == bodyCounter && mindCounter == soulCounter && mindCounter == 0)
        {
            potionAspect = Aspect.aspectless;
        }
        else if (mindCounter > bodyCounter && mindCounter > soulCounter)
        {
            potionAspect = Aspect.mind;
        }
        else if (mindCounter < bodyCounter && bodyCounter > soulCounter)
        {
            potionAspect = Aspect.body;
        }
        else if (soulCounter > bodyCounter && mindCounter < soulCounter)
        {
            potionAspect = Aspect.soul;
        }
        else if (mindCounter == bodyCounter && mindCounter > soulCounter)
        {
            potionAspect = Aspect.mind_body;
        }
        else if (bodyCounter == soulCounter && bodyCounter > mindCounter)
        {
            potionAspect = Aspect.body_soul;
        }
        else if (mindCounter == soulCounter && mindCounter > bodyCounter)
        {
            potionAspect = Aspect.mind_soul;
        }
        else if (mindCounter == bodyCounter && mindCounter == soulCounter && mindCounter > 0)
        {
            potionAspect = Aspect.amass;
        }

        switch (potionAspect)
        {
            case Aspect.aspectless:
                left.enabled = false;
                right.enabled = false;
                middle.enabled = true;
                middle.sprite = aspectlessSprite;
                break;
            case Aspect.mind:
                left.enabled = false;
                right.enabled = false;
                middle.enabled = true;
                middle.sprite = mindSprite;
                break;
            case Aspect.body:
                left.enabled = false;
                right.enabled = false;
                middle.enabled = true;
                middle.sprite = bodySprite;
                break;
            case Aspect.soul:
                left.enabled = false;
                right.enabled = false;
                middle.enabled = true;
                middle.sprite = soulSprite;
                break;
            case Aspect.mind_body:
                left.enabled = true;
                right.enabled = true;
                left.sprite = mindSprite;
                right.sprite = bodySprite;
                middle.enabled = false;
                break;
            case Aspect.body_soul:
                left.enabled = true;
                right.enabled = true;
                left.sprite = bodySprite;
                right.sprite = soulSprite;
                middle.enabled = false;
                break;
            case Aspect.mind_soul:
                left.enabled = true;
                right.enabled = true;
                left.sprite = mindSprite;
                right.sprite = soulSprite;
                middle.enabled = false;
                break;
            case Aspect.amass:
                left.enabled = false;
                right.enabled = false;
                middle.enabled = true;  
                middle.sprite = amassSprite;
                break;
        }
           if(oldAspect != potionAspect)
        {
            left.gameObject.GetComponent<Animation>().Play();
            right.gameObject.GetComponent<Animation>().Play();
            middle.gameObject.GetComponent<Animation>().Play();
        }
    }
}
