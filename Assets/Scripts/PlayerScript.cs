using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public float sensitivity;
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

    public Font useFont;
    private GameObject point;
    private Camera playerCamera;
    private GameObject canvas;
    private GameObject pickedObject;
    private bool sendinginput;
    private Cassa targetCassa;
    private Vector3 lasteyesPos;
    private Quaternion lasteyesRot;
    [SerializeField] private Transform pickSocket;
    public PlayerInput _input;
    private bool isGrounded;

    private float xRotation;
    [SerializeField] private CharacterController characterController;

    private Vector3 velocity;
    private float gravity = -9.81f;
    public LayerMask groundMask;

    private Transform groundCheck;
    // Use this for initialization

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.LeftClick.performed += context => LeftClick();
        _input.Player.RightClick.performed += context => RightClick();
        groundCheck = transform.GetChild(1);
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
        canvas = GameObject.Find("GameUI");
        point = canvas.transform.Find("Point").gameObject;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

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

    private void Move(Vector2 moveDirection)
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.4f, groundMask);

        float scaledMoveSpeed = movementSpeed * Time.deltaTime;
        Vector3 move = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        characterController.Move(move * scaledMoveSpeed);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }

    private void Look(Vector2 lookDirection)
    {
        float scaledLookSpeed = sensitivity * Time.deltaTime;

        xRotation -= lookDirection.y * scaledLookSpeed;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, lookDirection.x * scaledLookSpeed);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pickSocket.transform.LookAt(playerCamera.transform.position);
    }

    private bool CameraRaycast(out RaycastHit raycastHit)
    {
        if (Physics.Raycast(new Ray(playerCamera.transform.position, playerCamera.transform.forward),
            out RaycastHit rHit, 2f, ~Physics.IgnoreRaycastLayer))
        {
            raycastHit = rHit;
            return true;
        }

        raycastHit = new RaycastHit();
        return false;
    }

    private void Update()
    {
        Vector2 moveDirection = _input.Player.Move.ReadValue<Vector2>();
        Move(moveDirection);
        Vector2 lookDirection = _input.Player.Look.ReadValue<Vector2>();
        Look(lookDirection);
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