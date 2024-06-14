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

    [SerializeField] private Vector2 triggerRadiusConstraints = new Vector2(25.0f, 35.0f);

    [SerializeField] private Vector2 takeoffAngleConstraints = new Vector2(35.0f, 55.0f);

    #endregion

    #region Variables

    private float flyingSpeed;

    private float takeoffAngle;

    private BirdState currState = BirdState.Idle;

    private Vector3 flyingDirection;

    private Vector3 startPosition;

    #endregion

    #region Initialization

    void Start() {
        startPosition = transform.position;

        // RANDOMIZE TRIGGER RADIUS
        float triggerRadius = Random.Range(triggerRadiusConstraints.x, triggerRadiusConstraints.y);
        gameObject.GetComponent<SphereCollider>().radius = triggerRadius;

        // RANDOMIZE FLYING SPEED
        flyingSpeed = Random.Range(flyingSpeedConstraints.x, flyingSpeedConstraints.y);

        // RANDOMIZE TAKEOFF ANGLE
        takeoffAngle = Random.Range(takeoffAngleConstraints.x, takeoffAngleConstraints.y);
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

    #region Collision

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // SET FLYING DIRECTION OPPOSITE TO PLAYER
            flyingDirection = (transform.position - other.transform.position).normalized;

            // SET ROTATION OF FLYING DIRECTION TO ALWAYS FACE UPWARDS
            flyingDirection.y = 0;
            flyingDirection = Quaternion.AngleAxis(takeoffAngle, Vector3.right) * flyingDirection;
            flyingDirection.y = Mathf.Abs(flyingDirection.y);

            currState = BirdState.Flying;
        }
    }

    #endregion
}
