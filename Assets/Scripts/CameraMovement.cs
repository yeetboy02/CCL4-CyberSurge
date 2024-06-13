using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {

    #region RotationParameters
    [SerializeField] private float rotationSpeedX = 1.50f;

    [SerializeField] private float rotationSpeedY = 1.50f;

    [SerializeField] private Vector2 rotationYConstraints = new Vector2(10, 75);

    #endregion

    #region MovementParameters
    [SerializeField] private Vector3 offset = new Vector3(0, 3.5f, -7);

    [SerializeField] private float minDistanceToPlayer = 1.0f;

    #endregion


    #region Variables
    [SerializeField] private Transform player;

    private float baseDistanceToPlayer;

    private float currDistanceToPlayer;

    private Vector3 currPlayerOffset;

    private Vector3 prevPlayerOffset;


    #endregion

    void Start () {
        currPlayerOffset = offset;
        ApplyOffset();
        baseDistanceToPlayer = Vector3.Distance(transform.position, player.position);
        currDistanceToPlayer = baseDistanceToPlayer;
        prevPlayerOffset = currPlayerOffset;
        SetOffset();
    }

    void Update() {
        SetZoom();
        ApplyOffset();
        LookAtPlayer();
    }

    #region Movement
    public void SetOffset() {
        currPlayerOffset = gameObject.transform.position - player.position;
        currPlayerOffset.Normalize();
        currPlayerOffset *= currDistanceToPlayer;
    }

    void SetZoom() {
        RaycastHit hit;
        Physics.Raycast(player.position, currPlayerOffset, out hit, currDistanceToPlayer + 0.50001f);

        if (hit.collider != null && hit.collider.gameObject != player.gameObject && hit.collider.gameObject != gameObject) {
            if (hit.distance < minDistanceToPlayer) {
                ResetOffset();
            }
            else if (hit.distance > baseDistanceToPlayer) {
                currDistanceToPlayer = baseDistanceToPlayer;
            }
            else {
                currDistanceToPlayer = hit.distance;
            }
            SetOffset();
        }
        else {
            currDistanceToPlayer = baseDistanceToPlayer;
        }

        Debug.Log(currDistanceToPlayer);
    }

    void ApplyOffset() {
        gameObject.transform.position = player.position + currPlayerOffset;
    }

    void ResetOffset() {
        currPlayerOffset = prevPlayerOffset;
        currDistanceToPlayer = baseDistanceToPlayer;
    }

    #endregion

    #region Rotations

    void LookAtPlayer() {
        gameObject.transform.LookAt(player);
    }

    void OnCameraRotation(InputValue value) {
        Vector2 input = value.Get<Vector2>();
        // X ROTATION
        gameObject.transform.RotateAround(player.transform.position, Vector3.up, input.x * rotationSpeedX);

        // Y ROTATION
        Vector3 currRotations = gameObject.transform.rotation.eulerAngles;
        float nextYRotation = input.y;
        if (currRotations.x + input.y * rotationSpeedY >= rotationYConstraints.y) {
            if (input.y > 0) {
                nextYRotation = 0;
            }
        }
        else if (currRotations.x + input.y * rotationSpeedY <= rotationYConstraints.x) {
            if (input.y < 0) {
                nextYRotation = 0;
            }
        }
        else {
            nextYRotation = input.y * rotationSpeedY;
        }
        gameObject.transform.RotateAround(player.transform.position, gameObject.transform.right, nextYRotation);


        SetOffset();
    }

    #endregion
}
