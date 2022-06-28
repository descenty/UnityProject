using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsSystem : MonoBehaviour
{
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
        if (other.GetComponent<People>())
        {
            Vector3[] waypoints = new Vector3[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                waypoints[i] = transform.GetChild(i).position;
            }
            other.GetComponent<People>().Waypoints = waypoints;
        }
    }
}
