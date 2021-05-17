using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Target : MonoBehaviour
{

    static double minTargetX = 0.5;
    static double maxTargetX = MixingBoard.Length - 1.5;
    static double minTargetY = 0.5;
    static double maxTargetY = MixingBoard.Height - 1.5;

    [SerializeField]
    private GameObject targetFireLeft;

    [SerializeField]
    private GameObject targetFireRight;

    [SerializeField]
    private GameObject targetIceLeft;

    [SerializeField]
    private GameObject targetIceRight;

    [SerializeField]
    private GameObject targetAether;

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
    List<Orb> spinLeftOrbsAffected;
    List<Orb> spinRightOrbsAffected;

    GameObject[] defaultTargetList
    {
        get
        {
            return new GameObject[] { targetUpLeft, targetUpRight, targetDownLeft, targetDownRight };
        }
    }

    public Orb upLeftOrb
    {
        get;
        private set;
    }

    public Orb upRightOrb
    {
        get;
        private set;
    }

    public Orb downLeftOrb
    {
        get;
        private set;
    }

    public Orb downRightOrb
    {
        get;
        private set;
    }

    public Orb [] TargetedOrbs
    {
        get
        {
            return new Orb[4] {upLeftOrb, upRightOrb, downLeftOrb, downRightOrb};
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

    }


    // Update is called once per frame
    void Update()
    {
        upLeftOrb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y + .5)];
        upRightOrb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y + .5)];
        downLeftOrb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - .5), (int)Math.Round(transform.localPosition.y - .5)];
        downRightOrb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + .5), (int)Math.Round(transform.localPosition.y - .5)];

        if(oldList == null) oldList = MixingBoard.StaticInstance.orbs.Cast<Orb>().ToArray();
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
        spinCore = null;
        spinLeftOrbsAffected.Clear();
        spinRightOrbsAffected.Clear();
        foreach (GameObject gameObject in drawnTargets) Destroy(gameObject);
        drawnTargets.Clear();

        if (upLeftOrb && upLeftOrb.xStricted && upLeftOrb.yStricted && upLeftOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            spinCore = upLeftOrb.GetComponent<CoreOrb>();
            if (upLeftOrb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upLeftOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(upLeftOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upLeftOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (upLeftOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upLeftOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upLeftOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftOrb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upLeftOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(upLeftOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upLeftOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (upLeftOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upLeftOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upLeftOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftOrb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (upLeftOrb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }
               

            if (upLeftOrb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftOrb);
                    drawnTargets.Add(target);
                }
            }

            if (upLeftOrb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftOrb);
                    drawnTargets.Add(target);
                }


            }
            if (upLeftOrb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upLeftOrb);
                    drawnTargets.Add(target);
                }
            }
        }

        else if (upRightOrb && upRightOrb.xStricted && upRightOrb.yStricted && upRightOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            spinCore = upRightOrb.GetComponent<CoreOrb>();
            if (upRightOrb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upRightOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(upRightOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upRightOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (upRightOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upRightOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upRightOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upRightOrb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (upRightOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(upRightOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(upRightOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (upRightOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, upRightOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(upRightOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (upRightOrb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (upRightOrb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (upRightOrb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightOrb);
                    drawnTargets.Add(target);
                }
            }

            if (upRightOrb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightOrb);
                    drawnTargets.Add(target);
                }


            }
            if (upRightOrb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(upRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(upRightOrb);
                    drawnTargets.Add(target);
                }
            }
        }
        else if (downLeftOrb && downLeftOrb.xStricted && downLeftOrb.yStricted && downLeftOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            spinCore = downLeftOrb.GetComponent<CoreOrb>();
            if (downLeftOrb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downLeftOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(downLeftOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downLeftOrb.transform.localPosition.x), i]); 
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (downLeftOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downLeftOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downLeftOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftOrb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downLeftOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(downLeftOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downLeftOrb.transform.localPosition.x), i]); 
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (downLeftOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downLeftOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downLeftOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftOrb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (downLeftOrb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (downLeftOrb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftOrb);
                    drawnTargets.Add(target);
                }
            }

            if (downLeftOrb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftOrb);
                    drawnTargets.Add(target);
                }
            }
            if (downLeftOrb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downLeftOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downLeftOrb);
                    drawnTargets.Add(target);
                }
            }
        }

        else if (downRightOrb && downRightOrb.xStricted && downRightOrb.yStricted && downRightOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
        {
            spinCore = downRightOrb.GetComponent<CoreOrb>();
            if (downRightOrb.type == Orb.ORB_TYPES.FIRE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downRightOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetFireLeft, transform);
                    target.transform.localPosition = new Vector3(downRightOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downRightOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (downRightOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetFireRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downRightOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downRightOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downRightOrb.type == Orb.ORB_TYPES.ICE_CORE)
            {
                for (int i = 0; i < MixingBoard.Height; i++)
                {
                    if (downRightOrb.transform.localPosition.y == i) continue;
                    GameObject target = Instantiate(targetIceLeft, transform);
                    target.transform.localPosition = new Vector3(downRightOrb.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
                    spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(downRightOrb.transform.localPosition.x), i]);
                    drawnTargets.Add(target);
                }
                for (int i = 0; i < MixingBoard.Length; i++)
                {
                    if (downRightOrb.transform.localPosition.x == i) continue;
                    GameObject target = Instantiate(targetIceRight, transform);
                    target.transform.localPosition = new Vector3(i - transform.localPosition.x, downRightOrb.transform.localPosition.y - transform.localPosition.y, 0);
                    spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(downRightOrb.transform.localPosition.y)]);
                    drawnTargets.Add(target);
                }
            }

            if (downRightOrb.type == Orb.ORB_TYPES.AETHER_CORE)
            {
                addAetherTarget();
            }

            if (downRightOrb.type == Orb.ORB_TYPES.SUPERNOVA_CORE)
            {
                addSupernovaTargets();
            }

            if (downRightOrb.type == Orb.ORB_TYPES.BLUE_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeBlue, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightOrb);
                    drawnTargets.Add(target);
                }
            }

            if (downRightOrb.type == Orb.ORB_TYPES.RED_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeRed, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightOrb);
                    drawnTargets.Add(target);
                }
            }
            if (downRightOrb.type == Orb.ORB_TYPES.GREEN_DYE_CORE)
            {
                foreach (GameObject defaultTarget in defaultTargetList)
                {
                    GameObject target = Instantiate(targetDyeGreen, transform);
                    target.transform.localPosition = new Vector3(defaultTarget.transform.localPosition.x, defaultTarget.transform.localPosition.y, 0);
                    spinRightOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinRightOrbsAffected.Remove(downRightOrb);
                    spinLeftOrbsAffected = new List<Orb>(TargetedOrbs);
                    spinLeftOrbsAffected.Remove(downRightOrb);
                    drawnTargets.Add(target);
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

                if (spinCore != null)
                {
                    foreach (Orb orb in spinLeftOrbsAffected)
                    {
                        if(orb)
                        {
                            orb.affectWith(spinCore.coreEffect);
                        }
                    }
                    if (spinCore.type == Orb.ORB_TYPES.GREEN_DYE_CORE || spinCore.type == Orb.ORB_TYPES.RED_DYE_CORE || spinCore.type == Orb.ORB_TYPES.BLUE_DYE_CORE) addDyeParticles(spinCore.particleSystemSample);
                    if (spinCore.type == Orb.ORB_TYPES.FIRE_CORE || spinCore.type == Orb.ORB_TYPES.ICE_CORE || spinCore.type == Orb.ORB_TYPES.AETHER_CORE || spinCore.type == Orb.ORB_TYPES.SUPERNOVA_CORE) addAffectParticles(spinCore.particleSystemSample, spinLeftOrbsAffected, spinCore);
                    MixingBoard.StaticInstance.spinDelay += .5;
                    spinCore.DestroyIn(0.5);
                }

                if (upLeftOrb)
                {
                    upLeftOrb.moveDown();
                    if (upLeftOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upLeftOrb);
                    if (upLeftOrb.aetherImpact != 0) {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upLeftOrb.Level == 3)
                        {
                           
                        }
                        else upLeftOrb.decreaseAether(1);
                    }


                }

                if (upRightOrb)
                {
                    upRightOrb.moveLeft();
                    if (upRightOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upRightOrb);
                    if (upRightOrb.aetherImpact != 0)
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upRightOrb.Level == 3)
                        {

                        }
                        else upRightOrb.decreaseAether(1);
                    }
                }

                if (downLeftOrb)
                {
                    downLeftOrb.moveRight();
                    if (downLeftOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downLeftOrb);
                    if (downLeftOrb.aetherImpact != 0)
                    {
                    if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downLeftOrb.Level == 3)
                    {

                        }
                        else downLeftOrb.decreaseAether(1);
                    }
                }

                if (downRightOrb)
                {
                    downRightOrb.moveUp();
                    if (downRightOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downRightOrb);
                    if (downRightOrb.aetherImpact != 0)
                    {
                    if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downRightOrb.Level == 3)
                    {

                        }
                        else downRightOrb.decreaseAether(1);
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
                    foreach (Orb orb in spinRightOrbsAffected)
                    {
                        if (orb)
                        {
                            orb.affectWith(spinCore.coreEffect);
                        }
                    }
                    if (spinCore.type == Orb.ORB_TYPES.GREEN_DYE_CORE || spinCore.type == Orb.ORB_TYPES.RED_DYE_CORE || spinCore.type == Orb.ORB_TYPES.BLUE_DYE_CORE) addDyeParticles(spinCore.particleSystemSample);
                    if (spinCore.type == Orb.ORB_TYPES.FIRE_CORE || spinCore.type == Orb.ORB_TYPES.ICE_CORE || spinCore.type == Orb.ORB_TYPES.AETHER_CORE || spinCore.type == Orb.ORB_TYPES.SUPERNOVA_CORE) addAffectParticles(spinCore.particleSystemSample, spinRightOrbsAffected, spinCore);
                    
                    spinCore.DestroyIn(0.5);
                }

                if (upLeftOrb)
                {
                    upLeftOrb.moveRight();
                    if (upLeftOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upLeftOrb);
                    if (upLeftOrb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upLeftOrb.Level == 3)
                        {

                        }
                        else upLeftOrb.decreaseAether(1);
                    }
                }

                if (upRightOrb)
                {
                    upRightOrb.moveDown();
                    if (upRightOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(upRightOrb);
                    if (upRightOrb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || upRightOrb.Level == 3)
                        {

                        }
                        else upRightOrb.decreaseAether(1);
                    }
                }

                if (downLeftOrb)
                {
                    downLeftOrb.moveUp();
                    if (downLeftOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downLeftOrb);
                    if (downLeftOrb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downLeftOrb.Level == 3)
                        {

                        }
                        else downLeftOrb.decreaseAether(1);
                    }
                }

                if (downRightOrb)
                {
                    downRightOrb.moveLeft();
                    if (downRightOrb.channeling) MixingBoard.StaticInstance.breakReactionsWith(downRightOrb);
                    if (downRightOrb.aetherImpact != 0)
                    {
                        if ((spinCore && spinCore.type == Orb.ORB_TYPES.AETHER_CORE) || downRightOrb.Level == 3)
                        {

                        }
                        else downRightOrb.decreaseAether(1);
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
        MixingBoard.StaticInstance.spinDelay += .5;
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
        MixingBoard.StaticInstance.spinDelay += .5;
        List<Orb> orbListToAffect = new List<Orb>(affectedOrbs);
        orbListToAffect.Add(core);

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
        if ((int)Math.Round(spinCore.transform.localPosition.x + 1) < MixingBoard.Length)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.transform.localPosition.x + 1 - transform.localPosition.x, spinCore.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x + 1), (int)Math.Round(spinCore.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x + 1), (int)Math.Round(spinCore.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.transform.localPosition.x - 1) >= 0)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.transform.localPosition.x - 1 - transform.localPosition.x, spinCore.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x - 1), (int)Math.Round(spinCore.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x - 1), (int)Math.Round(spinCore.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.transform.localPosition.y + 1) < MixingBoard.Height)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.transform.localPosition.x - transform.localPosition.x, spinCore.transform.localPosition.y + 1 - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), (int)Math.Round(spinCore.transform.localPosition.y + 1)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), (int)Math.Round(spinCore.transform.localPosition.y + 1)]);
            drawnTargets.Add(target);
        }

        if ((int)Math.Round(spinCore.transform.localPosition.y - 1) >= 0)
        {
            GameObject target = Instantiate(targetLevelUp, transform);
            target.transform.transform.localPosition = new Vector3(spinCore.transform.localPosition.x - transform.localPosition.x, spinCore.transform.localPosition.y - 1 - transform.localPosition.y, 0);

            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), (int)Math.Round(spinCore.transform.localPosition.y - 1)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), (int)Math.Round(spinCore.transform.localPosition.y - 1)]);

            drawnTargets.Add(target);
        }
    }

    void addAetherTarget()
    {
        for (int i = 0; i < MixingBoard.Height; i++)
        {
            if (spinCore.transform.localPosition.y == i) continue;
            GameObject target = Instantiate(targetAether, transform);
            target.transform.localPosition = new Vector3(spinCore.transform.localPosition.x - transform.localPosition.x, i - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), i]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[(int)Math.Round(spinCore.transform.localPosition.x), i]);
            drawnTargets.Add(target);
        }
        for (int i = 0; i < MixingBoard.Length; i++)
        {
            if (spinCore.transform.localPosition.x == i) continue;
            GameObject target = Instantiate(targetAether, transform);
            target.transform.localPosition = new Vector3(i - transform.localPosition.x, spinCore.transform.localPosition.y - transform.localPosition.y, 0);
            spinLeftOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(spinCore.transform.localPosition.y)]);
            spinRightOrbsAffected.Add(MixingBoard.StaticInstance.orbs[i, (int)Math.Round(spinCore.transform.localPosition.y)]);
            drawnTargets.Add(target);
        }
    }
}
