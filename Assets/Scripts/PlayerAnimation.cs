using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    #region Parameters

    [SerializeField] private Animator animator;

    [SerializeField] private float runningAnimationSpeed = 1.0f;

    #endregion

    #region Variables

    private PlayerMovement movement;

    private float currHorizontalSpeed = 0.0f;

    private float currVerticalSpeed = 0.0f;

    private bool grounded = false;

    #endregion

    #region AnimationState

    private AnimationState currState;

    public enum AnimationState {
        Idle,
        Running,
        Jumping,
        Falling,
    }

    public void SetAnimationState(AnimationState newState) {
        // SET NEW STATE
        currState = newState;

        // UPDATE ANIMATOR STATE
        animator.SetInteger("state", (int)currState);
    }

    #endregion

    #region InterpretPlayerSpeed

    void Update() {
        // UPDATE PLAYER STATE DEPENDING ON SPEED
        GetStateByPlayerSpeed();

        // UPDATE ANIMATION SPEED
        UpdateAnimationSpeed();
    }

    private void GetStateByPlayerSpeed() {
        // RETRIEVE HORIZONTAL SPEED
        currHorizontalSpeed = movement.GetHorizontalVelocity();

        // RETRIEVE VERTICAL SPEED
        currVerticalSpeed = movement.GetVerticalVelocity();

        // CHECK IF PLAYER IS GROUNDED
        grounded = movement.GetGrounded();

        // SET ANIMATION STATE DEPENDING ON SPEED
        if (currVerticalSpeed == 0.0f && currHorizontalSpeed > 0.0f && grounded) {
            // SET RUNNING STATE
            SetAnimationState(AnimationState.Running);
        }
        else if (currVerticalSpeed == 0.0f && currHorizontalSpeed == 0.0f && grounded) {
            // SET IDLE STATE
            SetAnimationState(AnimationState.Idle);
        }
        else if (currVerticalSpeed > 0.0f) {
            // SET JUMPING STATE
            SetAnimationState(AnimationState.Jumping);
        }
        else if (currVerticalSpeed < 0.0f) {
            // SET FALLING STATE
            SetAnimationState(AnimationState.Falling);
        }
    }

    #endregion

    #region Setup

    void Start() {
        // GET PLAYER MOVEMENT SCRIPT
        movement = GetComponent<PlayerMovement>();

        // SET INITIAL STATE TO IDLE
        SetAnimationState(AnimationState.Idle);
    }

    #endregion

    #region AnimationSpeed

    private void UpdateAnimationSpeed() {
        // SET ANIMATION SPEED DEPENDING ON RUNNING SPEED
        if (currState == AnimationState.Running) {
            animator.speed = currHorizontalSpeed / movement.GetMaxSpeed() * runningAnimationSpeed;
        } else {
            animator.speed = 1.0f;
        }
    }

    #endregion
}
