using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOrb : Orb, AspectImpactInterface
{
    public override void affectWith(EFFECT_TYPES effect, int aetherIncreaseOn = 0, Orb.ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE)
    {
        if (archetype != ORB_ARCHETYPES.VOID)
        {
            switch (effect)
            {
                case EFFECT_TYPES.FROZE:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.blueVoid);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.FIRE:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.redVoid);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.AETHER:
                    if (affectTimer <= 0)
                    {
                        if (aetherCount != 0) increaseAether(aetherIncreaseOn);
                        else
                        {
                            replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.greenVoid, false, false, aetherIncreaseOn);
                            Invoke("replace", .2f);
                            affectTimer = .5f;
                        }
                    }
                    else replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.greenVoid, false, false, replacingOrb.aether + aetherIncreaseOn);
                    break;
                case EFFECT_TYPES.LEVEL_UP:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.yellowVoid);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.ANTIMATTER:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.purpleVoid);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.BLUE_DYE:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.bluePulsar);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.RED_DYE:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.redPulsar);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.GREEN_DYE:
                    if (affectTimer <= 0)
                    {
                        replacingOrb = new ReplacingOrbStruct(MixingBoard.StaticInstance.greenPulsar);
                        Invoke("replace", .2f);
                        affectTimer = .5f;
                    }
                    break;
                case EFFECT_TYPES.DISSOLVE:
                    break;
            }
        }
    }

    public EFFECT_TYPES voidEffect
    {
        get
        {
            return effectDictionary[type];
        }
    }

    public int aspectImpact
    {
        get
        {
            if (type == ORB_TYPES.BLUE_PULSAR || type == ORB_TYPES.RED_PULSAR || type == ORB_TYPES.GREEN_PULSAR)
            {
                return 1;
            }
            else return 0;
        }
    }

    static Dictionary<ORB_TYPES, EFFECT_TYPES> effectDictionary;

    // Start is called before the first frame update
    override protected void Start()
    {
        if (effectDictionary == null)
        {
            effectDictionary = new Dictionary<ORB_TYPES, EFFECT_TYPES>();
            effectDictionary.Add(ORB_TYPES.VOID, EFFECT_TYPES.ANTIMATTER);
            effectDictionary.Add(ORB_TYPES.FIRE_VOID, EFFECT_TYPES.FIRE);
            effectDictionary.Add(ORB_TYPES.ICE_VOID, EFFECT_TYPES.FROZE);
            effectDictionary.Add(ORB_TYPES.AETHER_VOID, EFFECT_TYPES.AETHER);
            effectDictionary.Add(ORB_TYPES.SUPERNOVA_VOID, EFFECT_TYPES.LEVEL_UP);
            effectDictionary.Add(ORB_TYPES.BLUE_PULSAR, EFFECT_TYPES.BLUE_DYE);
            effectDictionary.Add(ORB_TYPES.RED_PULSAR, EFFECT_TYPES.RED_DYE);
            effectDictionary.Add(ORB_TYPES.GREEN_PULSAR, EFFECT_TYPES.GREEN_DYE);
        }


        base.Start();
    }

    private double affectTimer = 0;

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        if (affectTimer >= 0) affectTimer -= Time.deltaTime;
    }
}
