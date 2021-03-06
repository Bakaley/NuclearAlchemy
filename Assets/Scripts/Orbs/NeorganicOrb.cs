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
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
                List<ReplacingOrbStruct> uncertaintList = new List<ReplacingOrbStruct>();

                if (fiery) uncertaintList.Add(new ReplacingOrbStruct(MixingBoard.StaticInstance.redCore));
                if (frozen) uncertaintList.Add(new ReplacingOrbStruct(MixingBoard.StaticInstance.blueCore));
                if (aetherCount != 0) uncertaintList.Add(new ReplacingOrbStruct(MixingBoard.StaticInstance.greenCore, false, false, aetherCount));
                if (antimatter) uncertaintList.Add(new ReplacingOrbStruct(MixingBoard.StaticInstance.purpleVoid));

                if(uncertaintList.Count == 0)
                {
                    DestroyIn(.5);
                }
                else if(uncertaintList.Count == 1)
                {
                    replacingOrb = uncertaintList[0];
                    replace(replacingOrb);
                }
                else
                {
                    replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.uncertaintyOrb, false, false, 0, false, uncertaintList);
                    replace(replacingOrb);
                }

                break;
            case EFFECT_TYPES.ANTIMATTER:
                addAntimatter();
                break;
            case EFFECT_TYPES.GREEN_DYE:
                replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.green1, fiery, frozen, aetherCount, antimatter);
                replace(replacingOrb);
                break;
            case EFFECT_TYPES.BLUE_DYE:
                replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.blue1, fiery, frozen, aetherCount, antimatter);
                replace(replacingOrb);
                break;
            case EFFECT_TYPES.RED_DYE:
                replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.red1, fiery, frozen, aetherCount, antimatter);
                replace(replacingOrb);
                break;
            case EFFECT_TYPES.DISSOLVE:
                DestroyIn(.5);
                break;
        }
    }
}
