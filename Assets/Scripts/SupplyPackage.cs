using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPackage : MonoBehaviour
{
    private SupplyContainer _supplyContainer;
    public SupplyContainer supplyContainer
    {
        get => _supplyContainer;
        set
        {
            _supplyContainer = value;
            StopAllCoroutines();
            StartCoroutine(RotatingPackage(_supplyContainer == null));
        }
    }

    public float rotationSpeed;
    public float pouringOutSpeed = 10;
    public float capacity = 250;
    public float currentGrams;
    private Coroutine pouringOutCoroutine;
    private GameObject progressBar;
    private float maxProgressBarScale;
    public Ingredient supply;
    private Pickable pickable;

    private void Awake()
    {
        currentGrams = capacity;
        progressBar = transform.GetChild(0).GetChild(0).gameObject;
        maxProgressBarScale = progressBar.transform.localScale.x;
        pickable = gameObject.AddComponent<Pickable>();
        pickable.Init();
    }

    IEnumerator RotatingPackage(bool reverse)
    {
        float needAngle = reverse ? 360 : 200;
        while (Mathf.Abs(transform.rotation.eulerAngles.z - needAngle) > 0.25f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, needAngle, Time.fixedDeltaTime * rotationSpeed));
            if (transform.rotation.eulerAngles.z <= 240 && pouringOutCoroutine == null)
            {
                pouringOutCoroutine = StartCoroutine(PouringOutPackage());
            }

            if (transform.rotation.eulerAngles.z > 240 && pouringOutCoroutine != null)
            {
                StopCoroutine(pouringOutCoroutine);
                pouringOutCoroutine = null;
            }
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, needAngle);
        /*
        if (currentGrams == 0)
        {
            Destroy(this);
        }
        */
    }

    IEnumerator PouringOutPackage()
    {
        while (supplyContainer != null)
        {
            float grams = Mathf.Clamp(Mathf.Clamp(pouringOutSpeed * Time.fixedDeltaTime, 0, currentGrams), 0,
                supplyContainer.capacity - supplyContainer.currentGrams);
            currentGrams -= grams;
            supplyContainer.currentGrams += grams;
            progressBar.transform.localScale = new Vector3(maxProgressBarScale * currentGrams / capacity, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            if (supplyContainer.currentGrams == supplyContainer.capacity)
            {
                supplyContainer = null;
            }
            if (currentGrams == 0)
            {
                supplyContainer = null;
                Destroy(this);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}