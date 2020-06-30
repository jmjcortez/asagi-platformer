using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float respawnWaitTime, tabDuration;

    public int tabsCollected;

    private void Awake() {
        instance = this;
    }
    void Start()
    {   
        tabsCollected = 0;
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

    public void ActivateTab() {
        if (tabsCollected > 0) {
            StartCoroutine(SetTabStatus());
        }    
    }

    public IEnumerator SetTabStatus() {
        BackgroundController.instance.animator.SetBool("isOnTabbed", true);

        tabsCollected -= 1;
        UIController.instance.UpdateTabCount();

        yield return new WaitForSeconds(tabDuration);

        BackgroundController.instance.animator.SetBool("isOnTabbed", false);
    }
}
