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
        Blue,
        Red,
        Green
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


    public override void affectWith(EFFECT_TYPES effect, int aetherIncreaseOn = 0)
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
                increaseAether(aetherIncreaseOn);
                break;
            case EFFECT_TYPES.LEVEL_UP:
                if(Level == 2 && antimatter)
                {
                    if (fiery && frozen)
                    {
                        if (Mathf.Round(gameObject.transform.localPosition.y) >= 2)
                        {
                            nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                        }
                        else if (Mathf.Round(gameObject.transform.localPosition.y) <= 1)
                        {
                            nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                        }
                    }
                    else if (fiery) nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                    else if (frozen) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                    else if (aetherCount != 0) nextLevelOrb = MixingBoard.StaticInstance.greenVoid;
                    else nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;

                    fiery = false;
                    frozen = false;
                    antimatter = false;
                }
                Invoke("levelUp", .2f);
                break;
            case EFFECT_TYPES.ANTIMATTER:
                if(Level == 3)
                {
                    if (fiery && frozen)
                    {
                        if (Mathf.Round(gameObject.transform.localPosition.y) >= 2)
                        {
                            nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                        }
                        else if (Mathf.Round(gameObject.transform.localPosition.y) <= 1)
                        {
                            nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                        }
                    }
                    else if (fiery) nextLevelOrb = MixingBoard.StaticInstance.redVoid;
                    else if (frozen) nextLevelOrb = MixingBoard.StaticInstance.blueVoid;
                    else if (aetherCount != 0) nextLevelOrb = MixingBoard.StaticInstance.greenVoid;
                    else nextLevelOrb = MixingBoard.StaticInstance.purpleVoid;

                    fiery = false;
                    frozen = false;
                    antimatter = false;
                    Invoke("levelUp", .2f);
                }
                else addAntimatter();
                break;
            case EFFECT_TYPES.BLUE_DYE:
                if(colorDictionary[this.type] != ORB_COLOR.Blue)
                {
                    colorIn(ORB_COLOR.Blue);
                }
                break;
            case EFFECT_TYPES.RED_DYE:
                if (colorDictionary[this.type] != ORB_COLOR.Red)
                {
                    colorIn(ORB_COLOR.Red);
                }
                break;
            case EFFECT_TYPES.GREEN_DYE:
                if (colorDictionary[this.type] != ORB_COLOR.Green)
                {
                    colorIn(ORB_COLOR.Green);
                }
                break;
            case EFFECT_TYPES.DISSOLVE:
                DestroyIn(0.2f);
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
        aetherParticle = orbToPaintIn.aetherParticle;
    }

   

    protected override void Start()
    {
        base.Start();

        colorDictionary = new Dictionary<ORB_TYPES, ORB_COLOR>();
        colorDictionary.Add(ORB_TYPES.MIND_ASPECT, ORB_COLOR.Blue);
        colorDictionary.Add(ORB_TYPES.BODY_ASPECT, ORB_COLOR.Red);
        colorDictionary.Add(ORB_TYPES.SOUL_ASPECT, ORB_COLOR.Green);

        counterTMP = counter.GetComponent<TextMeshPro>();
    }

    protected override void Update()
    {
        base.Update();
    }

}
