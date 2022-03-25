using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Camera cookingCamera, mainCamera;

    [SerializeField]
    GameObject MovingUI;

    [SerializeField]
    GameObject infoWindow;

    [SerializeField]
    GameObject hintPanel;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        staticInstance = this;
    }

    static UIManager staticInstance;

    public static Camera UICamera
    {
        get
        {
            return staticInstance.GetComponentInParent<Camera>();
        }
    }

    public static bool cookingMode
    {
        get; private set;
    } = false;


    static bool lensingMode;


    GameObject statBoard;
    void Start()
    {
        //openCookingUI();

        infoWindow.SetActive(true);
    }

    public static void openCookingUI()
    {
        cookingMode = true;
        staticInstance.mainCamera.gameObject.SetActive(false);
        staticInstance.MovingUI.SetActive(false);
        staticInstance.cookingCamera.gameObject.SetActive(true);
    }

    public static void closeCookingUI()
    {
        cookingMode = false;
        staticInstance.mainCamera.gameObject.SetActive(true);
        staticInstance.MovingUI.SetActive(true);
        staticInstance.cookingCamera.gameObject.SetActive(false);
    }

    public static void showHint(string hint)
    {
        staticInstance.hintPanel.GetComponent<TextMeshProUGUI>().text = hint;
        staticInstance.hintPanel.SetActive(true);
    }

    public static void showHint(string hint, float time)
    {
        staticInstance.hintPanel.GetComponent<TextMeshProUGUI>().text = hint;
        staticInstance.hintPanel.SetActive(true);
        staticInstance.Invoke("hideHintInvoking", time);
    }

    //Invoke нельзя применять к статическим методам - ставим костыль для монолита
    void hideHintInvoking()
    {
        hideHint();
    }

    public static void hideHint()
    {
        staticInstance.hintPanel.SetActive(false);
    }
}
