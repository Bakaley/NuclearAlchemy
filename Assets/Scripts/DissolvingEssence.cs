using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingEssence : MonoBehaviour, DissolvingElement
{

    double currentDissolvingTimer = 0;
    double dissolveTimer = .3;

    enum STANCE
    {
        NONE,
        APPEARING,
        DISAPPEARING,
    }

    STANCE stance = STANCE.NONE;

    public void appear()
    {
        currentDissolvingTimer = dissolveTimer;
        stance = STANCE.APPEARING;
    }

    public void disappear()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDissolvingTimer >= 0)
        {
            currentDissolvingTimer -= Time.deltaTime;
            switch (stance)
            {
                case STANCE.APPEARING:

                    GetComponent<SpriteRenderer>().material.SetFloat("Shading_Vector", GetComponent<SpriteRenderer>().material.GetFloat("Shading_Vector") + Time.deltaTime * 4);

                    if (GetComponent<SpriteRenderer>().material.GetFloat("Shading_Vector") >= 1)
                    {
                        dissolveTimer = 0;
                        GetComponent<SpriteRenderer>().material.SetFloat("Shading_Vector", 1);

                    }
                    break;

            }
        }
    }
}
