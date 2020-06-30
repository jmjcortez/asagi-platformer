using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float respawnWaitTime;

    public int tabsCollected;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer() {
        StartCoroutine(RespawnCo());    // start async task
    }

    // happens on normal execution, async
    public IEnumerator RespawnCo() {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnWaitTime);   // like async, await

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = AllCheckpointsController.instance.spawnPoint;
    
        HealthController.instance.currentHealth = HealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }
}
