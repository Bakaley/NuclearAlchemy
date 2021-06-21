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
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: вкус";
                break;

            case StatBoardView.FILTER_TYPE.ASPECT:
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: аспект";
                break;

            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: температура";
                break;

            case StatBoardView.FILTER_TYPE.AETHERNESS:
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: эфир";
                break;

            case StatBoardView.FILTER_TYPE.VISCOSITY:
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: вязкость";
                break;

            case StatBoardView.FILTER_TYPE.VOIDNESS:
                currentShowingPanel.GetComponent<TextMeshProUGUI>().text = "Показано: пустоты";
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
                    if(orb && orb.Level == 3) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.TEMPERATURE:
                    if (orb && (orb.frozen || orb.fiery)) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.AETHERNESS:
                    if (orb && orb.aetherImpact != 0) orb.enableCounter(filter);
                    break;

                case StatBoardView.FILTER_TYPE.VISCOSITY:
                    if (orb) orb.enableCounter(filter);
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
