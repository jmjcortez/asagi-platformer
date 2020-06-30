using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCheckpointsController : MonoBehaviour
{
    public static AllCheckpointsController instance;
    public Vector3 spawnPoint;

    private CheckpointController[] checkpoints;

    public void Awake() {
        instance = this;
    }

    void Start()
    {
        checkpoints = FindObjectsOfType<CheckpointController>(); // Get all CheckpointControllers attached
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints() {
        for (int i = 0; i < checkpoints.Length; i++) {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint) {
        spawnPoint = newSpawnPoint;
    }
}
