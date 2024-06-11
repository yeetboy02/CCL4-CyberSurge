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
    #endregion

    #region Variables
    [SerializeField] private Transform player;

    private float distanceToPlayer;

    private Vector3 currPlayerOffset;

    private float fixedYOffset;


    #endregion

    void Start () {
        currPlayerOffset = offset;
        ApplyOffset();
        fixedYOffset = transform.position.y - player.position.y;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        SetOffset();
    }

    void Update() {
        ApplyOffset();
        LookAtPlayer();
    }

    #region Movement
    public void SetOffset() {
        currPlayerOffset = gameObject.transform.position - player.position;
        currPlayerOffset.Normalize();
        currPlayerOffset *= distanceToPlayer;
        currPlayerOffset.y = fixedYOffset;
    }

    public void ApplyOffset() {
        gameObject.transform.position = player.position + currPlayerOffset;
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
}
