using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreOrb : Orb
{

    public EFFECT_TYPES coreEffect
    {
        get
        {
            return effectDictionary[type];
        }
    }

    static Dictionary<ORB_TYPES, EFFECT_TYPES> effectDictionary;

    public override void affectWith(EFFECT_TYPES effect)
    {
        switch (effect)
        {
            case EFFECT_TYPES.FROZE:
                break;
            case EFFECT_TYPES.FIRE:
                break;
            case EFFECT_TYPES.AETHER:
                break;
            case EFFECT_TYPES.LEVEL_UP:
                break;
            case EFFECT_TYPES.GREEN_DYE:
                break;
            case EFFECT_TYPES.BLUE_DYE:
                break;
            case EFFECT_TYPES.RED_DYE:
                break;
            case EFFECT_TYPES.DISSOLVE:
                break;
        }
    }
    protected override void Start()
    {

        if(effectDictionary == null)
        {
            effectDictionary = new Dictionary<ORB_TYPES, EFFECT_TYPES>();
            effectDictionary.Add(ORB_TYPES.ICE_CORE, EFFECT_TYPES.FROZE);
            effectDictionary.Add(ORB_TYPES.FIRE_CORE, EFFECT_TYPES.FIRE);
            effectDictionary.Add(ORB_TYPES.AETHER_CORE, EFFECT_TYPES.AETHER);
            effectDictionary.Add(ORB_TYPES.SUPERNOVA_CORE, EFFECT_TYPES.LEVEL_UP);
            effectDictionary.Add(ORB_TYPES.BLUE_DYE_CORE, EFFECT_TYPES.BLUE_DYE);
            effectDictionary.Add(ORB_TYPES.RED_DYE_CORE, EFFECT_TYPES.RED_DYE);
            effectDictionary.Add(ORB_TYPES.GREEN_DYE_CORE, EFFECT_TYPES.GREEN_DYE);
        }
       
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
