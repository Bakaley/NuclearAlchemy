using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropOrb : Orb
{
    [SerializeField]
    public GameObject coreSphereList;
    public override void affectWith(EFFECT_TYPES effect, int aetherCount = 0, Orb.ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE)
    {

    }

    public int targetY = 0;
    public OrbBox targetedtOrb;

    static Dictionary<ORB_TYPES, EFFECT_TYPES> effectDictionary;

    // Start is called before the first frame update
    override protected void Start()
    {
        if (effectDictionary == null)
        {
            effectDictionary = new Dictionary<ORB_TYPES, EFFECT_TYPES>();
            effectDictionary.Add(ORB_TYPES.ICE_DROP, EFFECT_TYPES.FROZE);
            effectDictionary.Add(ORB_TYPES.FIRE_DROP, EFFECT_TYPES.FIRE);
            effectDictionary.Add(ORB_TYPES.AETHER_DROP, EFFECT_TYPES.AETHER);
            effectDictionary.Add(ORB_TYPES.SUPERNOVA_DROP, EFFECT_TYPES.LEVEL_UP);
            effectDictionary.Add(ORB_TYPES.ANTIMATTER_DROP, EFFECT_TYPES.ANTIMATTER);
            effectDictionary.Add(ORB_TYPES.BLUE_DROP, EFFECT_TYPES.BLUE_DYE);
            effectDictionary.Add(ORB_TYPES.RED_DROP, EFFECT_TYPES.RED_DYE);
            effectDictionary.Add(ORB_TYPES.GREEN_DROP, EFFECT_TYPES.GREEN_DYE);
        }
        base.Start();
    }

    // Update is called once per frame
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if(Box.transform.parent.gameObject == MixingBoard.StaticInstance.OrbShift && (int)Math.Round(Box.transform.localPosition.y) == targetY && !shouldDestroyed)
        {
            if (targetedtOrb)
            {
                Instantiate(particleSystemSample, targetedtOrb.transform);
                targetedtOrb.Orb.affectWith(effectDictionary[type], aetherCount, archetype);
            }
            else Instantiate(particleSystemSample, Box.transform);
            symbolRenderer.enabled = false;
            DestroyIn(.5);
        }
    }
}