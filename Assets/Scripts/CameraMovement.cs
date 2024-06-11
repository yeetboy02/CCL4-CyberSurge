using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {

    #region RotationParameters
    private float rotationSpeedX = 1.50f;

    #endregion

    #region MovementParameters
    private Vector3 offset = new Vector3(0, 3.5f, -7);

    private float minDistanceToPlayer = 1.0f;

    #endregion


    #region Variables
    [SerializeField] private Transform player;

    private float baseDistanceToPlayer;

    private float currDistanceToPlayer;

    private Vector3 currPlayerOffset;

    private Vector3 prevPlayerOffset;

    private float fixedYOffset;

    private bool colliding = false;


    #endregion

    void Start () {
        currPlayerOffset = offset;
        ApplyOffset();
        fixedYOffset = transform.position.y - player.position.y;
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
        if (colliding) {
            prevPlayerOffset = currPlayerOffset;
        }
        currPlayerOffset = gameObject.transform.position - player.position;
        currPlayerOffset.Normalize();
        currPlayerOffset *= currDistanceToPlayer;
        currPlayerOffset.y = fixedYOffset;
    }

    void SetZoom() {
        RaycastHit hit;
        Physics.Raycast(player.position, currPlayerOffset, out hit, currDistanceToPlayer + 0.50001f);

        if (hit.collider != null && hit.collider.gameObject != player.gameObject && hit.collider.gameObject != gameObject) {
            Debug.Log(hit.collider);
            if (hit.distance < minDistanceToPlayer) {
                ResetOffset();
            }
            else {
                currDistanceToPlayer = hit.distance;
            }
            SetOffset();
        }
        else {
            currDistanceToPlayer = baseDistanceToPlayer;
        }
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
        gameObject.transform.RotateAround(player.transform.position, Vector3.up, input.x * rotationSpeedX);
        SetOffset();
    }

    #endregion

    #region Collisions

    void OnTriggerStay(Collider other) {
        SetZoom();
        colliding = true;
    }

    void OnTriggerExit(Collider other) {
        colliding = false;
    }

    #endregion
}
