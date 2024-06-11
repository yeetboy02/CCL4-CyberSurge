using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    #region MovementParameters
    [SerializeField] private float minSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float acceleration = 0.1f;
    #endregion

    #region AirMovementParameters
    [SerializeField] private float airMovementFactor = 0.25f;

    [SerializeField] private float airMovementScaling = 0.05f;

    [SerializeField] private float minAirSpeed = 0.0f;

    [SerializeField] private float maxAirSpeed = 7.0f;

    [SerializeField] private float airAcceleration = 0.1f;
    #endregion


    #region JumpingParameters
    [SerializeField] private float jumpPower = 4.0f;

    [SerializeField] private float jumpScaling = 0.25f;

    [SerializeField] private float playerDistanceToGround = 1.0f;

    #endregion

    #region RotationParameters
    private float rotationSpeed = 10.0f;
    #endregion


    #region Variables
    private Vector3 movementVector;
    private Vector3 directionalMovementVector;

    private Vector3 prevMovementVector;
    private Vector3 prevDirectionalMovementVector;

    private Vector3 airMovementVector;
    private Vector3 directionalAirMovementVector;

    private Vector3 jumpMovementVector;

    private Rigidbody rb;
    public bool grounded = false;

    private float currSpeed = 0.0f;

    private float currAirSpeed = 0.0f;

    private float currAirMovementSpeed = 0.0f;

    private float currAirMovementScaling = 0.0f;

    #endregion

    void Start() {
        currSpeed = minSpeed;
        prevMovementVector = Vector3.zero;
        movementVector = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckForGround());
    }

    void FixedUpdate() {

        UpdateMovementVectorDirection();

        if (grounded) {
            Move();
        }
        else {
            AirMove();
        }
    }

    
    #region Movement
    void Move() {
        if (grounded && movementVector != Vector3.zero) {
            if (!checkCollision(directionalMovementVector)) {
                transform.position += directionalMovementVector * currSpeed * Time.deltaTime;
                StartCoroutine(Acceleration());
            }
            else {
                StopMoving();
            }
        }
    }

    void StopMoving() {
        StopCoroutine(Acceleration());
        currSpeed = minSpeed;
        StopCoroutine(AirAcceleration());
        currAirMovementSpeed = minAirSpeed;
    }

    void OnMovement(InputValue value) {
        Vector2 currInput = value.Get<Vector2>();
        prevMovementVector = movementVector;
        movementVector = new Vector3(currInput.x, 0, currInput.y);

        if (movementVector == Vector3.zero || movementVector.x - prevMovementVector.x == 0 || movementVector.z - prevMovementVector.z == 0) {
            StopMoving();
        }

        airMovementVector = movementVector * airMovementFactor;
    }

    IEnumerator Acceleration() {
        while (currSpeed < maxSpeed) {
            if (grounded) {
                currSpeed += acceleration * Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    bool checkCollision(Vector3 inputVector) {
        Ray ray = new Ray(transform.position, inputVector);
        return Physics.Raycast(ray, inputVector.magnitude + 0.50001f);
    }
    #endregion

    #region Rotation
    void UpdateMovementVectorDirection() {
        Quaternion cameraRotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        prevDirectionalMovementVector = cameraRotation * prevMovementVector;
        directionalMovementVector = cameraRotation * movementVector;
        directionalAirMovementVector = cameraRotation * airMovementVector;
        FaceForward();
    }

    void FaceForward() {
        if (movementVector != Vector3.zero) {
            transform.forward = Vector3.Lerp(transform.forward, directionalMovementVector, rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region AirMovement

    void AirMove() {
        if (!checkCollision(directionalAirMovementVector)) {
            transform.position += (jumpMovementVector + (directionalAirMovementVector * (airMovementScaling * currAirSpeed) * currAirMovementSpeed)) * currAirSpeed * Time.deltaTime;
            StartCoroutine(AirAcceleration());
        }
        else {
            StopMoving();
        }
    }

    IEnumerator AirAcceleration() {
        while (currAirMovementSpeed < maxAirSpeed) {
            currAirMovementSpeed += airAcceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion


    #region Jumping
    void OnJump() {
        if (!grounded) {
            return;
        }
        float currJumpPower = jumpPower + (jumpScaling * (currSpeed - minSpeed));
        rb.AddForce(Vector3.up * currJumpPower, ForceMode.Impulse);
    }

    void UpdateJumpVector() {
        jumpMovementVector = directionalMovementVector;
        currAirSpeed = currSpeed;
        currAirMovementScaling = currSpeed;
        currAirMovementSpeed = minAirSpeed;
    }

    IEnumerator CheckForGround() {
        RaycastHit hit;

        while (true) {
            bool raycastSuccess = Physics.Raycast(transform.position, transform.up * -1, out hit);
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

