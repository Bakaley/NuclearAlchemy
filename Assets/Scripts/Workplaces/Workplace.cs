using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Workplace : MonoBehaviour
{
    protected BoxCollider boxCollider;

    [SerializeField]
    GameObject cloudIcon, cloudTarget, cloudBackground;

    public Sprite actionSprite
    {
        get
        {
            return cloudIcon.GetComponent<Image>().sprite;
        }
    }

    public void target()
    {
        cloudBackground.SetActive(true);
        cloudIcon.SetActive(true);
        cloudTarget.SetActive(true);
    }

    public void untarget()
    {
        //cloudBackground.SetActive(false);
        //cloudIcon.SetActive(false);
        cloudTarget.SetActive(false);
    }

    abstract public void openWorkplaceUI();
}
