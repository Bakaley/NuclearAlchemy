using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeorganicOrb : Orb
{
  
    public bool unstable;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

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
}
