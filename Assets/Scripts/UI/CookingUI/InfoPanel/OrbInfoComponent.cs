using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbInfoComponent : MonoBehaviour
{

    [SerializeField]
    public GameObject archetypeCounter;
    [SerializeField]
    public GameObject levelCounter;
    [SerializeField]
    public GameObject typeCounter;
    [SerializeField]
    public GameObject descriptionCounter;
    [SerializeField]
    public GameObject pointsCounter;

    [SerializeField]
    public GameObject archetypeCaption;
    [SerializeField]
    public GameObject typeCaption;
    [SerializeField]
    public GameObject levelCaption;
    [SerializeField]
    public GameObject pointsCaption;


    // Start is called before the first frame update
    void Start()
    {
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) archetypeCaption.GetComponent<TextMeshProUGUI>().text = "Архетип";
        else archetypeCaption.GetComponent<TextMeshProUGUI>().text = "Archetype";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) typeCaption.GetComponent<TextMeshProUGUI>().text = "Тип";
        else typeCaption.GetComponent<TextMeshProUGUI>().text = "Type";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) levelCaption.GetComponent<TextMeshProUGUI>().text = "Уровень";
        else levelCaption.GetComponent<TextMeshProUGUI>().text = "Level";
        if (GameSettings.CurrentLanguage == GameSettings.Language.RU) pointsCaption.GetComponent<TextMeshProUGUI>().text = "Сила";
        else pointsCaption.GetComponent<TextMeshProUGUI>().text = "Power";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
