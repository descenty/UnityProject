using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerminalInteraction : MonoBehaviour 
{
    private Player player;
    private PlayerInput playerInput;
    private RaycastHit cameraRaycastHit;
    private void Start()
    {
        player = GetComponent<Player>();
        playerInput = player.playerInput;
        playerInput.Main.LeftClick.performed += context => LeftClickPerformed();
    }

    private void LeftClickPerformed()
    {
        
    }
    private void Update()
    {
        cameraRaycastHit = player.CameraRayCastHit;
     //   if (cameraRaycastHit.collider.GetComponent<PyPointUITerminal>())
    }
}