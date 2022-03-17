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
    GameObject temperatureIcon;
    [SerializeField]
    Sprite fireTemperature;
    [SerializeField]
    Sprite iceTemperature;
    [SerializeField]
    GameObject textAetherCounter;
    [SerializeField]
    GameObject textPlasmicityCouter;
    [SerializeField]
    GameObject textVoidCounter;

    [SerializeField]
    GameObject temperatureBlock;
    [SerializeField]
    GameObject aetherBlock;
    [SerializeField]
    GameObject viscosityBlock;
    [SerializeField]
    GameObject voidnessBlock;

    public int pointsCounter
    {
        get; private set;
    }
    int mindCounter;
    int bodyCounter;
    int soulCounter;
    public int temperatureCounter
    {
        get; private set;
    }
    public int aetherCoutner
    {
        get; private set;
    }
    public int viscosityCounter
    {
        get; private set;
    }
    public int voidnessCounter
    {
        get; private set;
    }

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

    [SerializeField]
    GameObject pointsCaption;
    [SerializeField]
    GameObject aspectCaption;
    [SerializeField]
    GameObject temperatureCaption;
    [SerializeField]
    GameObject aetherCaption;
    [SerializeField]
    GameObject viscosityCaption;
    [SerializeField]
    GameObject voidsCaption;

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
        mind_and_no_body,
        mind_and_no_soul,
        body_and_no_mind,
        body_and_no_soul,
        soul_and_no_body,
        soul_and_no_mind
    }

    public Aspect potionAspect
    {
        get; private set;
    }

    public static StatBoardView staticInstance
    {
        get; private set;
    }

    private void Awake()
    {
        staticInstance = this;
        blocks = new Dictionary<GameObject, ConstellationManager.CONSTELLATION>();
    }

    public static void blocksRefresh()
    {
        if (staticInstance != null)
        {
            foreach (KeyValuePair<GameObject, ConstellationManager.CONSTELLATION> pair in blocks)
            {
                if (!(pair.Value == ConstellationManager.CONSTELLATION1 || pair.Value == ConstellationManager.CONSTELLATION2)) pair.Key.SetActive(false);
                else pair.Key.SetActive(true);
            }
        }

    }

    static Dictionary<GameObject, ConstellationManager.CONSTELLATION> blocks;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) pointsCaption.GetComponent<TextMeshProUGUI>().text = "Сила";
        else pointsCaption.GetComponent<TextMeshProUGUI>().text = "Power";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) aspectCaption.GetComponent<TextMeshProUGUI>().text = "Аспект";
        else aspectCaption.GetComponent<TextMeshProUGUI>().text = "Aspect";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) temperatureCaption.GetComponent<TextMeshProUGUI>().text = "Температ.";
        else temperatureCaption.GetComponent<TextMeshProUGUI>().text = "Temperat.";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) aetherCaption.GetComponent<TextMeshProUGUI>().text = "Эфир";
        else aetherCaption.GetComponent<TextMeshProUGUI>().text = "Aether";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) viscosityCaption.GetComponent<TextMeshProUGUI>().text = "Вязкость";
        else viscosityCaption.GetComponent<TextMeshProUGUI>().text = "Viscosity";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidsCaption.GetComponent<TextMeshProUGUI>().text = "Пустоты";
        else voidsCaption.GetComponent<TextMeshProUGUI>().text = "Voids";

        board = MixingBoard.StaticInstance;
        blocks.Add(temperatureBlock, ConstellationManager.CONSTELLATION.TEMPERATURE);
        blocks.Add(aetherBlock, ConstellationManager.CONSTELLATION.AETHER);
        blocks.Add(viscosityBlock, ConstellationManager.CONSTELLATION.SUPERNOVA);
        blocks.Add(voidnessBlock, ConstellationManager.CONSTELLATION.VOIDS);

        foreach (KeyValuePair <GameObject, ConstellationManager.CONSTELLATION> pair in blocks){
            if (!(pair.Value == ConstellationManager.CONSTELLATION1 || pair.Value == ConstellationManager.CONSTELLATION2)) pair.Key.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int oldPointsCounter = pointsCounter;
        pointsCounter = 0;

        int oldTemperature = temperatureCounter;
        temperatureCounter = 0;

        int oldAether = aetherCoutner;
        aetherCoutner = 0;

        int oldPlasmicity = viscosityCounter;
        viscosityCounter = 0;

        int oldVoidness = voidnessCounter;
        voidnessCounter = 0;


        Aspect oldAspect = potionAspect;
        mindCounter = 0;
        bodyCounter = 0;
        soulCounter = 0;

        foreach (Orb orb in MixingBoard.StaticInstance.orbs)
        {
            if (orb)
            {
                pointsCounter += orb.pointsImpact;
                if (orb.Level == 3)
                {
                    if (orb.type == Orb.ORB_TYPES.MIND_ASPECT) mindCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                    if (orb.type == Orb.ORB_TYPES.BODY_ASPECT) bodyCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                    if (orb.type == Orb.ORB_TYPES.SOUL_ASPECT) soulCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                }
                if (orb.archetype == Orb.ORB_ARCHETYPES.VOID)
                {
                    if (orb.type == Orb.ORB_TYPES.BLUE_PULSAR) mindCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                    if (orb.type == Orb.ORB_TYPES.RED_PULSAR) bodyCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                    if (orb.type == Orb.ORB_TYPES.GREEN_PULSAR) soulCounter += orb.GetComponent<AspectImpactInterface>().aspectImpact;
                }
                temperatureCounter += orb.temperatureCountImpact;
                aetherCoutner += orb.aetherImpact;
                viscosityCounter += orb.viscosityImpact;
                voidnessCounter += orb.voidnessImpact;
            }
        }

        if (mindCounter < 0) mindCounter = 0;
        if (bodyCounter < 0) bodyCounter = 0;
        if (soulCounter < 0) soulCounter = 0;

        if (oldPointsCounter != pointsCounter)
        {
            CookingModule.updatePreparedPotions();
            textScoreCounter.GetComponent<TextMeshProUGUI>().text = pointsCounter + "";
            textScoreCounter.GetComponent<Animation>().Play();
        }

        if(oldTemperature != temperatureCounter)
        {
            CookingModule.updatePreparedPotions();
            if(temperatureCounter == 0)
            {
                textTemperatureCounter.GetComponent<TextMeshProUGUI>().text = "" + temperatureCounter;
            }
            if(temperatureCounter > 0)
            {
                temperatureIcon.GetComponent<SpriteRenderer>().sprite = fireTemperature;
                textTemperatureCounter.GetComponent<TextMeshProUGUI>().text = "+" + temperatureCounter;
            }
            else
            {
                temperatureIcon.GetComponent<SpriteRenderer>().sprite = iceTemperature;
                textTemperatureCounter.GetComponent<TextMeshProUGUI>().text = "" + temperatureCounter;
            }
            textTemperatureCounter.GetComponent<Animation>().Play();
        }

        if (oldAether != aetherCoutner)
        {
            CookingModule.updatePreparedPotions();
            textAetherCounter.GetComponent<TextMeshProUGUI>().text = aetherCoutner + "";
            textAetherCounter.GetComponent<Animation>().Play();
        }

        if (oldPlasmicity != viscosityCounter)
        {
            CookingModule.updatePreparedPotions();
            textPlasmicityCouter.GetComponent<TextMeshProUGUI>().text = viscosityCounter + "";
            textPlasmicityCouter.GetComponent<Animation>().Play();
        }

        if (oldVoidness != voidnessCounter)
        {
            CookingModule.updatePreparedPotions();
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
            potionAspect = Aspect.aspectless;
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
        }
           if(oldAspect != potionAspect)
        {
            CookingModule.updatePreparedPotions();
            
            left.gameObject.GetComponent<Animation>().Play();
            right.gameObject.GetComponent<Animation>().Play();
            middle.gameObject.GetComponent<Animation>().Play();
        }
    }
}
