using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour {

    #region Parameters
    [SerializeField] private GameObject birdPrefab;

    [SerializeField] private GameObject playerCam;

    [SerializeField] private GameObject player;

    [SerializeField] private float minSpawnDistance = 10.0f;

    [SerializeField] private float maxSpawnDistance = 15.0f;

    #endregion

    #region Variables

    private bool visible = true;

    private GameObject currBird = null;


    #endregion

    #region Setup

    void Start() {
        // START WITH BIRD
        currBird = SpawnBird();
    }

    #endregion

    #region VisibilityCheck
    void CheckVisibility() {
        if (CheckDirection() && CheckDistance()) {
            visible = false;
        }
        else {
            visible = true;
        }
    }

    bool CheckDistance() {
        // CHECK IF PLAYER IS WITHIN SPAWNDISTANCE FROM THE BIRD SPAWNER
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distanceToPlayer >= minSpawnDistance && distanceToPlayer >= maxSpawnDistance) {
            return true;
        }
        return false;
    }

    bool CheckDirection() {
        // TRANSFORM BIRD SPAWNER COORDINATES INTO VIEW COORDINATES
        Vector3 cameraViewPosition = UnityEngine.Camera.main.WorldToViewportPoint(gameObject.transform.position);

        if (cameraViewPosition.x >= 0 && cameraViewPosition.x <= 1 && cameraViewPosition.y >= 0 && cameraViewPosition.y <= 1 && cameraViewPosition.z > 0) {
            return false;
        }
        return true;
    }

    #endregion

    #region BirdExists

    void Update() {
        // CHECK VISIBILITY
        CheckVisibility();

        // SPAWN BIRD IF NOT VISIBLE
        if (!CheckForBird()) {
            StartCoroutine(TrySpawnBird());
        }
    }

    bool CheckForBird() {
        if (currBird == null) {
            return false;
        }
        return true;
    }

    #endregion

    #region SpawnBird

    IEnumerator TrySpawnBird() {
        // WAIT UNTIL ALL CONDITIONS TURN TRUE
        while (visible) {
            // CHECK VISIBILITY EACH FRAME
            yield return new WaitForEndOfFrame();
        }

        // SPAWN BIRD WHEN NOT VISIBLE
        if (currBird == null) {
            currBird = SpawnBird();
        }
    }

    GameObject SpawnBird() {
        // GET RANDOM Y ROTATION
        float randomY = Random.Range(0f, 360f);

        // SPAWN BIRD WITH RANDOM Y ROTATION
        return Instantiate(birdPrefab, gameObject.transform.position, Quaternion.Euler(0, randomY, 0), gameObject.transform);
    }

    #endregion

}
