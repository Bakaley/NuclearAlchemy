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
    GameObject infoTargetSampler;

    static GameObject infoPanel;
    static GameObject infoTarget;
    static RectTransform infoPanelTransorm;
    static Orb clickedOrb;


    static Dictionary<Orb.ORB_ARCHETYPES, String> orbArchetypeDicrionary;
    static Dictionary<Orb.ORB_TYPES, String> orbTypeDicrionary;

    enum CLICKED_BOARD
    {
        ADDING_BOARD,
        MIXING_BOARD
    }

    void Start()
    {

        mixingBoard = MixingBoard.StaticInstance;

        orbTypeDicrionary = new Dictionary<Orb.ORB_TYPES, string>();
        orbArchetypeDicrionary = new Dictionary<Orb.ORB_ARCHETYPES, string>();

        orbTypeDicrionary.Add(Orb.ORB_TYPES.BODY_ASPECT, "Тело");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.MIND_ASPECT, "Разум");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.SOUL_ASPECT, "Душа");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.SEMIPLASMA, "Полуплазма");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.SUPERNOVA_CORE, "Сверхновая");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.ICE_CORE, "Ледяное ядро");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.FIRE_CORE, "Пылающее ядро");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.AETHER_CORE, "Эфирное ядро");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.BLUE_DYE_CORE, "Синий краситель");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.RED_DYE_CORE, "Красный краситель");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.GREEN_DYE_CORE, "Зелёный краситель");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.VOID, "Пустота");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.ICE_VOID, "Ледяная пустота");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.FIRE_VOID, "Пылающая пустота");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.AETHER_VOID, "Эфирная пустота");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.SUPERNOVA_VOID, "Сверхновая пустота");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.BLUE_DYE_VOID, "Синий пульсар");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.RED_DYE_VOID, "Красный пульсар");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.GREEN_DYE_VOID, "Зелёный пульсар");

        orbTypeDicrionary.Add(Orb.ORB_TYPES.BLUE_DROP, "Капля разума");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.RED_DROP, "Капля тела");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.GREEN_DROP, "Капля души");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.SUPERNOVA_DROP, "Капля сверхновой");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.ICE_DROP, "Капля льда");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.FIRE_DROP, "Капля огня");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.AETHER_DROP, "Капля эфира");
        orbTypeDicrionary.Add(Orb.ORB_TYPES.ANTIMATTER_DROP, "Капля антиматерии");

        orbArchetypeDicrionary.Add(Orb.ORB_ARCHETYPES.ASPECT, "Аспект");
        orbArchetypeDicrionary.Add(Orb.ORB_ARCHETYPES.NEORGANIC, "Неорганика");
        orbArchetypeDicrionary.Add(Orb.ORB_ARCHETYPES.CORE, "Ядро");
        orbArchetypeDicrionary.Add(Orb.ORB_ARCHETYPES.VOID, "Пустота");
        orbArchetypeDicrionary.Add(Orb.ORB_ARCHETYPES.DROP, "Капля");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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


            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 clickedPositionMixing = mixingBoard.gameObject.transform.InverseTransformPoint(clickedPosition);
           
            clickedPositionMixing = new Vector3(Convert.ToSingle(clickedPositionMixing.x - mixingBoard.OrbShift.transform.localPosition.x + .5), Convert.ToSingle(clickedPositionMixing.y - mixingBoard.OrbShift.transform.localPosition.y + .5), -1);
            if (clickedPositionMixing.x < MixingBoard.Length && clickedPositionMixing.x >= 0 && clickedPositionMixing.y < MixingBoard.Height && clickedPositionMixing.y >= 0)
            {
                clickedPositionMixing = new Vector3((int)clickedPositionMixing.x, (int)clickedPositionMixing.y, 0);

                infoTarget = mixingBoard.target.addInfoTarget(clickedPositionMixing.x - mixingBoard.target.transform.localPosition.x, clickedPositionMixing.y - mixingBoard.target.transform.localPosition.y, infoTargetSampler);

                Vector3 spawnPoint = new Vector3(0, 0, 0);
                infoPanel = Instantiate(infoPanelSample, spawnPoint, Quaternion.identity);
                clickedOrb = mixingBoard.orbs[(int)Math.Round(clickedPositionMixing.x), (int)Math.Round(clickedPositionMixing.y)];
                infoPanelTransorm = infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>();


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
                    header.GetComponent<RectTransform>().localPosition.y + infoPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 * infoPanelTransorm.localScale.y - header.size.y/8 * header.transform.localScale.y,
                    header.GetComponent<RectTransform>().localPosition.z);


                infoPanel.transform.position = new Vector3(clickedPositionMixing.x + Convert.ToSingle(.65) + infoPanelTransorm.sizeDelta.x / 4 * infoPanelTransorm.localScale.x, clickedPositionMixing.y + infoPanelTransorm.sizeDelta.y * infoPanelTransorm.transform.localScale.y /4 - Convert.ToSingle(.25), clickedPositionMixing.z);
                infoPanel.transform.SetParent(mixingBoard.OrbShift.transform, false);
                infoPanel.transform.SetParent(transform);
                infoPanel.transform.localPosition = new Vector3(infoPanel.transform.localPosition.x, infoPanel.transform.localPosition.y, 0);
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


                    infoPanel.transform.position = new Vector3(clickedPositionAdding.x + Convert.ToSingle(.65) + infoPanelTransorm.sizeDelta.x / 4 * infoPanelTransorm.localScale.x, clickedPositionAdding.y - infoPanelTransorm.sizeDelta.y * infoPanelTransorm.transform.localScale.y  / 4 - Convert.ToSingle(.1), clickedPositionAdding.z);
                    infoPanel.transform.SetParent(addingBoard.OrbShift.transform, false);
                    infoPanel.transform.SetParent(transform);
                    infoPanel.transform.localPosition = new Vector3(infoPanel.transform.localPosition.x, infoPanel.transform.localPosition.y, 0);

                }
            }
        }
    }

    void fillPanel(CLICKED_BOARD board)
    {


        Instantiate(orbCaption, infoPanelTransorm);
        RectTransform separatorTransform = Instantiate(separator, infoPanelTransorm).GetComponent<RectTransform>();
        if (!clickedOrb) Instantiate(orbInfoAbsent, infoPanelTransorm);
        else
        {
            GameObject orbInfo = Instantiate(orbInfoSampler, infoPanelTransorm);
            TextMeshProUGUI archetypeCounter = orbInfo.GetComponent<OrbInfoComponent>().archetypeCounter.GetComponent<TextMeshProUGUI>();
            archetypeCounter.SetText(orbArchetypeDicrionary[clickedOrb.archetype] + "");
            TextMeshProUGUI levelCounter = orbInfo.GetComponent<OrbInfoComponent>().levelCounter.GetComponent<TextMeshProUGUI>();
            levelCounter.SetText(clickedOrb.Level + "");
            TextMeshProUGUI typeCounter = orbInfo.GetComponent<OrbInfoComponent>().typeCounter.GetComponent<TextMeshProUGUI>();
            typeCounter.SetText(orbTypeDicrionary[clickedOrb.type] + "");
            TextMeshProUGUI pointsCounter = orbInfo.GetComponent<OrbInfoComponent>().pointsCounter.GetComponent<TextMeshProUGUI>();
            if(clickedOrb.aetherImpact == 0) pointsCounter.SetText(clickedOrb.pointsImpact + "");
            else pointsCounter.SetText(clickedOrb.pointsImpact + "");

            TextMeshProUGUI descriptionCounter = orbInfo.GetComponent<OrbInfoComponent>().descriptionCounter.GetComponent<TextMeshProUGUI>();
            descriptionCounter.SetText(clickedOrb.name + "");
        }


        Instantiate(orbEffectsCaption, infoPanelTransorm);
        Instantiate(separator, infoPanelTransorm);


        if (clickedOrb)
        {
           // Selection.activeGameObject = clickedOrb.gameObject;

            if (!clickedOrb.fiery && !clickedOrb.frozen && clickedOrb.aetherImpact == 0 && !clickedOrb.antimatter) Instantiate(orbEffectsAbsent, infoPanelTransorm);
            else
            {

                if (clickedOrb.fiery)
                {
                    if (clickedOrb.Level == 3)
                    {
                        GameObject fireLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().fireLevel3Desrcription, infoPanelTransorm);
                        fireLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = fireLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.Level + (int)Math.Round(clickedOrb.Level * .2 * clickedOrb.aetherImpact) + "");
                    }
                    else
                    {
                        GameObject fireLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().fireLevel12Desrcription, infoPanelTransorm);
                        fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = fireLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.Level + (int)Math.Round(clickedOrb.Level * .2 * clickedOrb.aetherImpact) + "");

                    }
                }
                if (clickedOrb.frozen)
                {
                    if (clickedOrb.Level == 3)
                    {
                        GameObject iceLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().iceLevel3Desrcription, infoPanelTransorm);
                        iceLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.Level + (int)Math.Round(clickedOrb.Level * .2 * clickedOrb.aetherImpact) + "");
                    }
                    else
                    {
                        GameObject iceLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().iceLevel12Desrcription, infoPanelTransorm);
                        iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = iceLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.Level + (int)Math.Round(clickedOrb.Level * .2 * clickedOrb.aetherImpact) + "");

                    }
                }
                if (clickedOrb.aetherImpact != 0)
                {
                    if (clickedOrb.Level == 3)
                    {
                        GameObject aetherLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel3Desrcription, infoPanelTransorm);
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.aetherImpact + "");
                        aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("Y", clickedOrb.aetherImpact * 20 + "");
                    }
                    else
                    {
                        GameObject aetherLevel12Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().aetherLevel12Desrcription, infoPanelTransorm);
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.aetherImpact + "");
                        aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text = aetherLevel12Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("Y", clickedOrb.aetherImpact * 20 + "");
                    }
                }
                if (clickedOrb.antimatter)
                {
                    if (clickedOrb.Level == 3)
                    {
                        GameObject antimatterLevel3Desrcription = Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel3Desrcription, infoPanelTransorm);
                        antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text = antimatterLevel3Desrcription.GetComponent<TextMeshProUGUI>().text.Replace("X", clickedOrb.GetComponent<AspectOrb>().aspectImpact + "");

                    }
                    else Instantiate(orbEffectsSampler.GetComponent<EffectInfoComponent>().antimatterLevel12Desrcription, infoPanelTransorm);

                }
            }
        }
        else
        {
            Instantiate(orbEffectsAbsent, infoPanelTransorm);
        }
    }
}
