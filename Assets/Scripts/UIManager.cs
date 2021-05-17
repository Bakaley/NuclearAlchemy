using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject addingModeButton;
    [SerializeField]
    GameObject mixingModeButton;
    [SerializeField]
    GameObject movingCross;
    [SerializeField]
    GameObject spinningCross;
    [SerializeField]
    GameObject choosingCross;
    [SerializeField]
    GameObject boardCross;
    [SerializeField]
    GameObject deployButton;
    [SerializeField]
    GameObject addingBoardPlane;
    [SerializeField]
    GameObject mixingBoardPlane;
    [SerializeField]
    Sprite defaultBoardSample;
    [SerializeField]
    Sprite selectedBoardSample;


    public static UIManager staticInstance
    {
        get;
        private set;
    }

    static bool addingMode = false;

    static List<DissolvingUIElement> addingElementsList;
    static List<DissolvingUIElement>  mixingElementsList;

    private void Awake()
    {
        addingElementsList = new List<DissolvingUIElement>();
        mixingElementsList = new List<DissolvingUIElement>();

        staticInstance = this;
    }

    GameObject statBoard;
    void Start()
    {        
        addingElementsList.Add(choosingCross.GetComponent<DissolvingUIElement>());
        addingElementsList.Add(boardCross.GetComponent<DissolvingUIElement>());
        addingElementsList.Add(mixingModeButton.GetComponent<DissolvingUIElement>());
        addingElementsList.Add(deployButton.GetComponent<DissolvingUIElement>());


        mixingElementsList.Add(movingCross.GetComponent<DissolvingUIElement>());
        mixingElementsList.Add(spinningCross.GetComponent<DissolvingUIElement>());
        mixingElementsList.Add(addingModeButton.GetComponent<DissolvingUIElement>());
    }

    void Update()
    {
        
    }



    public void switchToAddingMode()
    {

        if (!addingMode)
        {
            foreach (DissolvingUIElement element in mixingElementsList)
            {
                element.dissolve();
            }
            foreach (DissolvingUIElement element in addingElementsList)
            {
                element.appear();
            }
            mixingBoardPlane.GetComponent<SpriteRenderer>().sprite = defaultBoardSample;
            addingBoardPlane.GetComponent<SpriteRenderer>().sprite = selectedBoardSample;
            addingMode = true;
        }
    }

    public void switchToMixingMode()
    {
        if (addingMode)
        {
            foreach (DissolvingUIElement element in mixingElementsList)
            {
                element.appear();
            }
            foreach (DissolvingUIElement element in addingElementsList)
            {
                element.dissolve();
            }
            addingMode = false;
            addingBoardPlane.GetComponent<SpriteRenderer>().sprite = defaultBoardSample;
            mixingBoardPlane.GetComponent<SpriteRenderer>().sprite = selectedBoardSample;
        }

    }
}
