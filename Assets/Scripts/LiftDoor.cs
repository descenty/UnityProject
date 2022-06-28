using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour {
    private GameObject firstDoor;
    private GameObject secondDoor;
    private float openSpeed;
    public int floor;
	// Use this for initialization
	void Start () {
        firstDoor = transform.GetChild(0).gameObject;
        secondDoor = transform.GetChild(1).gameObject;
	}
    public void Use()
    {
        firstDoor.GetComponent<Animator>().SetTrigger("Use");
        secondDoor.GetComponent<Animator>().SetTrigger("Use");
    }
}
