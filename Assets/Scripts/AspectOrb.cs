using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEditor;

public class AspectOrb : Orb
{

    static Dictionary <ORB_TYPES, ORB_COLOR> colorDictionary;

    public enum ORB_COLOR
    {
        blue,
        red,
        green
    }

    public ORB_COLOR orbColor
    {
        get
        {
            return colorDictionary[type];
        }
    }

    public int aspectImpact
    {
        get
        {
            int counter = 1;
            counter = (int)(counter + counter * .2 * aetherCount);
            if (antimatter) counter = -counter;
            return counter;
        }
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
                break;
            case EFFECT_TYPES.ANTIMATTER:
                addAntimatter();
                break;
            case EFFECT_TYPES.BLUE_DYE:
                if(colorDictionary[this.type] != ORB_COLOR.blue)
                {
                    colorIn(ORB_COLOR.blue);
                }
                break;
            case EFFECT_TYPES.RED_DYE:
                if (colorDictionary[this.type] != ORB_COLOR.red)
                {
                    colorIn(ORB_COLOR.red);
                }
                break;
            case EFFECT_TYPES.GREEN_DYE:
                if (colorDictionary[this.type] != ORB_COLOR.green)
                {
                    colorIn(ORB_COLOR.green);
                }
                break;
            case EFFECT_TYPES.DISSOLVE:
                break;
        }
    }

    void colorIn (ORB_COLOR color)
    {

        AspectOrb orbToPaintIn = MixingBoard.orbDictionary[color + "" + Level].GetComponent<AspectOrb>();
        if (!frozen) this.coreSphereRenderer.material = orbToPaintIn.coreSphereRenderer.sharedMaterial;
        this.outerSphereRenderer.material = orbToPaintIn.outerSphereRenderer.sharedMaterial;
        if (!frozen)
        {
            var main = this.particleSphere.main;
            main.startColor = orbToPaintIn.particleSphere.main.startColor;
        }
        this.particleSphere.Stop();
        this.particleSphere.Clear();
        this.particleSphere.Play();
        if(antimatter) this.antimatterSymbolRenderer.sprite = orbToPaintIn.antimatterSymbolRenderer.sprite;
        else this.symbolRenderer.sprite = orbToPaintIn.symbolRenderer.sprite;

        FireParticlesList[] firelist = gameObject.GetComponentsInChildren<FireParticlesList>(true);
        foreach (FireParticlesList fireSystem in firelist)
        {
            var sparksMain = fireSystem.sparks.main;
            sparksMain.startColor = orbToPaintIn.GetComponentInChildren<FireParticlesList>(true).sparks.main.startColor;
            var fireMain = fireSystem.fire.main;
            fireMain.startColor = orbToPaintIn.GetComponentInChildren<FireParticlesList>(true).fire.main.startColor;
            var fireDarkMain = fireSystem.fireDark.main;
            fireDarkMain.startColor = orbToPaintIn.GetComponentInChildren<FireParticlesList>(true).fireDark.main.startColor;
        }

        if (!antimatter)
        {
            if (symbolRenderer.GetComponent<Animation>())
            {
                AnimationClip oldClip = this.symbolRenderer.GetComponent<Animation>().clip;
                this.symbolRenderer.GetComponent<Animation>().AddClip(orbToPaintIn.symbolRenderer.GetComponent<Animation>().clip, color + "Clip");
                this.symbolRenderer.GetComponent<Animation>().RemoveClip(oldClip);
                this.symbolRenderer.transform.localPosition = orbToPaintIn.symbolRenderer.transform.localPosition;
                this.symbolRenderer.transform.localRotation = orbToPaintIn.symbolRenderer.transform.localRotation;
                this.symbolRenderer.transform.localScale = orbToPaintIn.symbolRenderer.transform.localScale;
                if (!frozen) this.symbolRenderer.GetComponent<Animation>().Play(color + "Clip");
            }
        }


        this.orbType = orbToPaintIn.type;
        if(!frozen) this.channelParticleColor = orbToPaintIn.channelParticleColor;
        this.nextLevelOrb = orbToPaintIn.nextLevelOrb;

        if (counter.GetComponent<TextMeshPro>() && !frozen)
        {
            counter.GetComponent<TextMeshPro>().color = orbToPaintIn.GetComponent<AspectOrb>().counter.GetComponent<TextMeshPro>().color;
        }

        if(aetherImpact != 0 && !frozen)
        {
            foreach (GameObject particle in aetherParticles)
            {
                var particleMain = particle.GetComponentInChildren<ParticleSystem>().main;
                particleMain.startColor = orbToPaintIn.aetherParticle.GetComponentInChildren<ParticleSystem>().main.startColor;
            }
        }
    }

    void addFire()
    {
        fireParticles.gameObject.SetActive(true);
        fiery = true;
    }

    void addIce()
    {
        GameObject iceOrb = MixingBoard.orbDictionary["ice"];

        if (coreSphereRenderer.GetComponent<Floating>() != null) coreSphereRenderer.GetComponent<Floating>().enabled = false;
        Invoke("freezeMaterial", .5f);

        if(symbolRenderer)
        {
            symbolRenderer.color = mixingBoard.iceOrbSample.GetComponent<AspectOrb>().symbolRenderer.color;
            Animation animation = symbolRenderer.GetComponent<Animation>();
            if (animation)
            {
                animation.Stop();
            }
            symbolRenderer.transform.localPosition = MixingBoard.orbDictionary[orbColor + "" + Level].GetComponent<AspectOrb>().symbolRenderer.transform.localPosition;
            symbolRenderer.transform.localRotation = MixingBoard.orbDictionary[orbColor + "" + Level].GetComponent<AspectOrb>().symbolRenderer.transform.localRotation;
            symbolRenderer.transform.localScale = MixingBoard.orbDictionary[orbColor + "" + Level].GetComponent<AspectOrb>().symbolRenderer.transform.localScale;
            if (symbolRenderer.GetComponent<Floating>() != null) symbolRenderer.GetComponent<Floating>().enabled = false;
        }

        if (particleSphere)
        {
            var main = particleSphere.main;
            main.startColor = mixingBoard.iceOrbSample.GetComponent<AspectOrb>().particleSphere.main.startColor;
            particleSphere.Stop();
            particleSphere.Clear();
            particleSphere.Play();
        }

        FireParticlesList[] firelist = gameObject.GetComponentsInChildren<FireParticlesList>(true);
        foreach (FireParticlesList fireSystem in firelist)
        {
            var sparksMain = fireSystem.sparks.main;
            sparksMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).sparks.main.startColor;
            var fireMain = fireSystem.fire.main;
            fireMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fire.main.startColor;
            var fireDarkMain = fireSystem.fireDark.main;
            fireDarkMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fireDark.main.startColor;
            fireSystem.fireDark.GetComponent<ParticleSystemRenderer>().material = fireSystem.fire.GetComponent<ParticleSystemRenderer>().material;
        }

        if (counter.GetComponent<TextMeshPro>())
        {
            counter.GetComponent<TextMeshPro>().color = iceOrb.GetComponent<AspectOrb>().counter.GetComponent<TextMeshPro>().color;
        }

        if (aetherImpact != 0)
        {
            foreach (GameObject particle in aetherParticles)
            {
                var particleMain = particle.GetComponentInChildren<ParticleSystem>().main;
                particleMain.startColor = iceOrb.GetComponent<AspectOrb>().aetherParticle.GetComponentInChildren<ParticleSystem>().main.startColor;
            }
        }

        this.channelParticleColor = iceOrb.GetComponent<AspectOrb>().channelParticleColor;
        iceParticles.SetActive(true);
        frozen = true;
    }

    void freezeMaterial()
    {
        coreSphereRenderer.material = mixingBoard.iceOrbSample.GetComponent<AspectOrb>().coreSphereRenderer.sharedMaterial;
    }


    protected override void Start()
    {
        base.Start();

        colorDictionary = new Dictionary<ORB_TYPES, ORB_COLOR>();
        colorDictionary.Add(ORB_TYPES.MIND_ASPECT, ORB_COLOR.blue);
        colorDictionary.Add(ORB_TYPES.BODY_ASPECT, ORB_COLOR.red);
        colorDictionary.Add(ORB_TYPES.SOUL_ASPECT, ORB_COLOR.green);

        counterTMP = counter.GetComponent<TextMeshPro>();
    }

    protected override void Update()
    {
        base.Update();
    }

}
