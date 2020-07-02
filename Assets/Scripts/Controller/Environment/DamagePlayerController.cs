using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            AudioManager.instance.PlaySFX(9);
            HealthController.instance.ReceiveDamage();
        }       
    }
}
