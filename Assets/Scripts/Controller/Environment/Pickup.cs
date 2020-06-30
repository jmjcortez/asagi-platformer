using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool isCollected;

    public GameObject pickupEffect;
    
    public bool isTab, isShroom;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isCollected) {
            if (isTab) {
                LevelManager.instance.tabsCollected += 1;

                isCollected = true;
                
                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation); // create an instance of the first param

                UIController.instance.UpdateTabCount();
            }

            if (isShroom) {
                if (HealthController.instance.currentHealth != HealthController.instance.maxHealth) {
                    HealthController.instance.HealPlayer();
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation); // create an instance of the first param

                }
            }
        }
    }
}
