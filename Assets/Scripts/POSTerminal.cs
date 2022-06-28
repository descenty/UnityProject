using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class POSTerminal : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent payevent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CreditCard>())
            print("pay");
            payevent.Invoke();
    }
}
