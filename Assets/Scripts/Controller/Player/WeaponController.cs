using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;
    public GameObject projectile;

    private float timeBtwShots; //counter
    public float startTimeBtwShots;

    private void Awake() {
        instance = this;
    }

    void Update()
    {
        
    }

    public void Fire() {
        //dapat sa player
        if (timeBtwShots <= 0) {

            Instantiate(projectile, transform.position, transform.rotation);
            
            Instantiate(projectile, transform.position + new Vector3(-0.2f, -0.2f, 0f) , transform.rotation);
            timeBtwShots = startTimeBtwShots;
        } 
        else {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
