using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite checkpointOn, checkpointOff;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AllCheckpointsController.instance.DeactivateCheckpoints();

            spriteRenderer.sprite = checkpointOn;
            
            AllCheckpointsController.instance.SetSpawnPoint(transform.position);      
        }
    }

    public void ResetCheckpoint() {
        spriteRenderer.sprite = checkpointOff;
    }
}
