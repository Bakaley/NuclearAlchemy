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

        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BODY_ASPECT, "����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.MIND_ASPECT, "�����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SOUL_ASPECT, "����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SEMIPLASMA, "����������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_CORE, "����������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_CORE, "������� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_CORE, "�������� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_CORE, "������� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_DYE_CORE, "����� ���������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_DYE_CORE, "������� ���������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_DYE_CORE, "������ ���������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.VOID, "�������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_VOID, "������� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_VOID, "�������� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_VOID, "������� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_VOID, "���������� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_PULSAR, "����� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_PULSAR, "������� �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_PULSAR, "������ �������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.BLUE_DROP, "����� ������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.RED_DROP, "����� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.GREEN_DROP, "����� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.SUPERNOVA_DROP, "����� ����������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ICE_DROP, "����� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.FIRE_DROP, "����� ����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.AETHER_DROP, "����� �����");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.ANTIMATTER_DROP, "����� �����������");
        orbTypeDicrionaryRU.Add(Orb.ORB_TYPES.UNCERTAINTY, "����� ���������������");

        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.ASPECT, "��������� �����");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.NEORGANIC, "����������");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.CORE, "����");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.VOID, "�������");
        orbArchetypeDicrionaryRU.Add(Orb.ORB_ARCHETYPES.DROP, "�����");
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
        if (Input.GetMouseButtonDown(0) && !DraftModule.opened && !CancelMenu.Opened)
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



            Vector3 clickedPosition = UIManager.cameraObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            Vector3 clickedPositionMixing = mixingBoard.gameObject.transform.InverseTransformPoint(clickedPosition);
            clickedPositionMixing = new Vector3(Convert.ToSingle(clickedPositionMixing.x - mixingBoard.OrbShift.transform.localPosition.x + .5), Convert.ToSingle(clickedPositionMixing.y - mixingBoard.OrbShift.transform.localPosition.y + .5), -1);

            if (uncertaintyDrafter != null)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(uncertaintyDrafter.GetComponent<UncertaintyDrafter>().cellsBlock.GetComponent<RectTransform>(), Input.mousePosition, UIManager.cameraObject.GetComponent<Camera>()))
                {
                    if (clickedPositionMixing.x < MixingBoard.Length && clickedPositionMixing.x >= 0 && clickedPositionMixing.y < MixingBoard.Height && clickedPositionMixing.y >= 0)
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
                if (clickedPositionMixing.x < MixingBoard.Length && clickedPositionMixing.x >= 0 && clickedPositionMixing.y < MixingBoard.Height && clickedPositionMixing.y >= 0)
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
                    if (clickedPositionAdding.x < AddingBoard.length && clickedPositionAdding.x >= 0 && clickedPositionAdding.y < AddingBoard.height && clickedPositionAdding.y >= 0)
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

        Instantiate(orbCaption, infoPanelTransorm).GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.orb;
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

            TextMeshProUGUI descriptionCounter = orbInfo.GetComponent<OrbInfoComponent>().descriptionCounter.GetComponent<TextMeshProUGUI>();

            string descriptionString;
            if(GameSettings.CurrentLanguage == GameSettings.Language.RU) descriptionString = clickedOrb.orbDescription;
            else descriptionString = clickedOrb.orbDescriptionEN;
            if (clickedOrb)
            {
                string defaultString = "";
                if (clickedOrb.type == Orb.ORB_TYPES.AETHER_DROP || clickedOrb.type == Orb.ORB_TYPES.AETHER_CORE || clickedOrb.type == Orb.ORB_TYPES.AETHER_VOID)
                {
                    defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>";
                    descriptionString = descriptionString.Replace("XXX", defaultString);
                }
                else if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.ASPECT && clickedOrb.Level == 3)
                {
                    defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (1 + (int)(1 * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</b></color></font>";
                    string aetherString = "<font=\"InfoPanelSDF\"><#808080> [" + 1 + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (int)(1 * Orb.aetherMultiplier * clickedOrb.aetherImpact) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>";
                    if (clickedOrb.aetherImpact != 0)
                    {
                        defaultString += aetherString;
                    }
                }
                
                descriptionString = descriptionString.Replace("XXX", defaultString);

                if (ConstellationManager.CONSTELLATION1 == ConstellationManager.CONSTELLATION.SUPERNOVA || ConstellationManager.CONSTELLATION2 == ConstellationManager.CONSTELLATION.SUPERNOVA)
                {
                    string viscosity = InfoPanelLexemas.viscosity;
                    if (clickedOrb.viscosityImpact != 0)
                    {
                        if (clickedOrb.aetherImpact == 0) viscosity = "- ����������� �������� �� <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.viscosityImpact + "</b></color></font>.";
                        else viscosity = "- ����������� �������� �� <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.viscosityImpact + "</b></color></font><font=\"InfoPanelSDF\"><#808080> [" + clickedOrb.basicViscosity + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (int)(clickedOrb.basicViscosity * Orb.aetherMultiplier * clickedOrb.aetherImpact) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>.";
                        descriptionCounter.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCounter.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionCounter.transform.parent.GetComponent<RectTransform>().sizeDelta.y + 15);
                        Canvas.ForceUpdateCanvases();
                        descriptionString = descriptionString + "\n" + viscosity;
                    }
                }
            }
            descriptionCounter.SetText(descriptionString);
        }


        Instantiate(orbEffectsCaption, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.orbEffects;
        Instantiate(separator, infoPanelTransorm);


        if (clickedOrb)
        {
            //Selection.activeGameObject = clickedOrb.gameObject;

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

                    }
                }
                if (clickedOrb.aetherImpact != 0)
                {
                    if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.VOID || clickedOrb.archetype == Orb.ORB_ARCHETYPES.CORE)
                    {
                        GameObject aetherVoidCoreDesrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherVoidCoreDesrcription, infoPanelTransorm);
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherVoidCoreDescription;
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text = aetherVoidCoreDesrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * 20 + "%</b></color></font>");

                    }
                    else if (clickedOrb.Level == 3)
                    {
                        GameObject aetherLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel3Desrcription, infoPanelTransorm);
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherLevel3Description;
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * 20 + "%</b></color></font>");
                    }
                    else
                    {
                        GameObject aetherLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel12Desrcription, infoPanelTransorm);
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.aetherLevel12Description;
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact + "</b></color></font>");
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + clickedOrb.aetherImpact * 20 + "%</b></color></font>");
                    }
                }
                if (clickedOrb.antimatter)
                {
                    if (clickedOrb.archetype == Orb.ORB_ARCHETYPES.VOID)
                    {
                        string voidString = "";

                        switch (clickedOrb.type)
                        {
                            case Orb.ORB_TYPES.VOID:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "���������� ��������� ����� � �����������";
                                else voidString = "turn the rest in the antimatter";
                                break;
                            case Orb.ORB_TYPES.ICE_VOID:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "���������� ���������";
                                else voidString = "freeze the rest";
                                break;
                            case Orb.ORB_TYPES.FIRE_VOID:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "��������� ���������";
                                else voidString = "fire up the rest";
                                break;
                            case Orb.ORB_TYPES.AETHER_VOID:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "��������� ������ ���������";
                                else voidString = "fill up with aether the rest";
                                break;
                            case Orb.ORB_TYPES.SUPERNOVA_VOID:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "�������� ������� ���������";
                                else voidString = "level up the rest";
                                break;
                            case Orb.ORB_TYPES.BLUE_PULSAR:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "����������� ��������� � ������ ������";
                                else voidString = "paint in mind aspect the rest";
                                break;
                            case Orb.ORB_TYPES.RED_PULSAR:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "����������� ��������� � ������ ����";
                                else voidString = "paint in body aspect the rest";
                                break;
                            case Orb.ORB_TYPES.GREEN_PULSAR:
                                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) voidString = "����������� ��������� � ������ ����";
                                else voidString = "paint in soul aspect the rest";
                                break;
                        }

                        string defaultString = "<b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (1 + (int)(1 * Orb.aetherMultiplier * clickedOrb.aetherImpact)) + "</b></color></font>";
                        string aetherString = "<font=\"InfoPanelSDF\"><#808080> [" + 1 + " + </color></font><b><font=\"InfoPanelCounterSDF\"><#FFFFFF>" + (int)(1 * Orb.aetherMultiplier * clickedOrb.aetherImpact) + "</color></font></b><font=\"InfoPanelSDF\"><#808080>]</color></font>";
                        if (clickedOrb.aetherImpact != 0)
                        {
                            defaultString += aetherString;
                        }
                        GameObject antimatterLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel3Desrcription, infoPanelTransorm);
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.antimatterLevel4Description;
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("YYY", defaultString);
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("XXX", voidString);
                    }
                    else
                    {
                        GameObject antimatterLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel12Desrcription, infoPanelTransorm);
                        antimatterLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = InfoPanelLexemas.antimatterLevel12Description;


                    }
                }
            }
        }
        else
        {
            Instantiate(orbEffectsAbsent, infoPanelTransorm).GetComponentInChildren<TextMeshProUGUI>().text = InfoPanelLexemas.absentMultiples;
        }
    }

    class InfoPanelLexemas
    {
        public static string fireLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[�����]\n - ����� ��������� � ������� ������� �� ���.\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
                else return "[Fire]\n - ����� ��������� � ������� ������� �� ���.\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
            }
        }
        public static string fireLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[�����]\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
                else return "[Fire]\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
            }
        }
        public static string fireVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[�����]\n - ����������� ����� �������� �� XXX.";
                else return "[Fire]\n - ����������� ����� �������� �� XXX.";
            }
        }

        public static string iceLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[˸�]\n - � ������� ������� �������� ����� ������ ������� XXX ������.\n - ����������� ����� �������� �� YYY, ���� ��� ���� ������ ���� ����� ������..";
                else return "[Ice]\n - � ������� ������� �������� ����� ������ ������� XXX ������.\n - ����������� ����� �������� �� YYY, ���� ��� ���� ������ ���� ����� ������.";
            }
        }
        public static string iceLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[˸�]\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
                else return "[Ice]\n - ����������� ����� �������� �� XXX, ���� ��� ���� ������ ���� ����� ������.";
            }
        }

        public static string iceVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[˸�]\n - ����������� ����� �������� �� XXX.";
                else return "[Ice]\n - ����������� ����� �������� �� XXX.";
            }
        }

        public static string aetherLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[����]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.\n - ���� �������� ��� �������������, �� ��������� ��� �������";
                else return "[Aether]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.\n - ���� �������� ��� �������������, �� ��������� ��� �������";
            }
        }

        public static string aetherLevel3Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[����]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.\n - ���� �� �������� ��� �������������.";
                else return "[Aether]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.\n - ���� �� �������� ��� �������������.";
            }
        }

        public static string aetherVoidCoreDescription
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[����]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.";
                else return "[Aether]\n - �������� ���� ����� �� XXX.\n - ����������� ��������� <font=\"InfoPanelCounterSDF\"><#FFFFFF> <B>��������</B></font></color> ����� �� YYY.";
            }
        }
        public static string antimatterLevel12Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[�����������]\n - ����������� ��������� ��� �������.\n - �������� ����� �� �������� ������, ����� ���������� � � �������.";
                else return "[Antimatter]\n -����������� ��������� ��� �������.\n - �������� ����� �� �������� ������, ����� ���������� � � �������.";
            }
        }

        public static string antimatterLevel4Description
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "[�����������]\n - ����������� ����� ��������� �� YYY.";
                else return "[Antimatter]\n - ����������� ����� ��������� �� YYY.";
            }
        }

        public static string viscosity
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU)  return " - ����������� �������� �� <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>XXX</b></color></font>.";
                else return " - Increasing viscosity by <b><font=\"InfoPanelCounterSDF\"><#FFFFFF>XXX</b></color></font>.";
            }
        }
        public static string orb
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "�����";
                else return "Orb";
            }
        }
        public static string orbEffects
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "������� �����";
                else return "Orb effects";
            }
        }
        public static string cellEffects
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "������� ������";
                else return "Cell effects";
            }
        }
        public static string absent
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "(����������)";
                else return "(Absent)";
            }
        }
        public static string absentMultiples
        {
            get
            {
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) return "(����������)";
                else return "(Absent)";
            }
        }
    }
}
