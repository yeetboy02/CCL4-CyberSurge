using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdMovement : MonoBehaviour {
    #region States

    private enum BirdState {
        Idle,
        Flying
    }

    #endregion

    #region Parameters

    [SerializeField] private float flyingDistance = 75.0f;

    [SerializeField] private Vector2 flyingSpeedConstraints = new Vector2(10.0f, 25.0f);

    [SerializeField] private float animationFlyingSpeedFactor = 0.25f;

    [SerializeField] private Vector2 triggerRadiusConstraints = new Vector2(25.0f, 35.0f);

    [SerializeField] private Vector2 takeoffAngleConstraints = new Vector2(35.0f, 55.0f);

    [SerializeField] private Vector2 idleTimeConstraints = new Vector2(1.0f, 3.0f);

    [SerializeField] private int idleAnimationCount = 2;

    #endregion

    #region Variables

    private float flyingSpeed;

    private float takeoffAngle;

    private BirdState currState = BirdState.Idle;

    private Vector3 flyingDirection;

    private Vector3 startPosition;

    private Animator animator;

    #endregion

    #region Initialization

    void Start() {
        // SET START POSITION TO RANDOM POSITION
        startPosition = transform.position;

        // RANDOMIZE TRIGGER RADIUS
        float triggerRadius = Random.Range(triggerRadiusConstraints.x, triggerRadiusConstraints.y);
        gameObject.GetComponent<SphereCollider>().radius = triggerRadius;

        // RANDOMIZE FLYING SPEED
        flyingSpeed = Random.Range(flyingSpeedConstraints.x, flyingSpeedConstraints.y);

        // RANDOMIZE TAKEOFF ANGLE
        takeoffAngle = Random.Range(takeoffAngleConstraints.x, takeoffAngleConstraints.y);

        animator = gameObject.GetComponent<Animator>();

        // START IDLE ANIMATION
        StartCoroutine(Idle());
    }

    #endregion

    #region Idle

    IEnumerator Idle() {
        // SET ANIMATION STATE TO IDLE
        animator.SetInteger("state", 0);

        while(currState == BirdState.Idle) {
            // SET RANDOM IDLE ANIMATION
            animator.SetInteger("state", (int)Mathf.Round(Random.Range(0.0f, idleAnimationCount)));

            // WAIT FOR IDLE TIME
            yield return new WaitForSeconds(Random.Range(idleTimeConstraints.x, idleTimeConstraints.y));
        }
    }

    #endregion

    #region Movement

    void Update() {
        Move();
    }

    private void Move() {
        if (currState == BirdState.Flying) {
            // MOVE THE BIRD
            transform.position += flyingDirection * flyingSpeed * Time.deltaTime;

            // ROTATE TO FLYING DIRECTION
            transform.rotation = Quaternion.LookRotation(flyingDirection);

            // CHECK FOR DISTANCE TO START POSITION
            if ((startPosition - transform.position).magnitude >= flyingDistance) {
                Destroy(gameObject);
            }
        }
    }

    #endregion

    #region Takeoff

    IEnumerator TakeOff() {
        // SET ANIMATION STATE TO FLYING
        animator.SetInteger("state", 3);

        // WAIT FOR ANIMATION TO FINISH
        yield return new WaitForSeconds(0.3f);

        // SET STATE TO FLYING
        currState = BirdState.Flying;
    }

    #endregion

    #region Collision

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // SET FLYING DIRECTION OPPOSITE TO PLAYER
            flyingDirection = (transform.position - other.transform.position).normalized;

            // SET ROTATION OF FLYING DIRECTION TO ALWAYS FACE UPWARDS
            flyingDirection.y = 0;
            flyingDirection = Quaternion.AngleAxis(takeoffAngle, Vector3.right) * flyingDirection;
            flyingDirection.y = Mathf.Abs(flyingDirection.y);

            // SET ANIMATION SPEED RELATIVE TO FLYING SPEED
            animator.speed = flyingSpeed * animationFlyingSpeedFactor;

            // STOP IDLE
            StopCoroutine(Idle());

            // START TAKEOFF
            StartCoroutine(TakeOff());
        }
    }

    #endregion
}
