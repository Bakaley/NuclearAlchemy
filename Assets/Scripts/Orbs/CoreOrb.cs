using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoreOrb : Orb
{
    [SerializeField]
    public GameObject coreSphereList;
    public EFFECT_TYPES coreEffect
    {
        get
        {
            return effectDictionary[type];
        }
    }

    static Dictionary<ORB_TYPES, EFFECT_TYPES> effectDictionary;

    public override void affectWith(EFFECT_TYPES effect, int aetherCount = 0, Orb.ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE)
    {
        switch (effect)
        {
            case EFFECT_TYPES.FROZE:
                if(!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.FIRE:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.AETHER:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.LEVEL_UP:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.ANTIMATTER:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.GREEN_DYE:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.BLUE_DYE:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.RED_DYE:
                if (!shouldDestroyed) collapse();
                break;
            case EFFECT_TYPES.DISSOLVE:
                break;
        }
    }


    protected override void Start()
    {
        base.Start();

        if (effectDictionary == null)
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

        if (type == ORB_TYPES.ICE_CORE || type == ORB_TYPES.ICE_VOID) frozen = true;
        if (type == ORB_TYPES.FIRE_CORE || type == ORB_TYPES.FIRE_VOID) fiery = true;
        if (archetype == ORB_ARCHETYPES.VOID) antimatter = true;

    }

    protected override void Update()
    {
        base.Update();
    }

    void collapse()
    {
        MixingBoard.StaticInstance.spinDelay += .5;
        int aetherToIncrease = aetherCount;

        DestroyIn(.5f);


        if ((int)Math.Round(transform.localPosition.x + 1) < MixingBoard.Length && (int)Math.Round(transform.localPosition.y + 1) < MixingBoard.Height)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y + 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y + 1), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.x - 1) >= 0 && (int)Math.Round(transform.localPosition.y + 1) < MixingBoard.Height)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y + 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y + 1), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.x + 1) < MixingBoard.Length && (int)Math.Round(transform.localPosition.y - 1) >= 0)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y - 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y - 1), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.x - 1) >= 0 && (int)Math.Round(transform.localPosition.y - 1) >= 0)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y - 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y - 1), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.x + 1) < MixingBoard.Length)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x + 1), (int)Math.Round(transform.localPosition.y), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.x - 1) >= 0)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x - 1), (int)Math.Round(transform.localPosition.y), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.y + 1) < MixingBoard.Length)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y + 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y + 1), Convert.ToSingle(-1));
            }
        }

        if ((int)Math.Round(transform.localPosition.y - 1) >= 0)
        {
            Orb orb = MixingBoard.StaticInstance.orbs[(int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y - 1)];
            if (orb)
            {

                orb.affectWith(coreEffect, aetherToIncrease, archetype);
                orb.addAffectingSystem(particleSystemSample);
            }
            else
            {
                ParticleSystem particleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
                particleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y - 1), Convert.ToSingle(-1));
            }
        }
        ParticleSystem centralParticleSystem = Instantiate(particleSystemSample, MixingBoard.StaticInstance.OrbShift.transform);
        centralParticleSystem.transform.localPosition = new Vector3((int)Math.Round(transform.localPosition.x), (int)Math.Round(transform.localPosition.y), Convert.ToSingle(-1));
    }
}
