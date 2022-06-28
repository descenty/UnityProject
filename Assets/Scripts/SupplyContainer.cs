using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyContainer : MonoBehaviour
{
    public int capacity = 200;
    public Ingredient supply;
    public float currentGrams = 0;
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SupplyPackage>() != null && other.GetComponent<SupplyPackage>().supply == supply)
        {
            other.GetComponent<SupplyPackage>().supplyContainer = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SupplyPackage>() != null && other.GetComponent<SupplyPackage>().supply == supply)
        {
            other.GetComponent<SupplyPackage>().supplyContainer = null;
        }
    }
}