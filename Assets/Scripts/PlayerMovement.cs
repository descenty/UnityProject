using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Player player;
    private PlayerInput playerInput;
    [SerializeField]
    private Camera playerCamera;
    private float cameraXRotation = 0f;
    private CharacterController characterController;
    private readonly float gravity = -9.81f;
    public float movementSpeed = 5f;
    public float sensitivity = 5f;
    private void Start()
    {
        if (GetComponent<CharacterController>())
            characterController = GetComponent<CharacterController>();
        else
            Debug.LogError($"There is no character controller associated with gameobject ({name})");
        player = GetComponent<Player>();
        playerInput = player.playerInput;
    }

    private void Move(Vector2 moveDirection)
    {
        float scaledMoveSpeed = movementSpeed * Time.deltaTime;
        Vector3 move = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;
        move.y = gravity;
        characterController.Move(move * scaledMoveSpeed);
    }

    private void Look(Vector2 lookDirection)
    {
        float scaledLookSpeed = sensitivity * Time.deltaTime;
        cameraXRotation -= lookDirection.y * scaledLookSpeed;
        cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);
        transform.Rotate(Vector3.up, lookDirection.x * scaledLookSpeed);
    }

    private void Update()
    {
        Vector2 moveDirection = playerInput.Main.Move.ReadValue<Vector2>();
        Move(moveDirection);
        Vector2 lookDirection = playerInput.Main.Look.ReadValue<Vector2>();
        Look(lookDirection);
    }
}
