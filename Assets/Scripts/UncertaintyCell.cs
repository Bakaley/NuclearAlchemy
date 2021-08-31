using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncertaintyCell : MonoBehaviour
{
    public Orb.ReplacingOrbStruct orb;
    public GameObject cellOrb;

    public void pick()
    {
        UncertaintyDrafter.pick(orb);
    }

    private void Start()
    {
        if (orb.aether != 0)
        {
            cellOrb.GetComponent<Orb>().setStartAether(orb.aether);
            Invoke("delayedParticles", .07f);
        }
    }

    void delayedParticles()
    {
        cellOrb.GetComponent<Orb>().setAetherParticlesRenderingOrder(2);
    }
}
