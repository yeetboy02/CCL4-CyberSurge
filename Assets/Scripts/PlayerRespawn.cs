using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    #region Parameters

    [SerializeField] private float respawnDelay = 2.0f;

    [SerializeField] private CameraController cameraController;

    #endregion

    #region Variables

    private Vector3 currRespawnPoint = Vector3.zero;

    private Vector3 initialSpawnPoint = Vector3.zero;

    private PlayerController playerController;

    #endregion

    #region Setup

    void Start() {
        // SET RESPAWN POINT TO INITIAL SPAWN POSITION
        currRespawnPoint = gameObject.transform.position;

        // SET INITIAL SPAWN POINT
        initialSpawnPoint = currRespawnPoint;

        // GET PLAYER CONTROLLER
        playerController = gameObject.GetComponent<PlayerController>();
    }

    #endregion

    #region AddRespawnPoint

    public void SetCurrRespawnPoint(Vector3 newRespawnPoint) {
        // SET CURRENT RESPAWN POINT
        currRespawnPoint = newRespawnPoint;
    }

    #endregion

    #region Respawn

    void Respawn() {
        // START RESPAWN COROUTINE
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine() {
        // DISABLE PLAYER CONTROLS
        playerController.EnableMovement(false);
        playerController.EnableJump(false);

        // DISABLE CAMERA MOVEMENT
        cameraController.EnableCameraRotation(false);
        cameraController.EnableCameraMovement(false);


        // WAIT FOR RESPAWN DELAY
        yield return new WaitForSeconds(respawnDelay);


        // RESET PLAYER POSITION
        if (CourseHandler.instance.GetCurrCourse() != null) {
            // SET PLAYER POSITION TO CURRENT RESPAWN POINT
            playerController.SetPosition(currRespawnPoint);
        } else {
            // SET PLAYER POSITION TO INITIAL SPAWN POINT
            playerController.SetPosition(initialSpawnPoint);
        }


        // REENABLE PLAYER CONTROLS
        playerController.EnableMovement(true);
        playerController.EnableJump(true);

        // REENABLE CAMERA CONTROLS
        cameraController.EnableCameraRotation(true);
        cameraController.EnableCameraMovement(true);

        yield return null;
    }

    #endregion

    #region Collision

    void OnTriggerEnter(Collider other) {
        // CHECK IF COLLIDED WITH RESPAWN COLLIDER
        if (other.CompareTag("Respawn")) {
            Respawn();
        }
    }

    #endregion
}
