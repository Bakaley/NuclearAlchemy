using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    PotionList potionList;
    
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        potionList = GetComponent<PotionList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
