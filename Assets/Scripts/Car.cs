using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Usable {
    public GameObject[] forwardWheels;
    public GameObject[] backwardWheels;
    public GameObject[] wheelsMeshes;
    public Light[] forwardLights;
    public Light[] backwardLights;
    public Vector3 seatPos;
    public float force;
    private bool receiveInput = false;
    public float sitSpeed;
    public float sitRotSpeed;
    public float steerSpeed;
    public GameObject brakeIndicator;
    public GameObject headLights;
    public Color headLightsColor;
    private bool lightsOn;
    // Use this for initialization
    /*
    public override void Start()
    {
        base.Start();
        useString = "Sit";
    }

    // Update is called once per frame
    void Update () {
        if (receiveInput)
        {
            foreach(GameObject wheel in backwardWheels)
            {
                wheel.GetComponent<WheelCollider>().motorTorque = force * Input.GetAxis("Vertical");
 //               wheelsMeshes[i].transform.localEulerAngles += new Vector3(wheels[i].GetComponent<WheelCollider>().rpm * Time.deltaTime, 0, 0);
            }
            foreach(GameObject wheel in forwardWheels)
            {
                wheel.GetComponent<WheelCollider>().motorTorque = force * Input.GetAxis("Vertical");
                wheel.GetComponent<WheelCollider>().steerAngle = steerSpeed * Input.GetAxis("Horizontal");
            }
            float brakeMod = 1 - Mathf.Clamp(Input.GetAxis("Vertical") + 1, 0, 1);
            foreach (Light light in backwardLights)
            {
                light.intensity = brakeMod;
                brakeIndicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(brakeMod, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                lightsOn = !lightsOn;
                foreach(Light light in forwardLights)
                {
                    light.intensity = lightsOn ? 2 : 0;
                }
                headLights.GetComponent<Renderer>().material.SetColor("_EmissionColor", lightsOn ? headLightsColor : Color.black);
            }
        }
	}
    public override void Use(GameObject player)
    {
        player.GetComponent<PlayerScript>().AttachTo(transform);
        player.GetComponent<Animator>().SetTrigger("Sit");
        StartCoroutine(MoveToSeat(player));
    }
    private IEnumerator MoveToSeat(GameObject player)
    {
        Vector3 targetPos = transform.TransformPoint(seatPos);
        while (player.transform.position != targetPos && player.transform.rotation != Quaternion.identity)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, sitSpeed * Time.deltaTime);
            player.transform.localRotation = Quaternion.RotateTowards(player.transform.localRotation, Quaternion.identity, sitRotSpeed * Time.deltaTime);
            print("MOVETOSEAT");
            yield return null;
        }
        receiveInput = true;
    }
    */
}
