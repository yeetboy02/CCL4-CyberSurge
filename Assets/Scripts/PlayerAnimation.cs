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

    private CharacterController controller;

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
        currHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        // RETRIEVE VERTICAL SPEED
        currVerticalSpeed = controller.velocity.y;

        // GET IF PLAYER IS GROUNDED
        grounded = movement.GetGrounded();

        // CHECK IF PLAYER IS GROUNDED
        if (grounded) {
            // CHECK IF PLAYER IS MOVING
            if (currHorizontalSpeed > 0.0f) {
                // SET ANIMATION STATE TO RUNNING
                SetAnimationState(AnimationState.Running);
            } else {
                // SET ANIMATION STATE TO IDLE
                SetAnimationState(AnimationState.Idle);
            }
        } else {
            // CHECK IF PLAYER IS JUMPING
            if (currVerticalSpeed > 0.0f) {
                // SET ANIMATION STATE TO JUMPING
                SetAnimationState(AnimationState.Jumping);
            } else {
                // SET ANIMATION STATE TO FALLING
                SetAnimationState(AnimationState.Falling);
            }
        }
    }

    #endregion

    #region Setup

    void Start() {
        // GET PLAYER MOVEMENT SCRIPT
        movement = GetComponent<PlayerMovement>();

        // SET INITIAL STATE TO IDLE
        SetAnimationState(AnimationState.Idle);

        // GET CHARACTER CONTROLLER
        controller = GetComponent<CharacterController>();
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
