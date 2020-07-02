using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage;

    public Rigidbody2D bulletRigidBody;
    public GameObject impactEffect;
    public SpriteRenderer spriteRenderer;
    
    private int direction;

    public void Start() {
        Destroy(gameObject, lifetime);
        AudioManager.instance.PlaySFX(12);
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (PlayerController.instance.spriteRenderer.flipX) {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        else {
            direction = -1;
            spriteRenderer.flipX = false;
        }
        
    }

    void Update()
    {
            bulletRigidBody.velocity = new Vector2(speed * direction, 0f);
            // flip bullet sprites too
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Instantiate(impactEffect, transform.position, transform.rotation);

            // EnemyController enemy = other.GetComponent<EnemyController>();
            EnemyController enemy = other.transform.parent.GetComponent<EnemyController>();

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
