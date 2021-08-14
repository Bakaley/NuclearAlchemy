using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncertaintyDrafter : MonoBehaviour
{
    [SerializeField]
    GameObject cellSampler;
    [SerializeField]
    public GameObject cellsBlock;

    static bool opened = false;

    static UncertaintyDrafter StaticInstance;

    static UncertaintyOrb uncertaintyOrb;

    private void Awake()
    {
        StaticInstance = this;
    }

    public void beginDraft (UncertaintyOrb orb)
    {
        uncertaintyOrb = orb;
        opened = true;
        foreach(Orb.ReplacingOrbStruct replacingOrb in orb.uncertainOrbsList)
        {
            GameObject cell = Instantiate(cellSampler, cellsBlock.transform);
            GameObject cellOrb = Instantiate(replacingOrb.baseOrb, cell.transform);
            cell.GetComponent<UncertaintyCell>().orb = replacingOrb;
            cell.GetComponent<UncertaintyCell>().cellOrb = cellOrb;
        }
    }


    public static void pick(Orb.ReplacingOrbStruct orb)
    {
        uncertaintyOrb.replace(orb);
        Destroy(StaticInstance.gameObject);
    }

    private void Update()
    {
        if (opened)
        {

        }
    }

}
