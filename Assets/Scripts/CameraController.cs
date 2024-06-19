using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {

    #region Variables

    private PlayerInput playerInput;

    private CameraMovement cameraMovement;

    #endregion

    #region Setup

    void Start() {
        // GET PLAYER INPUT
        playerInput = GetComponent<PlayerInput>();

        // GET CAMERA MOVEMENT
        cameraMovement = GetComponent<CameraMovement>();
    }

    #endregion

    #region Methods

    public void EnableCameraRotation(bool enabled) {
        // ENABLE/DISABLE CAMERA INPUT
        if (enabled) {
            playerInput.actions.FindAction("CameraRotation").Enable();
        } else {
            playerInput.actions.FindAction("CameraRotation").Disable();
        }
    }

    public void EnableCameraMovement(bool enabled) {
        // ENABLE/DISABLE CAMERA MOVEMENT INPUT
        if (enabled) {
            cameraMovement.SetCameraLock(false);
        } else {
            cameraMovement.SetCameraLock(true);
        }
    }

    #endregion
}
