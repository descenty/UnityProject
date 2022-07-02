using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameObject _targetObj;

    private GameObject TargetObj
    {
        get => _targetObj;
        set
        {
            if (_targetObj)
                _targetObj.GetComponent<Outliner>().SetOutlineState(false);
            _targetObj = value;
            if (_targetObj && _targetObj.GetComponent<ItemSocket>())
            {
                if (pickedObject && pickedObject.GetComponent<Cup>())
                    _targetObj.GetComponent<Outliner>().SetOutlineState(true);
                else if (!pickedObject && _targetObj.GetComponent<ItemSocket>().item != null)
                    _targetObj.GetComponent<Outliner>().SetOutlineState(true);
            }

            if (_targetObj && !_targetObj.GetComponent<ItemSocket>())
                _targetObj.GetComponent<Outliner>().SetOutlineState(true);
        }
    }
    [SerializeField]
    private Camera playerCamera;
    private GameObject pickedObject;
    private bool sendinginput;
    private Cassa targetCassa;
    [SerializeField] 
    private Transform pickSocket;
    private RaycastHit cameraRaycastHit;
    public RaycastHit CameraRayCastHit
    {
        get { return cameraRaycastHit; }
    }
    public PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // _input.Player.LeftClick.performed += context => LeftClick();
        // _input.Player.RightClick.performed += context => RightClick();
    }
/*
    private void LeftClick()
    {
        if (pickedObject != null && TargetObj && TargetObj.GetComponent<ItemSocket>())
        {
            TargetObj.GetComponent<ItemSocket>().PlaceItem(pickedObject.GetComponent<Pickable>());
            pickedObject = null;
        }
        else if (TargetObj && TargetObj.GetComponent<Usable>())
            TargetObj.GetComponent<Usable>().Use();
        
        if (TargetObj && TargetObj.GetComponent<Cassa>())
        {
            sendinginput = true;
            targetCassa = TargetObj.GetComponent<Cassa>();
            targetCassa.Activate();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            lasteyesPos = playerCamera.transform.position;
            lasteyesRot = playerCamera.transform.rotation;
            float ypos = playerCamera.transform.position.y;
            playerCamera.transform.position = targetCassa.gameObject.transform.position -
                                              targetCassa.gameObject.transform.forward * 0.75f;
            playerCamera.transform.LookAt(targetCassa.gameObject.transform);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, ypos, playerCamera.transform.position.z);
        }
    }

    private void RightClick()
    {
        if (!pickedObject && TargetObj)
        {
            if (TargetObj.GetComponent<Pickable>())
            {
                TargetObj.GetComponent<Pickable>().Pick(pickSocket);
                pickedObject = TargetObj;
            }
            else if (TargetObj.GetComponent<ItemSocket>() && TargetObj.GetComponent<ItemSocket>().canPickItem)
            {
                pickedObject = TargetObj.GetComponent<ItemSocket>().PickItem().gameObject;
                pickedObject.GetComponent<Pickable>().Pick(pickSocket);
            }
        }

        else if (pickedObject && CameraRaycast(out RaycastHit rHit))
        {
            pickedObject.GetComponent<Pickable>().Unpick(rHit.point + Vector3.up * 0.1f);
            pickedObject = null;
        }
    }
*/
    void Start()
    {
        pickSocket.transform.LookAt(playerCamera.transform.position);
    }

    private void CameraRaycast()
    {
        Physics.Raycast(
            new Ray(playerCamera.transform.position, playerCamera.transform.forward),
            out cameraRaycastHit, 2f, ~Physics.IgnoreRaycastLayer);
    }

    private void Update()
    {
        CameraRaycast();

/*
        if (!sendinginput && CameraRaycast(out RaycastHit rayHit))
        {
            GameObject hitTarget = rayHit.collider.gameObject;
            if (TargetObj && hitTarget.gameObject == TargetObj.gameObject)
                return;
            if (rayHit.collider.GetComponent<Usable>() || rayHit.collider.GetComponent<Pickable>() ||
                rayHit.collider.GetComponent<ItemSocket>() || rayHit.collider.GetComponent<Cassa>())
            {
                TargetObj = rayHit.collider.gameObject;

            }
            else
                TargetObj = null;
        }
        else
            TargetObj = null;


        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            sendinginput = false;
            targetCassa.Deactivate();
            playerCamera.transform.rotation = lasteyesRot;
            playerCamera.transform.position = lasteyesPos;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }*/

    }
}