using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    private float minSpeed = 5.0f;
    private float maxSpeed = 15.0f;
    private float acceleration = 0.01f;
    private float currSpeed = 5.0f;


    private Vector3 movementVector;

    void Update() {
        if (movementVector != Vector3.zero) {
            Move();
        }
        else {
            StopMoving();
        }
        Debug.Log(currSpeed);
    }

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
            currSpeed += acceleration * Time.deltaTime;
            yield return null;
        }
    }

}

