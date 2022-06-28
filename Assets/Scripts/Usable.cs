using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onUse;

    private void Awake()
    {
        if(!gameObject.GetComponent<Outliner>()) 
            gameObject.AddComponent<Outliner>().Init();
    }

    public void Use()
    {
        onUse.Invoke();
    }
}