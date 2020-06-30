using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire() {
        Instantiate(projectile, shotPoint.position, transform.rotation);
    }
}
