using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeorganicOrb : Orb
{
  
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void affectWith(EFFECT_TYPES effect)
    {
        switch (effect)
        {
            case EFFECT_TYPES.FROZE:
                addIce();
                break;
            case EFFECT_TYPES.FIRE:
                addFire();
                break;
            case EFFECT_TYPES.AETHER:
                addAether(3);
                break;
            case EFFECT_TYPES.LEVEL_UP:
                if(fiery && frozen)
                {
                    if(Mathf.Round(gameObject.transform.localPosition.y) >= 2)
                    {
                        nextLevelOrb = MixingBoard.orbDictionary["BlueCore"];
                    }
                    else if (Mathf.Round(gameObject.transform.localPosition.y) <= 1)
                    {
                        nextLevelOrb = MixingBoard.orbDictionary["RedCore"];

                    }
                }
                else if (fiery) nextLevelOrb = MixingBoard.orbDictionary["RedCore"];
                else if (frozen) nextLevelOrb = MixingBoard.orbDictionary["BlueCore"];
                else if (aetherCount != 0) nextLevelOrb = MixingBoard.orbDictionary["GreenCore"];
                else if (antimatter) nextLevelOrb = MixingBoard.orbDictionary["PurpleVoid"];

                if (nextLevelOrb)
                {
                    fiery = false;
                    frozen = false;
                    aetherCount = 0;
                    antimatter = false;
                    Invoke("levelUp", .2f);
                }
                else DestroyIn(.5);

                break;
            case EFFECT_TYPES.ANTIMATTER:
                addAntimatter();
                break;
            case EFFECT_TYPES.GREEN_DYE:
                nextLevelOrb = MixingBoard.orbDictionary["Green1"];
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.BLUE_DYE:
                nextLevelOrb = MixingBoard.orbDictionary["Blue1"];
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.RED_DYE:
                nextLevelOrb = MixingBoard.orbDictionary["Red1"];
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.DISSOLVE:
                DestroyIn(.5);
                break;
        }
    }
}
