using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private Rigidbody rigid;
    private float speed = 5f;
    private float rotationSpeed = 5f;
    public bool needRotate = true;
    private bool _canPick = true;
    public Outliner outliner;
    public bool needRigid = true;

    public bool canPick
    {
        get => _canPick;
        set
        {
            _canPick = value;
            if (_canPick)
                GetComponent<Collider>().enabled = true;
            else
                GetComponent<Collider>().enabled = false;
        }
    }

    public bool picked
    {
        get;
        private set;
    }
    public void Init()
    {
        rigid = GetComponent<Rigidbody>();
        outliner = gameObject.AddComponent<Outliner>();
        outliner.Init();
    }

    public void Pick(Transform parent)
    {
        picked = true;
        transform.parent = parent;
        StopAllCoroutines();
        StartCoroutine(MoveTo(parent.position));
        if (needRotate)
            StartCoroutine(RotateTo(parent));
    }

    public void Unpick(Vector3 position)
    {
        picked = false;
        transform.parent = null;
        StopAllCoroutines();
        StartCoroutine(MoveTo(position));
    }
    public void Unpick(Vector3 position, Transform targetTransform)
    {
        picked = false;
        transform.parent = null;
        StopAllCoroutines();
        StartCoroutine(MoveTo(targetTransform.position));
        if (needRotate)
            StartCoroutine(RotateTo(targetTransform));
    }
    IEnumerator MoveTo(Vector3 targetPosition)
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;
        while (Vector3.Distance(transform.position, targetPosition) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        if (!picked && needRigid)
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }

    IEnumerator RotateTo(Transform target)
    {
        while (Vector3.Distance(transform.forward, target.forward) >= 0.05f)
        {
            transform.forward =
                Vector3.MoveTowards(transform.forward, target.forward, rotationSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
