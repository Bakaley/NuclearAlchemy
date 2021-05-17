﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public abstract class Orb : MonoBehaviour
{

    ORB_ARCHETYPES orbArchetype;
    public ORB_ARCHETYPES archetype
    {
        get
        {
            return orbArchetype;
        }
    }

    [SerializeField]
    protected ORB_TYPES orbType;
    public ORB_TYPES type
    {
        get
        {
            return orbType;
        }
    }

    public enum ORB_TYPES
    {
        NONE,
        MIND_ASPECT,
        BODY_ASPECT,
        SOUL_ASPECT,
        //GOLD_ASPECT,
        //BLACK_ASPECT,
        //GAS,
        //SEDIMENT,
        //SALTPETRE,
        SEMIPLASMA,
        SUPERNOVA_CORE,
        FIRE_CORE,
        ICE_CORE,
        AETHER_CORE,
        BLUE_DYE_CORE,
        RED_DYE_CORE,
        GREEN_DYE_CORE,
        //GOLD_DYE_CORE,
        //BLACK_DYE_CORE,
        VOID,
        FIRE_VOID,
        ICE_VOID,
        AETHER_VOID,
        SUPERNOVA_VOID,
        BLUE_DYE_VOID,
        RED_DYE_VOID,
        GREEN_DYE_VOID,
        //GOLD_DYE_VOID,
        //BLACK_DYE_VOID
        BLUE_DROP,
        RED_DROP,
        GREEN_DROP,
        FIRE_DROP,
        ICE_DROP,
        AETHER_DROP,
        ANTIMATTER_DROP,
        SUPERNOVA_DROP
    }

    public enum ORB_ARCHETYPES
    {
        NONE,
        ASPECT,
        NEORGANIC,
        CORE,
        VOID,
        DROP
    }

    public static Dictionary <ORB_TYPES, ORB_ARCHETYPES> typeArchetypeDictionary { get; private set; }

    public static double destroyingTimer = .7;
    public static double movingTime = .15f;

    double startDestroyTimer = 0;
    double currentDestroyingTimer = 0;
    double currentAppearingTimer = 0;
    double channelingTime = 0;


    ParticleSystem affectingSystem;

    [SerializeField]
    public GameObject OrbPreview;

    public bool lying { get; private set; } = true;
    public bool xStricted
    {
        get
        {
            return ((((int)(transform.localPosition.x * 100)) / 100.0) % 1) == 0;
        }
    }

    public bool yStricted
    {
        get
        {
            return ((((int)(transform.localPosition.y * 100)) / 100.0) % 1) == 0;
        }
    }



    public bool shouldDestroyed { get; private set; } = false;
    [HideInInspector]
    public int countOfReactionsIn = 0;

    [HideInInspector]
    public bool dissolvingAppearing = true;
    
    [SerializeField]
    public MeshRenderer coreSphereRenderer;
    [SerializeField]
    public MeshRenderer outerSphereRenderer;
    [SerializeField]
    public ParticleSystem particleSphere;
    [SerializeField]
    public SpriteRenderer symbolRenderer;
    [SerializeField]
    public SpriteRenderer antimatterSymbolRenderer;

    [SerializeField]
    public GameObject counter;

    protected static StatBoardView.FILTER_TYPE statToShow;
    protected TextMeshPro counterTMP;
    protected string counterString;

    public void disableCounter()
    {
        counter.SetActive(false);
    }

    public static MixingBoard mixingBoard
    {
        get
        {
            return MixingBoard.StaticInstance;
        }
    }

    [HideInInspector]
    public float movingSeed;

    public enum CHANNELING_MODES
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        CENTER,
        LEVEL_UP,
        REPLACE
    }

    CHANNELING_MODES mode;

    [SerializeField]
    public ParticleSystem particleSystemSample;

    private ParticleSystem channelingParticleSystem;


    [SerializeField]
    public Color channelParticleColor;

    [HideInInspector]
    public bool channeling = false;

    public enum EFFECT_TYPES
    {
        NONE,
        FROZE,
        FIRE,
        AETHER,
        LEVEL_UP,
        ANTIMATTER,
        BLUE_DYE,
        RED_DYE,
        GREEN_DYE,
        DISSOLVE
    }

    public bool frozen;
    public bool fiery;

    [SerializeField]
    public int aetherCount;

    public int aetherImpact
    {
        get
        {
            if (!shouldDestroyed) return aetherCount;
            else return 0;
        }
    }
    public bool antimatter;

    public bool comboAvaliable;

   

    public struct ReplacingOrbStruct
    {
        public GameObject baseOrb
        {
            get; private set;
        }
        public int aether
        {
            get; private set;
        }
        public bool antimatter
        {
            get; private set;
        }

        public ReplacingOrbStruct(GameObject orbToReplace, int aetherCounter = 0, bool antimatterFlag = false)
        {
            baseOrb = orbToReplace;
            aether = aetherCounter;
            antimatter = antimatterFlag;
        }
    }

    ReplacingOrbStruct replacingOrb;


    [SerializeField]
    protected GameObject nextLevelOrb;
    public GameObject NextLevelOrb
    {
        get
        {
            return nextLevelOrb;
        }
    }

    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
    }

    //используется, чтобы сфера не начинала падать, пока уже падает
    bool falling = false;

    [SerializeField]
    protected int points;
    public int comboCounter
    {
        get
        {
            if (fiery) return 2;
            return 1;
        }
    }

    public int pointsImpact
    {
        get
        {
            if (!shouldDestroyed)
                return (int)(points + aetherCount * points * .2);
            else return 0;
        }
    }

    public int DefaultPoints
    {
        get
        {
            return points;
        }
    }

    public int temperatureCountImpact
    {
        get
        {
            if (!shouldDestroyed)
            {
                int count = 0;
                if ((int)Math.Round(transform.localPosition.y) + 1 < MixingBoard.Height)
                {
                    if (fiery && mixingBoard.orbs[(int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y) + 1]) count += Level + (int)Math.Round(Level * .2 * aetherCount);
                }
                if (frozen && (int)Math.Round(transform.localPosition.y) != 0) count -= Level + (int)Math.Round(Level * .2 * aetherCount);
                return count;
            }
            else return 0;
        }
    }

    float startX;
    float startY;

    [SerializeField]
    public GameObject iceParticles;

    [SerializeField]
    public GameObject fireParticles;

    [SerializeField]
    public GameObject aetherParticle;
    [SerializeField]
    GameObject orbit;

    public List<GameObject> aetherParticles;

    private void Awake()
    {
        if (typeArchetypeDictionary == null)
        {
            typeArchetypeDictionary = new Dictionary<ORB_TYPES, ORB_ARCHETYPES>();

            typeArchetypeDictionary.Add(ORB_TYPES.NONE, ORB_ARCHETYPES.NONE);

            typeArchetypeDictionary.Add(ORB_TYPES.MIND_ASPECT, ORB_ARCHETYPES.ASPECT);
            typeArchetypeDictionary.Add(ORB_TYPES.BODY_ASPECT, ORB_ARCHETYPES.ASPECT);
            typeArchetypeDictionary.Add(ORB_TYPES.SOUL_ASPECT, ORB_ARCHETYPES.ASPECT);

            typeArchetypeDictionary.Add(ORB_TYPES.SEMIPLASMA, ORB_ARCHETYPES.NEORGANIC);

            typeArchetypeDictionary.Add(ORB_TYPES.SUPERNOVA_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.ICE_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.FIRE_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.AETHER_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.BLUE_DYE_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.RED_DYE_CORE, ORB_ARCHETYPES.CORE);
            typeArchetypeDictionary.Add(ORB_TYPES.GREEN_DYE_CORE, ORB_ARCHETYPES.CORE);

            typeArchetypeDictionary.Add(ORB_TYPES.VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.SUPERNOVA_VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.ICE_VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.FIRE_VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.AETHER_VOID, ORB_ARCHETYPES.VOID);

            typeArchetypeDictionary.Add(ORB_TYPES.BLUE_DYE_VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.RED_DYE_VOID, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.GREEN_DYE_VOID, ORB_ARCHETYPES.VOID);

            typeArchetypeDictionary.Add(ORB_TYPES.BLUE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.RED_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.GREEN_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.SUPERNOVA_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.ICE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.FIRE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.AETHER_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.ANTIMATTER_DROP, ORB_ARCHETYPES.DROP);

        }

        aetherParticles = new List<GameObject>();
        orbArchetype = typeArchetypeDictionary[type];
        dissolvingAppearing = true;
        counterTMP = counter.GetComponent<TextMeshPro>();
    }

    virtual protected void Start()
    {

        if (symbolRenderer != null)
        {
            var tempColor = symbolRenderer.color;
            tempColor.a = 0;
            symbolRenderer.color = tempColor;
        }

        coreSphereRenderer.material.SetFloat("DissolvingVector", 1);
        currentAppearingTimer = .5f;

        if (symbolRenderer != null) Invoke("synchronizeSymbolSeed", .01f);

        if(aetherCount != 0)
        {
            int aetherIncreaseOn = aetherCount;
            aetherCount = 0;
            addAether(aetherIncreaseOn);
        }
    }

    void synchronizeSymbolSeed()
    {
        if (coreSphereRenderer.gameObject.GetComponent<Floating>() != null) coreSphereRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
        if (symbolRenderer.gameObject.GetComponent<Floating>() != null) symbolRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
        if(antimatterSymbolRenderer) if (antimatterSymbolRenderer.gameObject.GetComponent<Floating>() != null) antimatterSymbolRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
    }

    virtual protected void Update()
    {
        if (transform.parent.gameObject == MixingBoard.StaticInstance.OrbShift)
        {
            if (xStricted && (int)(this.transform.localPosition.y) == 0) lying = true;
            else if ((int)this.transform.localPosition.y >= MixingBoard.Height) lying = false;
            else if (xStricted && yStricted && (int)(this.transform.localPosition.y) > 0 && mixingBoard.orbs[(int)this.transform.localPosition.x, (int)(this.transform.localPosition.y - 1)] != null) lying = true;
            else lying = false;

            if(!xStricted || !yStricted || !lying) mixingBoard.spinDelay = Math.Max(.05, mixingBoard.spinDelay);

            if (!falling)
            {
                if (transform.localPosition.y > 0 && xStricted && mixingBoard.stricted && (int)transform.localPosition.y < MixingBoard.Height)
                {
                    int fallDistance = 0;
                    for (int i = 1; i <= transform.localPosition.y; i++)
                    {
                        try
                        {
                            if (mixingBoard.orbs[(int)transform.localPosition.x, (int)transform.localPosition.y - i] == null)
                            {
                                fallDistance++;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Debug.LogError("IndexOutOfRangeException " + (int)transform.localPosition.x + " " + (int)(transform.localPosition.y - i));
                        }

                    }
                    if (fallDistance != 0 && mixingBoard.stricted)
                    {
                        mixingBoard.currentTargetDelay = mixingBoard.targetDelay * fallDistance;
                        for (int j = (int)transform.localPosition.y; j < MixingBoard.Height; j++)
                        {
                            if (mixingBoard.orbs[(int)transform.localPosition.x, j] != null)
                            {
                                if (mixingBoard.orbs[(int)transform.localPosition.x, j].channeling) mixingBoard.breakReactionsWith(mixingBoard.orbs[(int)transform.localPosition.x, j]);
                                lying = false;
                                iTween.MoveTo(mixingBoard.orbs[(int)transform.localPosition.x, j].gameObject, iTween.Hash("position", new Vector3(mixingBoard.orbs[(int)transform.localPosition.x, j].transform.localPosition.x, mixingBoard.orbs[(int)transform.localPosition.x, j].transform.localPosition.y - fallDistance, mixingBoard.orbs[(int)transform.localPosition.x, j].transform.localPosition.z), "islocal", true, "time", Orb.movingTime * fallDistance, "easetype", iTween.EaseType.easeInOutSine));
                                mixingBoard.spinDelay = Math.Max(Orb.movingTime * fallDistance, mixingBoard.spinDelay);
                                falling = true;
                                Invoke("fallingReset", Convert.ToSingle(Orb.movingTime * fallDistance));
                            }
                        }
                    }
                }
            }
        }

        if (channeling)
        {
            comboAvaliable = false;
            if (channelingTime >= 0)
            {
                channelingTime -= Time.deltaTime;
            }

            else if (channelingTime < 0)
            {
                switch (mode)
                {
                    
                    case CHANNELING_MODES.DOWN:
                        moveDownCombine();
                        break;
                    case CHANNELING_MODES.LEFT:
                        moveLeftCombine();
                        break;
                    case CHANNELING_MODES.RIGHT:
                        moveRightCombine();
                        break;
                    case CHANNELING_MODES.UP:
                        moveUpCombine();
                        break;
                    case CHANNELING_MODES.CENTER:
                        DestroyIn(destroyingTimer);
                        break;
                    case CHANNELING_MODES.REPLACE:
                        symbolRenderer.enabled = false;
                        GameObject orbObject = Instantiate(replacingOrb.baseOrb, new Vector3(startX, startY, 0), Quaternion.identity);
                        Orb orb = orbObject.GetComponent<Orb>();
                        orb.transform.SetParent(mixingBoard.OrbShift.transform, false);
                        if (replacingOrb.aether != 0) orb.aetherCount = replacingOrb.aether;
                        if (replacingOrb.antimatter) orb.addAntimatter();
                        DestroyIn(destroyingTimer);
                        break;
                }
                mixingBoard.finishReactionsWith(this);
            }
        }

        if (shouldDestroyed)
            if (currentDestroyingTimer >= 0)
            {
                currentDestroyingTimer -= Time.deltaTime;
                coreSphereRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(1 - currentDestroyingTimer * 1.2));
                if (symbolRenderer)
                {
                    symbolRenderer.color = new Color(symbolRenderer.color.r, symbolRenderer.color.b, symbolRenderer.color.g, Convert.ToSingle(currentDestroyingTimer / startDestroyTimer));
                }
            }

            else
            {
                if (affectingSystem)
                {
                    affectingSystem.transform.SetParent(mixingBoard.transform, true);
                }
                Destroy(this.gameObject);
            }

        if (dissolvingAppearing)
        {
            if (currentAppearingTimer >= 0)
            {
                currentAppearingTimer -= Time.deltaTime;
                coreSphereRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(currentAppearingTimer * 2));

                if(symbolRenderer != null)
                {
                    var tempColor = symbolRenderer.color;
                    tempColor.a = Convert.ToSingle(1 - currentAppearingTimer);
                    symbolRenderer.color = tempColor;
                }

                
            }
        }
        if (transform.parent.gameObject != MixingBoard.StaticInstance.OrbShift && dissolvingAppearing == true) instantAppear();
        if (counter && counter.activeSelf)
        {
            updateCounterString();
            counterTMP.text = counterString;
        }

    }


    void updateCounterString()
    {
        switch (statToShow)
        {
            case StatBoardView.FILTER_TYPE.POINTS:
                counterString = pointsImpact + "";
                break;

            case StatBoardView.FILTER_TYPE.ASPECT:
                counterString = GetComponent<AspectOrb>().aspectImpact + "";
                break;

            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                if (temperatureCountImpact > 0) counterString = "+" + temperatureCountImpact;
                else counterString = temperatureCountImpact + "";
                break;

            case StatBoardView.FILTER_TYPE.AETHER:
                counterString = aetherImpact + "";
                break;
        }
    }

    public void enableCounter(StatBoardView.FILTER_TYPE filter)
    {
        statToShow = filter;
        switch (filter)
        {
            case StatBoardView.FILTER_TYPE.POINTS:
                if (gameObject.GetComponent<AspectOrb>()) counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary[gameObject.GetComponent<AspectOrb>().orbColor + "" + Level].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                break;
            case StatBoardView.FILTER_TYPE.ASPECT:
                if (gameObject.GetComponent<AspectOrb>()) counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary[gameObject.GetComponent<AspectOrb>().orbColor + "" + Level].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                break;
            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                if (fiery && frozen) counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary["ice"].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                else if (fiery) counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary["red1"].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                else if (frozen) counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary["blue1"].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                break;
            case StatBoardView.FILTER_TYPE.AETHER:
                counter.GetComponent<TextMeshPro>().color = MixingBoard.orbDictionary["green1"].GetComponent<Orb>().counter.GetComponent<TextMeshPro>().color;
                break;
        }
        counter.SetActive(true);
    }

    public void instantAppear()
    {
        dissolvingAppearing = false;
        coreSphereRenderer.material.SetFloat("DissolvingVector", 0);
        if(symbolRenderer != null)
        {
            var tempColor = symbolRenderer.color;
            tempColor.a = 1;
            symbolRenderer.color = tempColor;
        }
       
    }


    public void fallingReset()
    {
        falling = false;
    }

    public void moveDown()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveUp()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveRight()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    public void moveLeft()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x - 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", movingTime, "easetype", iTween.EaseType.easeInSine));
    }

    private void moveRightCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", destroyingTimer, "easetype", iTween.EaseType.easeInOutBack));
        DestroyIn(destroyingTimer);

    }

    private void moveLeftCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x - 1, this.transform.localPosition.y, this.transform.localPosition.z), "islocal", true, "time", destroyingTimer, "easetype", iTween.EaseType.easeInOutBack));
        DestroyIn(destroyingTimer);
    }

    private void moveUpCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, this.transform.localPosition.z), "islocal", true, "time", destroyingTimer, "easetype", iTween.EaseType.easeInOutBack));
        DestroyIn(destroyingTimer);
    }

    private void moveDownCombine()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z), "islocal", true, "time", destroyingTimer, "easetype", iTween.EaseType.easeInOutBack));
        DestroyIn(destroyingTimer);
    }

    public void addAffectingSystem (ParticleSystem particleSystem)
    {
        affectingSystem = Instantiate(particleSystem, transform);
    }

    public void DestroyIn(double time)
    {

        points = 0;
        aetherCount = 0;
        mixingBoard.spinDelay = Math.Max(mixingBoard.spinDelay, .25);
        if (symbolRenderer)
        {
            symbolRenderer.enabled = false;
        }
        if (antimatterSymbolRenderer)
        {
            antimatterSymbolRenderer.enabled = false;
        }
        if (fireParticles)
        {
            fireParticles.GetComponent<FireParticlesList>().fire.Stop();
            fireParticles.GetComponent<FireParticlesList>().fireDark.Stop();
        }

        shouldDestroyed = true;
        if(orbit) orbit.SetActive (false);
        currentDestroyingTimer = time;
        startDestroyTimer = time;
        channeling = false;
        if (particleSphere)
        {
            particleSphere.Stop();
            particleSphere.Clear();
        }
        if(outerSphereRenderer) outerSphereRenderer.enabled = false;
    }

    public abstract void affectWith(EFFECT_TYPES effect);

    public void startChanneling(float time, CHANNELING_MODES mode)
    {
        if (!channeling)
        {
            channeling = true;
            channelingTime = time;
            this.mode = mode;
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            channelingParticleSystem = Instantiate(particleSystemSample, new Vector3(0, 0, 0), Quaternion.identity);
            channelingParticleSystem.transform.SetParent(this.transform, false);
            var main = channelingParticleSystem.main;
            main.startLifetime = time;
            main.startColor = channelParticleColor;
        }
      
    }

    public void startChanneling(float time, ReplacingOrbStruct replacingOrb)
    {
        if (!channeling)
        {
            channeling = true;
            channelingTime = time;
            this.mode = CHANNELING_MODES.REPLACE;
            this.replacingOrb = replacingOrb;
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            channelingParticleSystem = Instantiate(particleSystemSample, new Vector3(0, 0, 0), Quaternion.identity);
            channelingParticleSystem.transform.SetParent(this.transform, false);
            var main = channelingParticleSystem.main;
            main.startLifetime = time;
            main.startColor = channelParticleColor;
        }
    }

    protected void addAether (int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle;
            if (frozen) particle = Instantiate(MixingBoard.orbDictionary["ice"].GetComponent<Orb>().aetherParticle, orbit.transform);
            else particle = Instantiate(aetherParticle, orbit.transform);

            aetherParticles.Add(particle);
        }
        aetherCount += count;
        double updatedAngle = 360 / aetherImpact;
        double countingAngle = updatedAngle*2;
        foreach (GameObject particle in aetherParticles)
        {
            countingAngle += updatedAngle;
            double angleDif = particle.GetComponent<AetherParticle>().updatedAngle - countingAngle;
            particle.GetComponent<AetherParticle>().updatedAngle = countingAngle;
            iTween.RotateAdd(particle, iTween.Hash("x", -angleDif, "islocal", true));
        }
    }

    public void decreaseAether (int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle = aetherParticles[aetherParticles.Count - 1].gameObject;
            aetherParticles.Remove(particle);
            Destroy(particle);

        }

        aetherCount -= count;
        if (aetherImpact <= 0)
        {
            DestroyIn(.5);
            return;
        }


        double updatedAngle = 360 / aetherImpact;
        double countingAngle = updatedAngle * 2;
        foreach (GameObject particle in aetherParticles)
        {
            countingAngle += updatedAngle;
            //particle.transform.localEulerAngles = new Vector3(0, 0, 0);
            double angleDif = particle.GetComponent<AetherParticle>().updatedAngle - countingAngle;
            particle.GetComponent<AetherParticle>().updatedAngle = countingAngle;
            iTween.RotateAdd(particle, iTween.Hash("x", -angleDif, "islocal", true));
        }

    }

    protected void addAntimatter()
    {
        symbolRenderer.gameObject.SetActive(false);
        antimatterSymbolRenderer.gameObject.SetActive(true);
        if (level == 3)
        {
            coreSphereRenderer.GetComponent<Floating>().enabled = true;
        }
        channelParticleColor = new Color(221, 0, 231);
        antimatter = true;
    }

    public void chanellingBreak()
    {
        comboAvaliable = true;
        channelingParticleSystem.Clear();
        Destroy(channelingParticleSystem.gameObject);
        channeling = false;
        countOfReactionsIn = 0;
    }

    int oldX;
    public void shakeRight()
    {
        oldX = (int)gameObject.transform.localPosition.x;
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(gameObject.transform.localPosition.x + .1f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), "islocal", true, "time", .1f, "easetype", iTween.EaseType.easeInOutCubic));
        Invoke("shakeLeft", 0.1f);
        Invoke("shakeReset", 0.2f);
    }

    void shakeLeft()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(gameObject.transform.localPosition.x - .2f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), "islocal", true, "time", .1f, "easetype", iTween.EaseType.easeInOutCubic));
    }

    void shakeReset()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(oldX, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), "islocal", true, "time", .05f, "easetype", iTween.EaseType.easeInOutElastic));
    }

    public static bool operator true(Orb orb)
    {
        return orb != null;
    }

    public static bool operator false(Orb orb)
    {
        return orb == null;
    }
}
