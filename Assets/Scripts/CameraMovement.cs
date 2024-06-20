using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {

    #region MovementParameters

    [SerializeField] private Vector3 offset = new Vector3(0, 3.5f, -7);


    #endregion

    #region RotationParameters

    [SerializeField] private float rotationSpeedX = 1.50f;

    [SerializeField] private float rotationSpeedY = 1.50f;

    [SerializeField] private Vector2 rotationYConstraints = new Vector2(-45, 45);

    #endregion

    #region Variables

    [SerializeField] private Transform player;

    private Transform playerCamera;

    private Quaternion currRotation;

    private float baseCameraDistance;

    private float currCameraDistance;

    private bool cameraLocked = false;

    #endregion

    #region GetterSetter

    public void SetCameraLock(bool state) {
        cameraLocked = state;
    }

    #endregion

    void Start() {
        playerCamera = gameObject.transform.GetChild(0).gameObject.transform;

        // SET INITIAL OFFSET
        playerCamera.localPosition = offset;

        // GET CURRENT ROTATION
        currRotation = gameObject.transform.localRotation;
        Debug.Log(player.position + " " + playerCamera.position);
        // SET INITIAL CAMERA DISTANCE
        baseCameraDistance = Vector3.Distance(player.position, playerCamera.position);
        currCameraDistance = baseCameraDistance;
    }

    void Update() {
        if (!cameraLocked) {
            MoveWithPlayer();
        }
        SetCameraDistance();
        SetCameraOffset();
        Rotate();
        LookAtPlayer();
        
        //Debug.Log(currCameraDistance);
    }

    #region Movement

    public void MoveWithPlayer() {
        // SET CAMERA CONTAINER POSITION TO PLAYER POSITION
        gameObject.transform.position = player.position;
    }

    public void LookAtPlayer() {
        // SET CAMERA CONTAINER ROTATION TO LOOK AT PLAYER
        playerCamera.LookAt(player);
    }

    #endregion

    #region Rotation

    void Rotate() {
        // APPLY CAMERA CONTAINER ROTATION
        gameObject.transform.localRotation = Quaternion.Euler(currRotation.x, currRotation.y, 0);
    }        

    void OnCameraRotation(InputValue value) {
        // RETRIEVE INPUT VALUES
        Vector2 input = value.Get<Vector2>();

        // SET CURRENT X ROTATION
        currRotation.x += input.y * rotationSpeedX;

        // CLAMP CURRENT X ROTATION
        currRotation.x = Mathf.Clamp(currRotation.x, rotationYConstraints.x, rotationYConstraints.y);

        // SET CURRENT Y ROTATION
        currRotation.y += input.x * rotationSpeedY;
    }

    #endregion

    #region CameraDistance

    void SetCameraDistance() {

        // GET CAMERA WORLD POSITION
        Vector3 cameraWorldPosition = player.position + (Quaternion.Euler(currRotation.x, currRotation.y, 0) * offset);

        // CHECK IF CAMERA IS OBSTRUCTED
        if (Physics.Linecast(player.position, cameraWorldPosition, out RaycastHit hit, Physics.AllLayers, QueryTriggerInteraction.Ignore) && hit.collider.gameObject != player.gameObject && hit.collider.gameObject != gameObject) {
            // SET CAMERA DISTANCE TO NEW HIT POINT INSIDE CLEAR SPHERE
            currCameraDistance = Mathf.Abs(hit.distance - 0.5f);
        }
        else {
            // RESET CAMERA DISTANCE TO BASE DISTANCE
            currCameraDistance = Mathf.Lerp(currCameraDistance, baseCameraDistance, Time.deltaTime);
        }
    }

    void SetCameraOffset() {
        // GET CURRENT OFFSET
        Vector3 currOffset = offset;

        // SCALE OFFSET TO THE CURRENT CAMERA DISTANCE
        currOffset.Normalize();
        currOffset *= currCameraDistance;

        // APPLY OFFSET
        playerCamera.localPosition = currOffset;
    }

    #endregion
}