using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MixingBoard : MonoBehaviour
{
    public static int Length { get; private set; } = 4;
    public static int Height { get; private set; } = 4;
    public bool stricted { get; private set; }
    public double targetDelay { get; private set; } = Orb.movingTime;
    public bool stable { get; private set; }

    public static double ingredientMovementDelay = .5;
    public static double deployDelay = .5;

    [HideInInspector]
    public double moveDelay = .2;

    [HideInInspector]
    public double spinDelay = .2;

    static float channelingTime = 1f;

    [HideInInspector]
    public double currentCombiningTimer = 0;

    [HideInInspector]
    public double currentTargetDelay = 0;

    [SerializeField]
    public GameObject blue1;
    [SerializeField]
    public GameObject blue2;
    [SerializeField]
    public GameObject blue3;
    [SerializeField]
    public GameObject blueDye;
    [SerializeField]
    public GameObject bluePulsar;
    [SerializeField]
    public GameObject red1;
    [SerializeField]
    public GameObject red2;
    [SerializeField]
    public GameObject red3;
    [SerializeField]
    public GameObject redDye;
    [SerializeField]
    public GameObject redPulsar;
    [SerializeField]
    public GameObject green1;
    [SerializeField]
    public GameObject green2;
    [SerializeField]
    public GameObject green3;
    [SerializeField]
    public GameObject greenDye;
    [SerializeField]
    public GameObject greenPulsar;
    [SerializeField]
    public GameObject grey1;
    [SerializeField]
    public GameObject grey2_1;
    [SerializeField]
    public GameObject grey2_2;
    [SerializeField]
    public GameObject grey3;
    [SerializeField]
    public GameObject yellowCore;
    [SerializeField]
    public GameObject blueCore;
    [SerializeField]
    public GameObject redCore;
    [SerializeField]
    public GameObject greenCore;
    [SerializeField]
    public GameObject purpleVoid;
    [SerializeField]
    public GameObject blueVoid;
    [SerializeField]
    public GameObject redVoid;
    [SerializeField]
    public GameObject greenVoid;
    [SerializeField]
    public GameObject yellowVoid;
    [SerializeField]
    public GameObject iceOrbSample;

    public static Dictionary<string, GameObject> orbDictionary;

    [SerializeField]
    private GameObject targetGameObject;

    public Target target
    {
        get
        {
            return targetGameObject.GetComponent<Target>();
        }
    }

    SpriteRenderer[] targetSquares;

    [SerializeField]
    private GameObject orbShift;
    public GameObject OrbShift
    {
        get
        {
            return orbShift;
        }
        private set
        {
            orbShift = value;
        }
    }

    [SerializeField]
    StatBoardView statBoardView;
    [SerializeField]
    AddingBoard addingBoard;
    [SerializeField]
    EssencePanel essencePanel;

    static MixingBoard staticBoard;

    public static MixingBoard StaticInstance
    {
        get
        {
            return staticBoard;
        }
    }

    ComboManager comboManager;

    public Orb[,] orbs = new Orb[Length, Height];

    private void Awake()
    {

        staticBoard = this;

        targetSquares = targetGameObject.GetComponentsInChildren<SpriteRenderer>();
        orbDictionary = new Dictionary<string, GameObject>();

        orbDictionary.Add("Blue1", blue1);
        orbDictionary.Add("Blue2", blue2);
        orbDictionary.Add("Blue3", blue3);
        orbDictionary.Add("BlueDye", blueDye);
        orbDictionary.Add("BluePulsar", bluePulsar);
        orbDictionary.Add("Red1", red1);
        orbDictionary.Add("Red2", red2);
        orbDictionary.Add("Red3", red3);
        orbDictionary.Add("RedDye", redDye);
        orbDictionary.Add("RedPulsar", redPulsar);
        orbDictionary.Add("Green1", green1);
        orbDictionary.Add("Green2", green2);
        orbDictionary.Add("Green3", green3);
        orbDictionary.Add("GreenDye", greenDye);
        orbDictionary.Add("GreenPulsar", greenPulsar);
        orbDictionary.Add("Grey1", grey1);
        orbDictionary.Add("Grey2_1", grey2_1);
        orbDictionary.Add("Grey2_2", grey2_2);
        orbDictionary.Add("Grey3", grey3);
        orbDictionary.Add("YellowCore", yellowCore);
        orbDictionary.Add("BlueCore", blueCore);
        orbDictionary.Add("RedCore", redCore);
        orbDictionary.Add("GreenCore", greenCore);
        orbDictionary.Add("PurpleVoid", purpleVoid);
        orbDictionary.Add("BlueVoid", blueVoid);
        orbDictionary.Add("RedVoid", redVoid);
        orbDictionary.Add("GreenVoid", greenVoid);
        orbDictionary.Add("YellowVoid", yellowVoid);
        orbDictionary.Add("Ice", iceOrbSample);

        comboManager = new ComboManager(this);
    }

    void Start()
    {
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        if (currentTargetDelay > 0) currentTargetDelay -= Time.deltaTime;
        if (currentCombiningTimer > 0) currentCombiningTimer -= Time.deltaTime;
        if (spinDelay > 0) spinDelay -= Time.deltaTime;
        if (moveDelay > 0) moveDelay -= Time.deltaTime;
        if (ingredientMovementDelay > 0) ingredientMovementDelay -= Time.deltaTime;
        if (deployDelay > 0) deployDelay -= Time.deltaTime;

        orbListUpdate();
        strictedCheck();
        stableCheck();

        comboManager.reactionCheck();
    }

    void strictedCheck()
    {
        stricted = true;
        foreach (Orb orb in orbs)
        {
            if (orb)
            {
                if (!orb.xStricted || !orb.yStricted)
                {
                   stricted = false;
                   break;
                }
            }
        }
    }

    void stableCheck()
    {
        if (stricted)
        {
            stable = true;
            foreach (Orb orb in orbs)
            {
                if (orb)
                {
                    if (orb.lying == false)
                    {
                        stable = false;
                        break;
                    }
                }
            }
        }
    }


    void orbListUpdate()
    {
        if (deployDelay <= 0)
        {
            orbs = new Orb[Length, Height];
            foreach (Transform child in orbShift.transform)
            {
                if (child.tag == "Orb")
                {
                    orbs[(int)Math.Round(child.transform.localPosition.x), (int)Math.Round(child.transform.localPosition.y)] = child.gameObject.GetComponent<Orb>();
                }
            }
        }
    }

    public void breakReactionsWith(Orb orb)
    {
        orb.chanellingBreak();
        List<Reaction> _reactionList = new List<Reaction>(comboManager.reactionList);
        foreach (Reaction reaction in _reactionList)
        {
            if (reaction.reagents.Contains(orb)) {
                foreach (Orb _orb in reaction.reagents)
                {
                    if (_orb.countOfReactionsIn >= 2)
                    {
                        breakReactionsWith(_orb);
                    }
                    _orb.chanellingBreak();
                }
                comboManager.reactionList.Remove(reaction);
            }
        }

        currentCombiningTimer = 0.25;
        comboManager.reactionReset();
    }

    public void finishReactionsWith(Orb orb)
    {
        List<Reaction> _reactionList = new List<Reaction>(comboManager.reactionList);
        foreach (Reaction reaction in _reactionList)
        {
            if (reaction.reagents.Contains(orb)) {
                comboManager.reactionList.Remove(reaction);
            }
        }
        //spinDelay = Math.Max(spinDelay, 1);
        foreach (SpriteRenderer spriteRenderer in targetSquares)
        {
            spriteRenderer.color = new Color32(91, 104, 114, 255);
        }

        Invoke("resetTargetColor", 0.5f);
        currentCombiningTimer = 0.25;
        comboManager.reactionReset();
    }

    void resetTargetColor()
    {
        foreach (SpriteRenderer spriteRenderer in targetSquares)
        {
            spriteRenderer.color = new Color32(43, 117, 173, 255);
        }
    }

    public void deployIngredient()
    {
        if (ingredientMovementDelay <= 0 && spinDelay <= 0 && deployDelay <= 0)
        {
            Orb[,] ingredientOrbs = new Orb[Length, Height];

            ingredientMovementDelay = .5;
            deployDelay = .5;

            int[] places = new int[Length];
           
            foreach (Transform child in addingBoard.OrbShift.transform)
            {
                if (child.tag == "Orb")
                {
                    ingredientOrbs[(int)Math.Round(child.transform.localPosition.x), (int)Math.Round(child.transform.localPosition.y)] = child.gameObject.GetComponent<Orb>();
                }
            }

            for (int i = 0; i < Length; i++)
            {
                if (ingredientOrbs[i, 0] && ingredientOrbs[i, 0].archetype == Orb.ORB_ARCHETYPES.DROP)
                {
                    Orb tartgetedOrb = null;
                    int targetY = -1;
                    foreach (Orb _orb in orbs)
                    {
                        if (_orb)
                        {
                            if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                            {
                                if ((int)Math.Round(_orb.gameObject.transform.localPosition.y) > targetY)
                                {
                                    targetY = (int)Math.Round(_orb.gameObject.transform.localPosition.y);
                                    tartgetedOrb = _orb;
                                }
                            }
                        }
                    }
                    if (tartgetedOrb)
                    {
                        ingredientOrbs[i, 0].GetComponent<DropOrb>().targetedtOrb = tartgetedOrb;
                        ingredientOrbs[i, 0].GetComponent<DropOrb>().targetY = (int)Math.Round(tartgetedOrb.gameObject.transform.localPosition.y);
                        places[i] = (int)Math.Round(tartgetedOrb.gameObject.transform.localPosition.y);
                    }
                    else
                    {
                        ingredientOrbs[i, 0].GetComponent<DropOrb>().targetY = 0;
                        places[i] = 0;
                    }
                }
                else
                {
                    int maxY = -1;
                    int columnSize = 0;
                    foreach (Orb _orb in orbs)
                    {
                        if (_orb)
                        {
                            if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                            {
                                if ((int)Math.Round(_orb.gameObject.transform.localPosition.y) > maxY) maxY = (int)Math.Round(_orb.gameObject.transform.localPosition.y);
                            }
                        }
                    }
                    foreach (Orb _orb in ingredientOrbs)
                    {
                        if (_orb)
                        {
                            if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                            {
                                columnSize++;
                            }
                        }
                    }
                    if (maxY + columnSize >= MixingBoard.Length)
                    {
                        foreach (Orb orb in ingredientOrbs)
                        {
                            if (orb)
                            {
                                orb.shakeRight();
                            }
                        }
                        return;
                    }

                    places[i] = maxY + 1;
                }
            }

            for (int i = 0; i < Length; i++)
            {
                foreach (Orb _orb in ingredientOrbs)
                {
                    if (_orb)
                    {
                        //int oldX = (int)Math.Round_orb.transform.localPosition.x;
                        _orb.transform.SetParent(orbShift.transform, true);
                        _orb.transform.localPosition = new Vector3(Mathf.Round(_orb.transform.localPosition.x), Mathf.Round(_orb.transform.localPosition.y), 0);

                    }
                }
            }

            float[] fallDistances = new float[Length];


            for (int i = 0; i < Length; i++)
            {
                float minY = 0;
                foreach (Orb _orb in ingredientOrbs)
                {
                    if (_orb)
                    {

                        if (minY == 0) minY = (int)Math.Round(_orb.gameObject.transform.localPosition.y);
                        if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                        {
                            if ((int)Math.Round(_orb.gameObject.transform.localPosition.y) < minY) minY = (int)Math.Round(_orb.gameObject.transform.localPosition.y);
                        }
                    }
                }
                fallDistances[i] = minY - places[i];
            }

            for (int i = 0; i < Length; i++)
            {
                foreach (Orb _orb in ingredientOrbs)
                {
                    if (_orb)
                    {
                        if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                        {
                            iTween.MoveTo(_orb.gameObject, iTween.Hash("position", new Vector3((int)Math.Round(_orb.gameObject.transform.localPosition.x), (int)Math.Round(_orb.gameObject.transform.localPosition.y) - fallDistances[i], _orb.transform.localPosition.z), "islocal", true, "time", Orb.movingTime * fallDistances[i] / 2, "easetype", iTween.EaseType.easeInSine));
                            spinDelay = Math.Max(spinDelay,  Orb.movingTime * fallDistances[i] * 1.05f);
                            ingredientMovementDelay = Orb.movingTime * fallDistances[i] * 1.05f;
                            deployDelay = 4 * Orb.movingTime * 1.05f;
                            currentCombiningTimer = 4 * Orb.movingTime * 1.05f;
                        }
                    }
                }
            }
            foreach (Ingredient.ESSENSE essence in IngredientPanel.currentIngredient.essenceList)
            {
               if(essence != Ingredient.ESSENSE.None) essencePanel.addEssence(essence);
            }
          
            addingBoard.refreshIngredients();
        }
    }

    public void cleanUpBoard()
    {
        foreach (Orb orb in orbs)
        {
            if(orb) orb.DestroyIn(.5f);
        }
    }

    private class ComboManager
    {
        public ComboManager(MixingBoard board)
        {
            this.board = board;
            Reaction.board = board;
        }

        MixingBoard board;
        public enum REACTION_DIMENSIONS
        {
            HORIZONTAL,
            VERTICAL
        }

        int min_1lvl_counter = 3;
        int max_1lvl_counter = 4;
        int min_2lvl_counter = 3;
        int max_2lvl_counter = 4;
        int semiplasma_counter = 4;
        int reaction_counter;
        Orb.ORB_TYPES reaction_type = Orb.ORB_TYPES.NONE;

        List<Orb> reagents = new List<Orb>();
        public List<Reaction> reactionList = new List<Reaction>();

        public enum CHECK_MODES
        {
            VERTICAL_CHECK,
            HORIZONTAL_CHECK,
            FULL_CHECK
        }

        public void reactionCheck()
        {
            if (board.stable && board.currentTargetDelay <= 0 && board.currentCombiningTimer <= 0)
            {

                for (int j = 0; j < Height; j++)
                {
                    reactionMinimumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                    for (int i = 0; i < Length; i++)
                    {
                        if (board.orbs[i, j] != null && board.orbs[i, j].comboAvaliable)
                        {
                            if (reagents.Count == 0)
                            {
                                reagents.Add(board.orbs[i, j]);
                                reaction_counter += board.orbs[i, j].comboCounter;
                                if (!board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                            }
                            else
                            {
                                Orb last = reagents[reagents.Count - 1];
                                if (((!board.orbs[i, j].frozen && (reaction_type == Orb.ORB_TYPES.NONE || reaction_type == board.orbs[i, j].type)) || board.orbs[i, j].frozen) && last.Level == board.orbs[i, j].Level)
                                {
                                    reagents.Add(board.orbs[i, j]);
                                    reaction_counter += board.orbs[i, j].comboCounter;
                                    if (reaction_type == Orb.ORB_TYPES.NONE && !board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                                }
                                else
                                {
                                    reactionMinimumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                                    reagents.Add(board.orbs[i, j]);
                                    reaction_counter += board.orbs[i, j].comboCounter;
                                    if (reaction_type == Orb.ORB_TYPES.NONE && !board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                                }
                            }
                            reactionMaximumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                        }
                        else
                        {
                            reactionMinimumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                        }
                    }
                }
                reactionMinimumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                for (int i = 0; i < Length; i++)
                {
                    reactionMinimumCheck(REACTION_DIMENSIONS.VERTICAL);

                    for (int j = 0; j < Height; j++)
                    {
                        if (board.orbs[i, j] != null && board.orbs[i, j].comboAvaliable)
                        {
                            if (reagents.Count == 0)
                            {
                                reagents.Add(board.orbs[i, j]);
                                reaction_counter += board.orbs[i, j].comboCounter;
                                if (!board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                            }
                            else
                            {
                                Orb last = reagents[reagents.Count - 1];
                                if (((!board.orbs[i, j].frozen && (reaction_type == Orb.ORB_TYPES.NONE || reaction_type == board.orbs[i, j].type)) || board.orbs[i, j].frozen) && last.Level == board.orbs[i, j].Level)
                                {
                                    reagents.Add(board.orbs[i, j]);
                                    reaction_counter += board.orbs[i, j].comboCounter;
                                    if (reaction_type == Orb.ORB_TYPES.NONE && !board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                                }
                                else
                                {

                                    reactionMinimumCheck(REACTION_DIMENSIONS.VERTICAL);
                                    reagents.Add(board.orbs[i, j]);
                                    reaction_counter += board.orbs[i, j].comboCounter;
                                    if (reaction_type == Orb.ORB_TYPES.NONE && !board.orbs[i, j].frozen) reaction_type = board.orbs[i, j].type;
                                }
                            }
                            reactionMaximumCheck(REACTION_DIMENSIONS.VERTICAL);
                        }
                        else
                        {
                            reactionMinimumCheck(REACTION_DIMENSIONS.VERTICAL);
                        }
                    }
                }
                reactionMinimumCheck(REACTION_DIMENSIONS.VERTICAL);
                if (reactionList.Count != 0) channelingDistribution();
            }
        }

        void reactionMinimumCheck(REACTION_DIMENSIONS dimension)
        {
            if (reagents.Count != 0)
            {
                if (reaction_type == Orb.ORB_TYPES.SEMIPLASMA)
                {
                    if (reaction_counter >= semiplasma_counter)
                    {
                        int aether = 0;
                        bool antimatterFlag = false;
                        List<GameObject> coreList = new List<GameObject>();
                        foreach (Orb orb in reagents)
                        {
                            if (orb.fiery && !coreList.Contains(board.redCore)) coreList.Add(board.redCore);
                            if (orb.frozen && !coreList.Contains(board.blueCore)) coreList.Add(board.blueCore);
                            if (orb.aetherCount != 0) aether += orb.aetherCount;
                            if (orb.antimatter) antimatterFlag = true;
                        }
                        Reaction reaction = null;
                        if (antimatterFlag) reaction = new Reaction(dimension, reagents, coreList, board.purpleVoid, 0, false);
                        else if (aether != 0)
                        {
                            coreList.Add(board.yellowCore);
                            reaction = new Reaction(dimension, reagents, coreList, board.greenCore, aether, false);
                        }
                        else reaction = new Reaction(dimension, reagents, coreList, board.yellowCore, 0, false);

                        if (!reactionList.Contains(reaction))
                        {
                            reactionList.Add(reaction);
                        }

                    }
                }

                else if (reagents[reagents.Count - 1].Level == 1)
                {
                    if (reaction_counter == min_1lvl_counter)
                    {
                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;

                        foreach (Orb orb in reagents)
                        {
                            if (!orb.frozen) nextLevelOrb = orb.NextLevelOrb;
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        if (nextLevelOrb == null)
                        {
                            reactionReset();
                        }
                        else
                        {
                            Reaction reaction = new Reaction(dimension, reagents, new List<GameObject>(), nextLevelOrb, aether, antimatter);
                            if (!reactionList.Contains(reaction))
                            {
                                reactionList.Add(reaction);
                            }
                        }
                    }
                }

                else if (reagents[reagents.Count - 1].Level == 2)
                {
                    if (reaction_counter == min_2lvl_counter)
                    {
                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;
                        foreach (Orb orb in reagents)
                        {
                            if (!orb.frozen) nextLevelOrb = orb.NextLevelOrb;
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        if (nextLevelOrb == null)
                        {
                            reactionReset();
                        }
                        else
                        {
                            if (antimatter)
                            {
                                bool fiery = false;
                                bool frozen = false;
                                bool aetherFlag = false;
                                foreach (Orb orb in reagents)
                                {
                                    if (orb.fiery) fiery = true;
                                    if (orb.frozen) frozen = true;
                                    if (orb.aetherCount != 0) aetherFlag = true;
                                }

                                if (fiery && frozen)
                                {
                                    if (dimension == REACTION_DIMENSIONS.HORIZONTAL) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                                    else nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                                }
                                else if (fiery) nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                                else if (frozen) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                                else if (aetherFlag) nextLevelOrb = MixingBoard.StaticInstance.greenVoid;
                                else nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;

                                Reaction reaction = new Reaction(dimension, reagents, new List<GameObject>(), nextLevelOrb, aether, false);
                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                }
                            }
                            else
                            {
                                Reaction reaction = new Reaction(dimension, reagents, new List<GameObject>(), nextLevelOrb, aether, antimatter);
                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                }
                            }
                        }
                    }
                }
            }
            reactionReset();
        }


        void reactionMaximumCheck(REACTION_DIMENSIONS dimension)
        {
            if (reaction_type != Orb.ORB_TYPES.SEMIPLASMA)
            {
                if (reagents[reagents.Count - 1].Level == 1)
                {
                    if (reaction_counter >= max_1lvl_counter)
                    {
                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;
                        List<GameObject> coreList = new List<GameObject>();
                        GameObject coreToAdd = null;
                        foreach (Orb orb in reagents)
                        {
                            if (!orb.frozen)
                            {
                                nextLevelOrb = orb.NextLevelOrb;
                                if (nextLevelOrb.GetComponent<AspectOrb>()) coreToAdd = MixingBoard.orbDictionary[nextLevelOrb.GetComponent<AspectOrb>().orbColor + "Dye"];
                            }
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        if (coreToAdd) coreList.Add(coreToAdd);
                        if (nextLevelOrb == null)
                        {
                            reaction_counter -= reagents[0].comboCounter;
                            reagents.RemoveAt(0);
                            return;
                        }
                        else
                        {
                            Reaction reaction = new Reaction(dimension, reagents, coreList, nextLevelOrb, aether, antimatter);
                            if (!reactionList.Contains(reaction))
                            {
                                reactionList.Add(reaction);
                                reactionReset();
                            }
                        }
                    }
                }

                else if (reagents[reagents.Count - 1].Level == 2)
                {
                    if (reaction_counter >= max_2lvl_counter)
                    {

                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;
                        List<GameObject> coreList = new List<GameObject>();
                        GameObject coreToAdd = null;
                        foreach (Orb orb in reagents)
                        {
                            if (!orb.frozen)
                            {
                                nextLevelOrb = orb.NextLevelOrb;
                                if (nextLevelOrb.GetComponent<AspectOrb>()) coreToAdd = MixingBoard.orbDictionary[nextLevelOrb.GetComponent<AspectOrb>().orbColor + "Dye"];
                            }
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        if (coreToAdd) coreList.Add(coreToAdd);
                        if (nextLevelOrb == null)
                        {
                            reaction_counter -= reagents[0].comboCounter;
                            reagents.RemoveAt(0);
                            return;
                        }
                        else
                        {
                            if (antimatter)
                            {
                                bool fiery = false;
                                bool frozen = false;
                                bool aetherFlag = false;
                                foreach (Orb orb in reagents)
                                {
                                    if (orb.fiery) fiery = true;
                                    if (orb.frozen) frozen = true;
                                    if (orb.aetherCount != 0) aetherFlag = true;
                                }

                                if (fiery && frozen)
                                {
                                    if (dimension == REACTION_DIMENSIONS.HORIZONTAL) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                                    else nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                                }
                                else if (fiery) nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                                else if (frozen) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                                else if (aetherFlag) nextLevelOrb = MixingBoard.StaticInstance.greenVoid;
                                else nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;

                                Reaction reaction = new Reaction(dimension, reagents, coreList, nextLevelOrb, aether, false);
                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                    reactionReset();
                                }
                            }
                            else
                            {
                                Reaction reaction = new Reaction(dimension, reagents, coreList, nextLevelOrb, aether, antimatter);
                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                    reactionReset();
                                }
                            }
                        }
                    }
                }
            }
        }


        public void reactionReset()
        {
            reagents.Clear();
            reaction_type = Orb.ORB_TYPES.NONE;
            reaction_counter = 0;
        }

        void channelingDistribution()
        {

            foreach (Reaction reaction in reactionList)
            {
                foreach (OrbPlace orbPlace in reaction.placesList)
                {
                    if (orbPlace.orbNode.countOfReactionsIn >= 2)
                    {
                        orbPlace.occupied = true;
                        orbPlace.orbNode.startChanneling(channelingTime, new Orb.ReplacingOrbStruct(board.yellowCore));
                    }
                }

                foreach (Orb.ReplacingOrbStruct product in reaction.productList)
                {
                    foreach (OrbPlace orbPlace in reaction.placesList)
                    {
                        if (!orbPlace.occupied)
                        {
                            orbPlace.occupied = true;
                            orbPlace.orbNode.startChanneling(channelingTime, product);
                            break;
                        }
                    }
                }

                switch (reaction.dimension)
                {
                    case REACTION_DIMENSIONS.HORIZONTAL:

                        switch (reaction.reagents.Count)
                        {
                            case 2:

                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.RIGHT);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.LEFT);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case 3:

                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.RIGHT);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.CENTER);
                                                break;
                                            case 2:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.LEFT);
                                                break;
                                        }
                                    }
                                }
                                break;

                            case 4:

                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.RIGHT);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.RIGHT);
                                                break;
                                            case 2:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.LEFT);
                                                break;
                                            case 3:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.LEFT);
                                                break;

                                        }
                                    }
                                }
                                break;
                        }

                        break;

                    case REACTION_DIMENSIONS.VERTICAL:
                        switch (reaction.reagents.Count)
                        {
                            case 2:

                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.UP);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.DOWN);
                                                break;
                                        }
                                    }
                                }

                                break;

                            case 3:
                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.UP);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.CENTER);
                                                break;
                                            case 2:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.DOWN);
                                                break;
                                        }
                                    }
                                }
                                break;

                            case 4:
                                foreach (OrbPlace orbPlace in reaction.placesList)
                                {
                                    if (!orbPlace.occupied)
                                    {
                                        switch (orbPlace.place)
                                        {
                                            case 0:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.UP);
                                                break;
                                            case 1:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.UP);
                                                break;
                                            case 2:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.DOWN);
                                                break;
                                            case 3:
                                                orbPlace.orbNode.startChanneling(channelingTime, Orb.CHANNELING_MODES.DOWN);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            board.currentCombiningTimer = .8;
        }
    }

    private class OrbPlace
    {
        public Orb orbNode
        {
            get; private set;
        }

        public bool occupied;
        public int place
        {
            get; private set;
        }

        public OrbPlace(Orb orb, int place)
        {
            this.orbNode = orb;
            this.place = place;
        }
    }

    private class Reaction
    {
        public static MixingBoard board;
        public ComboManager.REACTION_DIMENSIONS dimension
        {
            get; private set;
        }

        public List<Orb> reagents
        {
            get; private set;
        }

        public List<OrbPlace> placesList
        {
            get; private set;
        }
        public List<Orb.ReplacingOrbStruct> productList
        {
            get; private set;
        }

        public Reaction(ComboManager.REACTION_DIMENSIONS dimension, List<Orb> reagents, List<GameObject> coreList, GameObject levelUppedOrb, int aether, bool antimatter)
        {
            this.dimension = dimension;
            this.reagents = new List <Orb>(reagents);
            productList = new List<Orb.ReplacingOrbStruct>();
            Orb.ReplacingOrbStruct replacingOrb = new Orb.ReplacingOrbStruct(levelUppedOrb, false, false, aether, antimatter);

            productList.Add(replacingOrb);
            foreach (GameObject core in coreList)
            {
                productList.Add(new Orb.ReplacingOrbStruct(core));
            }

            if (reagents.Count == 1)
            {
                //if (reagents[0].countOfReactionsIn > 1) reagents[0].startChanneling(channelingTime, Orb.CHANNELING_MODES.LEVEL_UP);
                return;
            }
            foreach (Orb orb in reagents)
            {
                orb.countOfReactionsIn += 1;
            }

            placesList = new List<OrbPlace>();

            switch (reagents.Count)
            {
                case 2:
                    placesList.Add(new OrbPlace(reagents[0], 0));
                    placesList.Add(new OrbPlace(reagents[1], 1));
                    break;

                case 3:
                    placesList.Add(new OrbPlace(reagents[1], 1));
                    placesList.Add(new OrbPlace(reagents[0], 0));
                    placesList.Add(new OrbPlace(reagents[2], 2));
                    break;

                case 4:
                    placesList.Add(new OrbPlace(reagents[1], 1));
                    placesList.Add(new OrbPlace(reagents[2], 2));
                    placesList.Add(new OrbPlace(reagents[0], 0));
                    placesList.Add(new OrbPlace(reagents[3], 3));
                    break;
            }
        }
        public override bool Equals(object obj)
        {
            //2 реакции равны, если состоят из одинаковых ингредиентов

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Reaction reaction = (Reaction)obj;
            if(reaction.reagents.Count == reagents.Count)
            {
                foreach (Orb orb in reagents)
                {
                    if (!reaction.reagents.Contains(orb)) return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}