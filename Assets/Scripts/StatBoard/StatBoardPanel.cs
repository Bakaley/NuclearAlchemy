using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatBoardPanel : MonoBehaviour
{
    [SerializeField]
    GameObject statBoardText;

    [SerializeField]
    GameObject statBoardCounter;

    [SerializeField]
    StatBoardView.FILTER_TYPE filter;

    [SerializeField]
    GameObject currentShowingPanel;

    public void enableOrbCounters ()
    {
        switch (filter)
        {
            case StatBoardView.FILTER_TYPE.POINTS:
                if(GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: сила";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: power";
                break;

            case StatBoardView.FILTER_TYPE.ASPECT:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: аспект";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: aspect";
                break;

            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: температура";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: temperature";
                break;

            case StatBoardView.FILTER_TYPE.AETHERNESS:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: эфир";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: aether";
                break;

            case StatBoardView.FILTER_TYPE.VISCOSITY:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: вязкость";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: viscosity";
                break;

            case StatBoardView.FILTER_TYPE.VOIDNESS:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: пустоты";
                else currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Shown: voids";
                break;
        }

        currentShowingPanel.SetActive(true);
       

        foreach (Orb orb in MixingBoard.StaticInstance.orbs)
        {
            switch (filter)
            {
                case StatBoardView.FILTER_TYPE.POINTS:
                    if (orb) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.ASPECT:
                    if(orb && (orb.Level == 3 || orb.type == Orb.ORB_TYPES.BLUE_PULSAR || orb.type == Orb.ORB_TYPES.RED_PULSAR || orb.type == Orb.ORB_TYPES.GREEN_PULSAR)) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.TEMPERATURE:
                    if (orb && (orb.frozen || orb.fiery)) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.AETHERNESS:
                    if (orb && orb.aetherImpact != 0) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.VISCOSITY:
                    if (orb && (orb.Level >= 3)) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.VOIDNESS:
                    if(orb && orb.archetype == Orb.ORB_ARCHETYPES.VOID) orb.enableCounter(filter);
                    break;
            }
        }
    }

    public void disableOrbCounters()
    {
        currentShowingPanel.SetActive(false);
        foreach (Orb orb in MixingBoard.StaticInstance.orbs)
        {
            if (orb) orb.disableCounter();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
