using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOrb : Orb
{
    public override void affectWith(EFFECT_TYPES effect, int aetherIncreaseOn = 0)
    {
        switch (effect)
        {
            case EFFECT_TYPES.FROZE:
                if (affectTimer<=0)
                {
                    if (fiery) fiery = false;
                    frozen = true;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.FIRE:
                if (affectTimer<=0)
                {
                    fiery = true;
                    if (frozen) frozen = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.AETHER:
                if (affectTimer<=0)
                {
                    if (aetherCount != 0) increaseAether(aetherIncreaseOn);
                    else
                    {
                        if (frozen) frozen = false;
                        if (fiery) fiery = false;
                        aetherCount = aetherIncreaseOn;
                        nextLevelOrb = MixingBoard.StaticInstance.greenVoid;
                        Invoke("levelUp", .2f);
                        affectTimer = .5f;
                    }
                }
                break;
            case EFFECT_TYPES.LEVEL_UP:
                if (affectTimer<=0)
                {
                    if (frozen) frozen = false;
                    if (fiery) fiery = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.yellowVoid;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.ANTIMATTER:
                if (affectTimer<=0)
                {
                    if (frozen) frozen = false;
                    if (fiery) fiery = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.BLUE_DYE:
                if (affectTimer<=0)
                {
                    if (frozen) frozen = false;
                    if (fiery) fiery = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.bluePulsar;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.RED_DYE:
                if (affectTimer<=0)
                {
                    if (frozen) frozen = false;
                    if (fiery) fiery = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.redPulsar;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.GREEN_DYE:
                if (affectTimer<=0)
                {
                    if (frozen) frozen = false;
                    if (fiery) fiery = false;
                    if (aetherIncreaseOn != 0) aetherCount = 0;
                    nextLevelOrb = MixingBoard.StaticInstance.greenPulsar;
                    Invoke("levelUp", .2f);
                    affectTimer = .5f;
                }
                break;
            case EFFECT_TYPES.DISSOLVE:
                break;
        }
    }

    public EFFECT_TYPES voidEffect
    {
        get
        {
            return effectDictionary[type];
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
