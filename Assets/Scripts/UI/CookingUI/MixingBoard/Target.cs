using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Target : MonoBehaviour
{

    static double minTargetX = 0.5;
    static double maxTargetX = MixingBoard.Width - 1.5;
    static double minTargetY = 0.5;
    static double maxTargetY = MixingBoard.Height - 1.5;

    [SerializeField]
    private GameObject targetFireLeft;

    [SerializeField]
    private GameObject targetFireRight;

    [SerializeField]
    private GameObject targetFire;

    [SerializeField]
    private GameObject targetIceLeft;

    [SerializeField]
    private GameObject targetIceRight;

    [SerializeField]
    private GameObject targetIce;

    [SerializeField]
    private GameObject targetAether;

    [SerializeField]
    private GameObject targetAntimatter;

    [SerializeField]
    private GameObject targetVoidLeft;

    [SerializeField]
    private GameObject targetVoidRight;

    [SerializeField]
    private GameObject targetVoid;

    [SerializeField]
    private GameObject targetDyeBlue;

    [SerializeField]
    private GameObject targetDyeRed;

    [SerializeField]
    private GameObject targetDyeGreen;

    [SerializeField]
    private GameObject targetLevelUp;

    [SerializeField]
    private GameObject targetUpLeft;

    [SerializeField]
    private GameObject targetUpRight;

    [SerializeField]
    private GameObject targetDownLeft;

    [SerializeField]
    private GameObject targetDownRight;


    CoreOrb spinCore;
    VoidOrb spinVoid;
    Orb leftOrbToDissolve;
    Orb rightOrbToDissolve;

    List<Orb> spinLeftOrbsAffected;
    List<Orb> spinRightOrbsAffected;

    static Dictionary<Orb.ORB_TYPES, GameObject> voidTargetsDictionary;

    GameObject[] defaultTargetList
    {
        get
        {
            return new GameObject[] { targetUpLeft, targetUpRight, targetDownLeft, targetDownRight };
        }
    }

    OrbBox upLeftBox;
    OrbBox upRightBox;
    OrbBox downLeftBox;
    OrbBox downRightBox;

    Orb [] TargetedOrbs
    {
        get
        {
            Orb[] orbs = new Orb[4];
            if (upLeftBox) orbs[0] = upLeftBox.Orb;
            if (upRightBox) orbs[1] = upRightBox.Orb;
            if (downLeftBox) orbs[2] = downLeftBox.Orb;
            if (downRightBox) orbs[3] = downRightBox.Orb;
            return orbs;
        }
    }

    Orb[] oldList;
    double oldX;
    double oldY;

    List<GameObject> drawnTargets;


    // Start is called before the first frame update
    void Start()
    {
        drawnTargets = new List<GameObject>();
        spinLeftOrbsAffected = new List<Orb>();
        spinRightOrbsAffected = new List<Orb>();

        if(voidTargetsDictionary == null)
        {
            voidTargetsDictionary = new Dictionary<Orb.ORB_TYPES, GameObject>();
            voidTargetsDictionary.Add(Orb.ORB_TYPES.VOID, targetAntimatter);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.ICE_VOID, targetIce);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.FIRE_VOID, targetFire);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.AETHER_VOID, targetAether);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.SUPERNOVA_VOID, targetLevelUp);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.BLUE_PULSAR, targetDyeBlue);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.RED_PULSAR, targetDyeRed);
            voidTargetsDictionary.Add(Orb.ORB_TYPES.GREEN_PULSAR, targetDyeGreen);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y + .5)])
            upLeftBox = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y + .5)].Box;
        else upLeftBox = null;
        if (MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y + .5)])
            upRightBox = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y + .5)].Box;
        else upRightBox = null;
        if (MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y - .5)])
            downLeftBox = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y - .5)].Box;
        else downLeftBox = null;
        if (MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y - .5)])
            downRightBox = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y - .5)].Box;
        else downRightBox = null;

        if (oldList == null) oldList = MixingBoard.StaticInstance.orbs.Cast<Orb>().ToArray();
        if (!Enumerable.SequenceEqual(oldList, MixingBoard.StaticInstance.orbs.Cast<Orb>().ToArray()) && MixingBoard.StaticInstance.stricted) {
            oldList = MixingBoard.StaticInstance.orbs.Cast<Orb>().ToArray();
            redrawTargets();
        }
        else if (oldX != transform.localPosition.x || oldY != transform.localPosition.y)
        {
            oldX = transform.localPosition.x;
            oldY = transform.localPosition.y;
            redrawTargets();
        }

        targetsAlphaReset();
    }

    public void redrawTargets()
    {
        if (CookingManager.addingMode) return;
        spinCore = null;
        spinVoid = null;
        leftOrbToDissolve = null;
        rightOrbToDissolve = null;
        spinLeftOrbsAffected.Clear();
        spinRightOrbsAffected.Clear();
        foreach (GameObject gameObject in drawnTargets) Destroy(gameObject);
        drawnTargets.Clear();

        if (upLeftBox && upLeftBox.xStricted && upLeftBox.yStricted && (upLeftBox.Orb.archetype == Orb.ORB_ARCHETYPES.CORE))
        {
            if (upLeftBox.GetComponentInChildren<CoreOrb>()) spinCore = upLeftBox.GetComponentInChildren<CoreOrb>();
            if (upLeftBox.Orb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upLeftBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(upLeftBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upLeftBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (upLeftBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upLeftBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftBox.Orb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upLeftBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(upLeftBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upLeftBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (upLeftBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upLeftBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (upLeftBox.Orb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }


            if (upLeftBox.Orb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftBox.Orb);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftBox.Orb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftBox.Orb);
                    drawnTargets.Add(target);
                }


            }
            if (upLeftBox.Orb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftBox.Orb);
                    drawnTargets.Add(target);
                }
            }
        }

        else if (upRightBox && upRightBox.xStricted && upRightBox.yStricted && upRightBox.Orb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            if (upRightBox.GetComponentInChildren<CoreOrb>()) spinCore = upRightBox.GetComponentInChildren<CoreOrb>();
            if (upRightBox.Orb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upRightBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(upRightBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upRightBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (upRightBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upRightBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upRightBox.Orb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upRightBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(upRightBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upRightBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (upRightBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upRightBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (upRightBox.Orb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (upRightBox.Orb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightBox.Orb);
                    drawnTargets.Add(target);
                }
            }

            if (upRightBox.Orb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightBox.Orb);
                    drawnTargets.Add(target);
                }


            }
            if (upRightBox.Orb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightBox.Orb);
                    drawnTargets.Add(target);
                }
            }
            if (upRightBox.GetComponent<VoidOrb>()) spinVoid = upRightBox.GetComponent<VoidOrb>();


        }
        else if (downLeftBox && downLeftBox.xStricted && downLeftBox.yStricted && downLeftBox.Orb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            if (downLeftBox.GetComponentInChildren<CoreOrb>()) spinCore = downLeftBox.GetComponentInChildren<CoreOrb>();
            if (downLeftBox.Orb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downLeftBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(downLeftBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downLeftBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (downLeftBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downLeftBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftBox.Orb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downLeftBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(downLeftBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downLeftBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (downLeftBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downLeftBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (downLeftBox.Orb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (downLeftBox.Orb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftBox.Orb);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftBox.Orb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftBox.Orb);
                    drawnTargets.Add(target);
                }
            }
            if (downLeftBox.Orb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftBox.Orb);
                    drawnTargets.Add(target);
                }
            }
        }

        else if (downRightBox && downRightBox.xStricted && downRightBox.yStricted && downRightBox.Orb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            if (downRightBox.GetComponentInChildren<CoreOrb>()) spinCore = downRightBox.GetComponentInChildren<CoreOrb>();
            if (downRightBox.Orb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downRightBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(downRightBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downRightBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (downRightBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downRightBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downRightBox.Orb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downRightBox.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(downRightBox.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downRightBox.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Width; i++)
                {
                    if (downRightBox.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downRightBox.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (downRightBox.Orb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (downRightBox.Orb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightBox.Orb);
                    drawnTargets.Add(target);
                }
            }

            if (downRightBox.Orb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightBox.Orb);
                    drawnTargets.Add(target);
                }
            }
            if (downRightBox.Orb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightBox.Orb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightBox.Orb);
                    drawnTargets.Add(target);
                }
            }
        }
        else if (upLeftBox && upLeftBox.xStricted && upLeftBox.yStricted && upLeftBox.Orb.archetype == Orb.ORB_ARCHETYPES.VOID)
        {
            spinVoid = upLeftBox.GetComponentInChildren<VoidOrb>();

            List<GameObject> targetsToPaint = new List<GameObject>();

            if (upRightBox) rightOrbToDissolve = upRightBox.Orb;
            else if (downRightBox) rightOrbToDissolve = downRightBox.Orb;
            else if (downLeftBox) rightOrbToDissolve = downLeftBox.Orb;

            if (downLeftBox) leftOrbToDissolve = downLeftBox.Orb;
            else if (downRightBox) leftOrbToDissolve = downRightBox.Orb;
            else if (upRightBox) leftOrbToDissolve = upRightBox.Orb;

            if (leftOrbToDissolve != null && rightOrbToDissolve != null)
            {
                if (leftOrbToDissolve == rightOrbToDissolve)
                {
                    GameObject target = Instantiate(targetVoid, transform);
                    target.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    targetsToPaint.Add(target);
                }
                else
                {
                    GameObject targetLeft = Instantiate(targetVoidLeft, transform);
                    GameObject targetRight = Instantiate(targetVoidRight, transform);
                    targetLeft.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    targetRight.transform.localPosition = new Vector3(rightOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, rightOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(targetLeft);
                    drawnTargets.Add(targetRight);
                    targetsToPaint.Add(targetLeft);
                    targetsToPaint.Add(targetRight);
                    spinLeftOrbsAffected.Add(rightOrbToDissolve);
                    spinRightOrbsAffected.Add(leftOrbToDissolve);

                }
                if (downRightBox && downRightBox.Orb != leftOrbToDissolve && downRightBox.Orb != rightOrbToDissolve)
                {
                    GameObject target = Instantiate(voidTargetsDictionary[spinVoid.type], transform);
                    target.transform.localPosition = new Vector3(downRightBox.transform.localPosition.x - transform.localPosition.x, downRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    spinLeftOrbsAffected.Add(downRightBox.Orb);
                    spinRightOrbsAffected.Add(downRightBox.Orb);
                }
                foreach (GameObject gameObject in targetsToPaint)
                {
                    SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                    if (sr) sr.color = spinVoid.channelParticleColor;
                }
            }

        }
        else if (upRightBox && upRightBox.xStricted && upRightBox.yStricted && upRightBox.Orb.archetype == Orb.ORB_ARCHETYPES.VOID)
        {
            spinVoid = upRightBox.GetComponentInChildren<VoidOrb>();

            List<GameObject> targetsToPaint = new List<GameObject>();

            if (downRightBox) rightOrbToDissolve = downRightBox.Orb;
            else if (downLeftBox) rightOrbToDissolve = downLeftBox.Orb;
            else if (upLeftBox) rightOrbToDissolve = upLeftBox.Orb;

            if (upLeftBox) leftOrbToDissolve = upLeftBox.Orb;
            else if (downLeftBox) leftOrbToDissolve = downLeftBox.Orb;
            else if (downRightBox) leftOrbToDissolve = downRightBox.Orb;

            if (leftOrbToDissolve != null && rightOrbToDissolve != null)
            {
                if (leftOrbToDissolve == rightOrbToDissolve)
                {
                    GameObject target = Instantiate(targetVoid, transform);
                    target.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    targetsToPaint.Add(target);
                }
                else
                {
                    GameObject targetLeft = Instantiate(targetVoidLeft, transform);
                    GameObject targetRight = Instantiate(targetVoidRight, transform);
                    targetLeft.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    targetRight.transform.localPosition = new Vector3(rightOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, rightOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(targetLeft);
                    drawnTargets.Add(targetRight);
                    targetsToPaint.Add(targetLeft);
                    targetsToPaint.Add(targetRight);
                    spinLeftOrbsAffected.Add(rightOrbToDissolve);
                    spinRightOrbsAffected.Add(leftOrbToDissolve);

                }
                if (downLeftBox && downLeftBox.Orb != leftOrbToDissolve && downLeftBox.Orb != rightOrbToDissolve)
                {
                    GameObject target = Instantiate(voidTargetsDictionary[spinVoid.type], transform);
                    target.transform.localPosition = new Vector3(downLeftBox.transform.localPosition.x - transform.localPosition.x, downLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    spinLeftOrbsAffected.Add(downLeftBox.Orb);
                    spinRightOrbsAffected.Add(downLeftBox.Orb);
                }
                foreach (GameObject gameObject in targetsToPaint)
                {
                    SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                    if (sr) sr.color = spinVoid.channelParticleColor;
                }
            }
        }

        else if (downLeftBox && downLeftBox.xStricted && downLeftBox.yStricted && downLeftBox.Orb.archetype == Orb.ORB_ARCHETYPES.VOID)
        {
            spinVoid = downLeftBox.GetComponentInChildren<VoidOrb>();

            List<GameObject> targetsToPaint = new List<GameObject>();

            if (upLeftBox) rightOrbToDissolve = upLeftBox.Orb;
            else if (upRightBox) rightOrbToDissolve = upRightBox.Orb;
            else if (downRightBox) rightOrbToDissolve = downRightBox.Orb;

            if (downRightBox) leftOrbToDissolve = downRightBox.Orb;
            else if (upRightBox) leftOrbToDissolve = upRightBox.Orb;
            else if (upLeftBox) leftOrbToDissolve = upLeftBox.Orb;

            if (leftOrbToDissolve != null && rightOrbToDissolve != null)
            {
                if (leftOrbToDissolve == rightOrbToDissolve)
                {
                    GameObject target = Instantiate(targetVoid, transform);
                    target.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    targetsToPaint.Add(target);
                }
                else
                {
                    GameObject targetLeft = Instantiate(targetVoidLeft, transform);
                    GameObject targetRight = Instantiate(targetVoidRight, transform);
                    targetLeft.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    targetRight.transform.localPosition = new Vector3(rightOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, rightOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(targetLeft);
                    drawnTargets.Add(targetRight);
                    targetsToPaint.Add(targetLeft);
                    targetsToPaint.Add(targetRight);
                    spinLeftOrbsAffected.Add(rightOrbToDissolve);
                    spinRightOrbsAffected.Add(leftOrbToDissolve);

                }
                if (upRightBox && upRightBox.Orb != leftOrbToDissolve && upRightBox.Orb != rightOrbToDissolve)
                {
                    GameObject target = Instantiate(voidTargetsDictionary[spinVoid.type], transform);
                    target.transform.localPosition = new Vector3(upRightBox.transform.localPosition.x - transform.localPosition.x, upRightBox.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    spinLeftOrbsAffected.Add(upRightBox.Orb);
                    spinRightOrbsAffected.Add(upRightBox.Orb);
                }
                foreach (GameObject gameObject in targetsToPaint)
                {
                    SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                    if (sr) sr.color = spinVoid.channelParticleColor;
                }
            }
        }


        else if (downRightBox && downRightBox.xStricted && downRightBox.yStricted && downRightBox.Orb.archetype == Orb.ORB_ARCHETYPES.VOID)
        {
            spinVoid = downRightBox.GetComponentInChildren<VoidOrb>();


            List<GameObject> targetsToPaint = new List<GameObject>();

            if (downLeftBox) rightOrbToDissolve = downLeftBox.Orb;
            else if (upLeftBox) rightOrbToDissolve = upLeftBox.Orb;
            else if (upRightBox) rightOrbToDissolve = upRightBox.Orb;

            if (upRightBox) leftOrbToDissolve = upRightBox.Orb;
            else if (upLeftBox) leftOrbToDissolve = upLeftBox.Orb;
            else if (downLeftBox) leftOrbToDissolve = downLeftBox.Orb;

            if (leftOrbToDissolve != null && rightOrbToDissolve != null)
            {
                if (leftOrbToDissolve == rightOrbToDissolve)
                {
                    GameObject target = Instantiate(targetVoid, transform);
                    target.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    targetsToPaint.Add(target);
                }
                else
                {
                    GameObject targetLeft = Instantiate(targetVoidLeft, transform);
                    GameObject targetRight = Instantiate(targetVoidRight, transform);
                    targetLeft.transform.localPosition = new Vector3(leftOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, leftOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    targetRight.transform.localPosition = new Vector3(rightOrbToDissolve.Box.transform.localPosition.x - transform.localPosition.x, rightOrbToDissolve.Box.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(targetLeft);
                    drawnTargets.Add(targetRight);
                    targetsToPaint.Add(targetLeft);
                    targetsToPaint.Add(targetRight);
                    spinLeftOrbsAffected.Add(rightOrbToDissolve);
                    spinRightOrbsAffected.Add(leftOrbToDissolve);

                }
                if (upLeftBox && upLeftBox.Orb != leftOrbToDissolve && upLeftBox.Orb != rightOrbToDissolve)
                {
                    GameObject target = Instantiate(voidTargetsDictionary[spinVoid.type], transform);
                    target.transform.localPosition = new Vector3(upLeftBox.transform.localPosition.x - transform.localPosition.x, upLeftBox.transform.localPosition.y - transform.localPosition.y, 0);
                    drawnTargets.Add(target);
                    spinLeftOrbsAffected.Add(upLeftBox.Orb);
                    spinRightOrbsAffected.Add(upLeftBox.Orb);
                }
                foreach (GameObject gameObject in targetsToPaint)
                {
                    SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                    if (sr) sr.color = spinVoid.channelParticleColor;
                }
            }
        }
    }

    public void targetsAlphaReset()
    {
        foreach (GameObject defaultTarget in defaultTargetList)
        {
            bool occupied = false;
            foreach (GameObject target in drawnTargets)
            {
                if (Math.Round(defaultTarget.transform.localPosition.x, 1) == Math.Round(target.transform.localPosition.x, 1) && Math.Round(defaultTarget.transform.localPosition.y, 1) == Math.Round(target.transform.localPosition.y, 1))
                {
                    occupied = true;
                }
            }

            if (occupied) defaultTarget.GetComponent<SpriteRenderer>().color = new Color(defaultTarget.GetComponent<SpriteRenderer>().color.r, defaultTarget.GetComponent<SpriteRenderer>().color.g, defaultTarget.GetComponent<SpriteRenderer>().color.b, 0);
            else defaultTarget.GetComponent<SpriteRenderer>().color = new Color(defaultTarget.GetComponent<SpriteRenderer>().color.r, defaultTarget.GetComponent<SpriteRenderer>().color.g, defaultTarget.GetComponent<SpriteRenderer>().color.b, 1);
        }

    }

    public GameObject addInfoTarget (float x, float y, GameObject infoTargetSampler)
    {
        GameObject infoTarget = Instantiate(infoTargetSampler, transform);
        infoTarget.transform.localPosition = new Vector3(x, y, 0);
        drawnTargets.Add(infoTarget);
       
        foreach (GameObject drawnTarget in drawnTargets)
        {
            if(Math.Round(drawnTarget.transform.localPosition.x, 1) == Math.Round(x, 1) && Math.Round(drawnTarget.transform.localPosition.y, 1) == Math.Round(y, 1))
            {
                drawnTarget.GetComponent<SpriteRenderer>().color = new Color(drawnTarget.GetComponent<SpriteRenderer>().color.r, drawnTarget.GetComponent<SpriteRenderer>().color.g, drawnTarget.GetComponent<SpriteRenderer>().color.b, 0);
            }
        }
        infoTarget.GetComponent<SpriteRenderer>().color = new Color(infoTarget.GetComponent<SpriteRenderer>().color.r, infoTarget.GetComponent<SpriteRenderer>().color.g, infoTarget.GetComponent<SpriteRenderer>().color.b, 1);
        return infoTarget;
    }

    public void removeInfoTarget (GameObject infoTarget)
    {
        drawnTargets.Remove(infoTarget);
        Destroy(infoTarget);
        redrawTargets();
    }

    public void spinLeft()
    {
        if (MixingBoard.StaticInstance.spinDelay <= 0)
        {
            MixingBoard.StaticInstance.moveDelay += .2;
            if (MixingBoard.StaticInstance.stable && MixingBoard.StaticInstance.currentTargetDelay <= 0)
            {

                if (spinCore)
                {
                    int aetherToIncrease = spinCore.aetherCount;
                    spinCore.DestroyIn(0.5);
                    foreach (Orb orb in spinLeftOrbsAffected)
                    {
                        if(orb)
                        {
                            orb.affectWith(spinCore.coreEffect, aetherToIncrease, spinCore.archetype);
                        }
                    }
                    if (spinCore.type == Orb.ORB_TYPES.GREEN_DYE_CORE || spinCore.type == Orb.ORB_TYPES.RED_DYE_CORE || spinCore.type == Orb.ORB_TYPES.BLUE_DYE_CORE) addDyeParticles(spinCore.particleSystemSample);
                    if (spinCore.type == Orb.ORB_TYPES.FIRE_CORE || spinCore.type == Orb.ORB_TYPES.ICE_CORE || spinCore.type == Orb.ORB_TYPES.AETHER_CORE || spinCore.type == Orb.ORB_TYPES.SUPERNOVA_CORE) addAffectParticles(spinCore.particleSystemSample, spinLeftOrbsAffected, spinCore);
                    MixingBoard.StaticInstance.spinDelay += .5;
                }

                if (spinVoid)
                {
                    foreach (Orb orb in spinLeftOrbsAffected)
                    {
                        if (orb)
                        {
                            orb.affectWith(spinVoid.voidEffect, spinVoid.aetherCount, spinVoid.archetype);
                        }
                    }
                    if (leftOrbToDissolve) leftOrbToDissolve.affectWith(Orb.EFFECT_TYPES.DISSOLVE);
                    if (spinLeftOrbsAffected.Count != 0) addAffectParticles(spinVoid.particleSystemSample, spinRightOrbsAffected, null);
                    MixingBoard.StaticInstance.spinDelay += .5;
                }

                if (upLeftBox)
                {
                    upLeftBox.moveDown();
                    if (upLeftBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upLeftBox.Orb);
                    if (upLeftBox.Orb.aetherImpact != 0) {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upLeftBox.Orb.Level == 3 || upLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || upLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {
                           
                        }
                        else upLeftBox.Orb.decreaseAether(1);
                    }


                }

                if (upRightBox)
                {
                    upRightBox.moveLeft();
                    if (upRightBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upRightBox.Orb);
                    if (upRightBox.Orb.aetherImpact != 0)
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upRightBox.Orb.Level == 3 || upRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || upRightBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {

                        }
                        else upRightBox.Orb.decreaseAether(1);
                    }
                }

                if (downLeftBox)
                {
                    downLeftBox.moveRight();
                    if (downLeftBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downLeftBox.Orb);
                    if (downLeftBox.Orb.aetherImpact != 0)
                    {
                    if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downLeftBox.Orb.Level == 3 || downLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || downLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                    {

                        }
                        else downLeftBox.Orb.decreaseAether(1);
                    }
                }

                if (downRightBox)
                {
                    downRightBox.moveUp();
                    if (downRightBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downRightBox.Orb);
                    if (downRightBox.Orb.aetherImpact != 0)
                    {
                    if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downRightBox.Orb.Level == 3 || downRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || downRightBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID  || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                    {

                        }
                        else downRightBox.Orb.decreaseAether(1);
                    }
                }
                MixingBoard.StaticInstance.currentTargetDelay += .25;
            }
        }

    public void spinRight()
    {
        if (MixingBoard.StaticInstance.spinDelay <= 0)
        {
            MixingBoard.StaticInstance.moveDelay += .2;
            if (MixingBoard.StaticInstance.stable && MixingBoard.StaticInstance.currentTargetDelay <= 0)
            {
                if (spinCore)
                {
                    int aetherToIncrease = spinCore.aetherCount;
                    spinCore.DestroyIn(0.5);
                    foreach (Orb orb in spinRightOrbsAffected)
                    {
                        if(orb)
                        {
                            orb.affectWith(spinCore.coreEffect, aetherToIncrease, spinCore.archetype);
                        }
                    }
                    if (spinCore.type == Orb.ORB_TYPES.GREEN_DYE_CORE || spinCore.type == Orb.ORB_TYPES.RED_DYE_CORE || spinCore.type == Orb.ORB_TYPES.BLUE_DYE_CORE) addDyeParticles(spinCore.particleSystemSample);
                    if (spinCore.type == Orb.ORB_TYPES.FIRE_CORE || spinCore.type == Orb.ORB_TYPES.ICE_CORE || spinCore.type == Orb.ORB_TYPES.AETHER_CORE || spinCore.type == Orb.ORB_TYPES.SUPERNOVA_CORE) addAffectParticles(spinCore.particleSystemSample, spinRightOrbsAffected, spinCore);
                    MixingBoard.StaticInstance.spinDelay += .5;
                }

                if (spinVoid)
                {
                    foreach (Orb orb in spinRightOrbsAffected)
                    {
                        if (orb)
                        {
                            orb.affectWith(spinVoid.voidEffect, spinVoid.aetherCount, spinVoid.archetype);
                        }
                    }
                    if (rightOrbToDissolve) rightOrbToDissolve.affectWith(Orb.EFFECT_TYPES.DISSOLVE);
                    if (spinRightOrbsAffected.Count != 0) addAffectParticles(spinVoid.particleSystemSample, spinRightOrbsAffected, null);
                    MixingBoard.StaticInstance.spinDelay += .5;
                }

                if (upLeftBox)
                {
                    upLeftBox.moveRight();
                    if (upLeftBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upLeftBox.Orb);
                    if (upLeftBox.Orb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upLeftBox.Orb.Level == 3 || upLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || upLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {

                        }
                        else upLeftBox.Orb.decreaseAether(1);
                    }
                }

                if (upRightBox)
                {
                    upRightBox.moveDown();
                    if (upRightBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upRightBox.Orb);
                    if (upRightBox.Orb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upRightBox.Orb.Level == 3 || upRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || upRightBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {

                        }
                        else upRightBox.Orb.decreaseAether(1);
                    }
                }

                if (downLeftBox)
                {
                    downLeftBox.moveUp();
                    if (downLeftBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downLeftBox.Orb);
                    if (downLeftBox.Orb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downLeftBox.Orb.Level == 3 || downLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || downLeftBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {

                        }
                        else downLeftBox.Orb.decreaseAether(1);
                    }
                }

                if (downRightBox)
                {
                    downRightBox.moveLeft();
                    if (downRightBox.Orb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downRightBox.Orb);
                    if (downRightBox.Orb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downRightBox.Orb.Level == 3 || downRightBox.Orb.type == Orb.ORB_TYPES.AETHER_CORE || downRightBox.Orb.type == Orb.ORB_TYPES.AETHER_VOID || (spinVoid && spinVoid.type == Orb.ORB_TYPES.AETHER_VOID))
                        {

                        }
                        else downRightBox.Orb.decreaseAether(1);
                    }
                }
                spinCore = null;
                MixingBoard.StaticInstance.currentTargetDelay += .25;
            }
        }
    }

    public void moveTargetDown()
    {
        if (MixingBoard.StaticInstance.moveDelay <= 0)
        {
            MixingBoard.StaticInstance.spinDelay += .1;
            if (Math.Round(transform.localPosition.y - 1, 1) >= Math.Round(minTargetY, 1) && MixingBoard.StaticInstance.currentTargetDelay <= 0)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z);
            MixingBoard.StaticInstance.currentTargetDelay = MixingBoard.StaticInstance.targetDelay / 2;
        }
    }

    public void moveTargetUp()
    {
        if (MixingBoard.StaticInstance.moveDelay <= 0)
        {
            MixingBoard.StaticInstance.spinDelay = .1;
            if (Math.Round(transform.localPosition.y + 1, 1) <= Math.Round(maxTargetY, 1) && MixingBoard.StaticInstance.currentTargetDelay <= 0)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
            MixingBoard.StaticInstance.currentTargetDelay = MixingBoard.StaticInstance.targetDelay / 2;
        }
    }

    public void moveTargetRight()
    {
        if (MixingBoard.StaticInstance.moveDelay <= 0)
        {
            MixingBoard.StaticInstance.spinDelay = .1;
            if (Math.Round(transform.localPosition.x + 1, 1) <= Math.Round(maxTargetX, 1) && MixingBoard.StaticInstance.currentTargetDelay <= 0)
                transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
            MixingBoard.StaticInstance.currentTargetDelay = MixingBoard.StaticInstance.targetDelay / 2;
        }
    }

    public void moveTargetLeft()
    {
        if (MixingBoard.StaticInstance.moveDelay <= 0)
        {
            MixingBoard.StaticInstance.spinDelay = .1;
            if (Math.Round(transform.localPosition.x - 1, 1) >= Math.Round(minTargetX, 1) && MixingBoard.StaticInstance.currentTargetDelay <= 0)
                transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
            MixingBoard.StaticInstance.currentTargetDelay = MixingBoard.StaticInstance.targetDelay / 2;
        }

    }

    void addDyeParticles(ParticleSystem particleSystem)
    {
        MixingBoard.StaticInstance.spinDelay += .35;
        ParticleSystem dyeSystem = Instantiate(particleSystem, MixingBoard.StaticInstance.OrbShift.transform);
        dyeSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y - .5), Convert.ToSingle(-1));
        ParticleSystem dyeSystem2 = Instantiate(particleSystem, MixingBoard.StaticInstance.OrbShift.transform);
        dyeSystem2.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y + .5), Convert.ToSingle(-1));
        ParticleSystem dyeSystem3 = Instantiate(particleSystem, MixingBoard.StaticInstance.OrbShift.transform);
        dyeSystem3.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y - .5), Convert.ToSingle(-1));
        ParticleSystem dyeSystem4 = Instantiate(particleSystem, MixingBoard.StaticInstance.OrbShift.transform);
        dyeSystem4.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y + .5), Convert.ToSingle(-1));
    }

    void addAffectParticles (ParticleSystem particleSystem, List<Orb> affectedOrbs, Orb core)
    {
        MixingBoard.StaticInstance.spinDelay += .35;
        List<Orb> orbListToAffect = new List<Orb>(affectedOrbs);
        if(core) orbListToAffect.Add(core);

        foreach (Orb orb in orbListToAffect)
        {
            if(orb!= null)
            {
                orb.addAffectingSystem(particleSystem);
            }
        }
    }

    void addSupernovaTargets()
    {
        if ((int)Math.Round(spinCore.Box.transform.localPosition.x + 1) < MixingBoard.Width)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.Box.transform.localPosition.x + 1 - transform.localPosition.x, spinCore.Box.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x + 1), (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x + 1), (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.Box.transform.localPosition.x - 1) >= 0)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.Box.transform.localPosition.x - 1 - transform.localPosition.x, spinCore.Box.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x - 1), (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x - 1), (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.Box.transform.localPosition.y + 1) < MixingBoard.Height)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.Box.transform.localPosition.x - transform.localPosition.x, spinCore.Box.transform.localPosition.y + 1 - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), (int)Math.Round(spinCore.Box.transform.localPosition.y + 1)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), (int)Math.Round(spinCore.Box.transform.localPosition.y + 1)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.Box.transform.localPosition.y - 1) >= 0)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.Box.transform.localPosition.x - transform.localPosition.x, spinCore.Box.transform.localPosition.y - 1 - transform.localPosition.y, 0);

            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), (int)Math.Round(spinCore.Box.transform.localPosition.y - 1)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), (int)Math.Round(spinCore.Box.transform.localPosition.y - 1)]);

            drawnTargets.Add(target);
        }
    }

    void addAetherTarget()
    {
        for (int i = 0; i < MixingBoard.Height; i++)
        {
            if (spinCore.Box.transform.localPosition.y == i) continue;
            GameObject target = Instantiate(targetAether, transform);
            target.transform.localPosition = new Vector3(spinCore.Box.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), i]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.Box.transform.localPosition.x), i]);
            drawnTargets.Add(target);
        }
        for (int i = 0; i < MixingBoard.Width; i++)
        {
            if (spinCore.Box.transform.localPosition.x == i) continue;
            GameObject target = Instantiate(targetAether, transform);
            target.transform.localPosition = new Vector3(i - transform.localPosition.x, spinCore.Box.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(spinCore.Box.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }
    }
}
