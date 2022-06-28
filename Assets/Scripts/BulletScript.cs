using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float startForce;
    public GameObject hitParticles;
    private Rigidbody rigid;
    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * startForce);
    }

    // Update is called once per frame
    void FixedUpdate() {
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("ENTER");
        Destroy(Instantiate(hitParticles, transform.position - transform.forward * 0.25f, Quaternion.LookRotation(-transform.forward)), 1f);
        Destroy(gameObject);

    }
}
