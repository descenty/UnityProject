using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticlesController : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.white;
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.CollisionModule collisionModule;
    private Transform collisionPlane;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
        collisionModule = particleSystem.collision;
        SetColor(startColor);
    }

    private void Start()
    {
        if (Physics.Raycast(new Ray(transform.position, -transform.up), out RaycastHit rHit, 1f))
        { 
            Transform collisionPlane = new GameObject("CollisionPlane").transform;
            collisionPlane.parent = transform;
            collisionPlane.localPosition = transform.InverseTransformPoint(rHit.point);
            collisionModule.AddPlane(collisionPlane);
        }
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);
    }

    public void SetColor(Color color)
    {
        mainModule.startColor = color;
    }

    public void SetParticlesSystemState(bool enabled)
    {
        if (enabled)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }
}
