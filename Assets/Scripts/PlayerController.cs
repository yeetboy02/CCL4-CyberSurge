using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    #region Variables

    private PlayerInput playerInput;

    private CharacterController controller;

    #endregion

    void Start() {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    #region CountDown

    public void EnableMovement(bool enabled) {
        // ENABLE/DISABLE PLAYER MOVEMENT INPUT
        if (enabled) {
            playerInput.actions.FindAction("Movement").Enable();
        } else {
            playerInput.actions.FindAction("Movement").Disable();
        }
    }

    public void SetPosition(Vector3 position) {
        // SET PLAYER POSITION
        controller.Move(position - transform.position);
    }

    #endregion
}
