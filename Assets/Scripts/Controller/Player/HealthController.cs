using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;
    public GameObject deathEffect;
    private SpriteRenderer spriteRenderer;
    private float invincibleCounter;
    public int currentHealth, maxHealth;

    public float invicincibleTimer; 

    // called before Start()
    public void Awake() {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0) {
            invincibleCounter -= Time.deltaTime; 
            //deltaTime time it takes for one update frame time to next
            //example 60fps = 1/60 secs

            if (invincibleCounter <= 0) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); 
            }
        }

    }

    public void ReceiveDamage() {

        if (invincibleCounter <= 0) {
            currentHealth -= 1;
            
            if (currentHealth <= 0) {
                Instantiate(deathEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(8);
                LevelManager.instance.RespawnPlayer();
            
            } else {
                invincibleCounter = invicincibleTimer;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f); 

                PlayerController.instance.KnockBack();
            }
        }

        UIController.instance.UpdateHealthDisplay();
    }

    public void HealPlayer() {
        if (currentHealth < maxHealth) {
            currentHealth++;
        }

        UIController.instance.UpdateHealthDisplay();
    }

}
