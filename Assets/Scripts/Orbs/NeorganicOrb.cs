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

    public override void affectWith(EFFECT_TYPES effect, int aetherToAdd = 0, Orb.ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE)
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
                increaseAether(aetherToAdd);
                break;
            case EFFECT_TYPES.LEVEL_UP:
                if (fiery && frozen)
                {
                    if (Mathf.Round(gameObject.transform.localPosition.y) >= 2)
                    {
                        nextLevelOrb = MixingBoard.StaticInstance.blueCore;
                    }
                    else if (Mathf.Round(gameObject.transform.localPosition.y) <= 1)
                    {
                        nextLevelOrb = MixingBoard.StaticInstance.redCore;
                    }
                }
                else if (fiery) nextLevelOrb = MixingBoard.StaticInstance.redCore;
                else if (frozen) nextLevelOrb = MixingBoard.StaticInstance.blueCore;
                else if (aetherCount != 0) nextLevelOrb = MixingBoard.StaticInstance.greenCore;
                else if (antimatter) nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;

                if (nextLevelOrb)
                {
                    fiery = false;
                    frozen = false;
                    antimatter = false;
                    Invoke("levelUp", .2f);
                }
                else DestroyIn(.5);

                break;
            case EFFECT_TYPES.ANTIMATTER:
                addAntimatter();
                break;
            case EFFECT_TYPES.GREEN_DYE:
                nextLevelOrb = MixingBoard.StaticInstance.green1;
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.BLUE_DYE:
                nextLevelOrb = MixingBoard.StaticInstance.blue1;
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.RED_DYE:
                nextLevelOrb = MixingBoard.StaticInstance.red1;
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.DISSOLVE:
                DestroyIn(.5);
                break;
        }
    }
}
