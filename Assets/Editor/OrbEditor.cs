using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

[CustomEditor(typeof(AspectOrb))]
[CanEditMultipleObjects]

public class OrbEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add fire"))
        {
            foreach (AspectOrb orb in targets)
            {
                orb.fireParticles.gameObject.SetActive(true);
                orb.fiery = true;
                orb.OrbPreview = Resources.Load("OrbPreviews/" + PrefabUtility.GetCorrespondingObjectFromSource(orb.gameObject).GetComponent<AspectOrb>().OrbPreview.name + "Fire") as GameObject;
            }
        }
        if (GUILayout.Button("Add ice"))
        {
            foreach (AspectOrb orb in targets)
            {
                GameObject iceOrb = Resources.Load<GameObject>("IceOrbSample") as GameObject;
                if (orb.coreSphereRenderer.GetComponent<Floating>() != null) orb.coreSphereRenderer.GetComponent<Floating>().enabled = false;
                orb.coreSphereRenderer.material = iceOrb.GetComponent<AspectOrb>().coreSphereRenderer.sharedMaterial;

                if (orb.symbolRenderer)
                {
                    orb.symbolRenderer.color = iceOrb.GetComponent<AspectOrb>().symbolRenderer.color;
                    Animation animation = orb.symbolRenderer.GetComponent<Animation>();
                    if (animation)
                    {
                        animation.playAutomatically = false;
                    }
                    if (orb.symbolRenderer.GetComponent<Floating>()) orb.symbolRenderer.GetComponent<Floating>().enabled = false;
                }

                if (orb.particleSphere)
                {
                    var main = orb.particleSphere.main;
                    main.startColor = iceOrb.GetComponent<AspectOrb>().particleSphere.main.startColor;
                }

                var sparksMain = orb.fireParticles.GetComponent<FireParticlesList>().sparks.main;
                sparksMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).sparks.main.startColor;
                var fireMain = orb.fireParticles.GetComponent<FireParticlesList>().fire.main;
                fireMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fire.main.startColor;
                var fireDarkMain = orb.fireParticles.GetComponent<FireParticlesList>().fireDark.main;
                fireDarkMain.startColor = iceOrb.GetComponentInChildren<FireParticlesList>(true).fireDark.main.startColor;
                orb.fireParticles.GetComponent<FireParticlesList>().fireDark.GetComponent<ParticleSystemRenderer>().material = orb.fireParticles.GetComponent<FireParticlesList>().fire.GetComponent<ParticleSystemRenderer>().sharedMaterial;

                if (orb.counter.GetComponent<TextMeshPro>())
                {
                    orb.counter.GetComponent<TextMeshPro>().color = iceOrb.GetComponent<AspectOrb>().counter.GetComponent<TextMeshPro>().color;
                }

                if (orb.aetherImpact != 0)
                {
                    foreach (GameObject particle in orb.aetherParticles)
                    {
                        var particleMain = particle.GetComponentInChildren<ParticleSystem>().main;
                        particleMain.startColor = iceOrb.GetComponent<AspectOrb>().aetherParticle.GetComponentInChildren<ParticleSystem>().main.startColor;
                    }
                }

                orb.channelParticleColor = iceOrb.GetComponent<AspectOrb>().channelParticleColor;
                orb.iceParticles.SetActive(true);
                orb.frozen = true;

                orb.OrbPreview = Resources.Load("OrbPreviews/" + PrefabUtility.GetCorrespondingObjectFromSource(orb.gameObject).GetComponent<AspectOrb>().OrbPreview.name + "Ice") as GameObject;
            }
        }
        if (GUILayout.Button("Add 1 aether"))
        {
            foreach (AspectOrb orb in targets)
            {
                orb.aetherCount += 1;
                orb.OrbPreview = Resources.Load("OrbPreviews/" + PrefabUtility.GetCorrespondingObjectFromSource(orb.gameObject).GetComponent<AspectOrb>().OrbPreview.name + "Aether") as GameObject;
            }
        }
        if (GUILayout.Button("Add antimatter"))
        {
            foreach (AspectOrb orb in targets)
            {
                orb.symbolRenderer.gameObject.SetActive(false);
                orb.antimatterSymbolRenderer.gameObject.SetActive(true);
                orb.coreSphereRenderer.gameObject.GetComponent<Floating>().enabled = true;
                orb.antimatterSymbolRenderer.gameObject.GetComponent<Floating>().enabled = true;
                orb.channelParticleColor = new Color(221, 0, 231);
                orb.antimatter = true;
                orb.OrbPreview = Resources.Load("OrbPreviews/" + PrefabUtility.GetCorrespondingObjectFromSource(orb.gameObject).GetComponent<AspectOrb>().OrbPreview.name + "Antimatter") as GameObject;
            }
        }
        if (GUILayout.Button("Revert"))
        {
            foreach (AspectOrb orb in targets)
            {
                PrefabUtility.RevertPrefabInstance(orb.gameObject, InteractionMode.UserAction);
            }
        }
    }
}
