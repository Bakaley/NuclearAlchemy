using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubPageContent : MonoBehaviour
{
    [SerializeField]
    GameObject[] pages;

    [SerializeField]
    GameObject pageCounter;

    int currentPage;

    private void OnEnable()
    {
        if (pages.Length != 0)
        {
            pages[0].SetActive(true);
            currentPage = 1;
            pageCounter.GetComponent<TextMeshProUGUI>().text = currentPage + "/" + pages.Length;
        }
    }

    public void nextPage()
    {
        if(currentPage < pages.Length)
        {
            pages[currentPage - 1].SetActive(false);
            currentPage++;
            pages[currentPage - 1].SetActive(true);
            pageCounter.GetComponent<TextMeshProUGUI>().text = currentPage + "/" + pages.Length;

        }
    }

    public void previousPage()
    {
        if (currentPage > 1)
        {
            pages[currentPage - 1].SetActive(false);
            currentPage--;
            pages[currentPage - 1].SetActive(true);
            pageCounter.GetComponent<TextMeshProUGUI>().text = currentPage + "/" + pages.Length;
        }
    }
}
