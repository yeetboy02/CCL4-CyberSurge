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

    private bool jumping = false;

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

        // CHECK IF PLAYER IS JUMPING
        jumping = movement.GetJumping();

        // SET ANIMATION STATES DEPENDING ON SPEED AND GROUNDED STATE
        if (grounded) {
            if (currHorizontalSpeed == 0.0f) {
                // SET IDLE STATE
                SetAnimationState(AnimationState.Idle);
            } else {
                // SET RUNNING STATE
                SetAnimationState(AnimationState.Running);
            }
        } 
        else {
            if (currVerticalSpeed > 0.0f) {
                // SET JUMPING STATE
                SetAnimationState(AnimationState.Jumping);
            } else {
                // SET FALLING STATE
                SetAnimationState(AnimationState.Falling);
            }
        }

        //Debug.Log(currState);
    }

    #endregion

    #region Setup

    void Start() {
        // GET PLAYER MOVEMENT SCRIPT
        movement = GetComponent<PlayerMovement>();
    }

    #endregion

    #region AnimationSpeed

    private void UpdateAnimationSpeed() {
        // GET CURRENT RUNNING SPEED
        float currSpeed = movement.GetCurrSpeed();

        // SET ANIMATION SPEED DEPENDING ON RUNNING SPEED
        if (currState == AnimationState.Running) {
            animator.speed = currSpeed / movement.GetMaxSpeed() * runningAnimationSpeed;
        } else {
            animator.speed = 1.0f;
        }
    }

    #endregion

    #region AnimationEvents

    public void TriggerVictoryAnimation() {
        // SET VICTORY ANIMATION
        animator.SetTrigger("victory");
    }

    #endregion
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name.Contains("Air") || hit.gameObject.name.Contains("Jump"))
        {
            AkSoundEngine.SetSwitch("Ground", "Metal", gameObject);
            Debug.Log("Metal");
            Debug.Log(AkSoundEngine.SetSwitch("Ground", "Metal", gameObject));
        }
        else if (hit.gameObject.name.Contains("House") || hit.gameObject.name.Contains("Floor"))
        {
            AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject);
            Debug.Log("Concrete");
            Debug.Log(AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject));

        }

    }
}
