using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationCircle : MonoBehaviour
{

    static ConstellationCircle first;
    static ConstellationCircle last;

    [SerializeField]
    ConstellationManager.CONSTELLATION constellation;

    [SerializeField]
    GameObject bridge, selectionCircle;

    static List<ConstellationManager.CONSTELLATION> pickedList;

    float pressingDelay = 0;

    private void OnEnable()
    {
        if (ConstellationManager.CONTAINS(constellation)) select();
    }

    public static List<ConstellationManager.CONSTELLATION> pickedConstellations
    {
        get
        {
            if (pickedList.Count == 0) pickedList.Add(ConstellationManager.CONSTELLATION.NONE);
            if (pickedList.Count == 1) pickedList.Add(ConstellationManager.CONSTELLATION.NONE);
            return pickedList;
        }
    }

    public void press()
    {
        if(pressingDelay <= 0)
        {
            if (pickedList.Contains(constellation)) unselect();
            else select();
            pressingDelay = .6f;
        }
    }

    private void Awake()
    {
        pickedList = new List<ConstellationManager.CONSTELLATION>();
    }

    private void Update()
    {
        pressingDelay -= Time.deltaTime;
    }


    void select()
    {
        if (last != null) last.unselect();
        pickedList.Add(constellation);
        selectionCircle.GetComponent<Animation>().Play();
        last = first;
        first = this;
        selectionCircle.SetActive(true);
        bridge.SetActive(true);
        selectionCircle.GetComponent<IDissolving>().appear();
        bridge.GetComponent<IDissolving>().appear();
    }

    public void unselect()
    {
        if (this == last) last = null;
        else if (this == first)
        {
            first = last;
            last = null;
        }
        pickedList.Remove(constellation);
        Transform[] elements = transform.parent.GetComponentsInChildren<Transform>();
        selectionCircle.GetComponent<IDissolving>().disappear();
        bridge.GetComponent<IDissolving>().disappear();
        Invoke("deactivate", .5f);
    }

    void deactivate()
    {
        bridge.SetActive(false);
        selectionCircle.SetActive(false);
    }

    private void OnDisable()
    {
        unselect();
        pickedList = new List<ConstellationManager.CONSTELLATION>();
        last = null;
        first = null;
    }
}
