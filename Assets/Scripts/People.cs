using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class People : MonoBehaviour
{
    Vector3[] waypoints;
    public Vector3[] Waypoints
    {
        get { return waypoints; }
        set { waypoints = value; waypointCount = 0; }
    }
    int waypointCount;
    public float moveSpeed;
    public Transform target;
    private NavMeshAgent agent;
    private void Start(){
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update(){
        agent.SetDestination(target.position);
    }
    /*
    private void FixedUpdate()
    {
        if (waypoints.Length != 0 && waypointCount != waypoints.Length)
        {
            if (Vector3.Distance(transform.position, waypoints[waypointCount]) > 0.01f)
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointCount], moveSpeed * Time.fixedDeltaTime);
            else
                waypointCount++;
        }
        else
            transform.position += transform.forward * Time.fixedDeltaTime * moveSpeed;
    }
    */
}
