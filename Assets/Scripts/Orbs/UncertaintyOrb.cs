using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncertaintyOrb : Orb
{
    public List<ReplacingOrbStruct> uncertainOrbsList;

    public override void affectWith(EFFECT_TYPES effect, int aetherIncreaseOn = 0, ORB_ARCHETYPES archetype = ORB_ARCHETYPES.NONE)
    {

    }
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    List<GameObject> orbVariants;

}
