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

    public bool grounded = false;

    public bool jumping = false;

    #endregion

    #region GetterSetter

    public bool GetGrounded() {
        return grounded;
    }

    public bool GetJumping() {
        return jumping;
    }

    public float GetMaxSpeed() {
        return maxSpeed;
    }

    public float GetHorizontalVelocity() {
        return new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
    }

    public float GetVerticalVelocity() {
        return currVelocityVector.y;
    }

    #endregion

    #region Setup

    void Start() {
        controller = GetComponent<CharacterController>();
        
        // INITIALIZE PLAYER SPEED
        currSpeed = minSpeed;
    }

    #endregion


    #region Movement

    void FixedUpdate() {
        UpdateMovementVectorDirection();
        Move();
        ApplyGravity();
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

        // CALCULATE ANGLE BETWEEN PLAYER FORWARD DIRECTION AND MOVEMENT DIRECTION
        float angle = Vector3.Angle(transform.forward, currDirectionalMovementVector);

        if (angle > 95.0f) {
            // ROTATE PLAYER TO THE FORWARD DIRECTION APRUPTLY
            transform.forward = currDirectionalMovementVector;
        }
        else {
            // ROTATE PLAYER TO THE FORWARD DIRECTION SMOOTHLY
            transform.forward = Vector3.Lerp(transform.forward, currDirectionalMovementVector, rotationSpeed * Time.deltaTime);
        }

    }

    #endregion

    #region Jumping

    void OnJump() {
        if (!grounded) return;

        // SET JUMPING TO TRUE
        jumping = true;

        // CALCULATE JUMPPOWER DEPENDENT ON CURRENT MOVEMENT SPEED
        float currJumpPower = jumpPower + (jumpScaling * (currSpeed - minSpeed));

        // APPLY JUMP
        currVelocityVector.y += Mathf.Sqrt(currJumpPower * gravityScaling * playerGravity);
    }

    void UpdateJumpVector() {
        // GET CURRENT MOVEMENT
        Vector3 currGroundMovement = currDirectionalMovementVector;

        // SET JUMP MOVEMENT VECTOR TO CURRENT VELOCITY WHEN JUMPING
        currJumpMovementVector = new Vector3(currGroundMovement.x, 0, currGroundMovement.z);
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
        currVelocityVector.y -= playerGravity * gravityScaling * Time.deltaTime;

        // APPLY VERTICAL MOVEMENT
        controller.Move(currVelocityVector * Time.deltaTime);

        // CHECK IF GROUNDED
        CheckGround();
    }

    void CheckGround() {
        // UPDATE JUMP VECTOR IF BECOMING UNGROUNDED
        if (grounded && !controller.isGrounded) {
            UpdateJumpVector();
        }

        // CHECK IF GROUNDED
        grounded = controller.isGrounded;

        // SET JUMPING TO FALSE IF GROUNDED
        if (grounded) {
            jumping = false;
        }
        else {
            jumping = true;
        }
    }

    #endregion
}