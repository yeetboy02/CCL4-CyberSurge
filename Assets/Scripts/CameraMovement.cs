using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {

    #region RotationParameters
    private float rotationSpeedX = 1.0f;

    private float rotationSpeedY = 1.0f;

    private float minRotationY = -45.0f;

    private float maxRotationY = 45.0f;
    #endregion

    #region Variables
    [SerializeField] private Transform player;
    private Vector3 offset;

    private float yOffset;

    private float currRotationY = 0.0f;

    private Transform other;
    private Transform own;

    float distance;

    #endregion

    void Start () {
        other = player.GetComponent<Transform>();
        own = gameObject.GetComponent<Transform>();
        yOffset = own.position.y - other.position.y;
        distance = Vector3.Distance(own.position, other.position);
        SetOffset();
    }

    void Update() {

        // OFFSET
        SetOffset();
        gameObject.transform.position = other.position + offset;
        transform.LookAt(player);
    }

    public void SetOffset() {
        offset = own.position - other.position;
        offset.Normalize();
        offset *= distance;
        Debug.Log(offset);
        offset.y = yOffset;
    }

    #region Rotations

    void OnCameraRotation(InputValue value) {
        Vector2 input = value.Get<Vector2>();

        // X ROTATION
        transform.RotateAround(player.position, new Vector3(0, 1, 0), input.x * rotationSpeedX);

        // Y ROTATION
        float prevRotationY = currRotationY;
        currRotationY += input.y * rotationSpeedY;
        currRotationY = Mathf.Clamp(currRotationY, minRotationY, maxRotationY);
        input.y = currRotationY - prevRotationY;
        transform.RotateAround(player.position, new Vector3(1, 0, 0), input.y);
    }

    #endregion
}
