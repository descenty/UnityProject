using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPlaces : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("HUY V GOVNE");
        if (other.GetComponent<People>())
        {
            Vector3 waypoint = Vector3.zero;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (waypoint == Vector3.zero)
                {
                    for (int j = 0; j < transform.GetChild(i).GetChild(0).childCount; i++)
                    {
                        if (transform.GetChild(i).GetChild(0).GetChild(j).GetComponent<EatSocket>().free)
                        {
                            waypoint = transform.GetChild(i).GetChild(0).GetChild(j).position;
                            break;
                        }
                    }
                }
            }
            other.GetComponent<People>().Waypoints = new Vector3[1] { waypoint };
        }
    }
}
