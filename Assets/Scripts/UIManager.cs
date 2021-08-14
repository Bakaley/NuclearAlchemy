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
    GameObject addingPlane;
    [SerializeField]
    GameObject mixingPlane;

    [SerializeField]
    GameObject target;
    [SerializeField]
    Sprite defaultBoardSample;
    [SerializeField]
    Sprite selectedBoardSample;

    public static UIManager staticInstance
    {
        get;
        private set;
    }

    public static GameObject cameraObject
    {
        get
        {
            return staticInstance.GetComponent<Canvas>().worldCamera.gameObject;
        }
    }

    static bool addingMode = false;

    static List<DissolvingSprite> addingElementsList;
    static List<DissolvingSprite>  mixingElementsList;

    private void Awake()
    {
        addingElementsList = new List<DissolvingSprite>();
        mixingElementsList = new List<DissolvingSprite>();

        staticInstance = this;
    }

    GameObject statBoard;
    void Start()
    {
        addingElementsList.Add(choosingCross.GetComponent<DissolvingSprite>());
        addingElementsList.Add(boardCross.GetComponent<DissolvingSprite>());
        addingElementsList.Add(mixingModeButton.GetComponent<DissolvingSprite>());
        addingElementsList.Add(deployButton.GetComponent<DissolvingSprite>());


        mixingElementsList.Add(movingCross.GetComponent<DissolvingSprite>());
        mixingElementsList.Add(spinningCross.GetComponent<DissolvingSprite>());
        mixingElementsList.Add(addingModeButton.GetComponent<DissolvingSprite>());
        DissolvingSprite[] targets = target.GetComponentsInChildren<DissolvingSprite>();
        foreach (DissolvingSprite element in targets) mixingElementsList.Add(element);
    }

    public void openCookingUI()
    {
        gameObject.SetActive(true);
    }

    public void closeCookingUI()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void switchToAddingMode()
    {
        if (!addingMode)
        {
            addingPlane.GetComponent<SpriteRenderer>().sprite = selectedBoardSample;
            mixingPlane.GetComponent<SpriteRenderer>().sprite = defaultBoardSample;
            foreach (DissolvingSprite element in mixingElementsList)
            {
                element.disappear();
                element.gameObject.SetActive(false);
            }
            foreach (DissolvingSprite element in addingElementsList)
            {
                element.appear();
                element.gameObject.SetActive(true);

            }

            addingMode = true;
        }
        
    }

    public void switchToMixingMode()
    {
        if (addingMode)
        {
            addingPlane.GetComponent<SpriteRenderer>().sprite = defaultBoardSample;
            mixingPlane.GetComponent<SpriteRenderer>().sprite = selectedBoardSample;
            foreach (DissolvingSprite element in mixingElementsList)
            {
                element.appear();
                element.gameObject.SetActive(true);

            }
            foreach (DissolvingSprite element in addingElementsList)
            {
                element.disappear();
                element.gameObject.SetActive(false);

            }
            addingMode = false;

        }

    }
}
