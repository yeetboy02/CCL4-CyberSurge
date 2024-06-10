using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    #region MovementParameters
    private float minSpeed = 5.0f;
    private float maxSpeed = 15.0f;
    private float acceleration = 0.01f;
    #endregion

    #region JumpingParameters
    private float jumpPower = 5.0f;

    private float playerDistanceToGround = 1.0f;

    #endregion


    #region Variables
    private Vector3 movementVector;
    private Vector3 airMovementVector;
    private Rigidbody rb;
    public bool grounded = false;

    private float currSpeed = 5.0f;
    private float currAirSpeed = 5.0f;

    #endregion

    void Start() {
        movementVector = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckForGround());
    }

    void Update() {
        if (grounded) {
            if (movementVector != Vector3.zero) {
                Move();
            }
            else {
                StopMoving();
            }
        }
        else {
            AirMove();
        }
    }

    
    #region Movement
    void Move() {
        transform.position += movementVector * currSpeed * Time.deltaTime;
        StartCoroutine(Acceleration());
    }

    void StopMoving() {
        currSpeed = minSpeed;
        StopCoroutine(Acceleration());
    }

    void OnMovement(InputValue value) {
        Vector2 currInput = value.Get<Vector2>();
        movementVector = new Vector3(currInput.x, 0, currInput.y);
    }

    IEnumerator Acceleration() {
        while (currSpeed < maxSpeed) {
            if (grounded) {    
                currSpeed += acceleration * Time.deltaTime;
            }
            yield return null;
        }
    }
    #endregion


    #region Jumping
    void OnJump() {
        if (!grounded) {
            return;
        }
        UpdateAirMovement();
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    void AirMove() {
        if (grounded) {
            transform.position += movementVector * currSpeed * Time.deltaTime;
        }
        else {
            transform.position += airMovementVector * currAirSpeed * Time.deltaTime;
        }
    }

    void UpdateAirMovement() {
        airMovementVector = movementVector;
        currAirSpeed = currSpeed;
    }

    IEnumerator CheckForGround() {
        RaycastHit hit;

        while (true) {
            bool raycastSuccess = Physics.Raycast(transform.position, transform.up * -1, out hit);
            if (raycastSuccess && hit.collider.gameObject.CompareTag("Ground") && hit.distance <= playerDistanceToGround) {
                grounded = true;
            }
            else {
                grounded = false;
            }
            yield return null;
        }
    }
    #endregion
    

}

