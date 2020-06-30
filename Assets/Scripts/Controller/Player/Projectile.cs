using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public Rigidbody2D bulletRigidBody;
    public GameObject impactEffect;

    public void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        bulletRigidBody.velocity = new Vector2(speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Destroy(gameObject);

            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }
}
