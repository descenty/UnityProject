using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigonometricMovement : MonoBehaviour {
    private Vector3 startPos;
    public float xAmplitude;
    public float yAmplitude;
    public float zAmplitude;
    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        transform.position = new Vector3(startPos.x + Mathf.Tan(Time.timeSinceLevelLoad) * xAmplitude, startPos.y + Mathf.Sin(Time.timeSinceLevelLoad) * yAmplitude, startPos.z + Mathf.Cos(Time.timeSinceLevelLoad) * zAmplitude);
	}
}
