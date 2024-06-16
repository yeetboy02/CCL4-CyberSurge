using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    #region MovementParameters

    [SerializeField] private float minSpeed = 10.0f;

    [SerializeField] private float maxSpeed = 20.0f;

    [SerializeField] private float acceleration = 0.05f;

    #endregion

    #region AirMovementParameters

    [SerializeField] private float minAirMovementSpeed = 0.0f;

    [SerializeField] private float maxAirMovementSpeed = 0.4f;

    [SerializeField] private float airAcceleration = 0.01f;

    [SerializeField] private float airMovementFactor = 0.005f;

    [SerializeField] private float airMovementScaling = 0.05f;

    #endregion

    #region RotationParameters

    [SerializeField] private float rotationSpeed = 10.0f;

    #endregion

    #region JumpParameters

    [SerializeField] private float playerDistanceToGround = 1.01f;

    [SerializeField] private float jumpPower = 4.0f;

    [SerializeField] private float jumpScaling = 0.25f;

    [SerializeField] private float playerGravity = 9.8f;

    [SerializeField] private float gravityScaling = 3.0f;

    #endregion

    #region Variables

    private CharacterController controller;

    private Vector3 currMovementVector = Vector3.zero;

    private Vector3 currDirectionalMovementVector = Vector3.zero;

    private Vector3 currAirMovementVector = Vector3.zero;


    private Vector3 currDirectionalAirMovementVector = Vector3.zero;

    private Vector3 currJumpMovementVector = Vector3.zero;

    private Vector3 totalAirMovementVector = Vector3.zero;

    private Vector3 currVelocityVector = Vector3.zero;

    private float currSpeed = 0.0f;

    private float currAirMovementSpeed = 0.0f;

    private float currTotalAirSpeed = 0.0f;

    private bool grounded = false;

    #endregion

    void Start() {
        controller = GetComponent<CharacterController>();
        
        // INITIALIZE PLAYER SPEED
        currSpeed = minSpeed;

        StartCoroutine(CheckForGround());
    }


    #region Movement

    void Update() {
        ApplyGravity();
        UpdateMovementVectorDirection();
        Move();
    }

    void Move() {
        if (grounded && currMovementVector != Vector3.zero) {
            // HORIZONTAL PLAYER MOVEMENT
            controller.Move(currDirectionalMovementVector * currSpeed * Time.deltaTime);
            StartCoroutine(Acceleration());
        }
        else if (!grounded) {

            // CALCULATE TOTAL CURRENT AIR MOVEMENT INCLUDING INITIAL JUMP MOVEMENT AND AIR MOVEMENT
            totalAirMovementVector += currDirectionalAirMovementVector * (airMovementScaling * currTotalAirSpeed) * currAirMovementSpeed;

            // PLAYER AIR MOVEMENT
            controller.Move(totalAirMovementVector * currTotalAirSpeed * Time.deltaTime);
            StartCoroutine(AirAcceleration());
        }
        else {
            StopMoving();
        }
    }

    void StopMoving() {
        // STOP PLAYER GROUND MOVEMENT
        StopCoroutine(Acceleration());
        currSpeed = minSpeed;

        // STOP PLAYER AIR MOVEMENT
        StopCoroutine(AirAcceleration());
        currAirMovementSpeed = minAirMovementSpeed;
    }

    void OnMovement(InputValue value) {
        // RETRIEVE MOVEMENT INPUT VECTOR
        Vector2 input = value.Get<Vector2>();

        // SET CURRENT MOVEMENT INPUT VECTOR
        currMovementVector = new Vector3(input.x, 0, input.y);

        if (currMovementVector == Vector3.zero) {
            StopMoving();
        }

        // SET CURRENT AIR MOVEMENT VECTOR
        currAirMovementVector = currMovementVector * airMovementFactor;
    }

    IEnumerator Acceleration() {
        while (currSpeed < maxSpeed) {
            // ACCELERATE CURRENT GROUND SPEED EVERY FRAME
            currSpeed += acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    #region AirMovement

    IEnumerator AirAcceleration() {
        while (currAirMovementSpeed < maxAirMovementSpeed) {
            // ACCELERATE CURRENT AIR SPEED EVERY FRAME
            currAirMovementSpeed += airAcceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    #region Rotation

    void UpdateMovementVectorDirection() {
        // GET CAMERA ROTATION
        Quaternion cameraRotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        // ROTATE PLAYER MOVEMENT VECTOR
        currDirectionalMovementVector = cameraRotation * currMovementVector;
        currDirectionalAirMovementVector = cameraRotation * currAirMovementVector;

        FaceForward();
    }

    void FaceForward() {
        if (currMovementVector != Vector3.zero) {
            // ROTATE PLAYER TO THE FORWARD DIRECTION
            transform.forward = Vector3.Lerp(transform.forward, currDirectionalMovementVector, rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Jumping

    void OnJump() {
        if (!grounded) return;

        // CALCULATE JUMPPOWER DEPENDENT ON CURRENT MOVEMENT SPEED
        float currJumpPower = jumpPower + (jumpScaling * (currSpeed - minSpeed));

        // APPLY JUMP
        currVelocityVector.y += Mathf.Sqrt(currJumpPower * gravityScaling * playerGravity);
    }

    void UpdateJumpVector() {
        // Set JUMP MOVEMENT VECTOR TO CURRENT DIRECTIONAL MOVEMENT VECTOR WHEN STARTING JUMP
        currJumpMovementVector = currDirectionalMovementVector;
        totalAirMovementVector = currJumpMovementVector;

        // SET CURRENT AIR SPEED TO CURRENT GROUND SPEED WHEN STARTING JUMP
        currTotalAirSpeed = currSpeed;
        currAirMovementSpeed = minAirMovementSpeed;
    }

    #endregion

    #region Gravity

    void ApplyGravity() {

        // RESET Y VELOCITY IF GROUNDED
        if (grounded && currVelocityVector.y < 0) {
            currVelocityVector.y = 0;
        }

        // APPLY GRAVITY
        if (!grounded) {
            currVelocityVector.y -= playerGravity * gravityScaling * Time.deltaTime;
        }

        // APPLY VERTICAL MOVEMENT
        controller.Move(currVelocityVector * Time.deltaTime);
    }

    IEnumerator CheckForGround() {
        RaycastHit hit;

        while (true) {
            // RAYCAST TO GROUND
            bool raycastSuccess = Physics.Raycast(transform.position, transform.up * -1, out hit);

            // CHECK IF GROUNDED
            if (raycastSuccess && hit.collider.gameObject.CompareTag("Ground") && hit.distance <= playerDistanceToGround + 0.00001f) {
                grounded = true;
            }
            else {
                if (grounded) {
                    UpdateJumpVector();
                }
                grounded = false;
            }
            yield return null;
        }
    }

    #endregion
}