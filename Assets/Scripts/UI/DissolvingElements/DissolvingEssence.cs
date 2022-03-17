using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolvingEssence : MonoBehaviour, IDissolving
{

    double currentDissolvingTimer = 0;
    static readonly double dissolveTimer = .3;

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
        currentDissolvingTimer = dissolveTimer;
        stance = STANCE.DISAPPEARING;
    }

    Material material;
    private void Awake()
    {
        material = Instantiate<Material>(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDissolvingTimer > 0)
        {
            currentDissolvingTimer -= Time.deltaTime;
            switch (stance)
            {
                case STANCE.APPEARING:

                    material.SetFloat("Shading_Vector", material.GetFloat("Shading_Vector") + Time.deltaTime * 4);

                    if (material.GetFloat("Shading_Vector") >= 1)
                    {
                        currentDissolvingTimer = 0;
                        material.SetFloat("Shading_Vector", 1);
                        stance = STANCE.NONE;
                    }
                    break;
                case STANCE.DISAPPEARING:
                    
                    material.SetFloat("Shading_Vector", material.GetFloat("Shading_Vector") - Time.deltaTime * 4);
                    if (material.GetFloat("Shading_Vector") <= 0)
                    {
                        currentDissolvingTimer = 0;
                        material.SetFloat("Shading_Vector", 0);
                        stance = STANCE.NONE;
                    }
                    break;

            }
        }
    }
}
