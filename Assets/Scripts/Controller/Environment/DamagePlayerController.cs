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
            // FindObjectOfType<HealthController>().ReceiveDamage();
            HealthController.instance.ReceiveDamage();
        }       
    }
}
