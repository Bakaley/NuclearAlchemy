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


    public void enableOrbCounters ()
    {
        switch (filter)
        {
            case StatBoardView.FILTER_TYPE.POINTS:
                if(GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: сила");
                else UIManager.showHint("Shown: power");
                break;

            case StatBoardView.FILTER_TYPE.ASPECT:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: аспект");
                else UIManager.showHint("Shown: aspect");
                break;

            case StatBoardView.FILTER_TYPE.TEMPERATURE:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: температура");
                else UIManager.showHint("Shown: temperature");
                break;

            case StatBoardView.FILTER_TYPE.AETHERNESS:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: эфир");
                else UIManager.showHint("Shown: aether");
                break;

            case StatBoardView.FILTER_TYPE.VISCOSITY:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: вязкость");
                else UIManager.showHint("Shown: viscosity");
                break;

            case StatBoardView.FILTER_TYPE.VOIDNESS:
                if (GameSettings.CurrentLanguage == GameSettings.Language.RU) UIManager.showHint("Показано: пустотность");
                else UIManager.showHint("Shown: voids");
                break;
        }
       

        foreach (Orb orb in MixingBoard.StaticInstance.orbs)
        {
            if (orb && orb.type != Orb.ORB_TYPES.UNCERTAINTY)
            {
                switch (filter)
                {
                    case StatBoardView.FILTER_TYPE.POINTS:
                        if (orb) orb.enableCounter(filter);
                        break;

                    case StatBoardView.FILTER_TYPE.ASPECT:
                        if (orb && (orb.Level == 3 || orb.type == Orb.ORB_TYPES.BLUE_PULSAR || orb.type == Orb.ORB_TYPES.RED_PULSAR || orb.type == Orb.ORB_TYPES.GREEN_PULSAR)) orb.enableCounter(filter);
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
                        if (orb && orb.antimatter) orb.enableCounter(filter);
                        break;
                }
            }
        }
    }

    public void disableOrbCounters()
    {
        UIManager.hideHint();
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
