using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class InfoPanelManager : MonoBehaviour
{
    MixingBoard mixingBoard;
    [SerializeField]
    AddingBoard addingBoard;
    [SerializeField]
    GameObject infoPanelSample;

    [SerializeField]
    GameObject orbCaption;
    [SerializeField]
    GameObject orbInfoSampler;
    [SerializeField]
    GameObject orbInfoAbsent;
    [SerializeField]
    GameObject orbEffectsCaption;
    [SerializeField]
    GameObject orbEffectsSampler;
    [SerializeField]
    GameObject orbEffectsAbsent;
    [SerializeField]
    GameObject cellEffectsCaption;
    [SerializeField]
    GameObject cellEffectsSampler;
    [SerializeField]
    GameObject cellEffectsAbsent;
    [SerializeField]
    GameObject separator;

    [SerializeField]
    GameObject uncertaintyDrafterSampler;

    [SerializeField]
    GameObject infoTargetSampler;

    static GameObject infoPanel;
    static GameObject infoTarget;
    static RectTransform infoPanelTransorm;
    static Orb clickedOrb;

    static GameObject uncertaintyDrafter;
    static UncertaintyOrb uncertaintyOrb;

    static Dictionary<Orb.ORB_ARCHETYPES, String> orbArchetypeDicrionaryRU;
    static Dictionary<Orb.ORB_TYPES, String> orbTypeDicrionaryRU;

    static Dictionary<Orb.ORB_ARCHETYPES, String> orbArchetypeDicrionaryEN;
    static Dictionary<Orb.ORB_TYPES, String> orbTypeDicrionaryEN;

    enum CLICKED_BOARD
    {
        ADDING_BOARD,
        MIXING_BOARD
    }

    void Start()
    {

        mixingBoard = MixingBoard.StaticInstance;

        orbTypeDicrionaryRU = new Dictionary<Orb.ORB_TYPES, string>();
        orbArchetypeDicrionaryRU = new Dictionary<Orb.ORB_ARCHETYPES, string>();

        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BODY_ASPECT, "Тело");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.MIND_ASPECT, "Разум");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SOUL_ASPECT, "Душа");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SEMIPLASMA, "Полуплазма");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_CORE, "Сверхновая");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_CORE, "Ледяное ядро");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_CORE, "Пылающее ядро");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_CORE, "Эфирное ядро");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_DYE_CORE, "Синий краситель");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_DYE_CORE, "Красный краситель");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_DYE_CORE, "Зелёный краситель");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.VOID, "Пустота");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_VOID, "Ледяная пустота");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_VOID, "Пылающая пустота");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_VOID, "Эфирная пустота");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_VOID, "Сверхновая пустота");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_PULSAR, "Синий пульсар");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_PULSAR, "Красный пульсар");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_PULSAR, "Зелёный пульсар");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_DROP, "Капля разума");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_DROP, "Капля тела");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_DROP, "Капля души");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_DROP, "Капля сверхновой");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_DROP, "Капля льда");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_DROP, "Капля огня");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_DROP, "Капля эфира");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ANTIMATTER_DROP, "Капля антиматерии");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.UNCERTAINTY, "Сфера неопределённости");

        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.ASPECT, "Аспектная сфера");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.NEORGANIC, "Неорганика");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.CORE, "Ядро");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.VOID, "Пустота");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.DROP, "Капля");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.UNCERTAINTY, "???");

        orbTypeDicrionaryEN = new Dictionary<Orb.ORB_TYPES, string>();
        orbArchetypeDicrionaryEN = new Dictionary<Orb.ORB_ARCHETYPES, string>();

        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.BODY_ASPECT, "Body");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.MIND_ASPECT, "Mind");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.SOUL_ASPECT, "Soul");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.SEMIPLASMA, "Semiplasma");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.SUPERNOVA_CORE, "Supernova");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.ICE_CORE, "Ice core");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.FIRE_CORE, "Fire core");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.AETHER_CORE, "Aether core");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.BLUE_DYE_CORE, "Blue dye");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.RED_DYE_CORE, "Red dye");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.GREEN_DYE_CORE, "Green dye");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.VOID, "Void");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.ICE_VOID, "Ice void");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.FIRE_VOID, "Fire void");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.AETHER_VOID, "Aether void");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.SUPERNOVA_VOID, "Supernova void");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.BLUE_PULSAR, "Blue pulsar");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.RED_PULSAR, "Red pulsar");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.GREEN_PULSAR, "Green pulsar");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.BLUE_DROP, "Drop of mind");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.RED_DROP, "Drop of body");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.GREEN_DROP, "Drop of soul");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.SUPERNOVA_DROP, "Drop of supernova");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.ICE_DROP, "Drop of ice");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.FIRE_DROP, "Drop of fire");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.AETHER_DROP, "Drop of aether");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.ANTIMATTER_DROP, "Drop of antimatter");
        orbTypeDicrionaryEN.Add(Orb.ORB_TYPES.UNCERTAINTY, "Orb of uncertainty");

        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.ASPECT, "Aspect orb");
        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.NEORGANIC, "Neorganic");
        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.CORE, "Core");
        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.VOID, "Void");
        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.DROP, "Drop");
        orbArchetypeDicrionaryEN.Add(Orb.ORB_ARCHETYPES.UNCERTAINTY, "???");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !DraftModule.opened && !PauseCanvas.Paused)
        {
            if (infoPanel != null) Destroy(infoPanel);
            if (infoTarget != null)
            {
                if (infoTarget.transform.parent.gameObject == mixingBoard.target.gameObject)
                {
                    mixingBoard.target.removeInfoTarget(infoTarget);
                }
                else
                {
                    Destroy(infoTarget);
                }
            }



            Vector3 clickedPosition = CookingManager.CookingCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 clickedPositionMixing = mixingBoard.gameObject.transform.InverseTransformPoint(clickedPosition);
            clickedPositionMixing = new Vector3(Convert.ToSingle(clickedPositionMixing.x - mixingBoard.OrbShift.transform.localPosition.x + .5), Convert.ToSingle(clickedPositionMixing.y - mixingBoard.OrbShift.transform.localPosition.y + .5), -1);

            if (uncertaintyDrafter != null)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(uncertaintyDrafter.GetComponent<UncertaintyDrafter>().cellsBlock.GetComponent<RectTransform>(), Input.mousePosition, CookingManager.CookingCamera))
                {
                    if (clickedPositionMixing.x < MixingBoard.Width && clickedPositionMixing.x >= 0 && clickedPositionMixing.y < MixingBoard.Height && clickedPositionMixing.y >= 0)
                    {
                        clickedPositionMixing = new Vector3((int)clickedPositionMixing.x, (int)clickedPositionMixing.y, 0);
                        clickedOrb = mixingBoard.orbs[(int)Math.Round(clickedPositionMixing.x), (int)Math.Round(clickedPositionMixing.y)];
                        if (clickedOrb && clickedOrb == uncertaintyOrb)
                        {

                        }
                        else
                        {
                            Destroy(uncertaintyDrafter.gameObject);
                            uncertaintyDrafter = null;
                            uncertaintyOrb = null;
                        }
                    }
                    else
                    {
                        Destroy(uncertaintyDrafter.gameObject);
                        uncertaintyDrafter = null;
                        uncertaintyOrb = null;
                    }
                }
            }
            else
            {
                if (clickedPositionMixing.x < MixingBoard.Width && clickedPositionMixing.x >= 0 && clickedPositionMixing.y < MixingBoard.Height && clickedPositionMixing.y >= 0)
                {
                    clickedPositionMixing = new Vector3((int)clickedPositionMixing.x, (int)clickedPositionMixing.y, 0);

                    infoTarget = mixingBoard.target.addInfoTarget(clickedPositionMixing.x - mixingBoard.target.transform.localPosition.x, clickedPositionMixing.y - mixingBoard.target.transform.localPosition.y, infoTargetSampler);

                    Vector3 spawnPoint = new Vector3(0, 0, 0);
                    infoPanel = Instantiate(infoPanelSample, spawnPoint, Quaternion.identity);
                    clickedOrb = mixingBoard.orbs[(int)Math.Round(clickedPositionMixing.x), (int)Math.Round(clickedPositionMixing.y)];
                    infoPanelTransorm = infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>();


                    if (clickedOrb && clickedOrb.type == Orb.ORB_TYPES.UNCERTAINTY)
                    {
                        uncertaintyOrb = clickedOrb.GetComponent<UncertaintyOrb>();
                        uncertaintyDrafter = Instantiate(uncertaintyDrafterSampler, spawnPoint, Quaternion.identity);
                        uncertaintyDrafter.transform.position = new Vector3(clickedPositionMixing.x, clickedPositionMixing.y + uncertaintyDrafter.GetComponent<RectTransform>().sizeDelta.y * .75f * uncertaintyDrafter.GetComponent<RectTransform>().localScale.y, clickedPositionMixing.z);
                        uncertaintyDrafter.transform.SetParent(mixingBoard.OrbShift.transform, false);
                        uncertaintyDrafter.transform.SetParent(transform);
                        uncertaintyDrafter.transform.localPosition = new Vector3(uncertaintyDrafter.transform.localPosition.x, uncertaintyDrafter.transform.localPosition.y, 0);
                        uncertaintyDrafter.GetComponent<UncertaintyDrafter>().cellsBlock.GetComponent<RectTransform>().sizeDelta = new Vector2(clickedOrb.GetComponent<UncertaintyOrb>().uncertainOrbsList.Count * uncertaintyDrafter.GetComponent<UncertaintyDrafter>().cellsBlock.GetComponent<RectTransform>().localScale.y, uncertaintyDrafter.GetComponent<UncertaintyDrafter>().cellsBlock.GetComponent<RectTransform>().sizeDelta.y);
                        uncertaintyDrafter.GetComponent<UncertaintyDrafter>().beginDraft(clickedOrb.GetComponent<UncertaintyOrb>());
                    }
                    else
                    {
                        fillPanel(CLICKED_BOARD.MIXING_BOARD);
                        Canvas.ForceUpdateCanvases();

                        SpriteRenderer background = infoPanel.GetComponentInChildren<VerticalLayoutGroup>().GetComponent<SpriteRenderer>();
                        SpriteRenderer border = infoPanel.GetComponent<InfoPanelSampler>().infoPanelBorder.GetComponent<SpriteRenderer>();
                        SpriteRenderer header = infoPanel.GetComponent<InfoPanelSampler>().infoPanelHeader.GetComponent<SpriteRenderer>();

                        background.size = new Vector2(
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.x,
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y);

                        border.size = new Vector2(
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.x * infoPanelTransorm.localScale.x,
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y * infoPanelTransorm.localScale.y);

                        header.GetComponent<RectTransform>().localPosition = new Vector3(
                            header.GetComponent<RectTransform>().localPosition.x,
                            header.GetComponent<RectTransform>().localPosition.y + infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 * infoPanelTransorm.localScale.y - header.size.y / 8 * header.transform.localScale.y,
                            header.GetComponent<RectTransform>().localPosition.z);


                        infoPanel.transform.position = new Vector3(clickedPositionMixing.x + Convert.ToSingle(.65) + infoPanelTransorm.sizeDelta.x / 4 * infoPanelTransorm.localScale.x, clickedPositionMixing.y + infoPanelTransorm.sizeDelta.y * infoPanelTransorm.transform.localScale.y / 4 - Convert.ToSingle(.25), clickedPositionMixing.z);
                        infoPanel.transform.SetParent(mixingBoard.OrbShift.transform, false);
                        infoPanel.transform.SetParent(transform);
                        infoPanel.transform.localPosition = new Vector3(infoPanel.transform.localPosition.x, infoPanel.transform.localPosition.y, 0);
                    }

                }

                else
                {
                    Vector3 clickedPositionAdding = addingBoard.gameObject.transform.InverseTransformPoint(clickedPosition);
                    clickedPositionAdding = new Vector3(Convert.ToSingle(clickedPositionAdding.x - addingBoard.OrbShift.transform.localPosition.x + .5), Convert.ToSingle(clickedPositionAdding.y - addingBoard.OrbShift.transform.localPosition.y + .5), -1);
                    if (clickedPositionAdding.x < AddingBoard.Width && clickedPositionAdding.x >= 0 && clickedPositionAdding.y < AddingBoard.Height && clickedPositionAdding.y >= 0)
                    {
                        clickedPositionAdding = new Vector3((int)clickedPositionAdding.x, (int)clickedPositionAdding.y, -1);

                        infoTarget = Instantiate(infoTargetSampler, clickedPositionAdding, Quaternion.identity);
                        infoTarget.transform.SetParent(addingBoard.OrbShift.transform, false);

                        Vector3 spawnPoint = new Vector3(0, 0, 0);
                        infoPanel = Instantiate(infoPanelSample, spawnPoint, Quaternion.identity);
                        clickedOrb = addingBoard.orbs[(int)Math.Round(clickedPositionAdding.x), (int)Math.Round(clickedPositionAdding.y)];
                        infoPanelTransorm = infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>();

                        fillPanel(CLICKED_BOARD.ADDING_BOARD);
                        Canvas.ForceUpdateCanvases();

                        SpriteRenderer background = infoPanel.GetComponentInChildren<VerticalLayoutGroup>().GetComponent<SpriteRenderer>();
                        SpriteRenderer border = infoPanel.GetComponent<InfoPanelSampler>().infoPanelBorder.GetComponent<SpriteRenderer>();
                        SpriteRenderer header = infoPanel.GetComponent<InfoPanelSampler>().infoPanelHeader.GetComponent<SpriteRenderer>();

                        background.size = new Vector2(
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.x,
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y);

                        border.size = new Vector2(
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.x * infoPanelTransorm.localScale.x,
                            infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y * infoPanelTransorm.localScale.y);

                        header.GetComponent<RectTransform>().localPosition = new Vector3(
                            header.GetComponent<RectTransform>().localPosition.x,
                            header.GetComponent<RectTransform>().localPosition.y + infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 * infoPanelTransorm.localScale.y - header.size.y / 8 * header.transform.localScale.y,
                            header.GetComponent<RectTransform>().localPosition.z);


                        infoPanel.transform.position = new Vector3(clickedPositionAdding.x + Convert.ToSingle(.65) + infoPanelTransorm.sizeDelta.x / 4 * infoPanelTransorm.localScale.x, clickedPositionAdding.y - infoPanelTransorm.sizeDelta.y * infoPanelTransorm.transform.localScale.y / 4 - Convert.ToSingle(.1), clickedPositionAdding.z);
                        infoPanel.transform.SetParent(addingBoard.OrbShift.transform, false);
                        infoPanel.transform.SetParent(transform);
                        infoPanel.transform.localPosition = new Vector3(infoPanel.transform.localPosition.x, infoPanel.transform.localPosition.y, 0);

                    }
                }
            }
        }
    }

    void fillPanel(CLICKED_BOARD board)
    {

        TextMeshProUGUI orbName = Instantiate(orbCaption, infoPanelTransorm).GetComponent<TextMeshProUGUI>();
        string name = "";
        if (!clickedOrb) name = InfoPanelLexemas.orb;
        else
        {
            if (GameSettings.CurrentLanguage == GameSettings.Language.RU)
            {
                name = orbTypeDicrionaryRU[clickedOrb.type] + "";
                if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.ASPECT) name += (" (ур. " + clickedOrb.Level + ")");
            }
            else
            {
                name = orbTypeDicrionaryEN[clickedOrb.type] + "";
                if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.ASPECT) name += (" (lvl " + clickedOrb.Level + ")");
            }
        }

        orbName.SetText(name);


        RectTransform separatorTransform = Instantiate(separator, infoPanelTransorm).GetComponent<RectTransform>();
        if (!clickedOrb) Instantiate(orbInfoAbsent, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.absent;
        else
        {
            GameObject orbInfo = Instantiate(orbInfoSampler, infoPanelTransorm);
            TextMeshProUGUI archetypeCounter = orbInfo.GetComponent<OrbInfoComponent>().archetypeCounter.GetComponent<TextMeshProUGUI>();
            if(GameSettings.CurrentLanguage == GameSettings.Language.RU) archetypeCounter.SetText(orbArchetypeDicrionaryRU[clickedOrb.archetype] + "");
            else archetypeCounter.SetText(orbArchetypeDicrionaryEN[clickedOrb.archetype] + "");
            TextMeshProUGUI levelCounter = orbInfo.GetComponent<OrbInfoComponent>().levelCounter.GetComponent<TextMeshProUGUI>();
            if(clickedOrb.type == Orb.ORB_TYPES.UNCERTAINTY) levelCounter.SetText("???");
            else levelCounter.SetText(clickedOrb.Level + "");
            TextMeshProUGUI typeCounter = orbInfo.GetComponent<OrbInfoComponent>().typeCounter.GetComponent<TextMeshProUGUI>();
            if (GameSettings.CurrentLanguage == GameSettings.Language.RU) typeCounter.SetText(orbTypeDicrionaryRU[clickedOrb.type] + "");
            else typeCounter.SetText(orbTypeDicrionaryEN[clickedOrb.type] + "");

            TextMeshProUGUI pointsCounter = orbInfo.GetComponent<OrbInfoComponent>().pointsCounter.GetComponent<TextMeshProUGUI>();
            if (clickedOrb.type == Orb.ORB_TYPES.UNCERTAINTY) pointsCounter.SetText("???");
            else if (clickedOrb.aetherImpact == 0) pointsCounter.SetText("<b>" + clickedOrb.pointsImpact);
            else pointsCounter.SetText("<b>" + clickedOrb.pointsImpact +"</b><font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.DefaultPoints + " + </font></color><b>" + (clickedOrb.pointsImpact - clickedOrb.DefaultPoints) +  "</b><font=\"InfoPanelSDF\"><#808080>]");

            TextMeshProUGUI descriptionCaption = orbInfo.GetComponent<OrbInfoComponent>().descriptionCounter.GetComponent<TextMeshProUGUI>();

            string descriptionString = "";

            switch (clickedOrb.type)
            {
                case Orb.ORB_TYPES.MIND_ASPECT:
                    if (clickedOrb.Level == 3) descriptionString = InfoPanelLexemas.mindAspect;
                    else descriptionString = InfoPanelLexemas.level12Description;
                    break;
                case Orb.ORB_TYPES.BODY_ASPECT:
                    if (clickedOrb.Level == 3) descriptionString = InfoPanelLexemas.bodyAspect;
                    else descriptionString = InfoPanelLexemas.level12Description;
                    break;
                case Orb.ORB_TYPES.SOUL_ASPECT:
                    if (clickedOrb.Level == 3) descriptionString = InfoPanelLexemas.soulAspect;
                    else descriptionString = InfoPanelLexemas.level12Description;
                    break;
                case Orb.ORB_TYPES.SEMIPLASMA:
                    descriptionString = InfoPanelLexemas.semiplasmaDescription;
                    descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 15);
                    Canvas.ForceUpdateCanvases();
                    break;
                case Orb.ORB_TYPES.BLUE_DYE_CORE:
                    descriptionString = InfoPanelLexemas.blueDyeDescription;
                    break;
                case Orb.ORB_TYPES.RED_DYE_CORE:
                    descriptionString = InfoPanelLexemas.redDyeDescription;
                    break;
                case Orb.ORB_TYPES.GREEN_DYE_CORE:
                    descriptionString = InfoPanelLexemas.greenDyeDescription;
                    break;
                case Orb.ORB_TYPES.ICE_CORE:
                    descriptionString = InfoPanelLexemas.iceCoreDescription;
                    break;
                case Orb.ORB_TYPES.FIRE_CORE:
                    descriptionString = InfoPanelLexemas.fireCoreDescription;
                    break;
                case Orb.ORB_TYPES.AETHER_CORE:
                    descriptionString = InfoPanelLexemas.aetherCoreDescription;
                    break;
                case Orb.ORB_TYPES.SUPERNOVA_CORE:
                    descriptionString = InfoPanelLexemas.supernovaCoreDescription;
                    break;
                case Orb.ORB_TYPES.BLUE_DROP:
                    descriptionString = InfoPanelLexemas.blueDropDescription;
                    break;
                case Orb.ORB_TYPES.RED_DROP:
                    descriptionString = InfoPanelLexemas.redDropDescription;
                    break;
                case Orb.ORB_TYPES.GREEN_DROP:
                    descriptionString = InfoPanelLexemas.greenDropDescription;
                    break;
                case Orb.ORB_TYPES.ICE_DROP:
                    descriptionString = InfoPanelLexemas.iceDropDescription;
                    break;
                case Orb.ORB_TYPES.FIRE_DROP:
                    descriptionString = InfoPanelLexemas.fireDropDescription;
                    break;
                case Orb.ORB_TYPES.AETHER_DROP:
                    descriptionString = InfoPanelLexemas.aetherDropDescription;
                    break;
                case Orb.ORB_TYPES.ANTIMATTER_DROP:
                    descriptionString = InfoPanelLexemas.antimatterDropDescription;
                    break;
                case Orb.ORB_TYPES.SUPERNOVA_DROP:
                    descriptionString = InfoPanelLexemas.supernovaDropDescription;
                    break;
                case Orb.ORB_TYPES.VOID:
                    descriptionString = InfoPanelLexemas.antimatterVoidDescription;
                    break;
                case Orb.ORB_TYPES.BLUE_PULSAR:
                    descriptionString = InfoPanelLexemas.mindAspect + "\n" + InfoPanelLexemas.blueVoidDescription;
                    descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 30);
                    Canvas.ForceUpdateCanvases();
                    break;
                case Orb.ORB_TYPES.RED_PULSAR:
                    descriptionString = InfoPanelLexemas.bodyAspect + "\n" + InfoPanelLexemas.redVoidDescription;
                    descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 30);
                    Canvas.ForceUpdateCanvases();
                    break;
                case Orb.ORB_TYPES.GREEN_PULSAR:
                    descriptionString = InfoPanelLexemas.soulAspect + "\n" + InfoPanelLexemas.greenVoidDescription;
                    descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 30);
                    Canvas.ForceUpdateCanvases();
                    break;
                case Orb.ORB_TYPES.ICE_VOID:
                    descriptionString = InfoPanelLexemas.iceVoidDescription;
                    break;
                case Orb.ORB_TYPES.FIRE_VOID:
                    descriptionString = InfoPanelLexemas.fireVoidDescription;
                    break;
                case Orb.ORB_TYPES.AETHER_VOID:
                    descriptionString = InfoPanelLexemas.aetherVoidDescription;
                    break;
                case Orb.ORB_TYPES.SUPERNOVA_VOID:
                    descriptionString = InfoPanelLexemas.supernovaVoidDescription;
                    break;
            }

            if (clickedOrb.type == Orb.ORB_TYPES.AETHER_DROP || clickedOrb.type == Orb.ORB_TYPES.AETHER_CORE || clickedOrb.type == Orb.ORB_TYPES.AETHER_VOID)
            {
                descriptionString = descriptionString.Replace("XXX", clickedOrb.aetherImpact + "");
            }

            if (ConstellationManager.CONSTELLATION1 == ConstellationManager.CONSTELLATION.SUPERNOVA || ConstellationManager.CONSTELLATION2 == ConstellationManager.CONSTELLATION.SUPERNOVA)
            {
                string viscosity = InfoPanelLexemas.viscosity;
                if (clickedOrb.viscosityImpact != 0)
                {
                    if (clickedOrb.aetherImpact == 0) viscosity = " - Увеличивает вязкость на <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.viscosityImpact + "</b></color></font>.";
                    else viscosity = " - Увеличивает вязкость на <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.viscosityImpact + "</b></color></font><font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.basicViscosity + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (int)(clickedOrb.basicViscosity * Orb.aetherMultiplier * clickedOrb.aetherImpact) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>.";
                    descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCaption.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 15);
                    Canvas.ForceUpdateCanvases();
                    descriptionString = descriptionString + "\n" + viscosity;
                }
            }

            descriptionCaption.SetText(descriptionString);
        }


        Instantiate(orbEffectsCaption, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.orbEffects;
        Instantiate(separator, infoPanelTransorm);


        if (clickedOrb)
        {
            //Selection.activeGameObject = clickedOrb.Box.gameObject;

            if ((!clickedOrb.fiery && !clickedOrb.frozen && clickedOrb.aetherImpact == 0 && !clickedOrb.antimatter) || clickedOrb.archetype == Orb.ORB_ARCHETYPES.DROP) Instantiate(orbEffectsAbsent, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.absentMultiples;
            else
            {
                if (clickedOrb.fiery)
                {
                    string defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (clickedOrb.Level + (int)Math.Round(clickedOrb.Level * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</b></color></font>";
                    string aetherString = "<font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.Level + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + ((int)Math.Round(clickedOrb.Level * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>";

                    if(clickedOrb.aetherImpact != 0)
                    {
                        defaultString += aetherString;
                    }
                    if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.VOID || clickedOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
                    {
                        GameObject fireVoidCoreDesrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().fireVoidCoreDesrcription, infoPanelTransorm);
                        fireVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.fireVoidCoreDescription;
                        fireVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = fireVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", defaultString);
                    }
                    else if (clickedOrb.Level == 3)
                    {
                        GameObject fireLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().fireLevel3Desrcription, infoPanelTransorm);
                        fireLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.fireLevel3Description;
                        fireLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = fireLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", defaultString);
                    }
                    else
                    {
                        GameObject fireLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().fireLevel12Desrcription, infoPanelTransorm);
                        fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.fireLevel12Description;
                        fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", defaultString);
                        if (clickedOrb.type == Orb.ORB_TYPES.SEMIPLASMA)
                        {
                            fireLevel12Desrcription.GetComponent<RectTransform>().sizeDelta = new Vector2(fireLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.x, fireLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.y + 45);
                            Canvas.ForceUpdateCanvases();
                            fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text + InfoPanelLexemas.fireSemiplasmaDescription;
                        }
                    }
                }
                if (clickedOrb.frozen)
                {
                    string defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (clickedOrb.Level + (int)Math.Round(clickedOrb.Level * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</b></color></font>";
                    string aetherString = "<font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.Level + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + ((int)Math.Round(clickedOrb.Level * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>";

                    if (clickedOrb.aetherImpact != 0)
                    {
                        defaultString += aetherString;
                    }

                    if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.VOID || clickedOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
                    {
                        GameObject iceVoidCoreDesrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().iceVoidCoreDesrcription, infoPanelTransorm);
                        iceVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.iceVoidCoreDescription;
                        iceVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = iceVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", defaultString);
                    }
                    else if (clickedOrb.Level == 3)
                    {
                        GameObject iceLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().iceLevel3Desrcription, infoPanelTransorm);
                        iceLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.iceLevel3Description;
                        iceLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", defaultString);
                    }
                    else
                    {
                        GameObject iceLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().iceLevel12Desrcription, infoPanelTransorm);
                        iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.iceLevel12Description;
                        iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", defaultString);
                        iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", clickedOrb.Level + "");
                        if (clickedOrb.type == Orb.ORB_TYPES.SEMIPLASMA)
                        {
                            iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text + InfoPanelLexemas.iceSemiplasmaDescription;
                            iceLevel12Desrcription.GetComponent<RectTransform>().sizeDelta = new Vector2(iceLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.x, iceLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.y + 45);
                            Canvas.ForceUpdateCanvases();
                        }
                    }
                }
                if (clickedOrb.aetherImpact != 0)
                {
                    if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.VOID || clickedOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
                    {
                        GameObject aetherVoidCoreDesrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherVoidCoreDesrcription, infoPanelTransorm);
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherVoidCoreDescription;
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * (int)Math.Round(Orb.aetherMultiplier * 100) + "%</b></color></font>");

                    }
                    else if (clickedOrb.Level == 3)
                    {
                        GameObject aetherLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel3Desrcription, infoPanelTransorm);
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherLevel3Description;
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * (int)Math.Round(Orb.aetherMultiplier * 100) + "%</b></color></font>");
                    }
                    else
                    {
                        GameObject aetherLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel12Desrcription, infoPanelTransorm);
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherLevel12Description;
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * (int)Math.Round(Orb.aetherMultiplier * 100) + "%</b></color></font>");
                        if (clickedOrb.type == Orb.ORB_TYPES.SEMIPLASMA)
                        {
                            aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text + InfoPanelLexemas.aetherSemiplasmaDescription;
                            aetherLevel12Desrcription.GetComponent<RectTransform>().sizeDelta = new Vector2(aetherLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.x, aetherLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.y + 45);
                            Canvas.ForceUpdateCanvases();
                        }
                    }
                }
                if (clickedOrb.antimatter)
                {
                    string defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.voidnessImpact + "</b></color></font>";
                    string aetherString = "<font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.Level + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (int)(clickedOrb.Level * Orb.aetherMultiplier * clickedOrb.aetherImpact) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>";
                    if (clickedOrb.aetherImpact != 0)
                    {
                        defaultString += aetherString;
                    }
                    if (clickedOrb.Level >= 3)
                    {
                        GameObject antimatterLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel3Desrcription, infoPanelTransorm);
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.antimatterLevel4Description;
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", defaultString);
                    }
                    else
                    {
                        GameObject antimatterLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel12Desrcription, infoPanelTransorm);
                        antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.antimatterLevel12Description;
                        antimatterLevel12Desrcription.GetComponent<RectTransform>().sizeDelta = new Vector2(antimatterLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.x, antimatterLevel12Desrcription.GetComponent<RectTransform>().sizeDelta.y + 15);
                        antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", defaultString);

                        if (clickedOrb.type == Orb.ORB_TYPES.SEMIPLASMA)
                        {
                            antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.antimatterSemiplasmaDescription;
                            antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", defaultString);
                            Canvas.ForceUpdateCanvases();
                        }
                    }
                }
            }
        }
        else
        {
            Instantiate(orbEffectsAbsent, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.absentMultiples;
        }

        if(board == CLICKED_BOARD.MIXING_BOARD)
        {
            Instantiate(cellEffectsCaption, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.cellEffects;
            Instantiate(separator, infoPanelTransorm);
            Instantiate(cellEffectsAbsent, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.absentMultiples;

        }
    }

    static class InfoPanelLexemas
    {
        public static string level12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Объедините с двумя другими, чтобы синтезировать сферу уровнем выше.";
                else return "";
            }
        }

        public static string mindAspect
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Увеличивает влияние зелья на разум на 1.";
                else return "";
            }
        }

        public static string bodyAspect
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Увеличивает влияние зелья на тело на 1.";
                else return "";
            }
        }

        public static string soulAspect
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Увеличивает влияние зелья на душу на 1.";
                else return "";
            }
        }

        public static string blueDyeDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы окрасить их в аспект разума.";
                else return "";
            }
        }

        public static string redDyeDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы окрасить их в аспект тела.";
                else return "";
            }
        }

        public static string greenDyeDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы окрасить их в аспект души.";
                else return "";
            }
        }

        public static string iceCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы заморозить сферы по вертикали или горизонтали.";
                else return "";
            }
        }

        public static string fireCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы разогреть сферы по вертикали или горизонтали.";
                else return "";
            }
        }

        public static string aetherCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы наполнить эфиром сферы по вертикали и горизонтали на XXX.";
                else return "";
            }
        }

        public static string supernovaCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы повысить уровень ближайших сфер.";
                else return "";
            }
        }

        public static string blueDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы окрасить её в аспект разума.";
                else return "";
            }
        }

        public static string redDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы окрасить её в аспект тела.";
                else return "";
            }
        }

        public static string greenDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы окрасить её в аспект души.";
                else return "";
            }
        }

        public static string iceDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы заморозить её.";
                else return "";
            }
        }

        public static string fireDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы её разогреть.";
                else return "";
            }
        }

        public static string aetherDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы наполнить её эфиром на XXX.";
                else return "";
            }
        }

        public static string supernovaDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы повысить её уровень.";
                else return "";
            }
        }

        public static string antimatterDropDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Капните на другую сферу, чтобы превратить её в антиматерию.";
                else return "";
            }
        }

        public static string blueVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и окрасить остальные в аспект разума.";
                else return "";
            }

        }
        public static string redVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и окрасить остальные в аспект тела.";
                else return "";
            }
        }

        public static string greenVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и окрасить остальные в аспект души.";
                else return "";
            }
        }

        public static string iceVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и заморозить остальные.";
                else return "";
            }
        }

        public static string fireVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и разогреть остальные.";
                else return "";
            }
        }

        public static string aetherVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и наполнить эфиром остальные на XXX.";
                else return "";
            }
        }

        public static string supernovaVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и повысить уровень остальных.";
                else return "";
            }
        }

        public static string antimatterVoidDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Перемешайте с другими сферами, чтобы растворить одну из них и превратить остальные в антиматерию.";
                else return "";
            }
        }

        public static string semiplasmaDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return " - Объедините с тремя другими, чтобы синтезировать сверхновую.\n - Сфера растворяется при повышении уровня, если на ней нет никакого эффета.";
                else return "";
            }
        }

        public static string fireLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Огонь]\n - Сфера считается в цепочке синтеза за две.\n - Температура зелья повышена на XXX, пока над этой сферой есть любая другая.";
                else return "[Fire]\n - Сфера считается в цепочке синтеза за две.\n - Температура зелья повышена на XXX, пока над этой сферой есть любая другая.";
            }
        }
        public static string fireLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Огонь]\n - Температура зелья повышена на XXX, пока над этой сферой есть любая другая.";
                else return "[Fire]\n - Температура зелья повышена на XXX, пока над этой сферой есть любая другая.";
            }
        }
        public static string fireVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Огонь]\n - Температура зелья повышена на XXX.";
                else return "[Fire]\n - Температура зелья повышена на XXX.";
            }
        }

        public static string fireSemiplasmaDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать пылающее ядро.";
                else return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать пылающее ядро.";
            }
        }

        public static string iceLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Лёд]\n - В цепочке синтеза заменяет сферу любого аспекта XXX уровня.\n - Температура зелья понижена на YYY, пока под этой сферой есть любая другая.";
                else return "[Ice]\n - В цепочке синтеза заменяет сферу любого аспекта XXX уровня.\n - Температура зелья понижена на YYY, пока под этой сферой есть любая другая.";
            }
        }
        public static string iceLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Лёд]\n - Температура зелья понижена на XXX, пока под этой сферой есть любая другая.";
                else return "[Ice]\n - Температура зелья понижена на XXX, пока под этой сферой есть любая другая.";
            }
        }

        public static string iceVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Лёд]\n - Температура зелья понижена на XXX.";
                else return "[Ice]\n - Температура зелья понижена на XXX.";
            }
        }

        public static string iceSemiplasmaDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать ледяное ядро.";
                else return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать ледяное ядро.";
            }

        }
        public static string aetherLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Эфир]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.\n - Эфир теряется при перемешивании, но передаётся при синтезе";
                else return "[Aether]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.\n - Эфир теряется при перемешивании, но передаётся при синтезе";
            }
        }

        public static string aetherLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Эфир]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.\n - Эфир не теряется при перемешивании.";
                else return "[Aether]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.\n - Эфир не теряется при перемешивании.";
            }
        }

        public static string aetherVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Эфир]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.";
                else return "[Aether]\n - Повышает эфир зелья на XXX.\n - Увеличивает остальные <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>счётчики</B></font></color> сферы на YYY.";
            }
        }
        public static string aetherSemiplasmaDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать эфирное ядро.";
                else return "\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать эфирное ядро.";
            }

        }

        public static string antimatterLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Антиматерия]\n - Пустотность зелья увеличена на YYY.\n - Этот эффект передаётся при синтезе.\n - Повысьте сферу до третьего уровня, чтобы превратить её в пустоту.";
                else return "[Antimatter]\n -Антиматерия передаётся при синтезе.\n - Повысьте сферу до третьего уровня, чтобы превратить её в пустоту.";
            }
        }

        public static string antimatterLevel4Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Антиматерия]\n - Пустотность зелья увеличена на YYY.";
                else return "[Antimatter]\n - Пустотность зелья увеличена на YYY.";
            }
        } 

        public static string antimatterSemiplasmaDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[Антиматерия]\n - Пустотность зелья увеличена на YYY.\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать пустоту.";
                else return "[Антиматерия]\n - Пустотность зелья увеличена на YYY.\n - Используйте эту сферу в цепочке синтеза, или повысьте её уровень, чтобы синтезировать пустоту."; 
            }
        }

        public static string viscosity
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU)  return " - Увеличивает вязкость на <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>XXX</b></color></font>.";
                else return " - Increasing viscosity by <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>XXX</b></color></font>.";
            }
        }
        public static string orb
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "Сфера";
                else return "Orb";
            }
        }
        public static string orbEffects
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "Эффекты сферы";
                else return "Orb effects";
            }
        }
        public static string cellEffects
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "Эффекты клетки";
                else return "Cell effects";
            }
        }
        public static string absent
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "(Отсуствует)";
                else return "(Absent)";
            }
        }
        public static string absentMultiples
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "(Отсуствуют)";
                else return "(Absent)";
            }
        }
    }
}