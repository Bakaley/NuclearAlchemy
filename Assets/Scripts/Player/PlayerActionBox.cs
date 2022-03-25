using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionBox : MonoBehaviour
{
    [SerializeField]
    GameObject rightCircle, actionButton;


    Workplace currentWorkplace;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Workplace>())
        {
            if (currentWorkplace) currentWorkplace.untarget();
            currentWorkplace = other.GetComponent<Workplace>();
            currentWorkplace.target();
            rightCircle.SetActive(true);
            actionButton.GetComponent<Image>().sprite = currentWorkplace.actionSprite;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Workplace>())
        {
            currentWorkplace.untarget();
            rightCircle.SetActive(false);
        }
    }

    public void openCurrentWorkplaceUI()
    {
        if(currentWorkplace) currentWorkplace.openWorkplaceUI();
    }
}
