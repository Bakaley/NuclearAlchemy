using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

using UnityEditor;

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
        BLUE_PULSAR,
        RED_PULSAR,
        GREEN_PULSAR,
        //GOLD_DYE_VOID,
        //BLACK_DYE_VOID
        BLUE_DROP,
        RED_DROP,
        GREEN_DROP,
        FIRE_DROP,
        ICE_DROP,
        AETHER_DROP,
        ANTIMATTER_DROP,
        SUPERNOVA_DROP,
        UNCERTAINTY
    }

    public enum ORB_ARCHETYPES
    {
        NONE,
        ASPECT,
        NEORGANIC,
        CORE,
        VOID,
        DROP,
        UNCERTAINTY
    }

    public static Dictionary <ORB_TYPES, ORB_ARCHETYPES> typeArchetypeDictionary { get; private set; }

    public static readonly double destroyingTimer = .35;

    double startDestroyTimer = 0;
    double currentDestroyingTimer = 0;
    double currentAppearingTimer = 0;
    double channelingTime = 0;

    public static readonly float aetherMultiplier = .25f;

    ParticleSystem affectingSystem;

    [SerializeField]
    public GameObject OrbPreview;

    public bool shouldDestroyed { get; private set; } = false;
    bool destoyBoxOnDestroyingOrb = true;

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
    protected string counterString;

    public OrbBox Box
    {
        get
        {
            return GetComponentInParent<OrbBox>();
        }
    }

    public void disableCounter()
    {
        counter.SetActive(false);
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
    public int voidnessImpact
    {
        get
        {
            if (!shouldDestroyed && archetype == ORB_ARCHETYPES.VOID)
            {
                if (aetherCount == 0) return 1;
                else return (int)(1 + 1 * aetherCount * aetherMultiplier);

            }
            else return 0;
        }
    }
    public int basicViscosity
    {
        get
        {
            if (Level >= 3) return 1;
            else return 0;
        }
    }
    public int viscosityImpact
    {
        get
        {
            if (!shouldDestroyed)
            {
                return (int)(basicViscosity + basicViscosity * aetherCount * aetherMultiplier);
            }
            else return 0;
        }
    }

    public bool comboAvaliable;

    public struct ReplacingOrbStruct
    {
        public GameObject baseOrb
        {
            get; private set;
        }
        public bool ice
        {
            get; private set;
        }
        public bool fire
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

        public List<ReplacingOrbStruct> uncertainList
        {
            get; private set;
        }

        public ReplacingOrbStruct(GameObject orbToReplace, bool fireFlag = false, bool iceFlag = false, int aetherCounter = 0, bool antimatterFlag = false, List<ReplacingOrbStruct> list = null)
        {
            Debug.Log(orbToReplace);
            baseOrb = orbToReplace.GetComponentInChildren<Orb>().gameObject;
            fire = fireFlag;
            ice = iceFlag;
            aether = aetherCounter;
            antimatter = antimatterFlag;
            uncertainList = list;
        }
    }

    protected ReplacingOrbStruct replacingOrb;

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
                return (int)(points + aetherCount * points * aetherMultiplier);
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

                if (type == ORB_TYPES.ICE_CORE || type == ORB_TYPES.ICE_VOID) count = -Level;
                else if (type == ORB_TYPES.FIRE_CORE || type == ORB_TYPES.FIRE_VOID) count = Level;
                else
                {
                    if ((int)Math.Round(Box.transform.localPosition.y) + 1 < MixingBoard.Height)
                    {
                        if (fiery && MixingBoard.StaticInstance.orbs[(int)Math.Round(Box.transform.localPosition.x), (int)Math.Round(Box.transform.localPosition.y) + 1]) count += Level + (int)Math.Round(Level * aetherMultiplier * aetherCount);
                    }
                    if (frozen && (int)Math.Round(Box.transform.localPosition.y) != 0) count -= Level + (int)Math.Round(Level * aetherMultiplier * aetherCount);
                }
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

            typeArchetypeDictionary.Add(ORB_TYPES.BLUE_PULSAR, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.RED_PULSAR, ORB_ARCHETYPES.VOID);
            typeArchetypeDictionary.Add(ORB_TYPES.GREEN_PULSAR, ORB_ARCHETYPES.VOID);

            typeArchetypeDictionary.Add(ORB_TYPES.BLUE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.RED_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.GREEN_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.SUPERNOVA_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.ICE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.FIRE_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.AETHER_DROP, ORB_ARCHETYPES.DROP);
            typeArchetypeDictionary.Add(ORB_TYPES.ANTIMATTER_DROP, ORB_ARCHETYPES.DROP);

            typeArchetypeDictionary.Add(ORB_TYPES.UNCERTAINTY, ORB_ARCHETYPES.UNCERTAINTY);
        }

        aetherParticles = new List<GameObject>();
        orbArchetype = typeArchetypeDictionary[type];
        dissolvingAppearing = true;

        if (type == ORB_TYPES.AETHER_CORE) aetherCount = 4;
        if (type == ORB_TYPES.AETHER_DROP) aetherCount = 5;
        Invoke("delayedAetherParticles", .05f);
    }

    void delayedAetherParticles()
    {
        if (aetherCount != 0)
        {
            instaintiateAetherParticles();
        }
    }

    public void setStartAether (int count)
    {
        aetherCount = count;
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


    }

    void synchronizeSymbolSeed()
    {
        if (coreSphereRenderer.gameObject.GetComponent<Floating>() != null) coreSphereRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
        if (symbolRenderer.gameObject.GetComponent<Floating>() != null) symbolRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
        if(antimatterSymbolRenderer) if (antimatterSymbolRenderer.gameObject.GetComponent<Floating>() != null) antimatterSymbolRenderer.gameObject.GetComponent<Floating>().seedShift = movingSeed;
    }

    virtual protected void FixedUpdate()
    {

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
                        DestroyIn(destroyingTimer);
                        Box.moveDownCombine();
                        break;
                    case CHANNELING_MODES.LEFT:
                        DestroyIn(destroyingTimer);
                        Box.moveLeftCombine();
                        break;
                    case CHANNELING_MODES.RIGHT:
                        DestroyIn(destroyingTimer);
                        Box.moveRightCombine();
                        break;
                    case CHANNELING_MODES.UP:
                        DestroyIn(destroyingTimer);
                        Box.moveUpCombine();
                        break;
                    case CHANNELING_MODES.CENTER:
                        DestroyIn(destroyingTimer);
                        break;
                    case CHANNELING_MODES.REPLACE:
                        //symbolRenderer.enabled = false;
                        GameObject orbObject = Instantiate(replacingOrb.baseOrb, Box.transform);
                        Orb orb = orbObject.GetComponent<Orb>();
                        Box.Orb = orb;
                        if (replacingOrb.fire) orb.addFire();
                        if (replacingOrb.ice) orb.addIce();
                        if (replacingOrb.aether != 0) orb.aetherCount = replacingOrb.aether;
                        if (replacingOrb.antimatter) orb.addAntimatter();
                        if (replacingOrb.uncertainList != null) orb.GetComponent<UncertaintyOrb>().uncertainOrbsList = replacingOrb.uncertainList;

                        DestroyIn(destroyingTimer, false);
                        break;
                }
                MixingBoard.StaticInstance.finishReactionsWith(this);
            }
        }

        if (shouldDestroyed)
            if (currentDestroyingTimer >= 0)
            {
                currentDestroyingTimer -= Time.deltaTime;

                if ((GetComponent<CoreOrb>() && GetComponent<CoreOrb>().coreSphereList))
                {
                    foreach (MeshRenderer meshRenderer in GetComponent<CoreOrb>().coreSphereList.GetComponent<CoreSphereList>().meshRenderers)
                    {
                        meshRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(.67 - (currentDestroyingTimer/destroyingTimer)/2));
                    }
                }

                if(GetComponent<DropOrb>() && GetComponent<DropOrb>().coreSphereList)
                {
                    foreach (MeshRenderer meshRenderer in GetComponent<DropOrb>().coreSphereList.GetComponent<CoreSphereList>().meshRenderers)
                    {
                        meshRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(.67 - (currentDestroyingTimer / destroyingTimer) / 2));
                    }
                }

                coreSphereRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(.67 - (currentDestroyingTimer / destroyingTimer) / 2));
                if (symbolRenderer)
                {
                    symbolRenderer.color = new Color(symbolRenderer.color.r, symbolRenderer.color.b, symbolRenderer.color.g, Convert.ToSingle((currentDestroyingTimer / destroyingTimer)));
                }
            }

            else
            {
                if (affectingSystem)
                {
                    affectingSystem.transform.SetParent(MixingBoard.StaticInstance.transform, true);
                }
                if(destoyBoxOnDestroyingOrb) Destroy(Box.gameObject);
                else Destroy(gameObject);
            }

        if (dissolvingAppearing)
        {
            if (currentAppearingTimer >= 0)
            {
                currentAppearingTimer -= Time.deltaTime;
                coreSphereRenderer.material.SetFloat("DissolvingVector", Convert.ToSingle(currentAppearingTimer * 2));

                if (symbolRenderer != null)
                {
                    var tempColor = symbolRenderer.color;
                    tempColor.a = Convert.ToSingle(1 - currentAppearingTimer);
                    symbolRenderer.color = tempColor;
                }


            }
        }
        if (Box && Box.transform.parent.gameObject != MixingBoard.StaticInstance.OrbShift && dissolvingAppearing == true) instantAppear();

        if (aetherTimerDelay >= 0)
        {
            aetherTimerDelay -= Time.deltaTime;
        }
        else
        {
            if (aetherFlagDelay)
            {
                aetherFlagDelay = false;
                increaseAether(delayedAether);
                delayedAether = 0;
            }
        }
    }


    public void enableCounter(StatBoardView.FILTER_TYPE filter)
    {
        statToShow = filter;
        switch (filter)
        {
            case StatBoardView.FILTER_TYPE.POINTS:
                if (gameObject.GetComponent<AspectOrb>()) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.orbDictionary[gameObject.GetComponent<AspectOrb>().orbColor + "" + Level].GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                counterString = pointsImpact + "";
                break;
            case StatBoardView.FILTER_TYPE.ASPECT:
                if (gameObject.GetComponent<AspectOrb>()) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.orbDictionary[gameObject.GetComponent<AspectOrb>().orbColor + "" + Level].GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                else if (gameObject.GetComponent<VoidOrb>()) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.orbDictionary[AspectOrb.colorDictionary[type] + "3"].GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                counterString = GetComponent<AspectImpactInterface>().aspectImpact + "";
                break;
            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                if (fiery && frozen) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.StaticInstance.iceOrbSample.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                else if (fiery) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.StaticInstance.red1.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                else if (frozen) counter.GetComponentInChildren<TextMeshPro>().color = MixingBoard.StaticInstance.blue1.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                if (temperatureCountImpact > 0) counterString = "+" + temperatureCountImpact;
                else counterString = temperatureCountImpact + "";
                break;
            case StatBoardView.FILTER_TYPE.AETHERNESS:
                counter.GetComponent<TextMeshPro>().color = MixingBoard.StaticInstance.green1.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                counterString = aetherImpact + "";
                break;
            case StatBoardView.FILTER_TYPE.VISCOSITY:
                counter.GetComponent<TextMeshPro>().color = MixingBoard.StaticInstance.yellowCore.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                counterString = viscosityImpact + "";
                break;
            case StatBoardView.FILTER_TYPE.VOIDNESS:
                counter.GetComponent<TextMeshPro>().color = MixingBoard.StaticInstance.purpleVoid.GetComponentInChildren<Orb>().counter.GetComponent<TextMeshPro>().color;
                counterString = voidnessImpact + "";
                break;
        }
        counter.GetComponent<TextMeshPro>().text = counterString;
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



    public void addAffectingSystem (ParticleSystem particleSystem)
    {
        affectingSystem = Instantiate(particleSystem, Box.transform);
    }

    public void DestroyIn(double time, bool destoyBoxOnDestroyingOrb = true)
    {
        this.destoyBoxOnDestroyingOrb = destoyBoxOnDestroyingOrb;
        comboAvaliable = false;
        points = 0;
        aetherCount = 0;
        MixingBoard.StaticInstance.spinDelay = Math.Max(MixingBoard.StaticInstance.spinDelay, .1);
        if (symbolRenderer)
        {
            //symbolRenderer.enabled = false;
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
        if (archetype == ORB_ARCHETYPES.VOID) coreSphereRenderer.enabled = false;

    }

    public abstract void affectWith(EFFECT_TYPES effect, int aetherIncreaseOn = 0, Orb.ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE);

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

    private bool aetherFlagDelay = false;
    private double aetherTimerDelay = 0;
    private int delayedAether = 0;

    void instaintiateAetherParticles()
    {
        for (int i = 0; i < aetherCount; i++)
        {
            GameObject particle;
            if (frozen) particle = Instantiate(MixingBoard.orbDictionary["Ice"].GetComponent<Orb>().aetherParticle, orbit.transform);
            else particle = Instantiate(aetherParticle, orbit.transform);

            aetherParticles.Add(particle);
        }
        double updatedAngle = 360 / aetherCount;
        double countingAngle = updatedAngle * 2;
        foreach (GameObject particle in aetherParticles)
        {
            countingAngle += updatedAngle;
            double angleDif = particle.GetComponent<AetherParticle>().updatedAngle - countingAngle;
            particle.GetComponent<AetherParticle>().updatedAngle = countingAngle;
            iTween.RotateAdd(particle, iTween.Hash("x", -angleDif, "islocal", true));
        }
    }

    protected void increaseAether (int count)
    {
        if (aetherTimerDelay >= 0)
        {
            delayedAether += count;
            aetherFlagDelay = true;
        }
        else
        {
            aetherTimerDelay = 1f;
            for (int i = 0; i < count; i++)
            {
                GameObject particle;
                if (frozen) particle = Instantiate(MixingBoard.orbDictionary["Ice"].GetComponent<Orb>().aetherParticle, orbit.transform);
                else particle = Instantiate(aetherParticle, orbit.transform);

                aetherParticles.Add(particle);
            }
            aetherCount += count;
            double updatedAngle = 360 / aetherCount;
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
    }

    public void setAether (int count)
    {
        if (aetherCount != count)
        {
            if (count > aetherCount) increaseAether(count - aetherCount);
            else decreaseAether(aetherCount - count);
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
        if (aetherCount <= 0)
        {
            DestroyIn(.5);
            return;
        }

        double updatedAngle = 360 / aetherCount;
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

    public void setAetherParticlesRenderingOrder(int number)
    {
        ParticleSystemRenderer[] particles = orbit.GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (ParticleSystemRenderer particle in particles)
        {
            particle.sortingOrder = number;
        }
    }



    protected void addFire()
    {
        fireParticles.gameObject.SetActive(true);
        fiery = true;
    }

    protected void addIce()
    {
        GameObject iceOrb = MixingBoard.orbDictionary["Ice"];

        if (coreSphereRenderer.GetComponent<Floating>() != null) coreSphereRenderer.GetComponent<Floating>().enabled = false;
        Invoke("freezeMaterial", .5f);

        if (symbolRenderer)
        {
            symbolRenderer.color = MixingBoard.StaticInstance.iceOrbSample.GetComponent<AspectOrb>().symbolRenderer.color;
            Animation animation = symbolRenderer.GetComponent<Animation>();
            if (animation)
            {
                animation.Stop();
            }
            string part = "";
            if (gameObject.name.IndexOf(' ') != -1)
                part = gameObject.name.Substring(0, gameObject.name.IndexOf(' '));
            else if (gameObject.name.IndexOf('(') != -1)
                part = gameObject.name.Substring(0, gameObject.name.IndexOf('('));
            else part = gameObject.name;

            if(archetype == ORB_ARCHETYPES.ASPECT)
            {
                Debug.Log(GetComponent<AspectOrb>().orbColor + "" + Level);
                symbolRenderer.transform.localPosition = MixingBoard.orbDictionary[GetComponent<AspectOrb>().orbColor + "" + Level ].GetComponentInChildren<Orb>().symbolRenderer.transform.localPosition;
                symbolRenderer.transform.localRotation = MixingBoard.orbDictionary[GetComponent<AspectOrb>().orbColor + "" + Level + ""].GetComponentInChildren<Orb>().symbolRenderer.transform.localRotation;
                symbolRenderer.transform.localScale = MixingBoard.orbDictionary[GetComponent<AspectOrb>().orbColor + ""  + Level].GetComponentInChildren<Orb>().symbolRenderer.transform.localScale;
            }
            else if (type == ORB_TYPES.SEMIPLASMA)
            {
                symbolRenderer.transform.localPosition = MixingBoard.orbDictionary["Grey1"].GetComponentInChildren<Orb>().symbolRenderer.transform.localPosition;
                symbolRenderer.transform.localRotation = MixingBoard.orbDictionary["Grey1"].GetComponentInChildren<Orb>().symbolRenderer.transform.localRotation;
                symbolRenderer.transform.localScale = MixingBoard.orbDictionary["Grey1"].GetComponentInChildren<Orb>().symbolRenderer.transform.localScale;
            }

            if (symbolRenderer.GetComponent<Floating>() != null) symbolRenderer.GetComponent<Floating>().enabled = false;
        }

        if (particleSphere)
        {
            var main = particleSphere.main;
            main.startColor = MixingBoard.StaticInstance.iceOrbSample.GetComponent<AspectOrb>().particleSphere.main.startColor;
            particleSphere.Stop();
            particleSphere.Clear();
            particleSphere.Play();
        }

        FireParticlesList[] firelist = gameObject.GetComponentsInChildren<FireParticlesList>(true);
        foreach (FireParticlesList fireSystem in firelist)
        {
            var sparksMain = fireSystem.sparks.main;
            sparksMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).sparks.main.startColor;
            var fireMain = fireSystem.fire.main;
            fireMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fire.main.startColor;
            var fireDarkMain = fireSystem.fireDark.main;
            fireDarkMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fireDark.main.startColor;
            fireSystem.fireDark.GetComponent<ParticleSystemRenderer>().material = fireSystem.fire.GetComponent<ParticleSystemRenderer>().material;
        }

        if (counter.GetComponent<TextMeshPro>())
        {
            counter.GetComponent<TextMeshPro>().color = iceOrb.GetComponent<AspectOrb>().counter.GetComponent<TextMeshPro>().color;
        }

        if (aetherCount != 0)
        {
            foreach (GameObject particle in aetherParticles)
            {
                var particleMain = particle.GetComponentInChildren<ParticleSystem>().main;
                particleMain.startColor = iceOrb.GetComponent<AspectOrb>().aetherParticle.GetComponentInChildren<ParticleSystem>().main.startColor;
            }
        }

        this.channelParticleColor = iceOrb.GetComponent<AspectOrb>().channelParticleColor;
        iceParticles.SetActive(true);
        frozen = true;
    }

    void freezeMaterial()
    {
        coreSphereRenderer.material = MixingBoard.StaticInstance.iceOrbSample.GetComponent<AspectOrb>().coreSphereRenderer.sharedMaterial;
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
    protected void levelUp()
    {
        if (nextLevelOrb)
        {
            replacingOrb = new ReplacingOrbStruct(nextLevelOrb, fiery, frozen, aetherCount, antimatter);
            replace(replacingOrb);
        }
    }

    public void replace (ReplacingOrbStruct replacingOrbStruct)
    {
        replacingOrb = replacingOrbStruct;

        GameObject newOrb = Instantiate(replacingOrb.baseOrb, Box.transform);
        Orb orb = newOrb.GetComponent<Orb>();
        Box.Orb = orb;
        if (archetype != ORB_ARCHETYPES.VOID)
        {
            if (replacingOrb.fire) orb.addFire();
            if (replacingOrb.ice) orb.addIce();
            if (replacingOrb.antimatter) orb.addAntimatter();
        }
        if (replacingOrb.aether != 0) orb.aetherCount = replacingOrb.aether;
        if (affectingSystem) affectingSystem.transform.SetParent(orb.transform, false);
        if (replacingOrb.uncertainList != null) orb.GetComponent<UncertaintyOrb>().uncertainOrbsList = replacingOrb.uncertainList;
        Destroy(gameObject);
        MixingBoard.StaticInstance.orbListUpdate();
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
