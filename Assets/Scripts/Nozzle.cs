using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : MonoBehaviour
{
    [SerializeField] private SupplyContainer supplyContainer;
    public Cup targetCup;
    public void Open()
    {
    }

    public void Close()
    {
        StopAllCoroutines();
    }
    /*
    IEnumerator PouringOut()
    {
        
    }
    */
}
