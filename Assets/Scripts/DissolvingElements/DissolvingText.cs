using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DissolvingText : MonoBehaviour, IDissolving
{


    [SerializeField]
    double textApperingTimer = .4;
    double currentDissolvingTimer;
    TextMeshPro tmp;
    TextMeshProUGUI tmpu;

    enum STANCE
    {
        NONE,
        APPEARING,
        DISAPPEARING
    }

    STANCE stance = STANCE.NONE;

    public void appear()
    {
        if (tmp == null && tmpu == null) initialize();
        stance = STANCE.APPEARING;
        currentDissolvingTimer = textApperingTimer;
        if (tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0);
        else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, 0);
    }

    public void disappear()
    {
        if (tmp == null && tmpu == null) initialize();
        stance = STANCE.DISAPPEARING;
        currentDissolvingTimer = textApperingTimer;
        if (tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1);
        else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, 1);
    }

    void initialize()
    {
        tmp = GetComponent<TextMeshPro>();
        if (!tmp) tmpu = GetComponent<TextMeshProUGUI>();
    }

    void Awake()
    {
        if (tmp == null && tmpu == null) initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentDissolvingTimer <= 0) stance = STANCE.NONE;
        switch (stance)
        {
            case STANCE.NONE:
                break;
            case STANCE.APPEARING:
            if (currentDissolvingTimer >= 0)
                {
                    currentDissolvingTimer -= Time.deltaTime;
                    if (tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, Convert.ToSingle(1 - currentDissolvingTimer / textApperingTimer));
                    else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, Convert.ToSingle(1 - currentDissolvingTimer / textApperingTimer));
                }
                else {
                   if(tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1);
                    else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, 1);
                    stance = STANCE.NONE;
                }
                break;
            case STANCE.DISAPPEARING:
                
                if (currentDissolvingTimer >= 0)
                {
                    currentDissolvingTimer -= Time.deltaTime;
                    if (tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, Convert.ToSingle(currentDissolvingTimer / textApperingTimer));
                    else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, Convert.ToSingle(currentDissolvingTimer / textApperingTimer));
                }
                else
                {
                    if (tmp) tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0);
                    else tmpu.color = new Color(tmpu.color.r, tmpu.color.g, tmpu.color.b, 0);
                    stance = STANCE.NONE;
                }
                break;
        }
    }
}
