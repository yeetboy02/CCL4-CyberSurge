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

    #region Methods

    public void EnableMovement(bool enabled) {
        // ENABLE/DISABLE PLAYER MOVEMENT INPUT
        if (enabled) {
            playerInput.actions.FindAction("Movement").Enable();
        } else {
            playerInput.actions.FindAction("Movement").Disable();
        }
    }

    public void EnableJump(bool enabled) {
        // ENABLE/DISABLE PLAYER JUMP INPUT
        if (enabled) {
            playerInput.actions.FindAction("Jump").Enable();
        } else {
            playerInput.actions.FindAction("Jump").Disable();
        }
    }

    public void SetPosition(Vector3 position) {
        // RESET PLAYER VELOCITY
        controller.enabled = false;

        // SET PLAYER POSITION
        gameObject.transform.position = position;

        // REACTIVE PLAYER CONTROLLER
        controller.enabled = true;
    }

    #endregion
}
