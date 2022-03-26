using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookingManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] mixingModeOnly;
    [SerializeField]
    GameObject[] addingModeOnly;

    [SerializeField]
    GameObject addingPlane, mixingPlane;

    [SerializeField]
    GameObject target;
    [SerializeField]
    Sprite defaultBoardSample;
    [SerializeField]
    Sprite selectedBoardSample;



    static List<DissolvingSprite> addingModeElements;
    static List<DissolvingSprite> mixingModeElements;

    public static CookingManager staticInstance
    {
        get; private set;
    }

    private void Awake()
    {
        staticInstance = this;

        addingModeElements = new List<DissolvingSprite>();
        mixingModeElements = new List<DissolvingSprite>();

        foreach(GameObject obj in mixingModeOnly)
        {
            mixingModeElements.Add(obj.GetComponent<DissolvingSprite>());
        }

        foreach (GameObject obj in addingModeOnly)
        {
            addingModeElements.Add(obj.GetComponent<DissolvingSprite>());
        }
    }

    public static bool addingMode { get; private set; } = false;

    public static Camera CookingCamera
    {
        get
        {
            return staticInstance.GetComponentInParent<Camera>();
        }
    }
    public void switchToAddingMode()
    {
        if (!addingMode)
        {
            addingPlane.GetComponent<SpriteRenderer>().sprite = selectedBoardSample;
            mixingPlane.GetComponent<SpriteRenderer>().sprite = defaultBoardSample;
            foreach (DissolvingSprite element in mixingModeElements)
            {
                element.disappear();
                element.gameObject.SetActive(false);
            }
            foreach (DissolvingSprite element in target.GetComponentsInChildren<DissolvingSprite>())
            {
                element.disappear();
            }
            foreach (DissolvingSprite element in addingModeElements)
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
            foreach (DissolvingSprite element in mixingModeElements)
            {
                element.appear();
                element.gameObject.SetActive(true);

            }
            foreach (DissolvingSprite element in target.GetComponentsInChildren<DissolvingSprite>())
            {
                element.appear();
            }
            foreach (DissolvingSprite element in addingModeElements)
            {
                element.disappear();
                element.gameObject.SetActive(false);

            }
            addingMode = false;

            target.GetComponent<Target>().redrawTargets();
        }
    }

    public void switchLensingMode()
    {
        UIManager.showHint("Линзирование недоступно в демо-версии");
    }

   

    private void OnEnable()
    {
        if (MixingBoard.isEmpty) switchToAddingMode();
    }
}
