using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MixingBoard : MonoBehaviour
{
    public static int Width { get; private set; } = 4;
    public static int Height { get; private set; } = 4;
    public double targetDelay { get; private set; } = OrbBox.movingTime;

    public static double ingredientMovementDelay;
    public static double deployDelay;

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
    [SerializeField]
    public GameObject uncertaintyOrb;

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

    public Orb[,] orbs = new Orb[Width, Height];

    OrbBox[] orbBoxes;

    private void Awake()
    {
        orbBoxes = new OrbBox[Width * Height];

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

    void OnEnable()
    {
        StartCoroutine(comboManager.reactionCheck());
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
    }

    public bool stricted { get; private set; }
    public bool stable { get; private set; }

    public void orbListUpdate()
    {
        orbs = new Orb[Width, Height];
        orbBoxes = GetComponentsInChildren<OrbBox>();
        foreach (OrbBox orbBox in orbBoxes)
        {
            if((int)Math.Round(orbBox.transform.localPosition.y) < Height && orbBox.GetComponent<OrbBox>().Orb.archetype != Orb.ORB_ARCHETYPES.DROP) orbs[(int)Math.Round(orbBox.transform.localPosition.x), (int)Math.Round(orbBox.transform.localPosition.y)] = orbBox.GetComponent<OrbBox>().Orb;
        }
        stricted = true;
        foreach (OrbBox orbBox in orbBoxes)
        {
            if (!orbBox.xStricted || !orbBox.yStricted) stricted = false;
        }

        stable = true;
        foreach (OrbBox orbBox in orbBoxes)
        {
            if (!orbBox.lying) stable = false;
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

    [SerializeField]
    bool changeOnDeployment = true;

    public void deployIngredient()
    {
        if (ingredientMovementDelay <= 0 && spinDelay <= 0 && deployDelay <= 0)
        {
            OrbBox[,] ingredientOrbs = new OrbBox[Width, Height];

            ingredientMovementDelay = .5;
            deployDelay = .5;

            int[] places = new int[Width];
           
            foreach (OrbBox orbBox in addingBoard.orbBoxes)
            {
                ingredientOrbs[(int)Math.Round(orbBox.transform.localPosition.x), (int)Math.Round(orbBox.transform.localPosition.y)] = orbBox;
            }

            for (int i = 0; i < Width; i++)
            {
                if (ingredientOrbs[i, 0] && ingredientOrbs[i, 0].Orb.archetype == Orb.ORB_ARCHETYPES.DROP)
                {
                    OrbBox tartgetedOrb = null;
                    int targetY = -1;
                    foreach (Orb _orb in orbs)
                    {
                        if (_orb)
                        {
                            if ((int)Math.Round(_orb.Box.transform.localPosition.x) == i)
                            {
                                if ((int)Math.Round(_orb.Box.transform.localPosition.y) > targetY)
                                {
                                    targetY = (int)Math.Round(_orb.Box.transform.localPosition.y);
                                    tartgetedOrb = _orb.Box;
                                }
                            }
                        }
                    }
                    if (tartgetedOrb)
                    {
                        ingredientOrbs[i, 0].GetComponentInChildren<DropOrb>().targetedtOrb = tartgetedOrb;
                        ingredientOrbs[i, 0].GetComponentInChildren<DropOrb>().targetY = (int)Math.Round(tartgetedOrb.gameObject.transform.localPosition.y);
                        places[i] = (int)Math.Round(tartgetedOrb.gameObject.transform.localPosition.y);
                    }
                    else
                    {
                        ingredientOrbs[i, 0].GetComponentInChildren<DropOrb>().targetY = 0;
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
                            if ((int)Math.Round(_orb.Box.transform.localPosition.x) == i)
                            {
                                if ((int)Math.Round(_orb.Box.transform.localPosition.y) > maxY) maxY = (int)Math.Round(_orb.Box.transform.localPosition.y);
                            }
                        }
                    }
                    foreach (OrbBox _orb in ingredientOrbs)
                    {
                        if (_orb)
                        {
                            if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                            {
                                columnSize++;
                            }
                        }
                    }
                    if (maxY + columnSize >= MixingBoard.Width)
                    {
                        foreach (OrbBox orbBox in ingredientOrbs)
                        {
                            if (orbBox)
                            {
                                orbBox.Orb.shakeRight();
                            }
                        }
                        return;
                    }

                    places[i] = maxY + 1;
                }
            }

            for (int i = 0; i < Width; i++)
            {
                foreach (OrbBox _orb in ingredientOrbs)
                {
                    if (_orb)
                    {
                        _orb.transform.SetParent(orbShift.transform, true);
                        _orb.transform.localPosition = new Vector3(Mathf.Round(_orb.transform.localPosition.x), Mathf.Round(_orb.transform.localPosition.y), 0);

                    }
                }
            }

            float[] fallDistances = new float[Width];


            for (int i = 0; i < Width; i++)
            {
                float minY = 0;
                foreach (OrbBox _orb in ingredientOrbs)
                {
                    if (_orb)
                    {

                        if (minY == 0) minY = (int)Math.Round(_orb.transform.localPosition.y);
                        if ((int)Math.Round(_orb.transform.localPosition.x) == i)
                        {
                            if ((int)Math.Round(_orb.transform.localPosition.y) < minY) minY = (int)Math.Round(_orb.transform.localPosition.y);
                        }
                    }
                }
                fallDistances[i] = minY - places[i];
            }

            for (int i = 0; i < Width; i++)
            {
                foreach (OrbBox _orb in ingredientOrbs)
                {
                    if (_orb)
                    {
                        if ((int)Math.Round(_orb.gameObject.transform.localPosition.x) == i)
                        {
                            _orb.falling = true;
                            iTween.MoveTo(_orb.gameObject, iTween.Hash("position", new Vector3((int)Math.Round(_orb.gameObject.transform.localPosition.x), (int)Math.Round(_orb.gameObject.transform.localPosition.y) - fallDistances[i], _orb.transform.localPosition.z), "islocal", true, "time", OrbBox.movingTime * fallDistances[i] / 2, "easetype", iTween.EaseType.easeInSine));
                            spinDelay = Math.Max(spinDelay, OrbBox.movingTime * fallDistances[i] * 1.05f);
                            ingredientMovementDelay = OrbBox.movingTime * fallDistances[i] * 1.05f;
                            deployDelay = 4 * OrbBox.movingTime * 1.05f;
                            currentCombiningTimer = 4 * OrbBox.movingTime * 1.05f;
                            _orb.Invoke("fallingReset", Convert.ToSingle(4f * OrbBox.movingTime * 1.05));
                        }
                    }
                }
            }
            foreach (Ingredient.ESSENSE essence in IngredientPanel.currentIngredient.essenceList)
            {
               if(essence != Ingredient.ESSENSE.None) essencePanel.addEssence(essence);
            }
          
            if(changeOnDeployment) IngredientPanel.refreshIngredientsWithDelay();
        }
    }

    public static void cleanUpBoard()
    {
        if (StaticInstance != null)
        {
            foreach (Orb orb in StaticInstance.orbs)
            {
                if (orb)
                {
                    if(orb.channeling) StaticInstance.breakReactionsWith(orb);
                    orb.DestroyIn(.25f);
                }
            }
            StaticInstance.essencePanel.clearEssences();
        }
    }

    public static bool isEmpty
    {
        get
        {
            if (StaticInstance == null)
            {
                return true;
            }
            else foreach (Orb orb in StaticInstance.orbs)
                {
                    if (orb) return false;
                }
            return true;
        }
    }

    public void wipe (int columnsCount, float percentageEssence)
    {
        List<List<Orb>> columns = new List<List<Orb>>();
        for (int i = 0; i < MixingBoard.Width; i++)
        {
            List<Orb> list = new List<Orb>();
            for (int j = 0; j < MixingBoard.Height; j++)
            {
                if (orbs[i, j] != null) list.Add(orbs[i, j]);
            }
            if(list.Count != 0) columns.Add(list);

        }
        List<List<Orb>> listToWipe = new List<List<Orb>>();
        for (int i = 0; i < columnsCount; i++)
        {
            if (columns.Count == 0) break;
            int n = UnityEngine.Random.Range(0, columns.Count);
            List<Orb> list = columns[n];
            columns.Remove(list);
            listToWipe.Add(list);
        }
        foreach (List<Orb> list in listToWipe)
        {
            foreach (Orb orb in list)
            {
                if (orb.channeling) StaticInstance.breakReactionsWith(orb);
                orb.DestroyIn(.25f);
            }
        }

        if (percentageEssence <= 0 || percentageEssence >= 1)
        {
            Debug.Log("Попытка удалить " + percentageEssence + "% эссенции");
            return;
        }
        int essenceToWipe = Math.Max((int)Math.Round(essencePanel.essenceCounter * percentageEssence), 1);
        essencePanel.wipeRandomEssence(essenceToWipe);
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
        int min_semiplasma_counter = 4;
        int max_semiplasma_counter = 4;
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

        public IEnumerator reactionCheck()
        {
            while (true)
            {
                if (board.stable && board.currentTargetDelay <= 0 && board.currentCombiningTimer <= 0)
                {

                    for (int j = 0; j < Height; j++)
                    {
                        reactionMinimumCheck(REACTION_DIMENSIONS.HORIZONTAL);
                        for (int i = 0; i < Width; i++)
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
                    for (int i = 0; i < Width; i++)
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
                yield return new WaitForSeconds(.07f);
            }
        }

        void reactionMinimumCheck(REACTION_DIMENSIONS dimension)
        {
            if (reagents.Count != 0)
            {
                if (reaction_type == Orb.ORB_TYPES.SEMIPLASMA)
                {
                    if (reaction_counter >= min_semiplasma_counter)
                    {
                        bool fireFlag = false;
                        bool iceFlag = false;
                        int aether = 0;
                        bool antimatterFlag = false;

                        List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                        foreach (Orb orb in reagents)
                        {
                            if (orb.fiery) fireFlag = true;
                            if (orb.frozen) iceFlag = true;
                            if (orb.aetherCount != 0) aether += orb.aetherCount;
                            if (orb.antimatter) antimatterFlag = true;
                        }

                        if (fireFlag) products.Add(new Orb.ReplacingOrbStruct(board.redCore));
                        if (iceFlag) products.Add(new Orb.ReplacingOrbStruct(board.blueCore));
                        if (aether != 0) products.Add(new Orb.ReplacingOrbStruct(board.greenCore, false, false, aether, false, null));
                        if (antimatterFlag) products.Add(new Orb.ReplacingOrbStruct(board.purpleVoid));

                        Reaction reaction = new Reaction(dimension, reagents, products);

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
                        List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;

                        foreach (Orb orb in reagents)
                        {
                            if(orb.type == Orb.ORB_TYPES.SEMIPLASMA)
                            {
                                products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.yellowCore));
                                if (orb.fiery) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.redCore));
                                if (orb.frozen) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.blueCore));
                                if (orb.aetherCount != 0) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.greenCore, false, false, orb.aetherCount));
                                if (orb.antimatter) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.purpleVoid));
                            }
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
                            
                            products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, aether, antimatter, null));
                            Reaction reaction = new Reaction(dimension, reagents, products);
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
                                bool fireFlag = false;
                                bool iceFlag = false;
                                aether = 0;

                                List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                                foreach (Orb orb in reagents)
                                {
                                    if (orb.fiery) fireFlag = true;
                                    if (orb.frozen) iceFlag = true;
                                    if (orb.aetherCount != 0) aether += orb.aetherCount;
                                }

                                if (nextLevelOrb.gameObject == StaticInstance.blue3) nextLevelOrb = StaticInstance.bluePulsar;
                                if (nextLevelOrb.gameObject == StaticInstance.red3) nextLevelOrb = StaticInstance.redPulsar;
                                if (nextLevelOrb.gameObject == StaticInstance.green3) nextLevelOrb = StaticInstance.greenPulsar;

                                List<Orb.ReplacingOrbStruct> voidsList = new List<Orb.ReplacingOrbStruct>();

                                if (fireFlag) voidsList.Add(new Orb.ReplacingOrbStruct(board.redVoid));
                                if (iceFlag) voidsList.Add(new Orb.ReplacingOrbStruct(board.blueVoid));
                                if (aether != 0) voidsList.Add(new Orb.ReplacingOrbStruct(board.greenVoid, false, false, aether, false, null));

                                if (voidsList.Count != 0)
                                {
                                    voidsList.Add(new Orb.ReplacingOrbStruct(nextLevelOrb));
                                    nextLevelOrb = board.uncertaintyOrb;
                                    products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, 0, false, voidsList));
                                }
                                else
                                {
                                    products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb));
                                }
                                Reaction reaction = new Reaction(dimension, reagents, products);

                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                }
                            }
                            else
                            {
                                List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                                products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, aether, antimatter));
                                Reaction reaction = new Reaction(dimension, reagents, products);
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
            if (reaction_type == Orb.ORB_TYPES.SEMIPLASMA)
            {
                if (reaction_counter >= max_semiplasma_counter)
                {
                    bool fireFlag = false;
                    bool iceFlag = false;
                    int aether = 0;
                    bool antimatterFlag = false;

                    List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                    foreach (Orb orb in reagents)
                    {
                        if (orb.fiery) fireFlag = true;
                        if (orb.frozen) iceFlag = true;
                        if (orb.aetherCount != 0) aether += orb.aetherCount;
                        if (orb.antimatter) antimatterFlag = true;
                    }

                    products.Add(new Orb.ReplacingOrbStruct(board.yellowCore));
                    if (fireFlag) products.Add(new Orb.ReplacingOrbStruct(board.redCore));
                    if (iceFlag) products.Add(new Orb.ReplacingOrbStruct(board.blueCore));
                    if (aether != 0) products.Add(new Orb.ReplacingOrbStruct(board.greenCore, false, false, aether, false, null));
                    if (antimatterFlag) products.Add(new Orb.ReplacingOrbStruct(board.purpleVoid));

                    Reaction reaction = new Reaction(dimension, reagents, products);

                    if (!reactionList.Contains(reaction))
                    {
                        reactionList.Add(reaction);
                        reactionReset();
                    }
                   
                }
            }
            else
            {
                if (reagents[reagents.Count - 1].Level == 1)
                {
                    if (reaction_counter >= max_1lvl_counter)
                    {
                        GameObject nextLevelOrb = null;
                        int aether = 0;
                        bool antimatter = false;
                        GameObject coreToAdd = null;
                        List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();

                        foreach (Orb orb in reagents)
                        {
                            if (orb.type == Orb.ORB_TYPES.SEMIPLASMA)
                            {
                                if (orb.fiery) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.redCore));
                                if (orb.frozen) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.blueCore));
                                if (orb.aetherCount != 0) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.greenCore, false, false, orb.aetherCount));
                                if (orb.antimatter) products.Add(new Orb.ReplacingOrbStruct(MixingBoard.StaticInstance.purpleVoid));
                            }
                            if (!orb.frozen)
                            {
                                nextLevelOrb = orb.NextLevelOrb;
                                if (nextLevelOrb.GetComponentInChildren<AspectOrb>()) coreToAdd = MixingBoard.orbDictionary[nextLevelOrb.GetComponentInChildren<AspectOrb>().orbColor + "Dye"];
                            }
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        products.Add(new Orb.ReplacingOrbStruct(coreToAdd));
                        products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, aether, antimatter));
                        if (nextLevelOrb == null)
                        {
                            reaction_counter -= reagents[0].comboCounter;
                            reagents.RemoveAt(0);
                            return;
                        }
                        else
                        {
                            Reaction reaction = new Reaction(dimension, reagents, products);
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
                        GameObject coreToAdd = null;
                        foreach (Orb orb in reagents)
                        {
                            if (!orb.frozen)
                            {
                                nextLevelOrb = orb.NextLevelOrb;
                                if (nextLevelOrb.GetComponentInChildren<AspectOrb>()) coreToAdd = MixingBoard.orbDictionary[nextLevelOrb.GetComponentInChildren<AspectOrb>().orbColor + "Dye"];
                            }
                            aether += orb.aetherImpact;
                            if (orb.antimatter) antimatter = true;
                        }
                        List<Orb.ReplacingOrbStruct> products = new List<Orb.ReplacingOrbStruct>();
                        products.Add(new Orb.ReplacingOrbStruct(coreToAdd));
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
                                bool fireFlag = false;
                                bool iceFlag = false;
                                aether = 0;

                                foreach (Orb orb in reagents)
                                {
                                    if (orb.fiery) fireFlag = true;
                                    if (orb.frozen) iceFlag = true;
                                    if (orb.aetherCount != 0) aether += orb.aetherCount;
                                }

                                if (nextLevelOrb.gameObject == StaticInstance.blue3) nextLevelOrb = StaticInstance.bluePulsar;
                                if (nextLevelOrb.gameObject == StaticInstance.red3) nextLevelOrb = StaticInstance.redPulsar;
                                if (nextLevelOrb.gameObject == StaticInstance.green3) nextLevelOrb = StaticInstance.greenPulsar;

                                List<Orb.ReplacingOrbStruct> voidsList = new List<Orb.ReplacingOrbStruct>();

                                if (fireFlag) voidsList.Add(new Orb.ReplacingOrbStruct(board.redVoid));
                                if (iceFlag) voidsList.Add(new Orb.ReplacingOrbStruct(board.blueVoid));
                                if (aether != 0) voidsList.Add(new Orb.ReplacingOrbStruct(board.greenVoid, false, false, aether, false, null));

                                if (voidsList.Count != 0)
                                {
                                    voidsList.Add(new Orb.ReplacingOrbStruct(nextLevelOrb));
                                    nextLevelOrb = board.uncertaintyOrb;
                                    products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, 0, false, voidsList));
                                }
                                else
                                {
                                    products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb));
                                }
                                Reaction reaction = new Reaction(dimension, reagents, products);

                                if (!reactionList.Contains(reaction))
                                {
                                    reactionList.Add(reaction);
                                    reactionReset();
                                }
                            }
                            else
                            {
                                products.Add(new Orb.ReplacingOrbStruct(nextLevelOrb, false, false, aether));
                                Reaction reaction = new Reaction(dimension, reagents, products);
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

        public Reaction(ComboManager.REACTION_DIMENSIONS dimension, List<Orb> reagents, List<Orb.ReplacingOrbStruct> products)
        {
            this.dimension = dimension;
            this.reagents = new List <Orb>(reagents);
            productList = products;

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